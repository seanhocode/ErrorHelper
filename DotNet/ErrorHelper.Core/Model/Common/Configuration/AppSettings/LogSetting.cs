using System.ComponentModel;

namespace ErrorHelper.Core.Model.Common.Configuration.AppSettings
{
    public sealed class LogSetting
    {
        [DisplayName("預設Log資料夾路徑")]
        public string DefaultLogFolderPath { get; set; }
        [DisplayName("預設查詢幾天資料")]
        public int DefaultLogQueryDays { get; set; }
    }
}
