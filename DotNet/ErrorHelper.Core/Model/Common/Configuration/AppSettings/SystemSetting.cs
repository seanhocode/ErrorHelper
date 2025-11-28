using System.ComponentModel;

namespace ErrorHelper.Core.Model.Common.Configuration.AppSettings
{
    public sealed class SystemSetting
    {
        [DisplayName("時間格式")]
        public string TimeFormatStr { get; set; }

        [DisplayName("選擇時間控制項Format字串")]
        public string TimePickerFormatStr { get; set; }

        [DisplayName("台灣時區ID")]
        public string TaiwanTimeZoneID { get; set; }
    }
}
