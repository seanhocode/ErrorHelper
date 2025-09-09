
namespace ErrorHelper.Core.Model.Service.LogHelper
{
    public class LogFile<T> where T : LogInfo
    {
        /// <summary>
        /// LogInfo
        /// </summary>
        public T? LogInfo { get; set; }

        /// <summary>
        /// ErrorFileName
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// 檔案產生時間
        /// </summary>
        public DateTime? FileTime { get; set; }

        /// <summary>
        /// 來自哪個ZIP(完整路徑)
        /// </summary>
        public string? SourceZIPPath { get; set; }

        /// <summary>
        /// 上層資料夾
        /// </summary>
        public string? ParentFolderPath { get; set; }
    }
}
