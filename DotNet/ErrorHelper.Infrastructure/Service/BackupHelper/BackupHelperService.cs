using ErrorHelper.Core.Model.BackupHelper;
using System.Collections.Concurrent;
using SeanTool.CSharp;

namespace ErrorHelper.Infrastructure.Service.BackupHelper
{
    public class BackupHelperService : IBackupHelperService
    {
        public void BackupFolderByTemp(BackupFolder backupFolder)
        {
            IList<string> tempFolderFilePathList = FileTool.GetAllFileInFolder(backupFolder.TempFolderPath, true);

            ConcurrentBag<(string Source, string Backup)> successFiles = new ConcurrentBag<(string Source, string Backup)>();
            ConcurrentBag<string> notFoundFiles = new ConcurrentBag<string>();

            //建立Backup資料夾
            FileTool.CheckFolderExist(backupFolder.BackupFolderPath, true);

            //平行處理複製
            Parallel.ForEach(tempFolderFilePathList, tempFilePath =>
            {
                string sourceFilePath = tempFilePath.Replace(backupFolder.TempFolderPath, backupFolder.SourceFolderPath)
                    ,  backupFilePath = tempFilePath.Replace(backupFolder.TempFolderPath, backupFolder.BackupFolderPath);

                if (FileTool.CheckFileExist(sourceFilePath))
                {
                    //確保備份資料夾存在（multiple thread 安全）
                    Directory.CreateDirectory(Path.GetDirectoryName(backupFilePath)!);

                    File.Copy(sourceFilePath, backupFilePath, true);
                    successFiles.Add((sourceFilePath, backupFilePath));
                }
                else
                {
                    notFoundFiles.Add(sourceFilePath);
                }
            });

            int maxLeftLength =
                successFiles.Any()
                ? successFiles.Max(file => file.Source.Length)
                : 0;

            string successMsg = string.Join(
                Environment.NewLine,
                successFiles.Select(file =>
                    $"{file.Source.PadRight(maxLeftLength)} => {file.Backup}")
            );

            string notFoundMsg = string.Join(Environment.NewLine, notFoundFiles);

            if (!string.IsNullOrEmpty(successMsg))
                File.WriteAllText(Path.Combine(backupFolder.BackupFolderPath, "Success.txt"), successMsg);

            if (!string.IsNullOrEmpty(notFoundMsg))
                File.WriteAllText(Path.Combine(backupFolder.BackupFolderPath, "NotFound.txt"), notFoundMsg);
        }
    }
}
