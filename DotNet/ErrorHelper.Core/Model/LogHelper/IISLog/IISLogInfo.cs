
namespace ErrorHelper.Core.Model.LogHelper.IISLog
{
    public class IISLogInfo : LogInfo
    {
        /// <summary>
        /// 客戶端 IP 地址 (c-ip)
        /// </summary>
        public string ClientIP { get; set; } = string.Empty;

        /// <summary>
        /// 伺服器 IP 地址 (s-ip)
        /// </summary>
        public string ServerIP { get; set; } = string.Empty;

        /// <summary>
        /// 伺服器連接埠 (s-port)
        /// </summary>
        public string ServerPort { get; set; } = string.Empty;

        /// <summary>
        /// 客戶端連接埠 (c-port)
        /// </summary>
        public string ClientPort { get; set; } = string.Empty;

        /// <summary>
        /// 經過認證的使用者名稱 (cs-username)
        /// </summary>
        public string CSUsername { get; set; } = string.Empty;

        /// <summary>
        /// 傳送的 Cookie 資料 (cs(Cookie))
        /// </summary>
        public string CSCookie { get; set; } = string.Empty;

        /// <summary>
        /// 使用者代理字串 (瀏覽器/客戶端資訊) (cs(User-Agent))
        /// </summary>
        public string CSUserAgent { get; set; } = string.Empty;

        /// <summary>
        /// 引用網址 (Referer) (cs(Referer))
        /// </summary>
        public string CSReferer { get; set; } = string.Empty;

        /// <summary>
        /// HTTP 請求方法 (GET, POST, etc.) (cs-method)
        /// </summary>
        public string CSMethod { get; set; } = string.Empty;

        /// <summary>
        /// 請求的 URI Stem (資源路徑) (cs-uri-stem)
        /// </summary>
        public string CSUriStem { get; set; } = string.Empty;

        /// <summary>
        /// URI 查詢字串 (問號後面的參數) (cs-uri-query)
        /// </summary>
        public string CSUriQuery { get; set; } = string.Empty;

        /// <summary>
        /// Host 標頭值 (cs-host)
        /// </summary>
        public string CSHost { get; set; } = string.Empty;

        /// <summary>
        /// HTTP 狀態碼 (200, 404, 500, etc.) (sc-status)
        /// </summary>
        public string SCStatus { get; set; } = string.Empty;

        /// <summary>
        /// HTTP 子狀態碼 (sc-substatus)
        /// </summary>
        public string SCSubstatus { get; set; } = string.Empty;

        /// <summary>
        /// Win32 錯誤碼 (sc-win32-status)
        /// </summary>
        public string SCWin32Status { get; set; } = string.Empty;

        /// <summary>
        /// 處理請求所花費的時間 (毫秒) (time-taken)
        /// </summary>
        public string TimeTaken { get; set; } = string.Empty;

        /// <summary>
        /// 伺服器傳送到客戶端的位元組數 (Response size) (sc-bytes)
        /// </summary>
        public string SCBytes { get; set; } = string.Empty;

        /// <summary>
        /// 客戶端傳送到伺服器的位元組數 (Request size) (cs-bytes)
        /// </summary>
        public string CSBytes { get; set; } = string.Empty;

        /// <summary>
        /// 協定名稱 (HTTP, HTTPS, etc.) (cs-protocol)
        /// </summary>
        public string CSProtocol { get; set; } = string.Empty;

        /// <summary>
        /// 協定版本 (HTTP/1.1, HTTP/2.0) (cs-protocol-version)
        /// </summary>
        public string CSProtocolVersion { get; set; } = string.Empty;

        /// <summary>
        /// 服務名稱 (通常是 W3SVC1) (s-sitename)
        /// </summary>
        public string ServerSiteName { get; set; } = string.Empty;

        /// <summary>
        /// 伺服器電腦名稱 (s-computername)
        /// </summary>
        public string ServerComputerName { get; set; } = string.Empty;
    }
}
