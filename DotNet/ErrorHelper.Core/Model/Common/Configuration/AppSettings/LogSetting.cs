using System.ComponentModel;

namespace ErrorHelper.Core.Model.Common.Configuration.AppSettings
{
    public sealed class LogSetting
    {
        [DisplayName("預設Log資料夾路徑")]
        public string DefaultLogFolderPath { get; set; }

        [DisplayName("預設查詢幾天資料")]
        public int DefaultLogQueryDays { get; set; }

        [DisplayName("Elmah檔案解析RegexPattern")]
        public string ElmahFileNamePattern{ get; set; }

        [DisplayName("Elmah檔案時間解析Pattern")]
        public string ElmahFileTimeFormatPattern { get; set; }

        [DisplayName("Elmah壓縮檔時間解析Pattern")]
        public string ElmahZipTimeFormatPattern { get; set; }

        [DisplayName("IISLog檔案解析RegexPattern")]
        public string IISLogFileNamePattern { get; set; }
    }
}
