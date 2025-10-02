using System.Xml.Linq;

namespace ErrorHelper.Core.Service.LogHelper
{
    public interface IElmahHelperService<TLogFile, TLogInfo, TLogQueryCondition> : ILogHelperService<TLogFile, TLogInfo, TLogQueryCondition>
    {
        /// <summary>
        /// 取得Elmah時間、GUID by 檔名
        /// </summary>
        /// <param name="elmahName"></param>
        /// <returns></returns>
        (DateTime? ElmahTime, string GUID)? GetElmahFileNameData(string elmahName);

        /// <summary>
        /// 解析Elmah
        /// </summary>
        /// <param name="info"></param>
        TLogInfo GetElmahInfo(XDocument info);

        /// <summary>
        /// 取得Elmah壓縮檔檔名
        /// </summary>
        /// <param name="zipFileName"></param>
        /// <returns></returns>
        DateTime? GetElmahZipDateTime(string zipFileName);

        TLogFile GetLogFile(string logName, string logZipPath);

        /// <summary>
        /// 將特殊格式的UTC時間字串轉換為台灣時間(UTC+8)。
        /// </summary>
        /// <param name="zTimeStr">時間字串，格式:yyyy-MM-ddHHmmssZ</param>
        /// <returns>轉換後的台灣時間(DateTime)，若格式錯誤則回傳 null</returns>
        DateTime? ConvertZFormatToTaiwanTime(string zTimeStr);
    }
}
