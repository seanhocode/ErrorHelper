using ErrorHelper.Core.Model.Common.Configuration;

namespace ErrorHelper.Core.Common.Configuration
{
    public interface IAppSettings
    {
        public static BackupSetting BackupSetting { get; }
        public static LogSetting LogSetting { get; }
    }
}
