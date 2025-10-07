using System.ComponentModel;

namespace ErrorHelper.Core.Model.Common.Configuration
{
    public sealed class BackupSetting
    {
        [DisplayName("樣板資料夾路徑")]
        public string? TempFolderPath { get; set; }
        [DisplayName("欲備份資料夾路徑")]
        public string? SourceFolderPath { get; set; }
        [DisplayName("備份資料夾路徑")]
        public string? BackupFolderPath { get; set; }
    }
}
