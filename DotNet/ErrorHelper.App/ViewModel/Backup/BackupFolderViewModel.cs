using ErrorHelper.Core.Model.BackupHelper;
using ErrorHelper.Infrastructure.Common.Configuration;
using SeanTool.CSharp.Forms;

namespace ErrorHelper.App.ViewModel.Backup
{
    public class BackupFolderViewModel : ViewModelBase
    {
        private readonly BackupFolder _BackupFolder;

        public BackupFolderViewModel(BackupFolder backupFolder)
        {
            _BackupFolder = backupFolder;
            TempFolderPath = AppSettings.BackupSetting.TempFolderPath ?? string.Empty;
            SourceFolderPath = AppSettings.BackupSetting.SourceFolderPath ?? string.Empty;
            BackupFolderPath = AppSettings.BackupSetting.BackupFolderPath ?? string.Empty;
        }

        [EditorPath(PathType.Folder)]
        public string TempFolderPath
        {
            get => _BackupFolder.TempFolderPath ?? string.Empty;
            set { if (_BackupFolder.TempFolderPath != value) { _BackupFolder.TempFolderPath = value; OnPropertyChanged(nameof(Message)); } }
        }

        [EditorPath(PathType.Folder)]
        public string SourceFolderPath
        {
            get => _BackupFolder.SourceFolderPath ?? string.Empty;
            set { if (_BackupFolder.SourceFolderPath != value) { _BackupFolder.SourceFolderPath = value; OnPropertyChanged(nameof(SourceFolderPath)); } }
        }

        [EditorPath(PathType.Folder)]
        public string BackupFolderPath
        {
            get => _BackupFolder.BackupFolderPath ?? string.Empty;
            set { if (_BackupFolder.BackupFolderPath != value) { _BackupFolder.BackupFolderPath = value; OnPropertyChanged(nameof(BackupFolderPath)); } }
        }

        public BackupFolder BackupFolder => _BackupFolder;
    }
}
