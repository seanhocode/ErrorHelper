using ErrorHelper.Core.Model.LogHelper.IISLog;
using System.ComponentModel; // 新增此命名空間以支援 DisplayName 屬性

namespace ErrorHelper.App.ViewModel.Viewer.LogViewer
{
    public class IISLogInfoViewModel : ViewModelBase
    {
        private readonly IISLogInfo _IISLogInfo;

        public IISLogInfoViewModel(IISLogInfo iisLogInfo)
        {
            _IISLogInfo = iisLogInfo;
        }

        [DisplayName("Date")]
        public string DateStr
        {
            get => _IISLogInfo.DateStr;
            set { if (_IISLogInfo.DateStr != value) { _IISLogInfo.DateStr = value; OnPropertyChanged(nameof(DateStr)); } }
        }

        [DisplayName("Time")]
        public string TimeStr
        {
            get => _IISLogInfo.TimeStr;
            set { if (_IISLogInfo.TimeStr != value) { _IISLogInfo.TimeStr = value; OnPropertyChanged(nameof(TimeStr)); } }
        }

        [DisplayName("請求端 IP 地址 (c-ip)")]
        public string ClientIP
        {
            get => _IISLogInfo.ClientIP;
            set { if (_IISLogInfo.ClientIP != value) { _IISLogInfo.ClientIP = value; OnPropertyChanged(nameof(ClientIP)); } }
        }

        [DisplayName("伺服器 IP 地址 (s-ip)")]
        public string ServerIP
        {
            get => _IISLogInfo.ServerIP;
            set { if (_IISLogInfo.ServerIP != value) { _IISLogInfo.ServerIP = value; OnPropertyChanged(nameof(ServerIP)); } }
        }

        [DisplayName("伺服器連接埠 (s-port)")]
        public string ServerPort
        {
            get => _IISLogInfo.ServerPort;
            set { if (_IISLogInfo.ServerPort != value) { _IISLogInfo.ServerPort = value; OnPropertyChanged(nameof(ServerPort)); } }
        }

        [DisplayName("請求端連接埠 (c-port)")]
        public string ClientPort
        {
            get => _IISLogInfo.ClientPort;
            set { if (_IISLogInfo.ClientPort != value) { _IISLogInfo.ClientPort = value; OnPropertyChanged(nameof(ClientPort)); } }
        }

        [DisplayName("經過認證的使用者名稱 (cs-username)")]
        public string CSUsername
        {
            get => _IISLogInfo.CSUsername;
            set { if (_IISLogInfo.CSUsername != value) { _IISLogInfo.CSUsername = value; OnPropertyChanged(nameof(CSUsername)); } }
        }

        [DisplayName("傳送的 Cookie 資料 (cs(Cookie))")]
        public string CSCookie
        {
            get => _IISLogInfo.CSCookie;
            set { if (_IISLogInfo.CSCookie != value) { _IISLogInfo.CSCookie = value; OnPropertyChanged(nameof(CSCookie)); } }
        }

        [DisplayName("使用者代理字串 (瀏覽器/請求端資訊) (cs(User-Agent))")]
        public string CSUserAgent
        {
            get => _IISLogInfo.CSUserAgent;
            set { if (_IISLogInfo.CSUserAgent != value) { _IISLogInfo.CSUserAgent = value; OnPropertyChanged(nameof(CSUserAgent)); } }
        }

        [DisplayName("引用網址 (Referer) (cs(Referer))")]
        public string CSReferer
        {
            get => _IISLogInfo.CSReferer;
            set { if (_IISLogInfo.CSReferer != value) { _IISLogInfo.CSReferer = value; OnPropertyChanged(nameof(CSReferer)); } }
        }

        [DisplayName("HTTP 請求方法 (GET, POST, etc.) (cs-method)")]
        public string CSMethod
        {
            get => _IISLogInfo.CSMethod;
            set { if (_IISLogInfo.CSMethod != value) { _IISLogInfo.CSMethod = value; OnPropertyChanged(nameof(CSMethod)); } }
        }

        [DisplayName("請求的 URI Stem (資源路徑) (cs-uri-stem)")]
        public string CSUriStem
        {
            get => _IISLogInfo.CSUriStem;
            set { if (_IISLogInfo.CSUriStem != value) { _IISLogInfo.CSUriStem = value; OnPropertyChanged(nameof(CSUriStem)); } }
        }

