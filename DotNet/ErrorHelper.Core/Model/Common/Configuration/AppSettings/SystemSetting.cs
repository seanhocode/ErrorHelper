using System.ComponentModel;

namespace ErrorHelper.Core.Model.Common.Configuration.AppSettings
{
    public sealed class SystemSetting
    {
        [DisplayName("時間格式")]
        public string TimeFormatStr { get; set; }
    }
}
