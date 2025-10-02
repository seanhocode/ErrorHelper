using ErrorHelper.Core.Model.BackupHelper;
using ErrorHelper.Tool;

namespace ErrorHelper.Infrastructure.Service.BackupHelper
{
    public class BackupHelperService : IBackupHelperService
    {
        public void BackupFolderByTemp(BackupFolder backupFolder)
        {
            List<string> tempFolderFilePathList = FileTool.GetAllFileInFolder(backupFolder.TempFolderPath, true);
            string sourceFilePath, backupFilePath = string.Empty, successMsg = string.Empty, notFoundMsg = string.Empty;
            List<(string Source, string Backup)> successFile = new List<(string Source, string Backup)>();
            int maxLeftLength = 0;

            //建立Backup資料夾
            FileTool.CheckFolderExist(backupFolder.BackupFolderPath, true);

            foreach (string tempFilePath in tempFolderFilePathList)
            {
                sourceFilePath = tempFilePath.Replace(backupFolder.TempFolderPath, backupFolder.SourceFolderPath);
                backupFilePath = tempFilePath.Replace(backupFolder.TempFolderPath, backupFolder.BackupFolderPath);

                //如果source有檔案才備份
                if (FileTool.CheckFileExist(sourceFilePath))
                {
                    FileTool.CheckFolderExist(Path.GetDirectoryName(backupFilePath), true);
                    File.Copy(sourceFilePath, backupFilePath, true);
                    successFile.Add((sourceFilePath, backupFilePath));
                }
                else
                {
                    notFoundMsg += $"{sourceFilePath}\r\n";
                }
            }

            if( successFile.Count > 0 ) 
                maxLeftLength = successFile.Where(file => !string.IsNullOrEmpty(file.Source)).Max(file => file.Source.Length);
            else
                maxLeftLength = 0;

                successMsg = string.Join(Environment.NewLine
                    , successFile.Select(file => $"{file.Source.PadRight(maxLeftLength)} => {file.Backup}"));

            if (!string.IsNullOrEmpty(successMsg))
                File.WriteAllText(Path.Combine(backupFolder.BackupFolderPath, "Success.txt"), successMsg);
            if (!string.IsNullOrEmpty(notFoundMsg))
                File.WriteAllText(Path.Combine(backupFolder.BackupFolderPath, "NotFound.txt"), notFoundMsg);
        }
    }
}
