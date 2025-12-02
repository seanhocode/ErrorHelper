using ErrorHelper.Core.Model.BackupHelper;
using ErrorHelper.Infrastructure.Common.Configuration;

namespace ErrorHelper.App.ViewModel.Backup
{
    public class BackupFolderViewModel : ViewModelBase
    {
        private readonly BackupFolder _BackupFolder;

        public BackupFolderViewModel(BackupFolder backupFolder)
        {
            _BackupFolder = backupFolder;
            TempFolderPath = AppSettings.BackupSetting.TempFolderPath;
            SourceFolderPath = AppSettings.BackupSetting.SourceFolderPath;
            BackupFolderPath = AppSettings.BackupSetting.BackupFolderPath;
        }

        public string TempFolderPath
        {
            get => _BackupFolder.TempFolderPath;
            set { if (_BackupFolder.TempFolderPath != value) { _BackupFolder.TempFolderPath = value; OnPropertyChanged(nameof(Message)); } }
        }

        public string SourceFolderPath
        {
            get => _BackupFolder.SourceFolderPath;
            set { if (_BackupFolder.SourceFolderPath != value) { _BackupFolder.SourceFolderPath = value; OnPropertyChanged(nameof(SourceFolderPath)); } }
        }

        public string BackupFolderPath
        {
            get => _BackupFolder.BackupFolderPath;
            set { if (_BackupFolder.BackupFolderPath != value) { _BackupFolder.BackupFolderPath = value; OnPropertyChanged(nameof(BackupFolderPath)); } }
        }

        public BackupFolder BackupFolder => _BackupFolder;
    }
}
