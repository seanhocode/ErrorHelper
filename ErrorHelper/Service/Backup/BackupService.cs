namespace ErrorHelper.Service.Backup
{
    public class BackupService : ServiceBase
    {
        /// <summary>
        /// 將Source裡與Temp相同路徑的檔案備份到Backup資料夾
        /// </summary>
        /// <param name="tempFolder">Temp資料夾</param>
        /// <param name="sourceFolder">Source資料夾</param>
        /// <param name="backupFolder">Backup資料夾</param>
        /// <remarks>Backup裡衝突的檔案會覆蓋</remarks>
        public void BackupFolder(string tempFolder, string sourceFolder, string backupFolder)
        {
            List<string> tempFolderFilePathList = fileTool.GetAllFileInFolder(tempFolder, true);
            string sourceFilePath, backupFilePath = string.Empty, successMsg = string.Empty, notFoundMsg = string.Empty;
            List<(string Source, string Backup)> successFile = new List<(string Source, string Backup)>();
            int maxLeftLength = 0;

            //建立Backup資料夾
            fileTool.CheckFolderExist(backupFolder, true);

            foreach (string tempFilePath in tempFolderFilePathList)
            {
                sourceFilePath = tempFilePath.Replace(tempFolder, sourceFolder);
                backupFilePath = tempFilePath.Replace(tempFolder, backupFolder);

                //如果source有檔案才備份
                if (fileTool.CheckFileExist(sourceFilePath))
                {
                    fileTool.CheckFolderExist(Path.GetDirectoryName(backupFilePath), true);
                    File.Copy(sourceFilePath, backupFilePath, true);
                    successFile.Add((sourceFilePath, backupFilePath));
                }
                else{
                    notFoundMsg += $"{sourceFilePath}\r\n";
                }
            }

            maxLeftLength = successFile.Where(file => !string.IsNullOrEmpty(file.Source)).Max(file => file.Source.Length);

            successMsg = string.Join(Environment.NewLine
                , successFile.Select(file => $"{file.Source.PadRight(maxLeftLength)} => {file.Backup}"));

            if (!string.IsNullOrEmpty(successMsg))
                File.WriteAllText(Path.Combine(backupFolder, "Success.txt"), successMsg);
            if (!string.IsNullOrEmpty(notFoundMsg))
                File.WriteAllText(Path.Combine(backupFolder, "NotFound.txt"), notFoundMsg);
        }
    }
}
