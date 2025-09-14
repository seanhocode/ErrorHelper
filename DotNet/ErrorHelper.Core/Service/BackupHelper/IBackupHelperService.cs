using ErrorHelper.Core.Model.Service.BackupHelper;

namespace ErrorHelper.Infrastructure.Service.BackupHelper
{
    public interface IBackupHelperService
    {
        /// <summary>
        /// 將Source裡與Temp相同路徑的檔案備份到Backup資料夾
        /// </summary>
        /// <param name="backupFolder">資料夾資訊</param>
        /// <remarks>Backup裡衝突的檔案會覆蓋</remarks>
        void BackupFolderByTemp(BackupFolder backupFolder);
    }
}