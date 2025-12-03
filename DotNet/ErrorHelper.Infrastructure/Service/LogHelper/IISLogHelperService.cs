using ErrorHelper.Core.Model.LogHelper.IISLog;
using ErrorHelper.Infrastructure.Common.Configuration;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.RegularExpressions;
using SeanTool.CSharp.Net8;

namespace ErrorHelper.Infrastructure.Service.LogHelper
{
    public class IISLogHelperService : ServiceBase, IIISLogHelperService<IISLogFile, IISLogInfo, IISLogQueryCondition>
    {
        public IList<IISLogFile> GetLogFileList(IISLogQueryCondition iisLogQueryCondition)
        {
            IList<IISLogFile> iisLogFileList = new List<IISLogFile>();
            ConcurrentBag<IISLogFile> iisLogBag = new ConcurrentBag<IISLogFile>();
            IList<string> filePathList = FileTool.GetAllFileInFolder(iisLogQueryCondition.LogSourceFolderPath ?? string.Empty);

            IList<string> logFiles = filePathList.Where(p => p.EndsWith(".log", StringComparison.OrdinalIgnoreCase)).ToList();

            if (logFiles == null || logFiles.Count == 0)
                return iisLogFileList;

            //限制並行度，避免同時打開過多zip檔造成I/O與ThreadPool壓力
            ParallelOptions parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = Math.Max(1, Environment.ProcessorCount - 1)
            };

            //處理zip：列出zip內部檔名
            Parallel.ForEach(logFiles, parallelOptions, logPath =>
            {
                try
                {
                    DateTime? logDateTime = GetIISLogFileTime(Path.GetFileName(logPath)) ?? new DateTime(1900, 1, 1);

                    if (logDateTime >= iisLogQueryCondition.StartTime.Date && logDateTime <= iisLogQueryCondition.EndTime.Date)
                        iisLogBag.Add(GetLogFile(logPath, iisLogQueryCondition));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            });

            iisLogFileList = iisLogBag
                        .Where(iisLog => iisLog.LogList != null)
                        .OrderByDescending(iisLog => iisLog.FileTime)
                        .ToList();

            return iisLogFileList;
        }

        public IISLogFile GetLogFile(string logPath){
            IISLogQueryCondition iisLogQueryCondition = 
            new IISLogQueryCondition(Path.GetDirectoryName(logPath) ?? string.Empty)
            {
                StartTime = DateTime.MinValue,
                EndTime = DateTime.MaxValue
            };

            return GetLogFile(logPath, iisLogQueryCondition);
        }

        private IISLogFile GetLogFile(string logPath, IISLogQueryCondition iisLogQueryCondition)
        {
            string logName = Path.GetFileName(logPath);

            if (!FileTool.CheckFileExist(logPath))
                Debug.WriteLine($"找不到log，請檢查此路徑{logPath}");

            return new IISLogFile()
            {
                FileName = logName,
                FileTime = GetIISLogFileTime(logName),
                LogInfo = null,
                LogList = GetIISLogInfo(logPath, iisLogQueryCondition),
                SourceZIPPath = null,
                ParentFolderPath = Path.GetDirectoryName(logPath) ?? string.Empty
            };
        }