        [DisplayName("URI 查詢字串 (問號後面的參數) (cs-uri-query)")]
        public string CSUriQuery
        {
            get => _IISLogInfo.CSUriQuery;
            set { if (_IISLogInfo.CSUriQuery != value) { _IISLogInfo.CSUriQuery = value; OnPropertyChanged(nameof(CSUriQuery)); } }
        }

        [DisplayName("Host 標頭值 (cs-host)")]
        public string CSHost
        {
            get => _IISLogInfo.CSHost;
            set { if (_IISLogInfo.CSHost != value) { _IISLogInfo.CSHost = value; OnPropertyChanged(nameof(CSHost)); } }
        }

        [DisplayName("HTTP 狀態碼 (200, 404, 500, etc.) (sc-status)")]
        public string SCStatus
        {
            get => _IISLogInfo.SCStatus;
            set { if (_IISLogInfo.SCStatus != value) { _IISLogInfo.SCStatus = value; OnPropertyChanged(nameof(SCStatus)); } }
        }

        [DisplayName("HTTP 子狀態碼 (sc-substatus)")]
        public string SCSubstatus
        {
            get => _IISLogInfo.SCSubstatus;
            set { if (_IISLogInfo.SCSubstatus != value) { _IISLogInfo.SCSubstatus = value; OnPropertyChanged(nameof(SCSubstatus)); } }
        }

        [DisplayName("Win32 錯誤碼 (sc-win32-status)")]
        public string SCWin32Status
        {
            get => _IISLogInfo.SCWin32Status;
            set { if (_IISLogInfo.SCWin32Status != value) { _IISLogInfo.SCWin32Status = value; OnPropertyChanged(nameof(SCWin32Status)); } }
        }

        [DisplayName("處理請求所花費的時間 (毫秒) (time-taken)")]
        public int TimeTaken
        {
            get => _IISLogInfo.TimeTaken;
            // 由於 TimeTaken 是 int 類型，需要進行值比較
            set { if (_IISLogInfo.TimeTaken != value) { _IISLogInfo.TimeTaken = value; OnPropertyChanged(nameof(TimeTaken)); } }
        }

        [DisplayName("伺服器傳送到請求端的位元組數 (Response size) (sc-bytes)")]
        public string SCBytes
        {
            get => _IISLogInfo.SCBytes;
            set { if (_IISLogInfo.SCBytes != value) { _IISLogInfo.SCBytes = value; OnPropertyChanged(nameof(SCBytes)); } }
        }

        [DisplayName("請求端傳送到伺服器的位元組數 (Request size) (cs-bytes)")]
        public string CSBytes
        {
            get => _IISLogInfo.CSBytes;
            set { if (_IISLogInfo.CSBytes != value) { _IISLogInfo.CSBytes = value; OnPropertyChanged(nameof(CSBytes)); } }
        }

        [DisplayName("協定名稱 (HTTP, HTTPS, etc.) (cs-protocol)")]
        public string CSProtocol
        {
            get => _IISLogInfo.CSProtocol;
            set { if (_IISLogInfo.CSProtocol != value) { _IISLogInfo.CSProtocol = value; OnPropertyChanged(nameof(CSProtocol)); } }
        }

        [DisplayName("協定版本 (HTTP/1.1, HTTP/2.0) (cs-protocol-version)")]
        public string CSProtocolVersion
        {
            get => _IISLogInfo.CSProtocolVersion;
            set { if (_IISLogInfo.CSProtocolVersion != value) { _IISLogInfo.CSProtocolVersion = value; OnPropertyChanged(nameof(CSProtocolVersion)); } }
        }

        [DisplayName("服務名稱 (通常是 W3SVC1) (s-sitename)")]
        public string ServerSiteName
        {
            get => _IISLogInfo.ServerSiteName;
            set { if (_IISLogInfo.ServerSiteName != value) { _IISLogInfo.ServerSiteName = value; OnPropertyChanged(nameof(ServerSiteName)); } }
        }

        [DisplayName("伺服器電腦名稱 (s-computername)")]
        public string ServerComputerName
        {
            get => _IISLogInfo.ServerComputerName;
            set { if (_IISLogInfo.ServerComputerName != value) { _IISLogInfo.ServerComputerName = value; OnPropertyChanged(nameof(ServerComputerName)); } }
        }
    }
}