using ErrorHelper.Core.Model.LogHelper.Elmah;
using ErrorHelper.Core.Service.LogHelper;
using ErrorHelper.Infrastructure.Common.Configuration;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using SeanTool.CSharp;

namespace ErrorHelper.Infrastructure.Service.LogHelper
{
    public class ElmahHelperService : ServiceBase, IElmahHelperService<ElmahFile, ElmahInfo, ElmahQueryCondition>
    {
        public IList<ElmahFile> GetLogFileList(ElmahQueryCondition elmahQueryCondition)
        {
            IList<ElmahFile> elmahFileList = new List<ElmahFile>();
            ConcurrentBag<ElmahFile> elmahBag = new ConcurrentBag<ElmahFile>();
            IList<string> filePathList = FileTool.GetAllFileInFolder(elmahQueryCondition.LogSourceFolderPath ?? string.Empty);

            if (filePathList == null || filePathList.Count == 0)
                return elmahFileList;

            //分離出xml與zip，避免不必要的判斷與讓處理更可控
            IList<string> xmlFiles = filePathList.Where(p => p.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)).ToList()
                        , zipFiles = filePathList.Where(p => p.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)).ToList();

            //限制並行度，避免同時打開過多zip檔造成I/O與ThreadPool壓力
            ParallelOptions parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = Math.Max(1, Environment.ProcessorCount - 1)
            };

            //處理獨立的xml檔
            Parallel.ForEach(xmlFiles, parallelOptions, filePath =>
            {
                try
                {
                    DateTime? elmahTime = GetElmahFileNameData(Path.GetFileName(filePath))?.ElmahTime;
                    if (elmahTime >= elmahQueryCondition.StartTime && elmahTime <= elmahQueryCondition.EndTime)
                        elmahBag.Add(GetLogFile(filePath));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            });

            //處理zip：列出zip內部檔名
            Parallel.ForEach(zipFiles, parallelOptions, zipPath =>
            {
                try
                {
                    DateTime? elmahZipTime = GetElmahZipDateTime(Path.GetFileName(zipPath)) ?? new DateTime(1900, 1, 1);

                    if (
                        (elmahZipTime >= elmahQueryCondition.StartTime.Date.AddDays(-1) && elmahZipTime <= elmahQueryCondition.EndTime.Date.AddDays(1))
                        || AppSettings.LogSetting.IsSearchAllZipFile
                    ){
                        IList<string> innerFile = ZipTool.GetFileNameInZip(zipPath);
                        if (innerFile == null || innerFile.Count == 0) return;

                        foreach (var fileName in innerFile)
                        {
                            try
                            {
                                if (!fileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)) continue;

                                var elmahTime = GetElmahFileNameData(fileName)?.ElmahTime;
                                if (elmahTime >= elmahQueryCondition.StartTime && elmahTime <= elmahQueryCondition.EndTime)
                                {
                                    elmahBag.Add(GetLogFile(fileName, zipPath));
                                }
                            }
                            catch (Exception innerEx)
                            {
                                Debug.WriteLine(innerEx.ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            });

            elmahFileList = elmahBag
                        .Where(elmah => elmah.LogInfo != null)
                        .Where(elmah =>
                            elmah.FileName.Contains(elmahQueryCondition.FileName)             //FileName查詢條件
                            && elmah.LogInfo.Message.Contains(elmahQueryCondition.Message))   //Message查詢條件
                        .Where(elmah =>
                            elmah.LogInfo.GetDetail().Contains(elmahQueryCondition.Detail))   //Detail查詢條件
                        .Where(elmah =>
                            elmahQueryCondition.IgnoreMessageList.All(ignoreMsg => !elmah.LogInfo.Message.Contains(ignoreMsg)))
                        .OrderByDescending(elmah => elmah.LogInfo.Time)
                        .ToList<ElmahFile>();

            return elmahFileList;
        }

        public ElmahFile GetLogFile(string elmahPath)
        {
            string elmahName = Path.GetFileName(elmahPath);

            if (!FileTool.CheckFileExist(elmahPath))
                Debug.WriteLine($"找不到elmah檔，請檢查此路徑{elmahPath}");

            (DateTime? ElmahTime, string GUID)? elmahFileData = GetElmahFileNameData(elmahName);

            return new ElmahFile()
            {
                FileName = elmahName,
                FileTime = elmahFileData?.ElmahTime,
                GUID = elmahFileData?.GUID,
                LogInfo = GetElmahInfo(XMLTool.GetXDocument(elmahPath)),
                SourceZIPPath = null,
                ParentFolderPath = Path.GetDirectoryName(elmahPath) ?? string.Empty
            };
        }

        public ElmahFile GetLogFile(string elmahName, string elmahZipPath)
        {
            try
            {
                if (!FileTool.CheckFileExist(elmahZipPath))
                    Debug.WriteLine($"找不到ZIP檔，請檢查此路徑{elmahZipPath}");

                byte[]? elmahBytes = ZipTool.ExtractSingleFileToMemory(elmahZipPath, elmahName);

                if (elmahBytes == null)
                    Debug.WriteLine($"ZIP檔裡面無{elmahName}");

                (DateTime? ElmahTime, string GUID)? elmahFileData = GetElmahFileNameData(elmahName);

                return new ElmahFile()
                {
                    FileName = elmahName,
                    FileTime = elmahFileData?.ElmahTime,
                    GUID = elmahFileData?.GUID,
                    LogInfo = GetElmahInfo(XMLTool.GetXDocumentFromBytes(elmahBytes)),
                    SourceZIPPath = elmahZipPath,
                    ParentFolderPath = Path.GetDirectoryName(elmahZipPath) ?? string.Empty
                };
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.ToString());
                return new ElmahFile();
            }
        }

        public ElmahInfo GetElmahInfo(XDocument info)
        {
            ElmahInfo elmahInfo = new ElmahInfo();

            XElement? errorElement = info.Descendants("error").FirstOrDefault();
            if (errorElement == null) return elmahInfo;

            elmahInfo.LogID = errorElement.Attribute("errorId")?.Value ?? string.Empty;
            elmahInfo.Application = errorElement.Attribute("application")?.Value ?? string.Empty;
            elmahInfo.Host = errorElement.Attribute("host")?.Value ?? string.Empty;
            elmahInfo.Type = errorElement.Attribute("type")?.Value ?? string.Empty;
            elmahInfo.Message = errorElement.Attribute("message")?.Value ?? string.Empty;
            elmahInfo.Source = errorElement.Attribute("source")?.Value ?? string.Empty;
            elmahInfo.SetDetail(errorElement.Attribute("detail")?.Value ?? string.Empty);
            if (DateTime.TryParse(errorElement.Attribute("time")?.Value ?? string.Empty, out DateTime time))
                elmahInfo.Time = time;

            return elmahInfo;
        }

        public DateTime? GetElmahZipDateTime(string zipFileName)
        {

            if (string.IsNullOrWhiteSpace(zipFileName) || zipFileName.Length < 8)
                return null;

            string dateStr = zipFileName.Substring(0, 8); // ex. 20250701

            if (DateTime.TryParseExact(dateStr, AppSettings.LogSetting.ElmahZipTimeFormatPattern, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
                return dt;

            return null;
        }

        public (DateTime? ElmahTime, string GUID)? GetElmahFileNameData(string elmahName)
        {
            Match match = Regex.Match(elmahName, AppSettings.LogSetting.ElmahFileNamePattern);

            if (match.Success && match.Groups.Count >= 3)
                return (ConvertZFormatToTaiwanTime(match.Groups[1].Value), match.Groups[2].Value);

            return null;
        }

        /// <summary>
        /// 將特殊格式的UTC時間字串轉換為台灣時間(UTC+8)。
        /// </summary>
        /// <param name="zTimeStr">時間字串，格式:yyyy-MM-ddHHmmssZ</param>
        /// <returns>轉換後的台灣時間(DateTime)，若格式錯誤則回傳 null</returns>
        public DateTime? ConvertZFormatToTaiwanTime(string zTimeStr)
        {
            // 嘗試將輸入字串解析為 DateTime 物件(UTC)
            if (DateTime.TryParseExact(
                    zTimeStr
                    , AppSettings.LogSetting.ElmahFileTimeFormatPattern //定義輸入時間的格式('Z' 是字面值，需加上單引號)
                    , CultureInfo.InvariantCulture //文化資訊，InvariantCulture確保解析不受本地文化影響
                    , DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal //說明輸入字串為UTC，並將結果轉為UTC
                    , out DateTime utcDateTime
             ))
            {
                try
                {
                    // 取得台灣的時區資訊(Windows時區ID)
                    TimeZoneInfo taiwanTimeZone = TimeZoneInfo.FindSystemTimeZoneById(AppSettings.SystemSetting.TaiwanTimeZoneID);

                    // 將UTC時間轉換為台灣時間
                    DateTime taiwanTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, taiwanTimeZone);

                    return taiwanTime;
                }
                catch (TimeZoneNotFoundException)
                {
                    Debug.WriteLine($"找不到台灣時區資訊(ID:{AppSettings.SystemSetting.TaiwanTimeZoneID})");
                }
                catch (InvalidTimeZoneException)
                {
                    Debug.WriteLine("台灣時區設定無效");
                }
            }
            else
            {
                Debug.WriteLine("時間格式解析失敗：" + zTimeStr);
            }

            return null;
        }
    }
}