        private List<IISLogInfo> GetIISLogInfo(string logPath, IISLogQueryCondition iisLogQueryCondition)
        {
            //IIS Log的標準欄位分隔符是空格
            const char separator = ' ';
            List<IISLogInfo> result = new List<IISLogInfo>();
            //用來存放檔案中定義的欄位順序
            string[] fieldNames = Array.Empty<string>();
            //取得台灣的時區資訊(Windows時區ID)
            TimeZoneInfo taiwanTimeZone = TimeZoneInfo.FindSystemTimeZoneById(AppSettings.SystemSetting.TaiwanTimeZoneID);
            int count = 0, successCount = 0, deCodeError = 0, notInCondition = 0, noField = 0;

            if (!File.Exists(logPath)) return result;

            //定義欄位在IisLogInfo類別中的名稱，解析時能透過欄位名稱找到對應的屬性
            Dictionary<string, Action<IISLogInfo, string>> targetFields = new Dictionary<string, Action<IISLogInfo, string>>
            {
                {"date", (log, value) => log.DateStr = value},
                {"time", (log, value) => log.TimeStr = value},

                //客戶端與伺服器連線資訊
                {"c-ip", (log, value) => log.ClientIP = value},                         // Client IP Address
                {"s-ip", (log, value) => log.ServerIP = value},                         // Server IP Address
                {"s-port", (log, value) => log.ServerPort = value},                     // Server Port
                {"c-port", (log, value) => log.ClientPort = value},                     // Client Port

                //認證與使用者資訊
                {"cs-username", (log, value) => log.CSUsername = value},                // Authenticated user name
                {"cs(Cookie)", (log, value) => log.CSCookie = value},                   // Cookie data (Note: often empty or truncated)
                {"cs(User-Agent)", (log, value) => log.CSUserAgent = value},            // User Agent string
                {"cs(Referer)", (log, value) => log.CSReferer = value},                 // Referer (typo is intentional for W3C field)

                //請求相關資訊
                {"cs-method", (log, value) => log.CSMethod = value},                    // HTTP Method (GET, POST, etc.)
                {"cs-uri-stem", (log, value) => log.CSUriStem = value},                 // URI Stem (The resource being accessed)
                {"cs-uri-query", (log, value) => log.CSUriQuery = value},               // URI Query (Parameters after '?')
                {"cs-host", (log, value) => log.CSHost = value},                        // Host header value

                //伺服器響應與狀態
                {"sc-status", (log, value) => log.SCStatus = value},                    // HTTP Status Code (200, 404, 500, etc.)
                {"sc-substatus", (log, value) => log.SCSubstatus = value},              // Substatus code (e.g., 401.2)
                {"sc-win32-status", (log, value) => log.SCWin32Status = value},         // Win32 Error Code
                {"time-taken", (log, value) => log.TimeTaken = int.Parse(value)},       // Time taken to process the request (milliseconds)

                //傳輸位元組數
                {"sc-bytes", (log, value) => log.SCBytes = value},                      // Server to Client bytes sent (Response size)
                {"cs-bytes", (log, value) => log.CSBytes = value},                      // Client to Server bytes received (Request size)

                //協定與版本
                {"cs-protocol", (log, value) => log.CSProtocol = value},                // Protocol (HTTP/1.1, HTTP/2.0)
                {"cs-protocol-version", (log, value) => log.CSProtocolVersion = value}, // Protocol Version

                //網站名稱與應用程式相關
                {"s-sitename", (log, value) => log.ServerSiteName = value},             // Service name (usually W3SVC1)
                {"s-computername", (log, value) => log.ServerComputerName = value}      // Server name
            };

            foreach(string line in FileTool.ReadFile(logPath)){
                count++;
                //忽略空白行或註釋行 (以#開頭)
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
                {
                    //解析欄位定義行 (以 #Fields: 開頭)
                    if (line.StartsWith("#Fields:"))
                    {
                        //分隔並去除 #Fields: 前綴，然後去除空白並轉換為陣列
                        fieldNames = line.Substring("#Fields:".Length)
                                         .Split(separator, StringSplitOptions.RemoveEmptyEntries)
                                         //.Select(f => f.ToLowerInvariant()) // 統一轉為小寫以便匹配
                                         .ToArray();
                    }
                    continue;
                }

                //如果在資料行之前沒有找到#Fields:，則無法解析，跳過檔案
                if (fieldNames.Length == 0) { noField++; continue; }

                //解析資料行，StringSplitOptions.RemoveEmptyEntries處理多個連續空格
                string[] values = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                if (values.Length != fieldNames.Length) { deCodeError++; continue; };

                IISLogInfo currentLog = new IISLogInfo();
                string dateString = string.Empty;
                string timeString = string.Empty;

                //根據欄位名稱，將值設置到IisLogInfo物件中
                for (int i = 0; i < fieldNames.Length; i++)
                {
                    if (fieldNames[i] == "date") dateString = values[i];
                    if (fieldNames[i] == "time") timeString = values[i];
                    if (targetFields.ContainsKey(fieldNames[i]))
                        // 使用Action<IisLogInfo, string> 委派來設置對應的屬性
                        targetFields[fieldNames[i]].Invoke(currentLog, values[i]);
                }

                //解析LogTime為DateTime，IIS Log格式通常為yyyy-MM-dd和HH:mm:ss
                if (!string.IsNullOrEmpty(dateString) && !string.IsNullOrEmpty(timeString))
                    if (DateTime.TryParse($"{dateString} {timeString}", out DateTime parsedUTCTime))
                        currentLog.Time = TimeZoneInfo.ConvertTimeFromUtc(parsedUTCTime, taiwanTimeZone);

                currentLog.LogID = Path.GetFileName(logPath);

                if (
                    currentLog.Time >= iisLogQueryCondition.StartTime && currentLog.Time <= iisLogQueryCondition.EndTime
                    && (currentLog.SCStatus == iisLogQueryCondition.SCStatus || string.IsNullOrEmpty(iisLogQueryCondition.SCStatus))
                    && currentLog.CSUriStem.Contains(iisLogQueryCondition.CSUriStem)
                    && iisLogQueryCondition.IgnoreUriList.All(ignoreUri => currentLog.CSUriStem != ignoreUri)
                    && currentLog.TimeTaken >= iisLogQueryCondition.TimeTaken
                ){
                    result.Add(currentLog);
                    successCount++;
                }
                else{ notInCondition++; }
                    
            }

            return result;
        }

        private DateTime? GetIISLogFileTime(string logFileName)
        {
            DateTime? result = null;
            Match match = Regex.Match(logFileName, AppSettings.LogSetting.IISLogFileNamePattern);

            if (match.Success)
            {
                string year = match.Groups["year"].Value
                        , month = match.Groups["month"].Value
                        , day = match.Groups["day"].Value;
                if (int.TryParse(year, out int y) && int.TryParse(month, out int m) && int.TryParse(day, out int d))
                {
                    //為了建立 DateTime，需要一個世紀資訊
                    int fullYear = y + 2000;
                    try
                    {
                        result = new DateTime(fullYear, m, d);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Debug.WriteLine("警告: 解析的日期數值超出有效範圍。");
                    }
                }
            }
            return result;
        }
    }
}
