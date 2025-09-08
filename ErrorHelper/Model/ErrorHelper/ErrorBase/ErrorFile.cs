namespace ErrorHelper.Model.ErrorHelper.ErrorBase
{
    public class ErrorFile : IErrorFile
    {
        /// <summary>
        /// ErrorInfo
        /// </summary>
        public required IErrorInfo ErrorInfo { get; set; }

        /// <summary>
        /// ErrorFileName
        /// </summary>
        public required string FileName { get; set; }

        /// <summary>
        /// 檔案產生時間
        /// </summary>
        public DateTime FileTime { get; set; }

        /// <summary>
        /// 來自哪個ZIP(完整路徑)
        /// </summary>
        public string? SourceZIPPath { get; set; }

        /// <summary>
        /// 上層資料夾
        /// </summary>
        public required string ParentFolderPath { get; set; }
    }
}
