namespace ErrorHelper.Core.Model.LogHelper
{
    public class LogInfo
    {
        /// <summary>
        /// LogID
        /// </summary>
        private string? LogID { get; set; } = string.Empty;

        /// <summary>
        /// 發生時間
        /// </summary>
        public DateTime Time { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string? Message { get; set; } = string.Empty;

        /// <summary>
        /// 詳細錯誤訊息
        /// </summary>
        private string? Detail { get; set; } = string.Empty;

        /// <summary>
        /// 取得詳細錯誤訊息
        /// </summary>
        /// <returns>詳細錯誤訊息</returns>
        public string GetDetail()
        {
            return Detail ?? string.Empty;
        }

        public void SetDetail(string detail)
        {
            Detail = detail;
        }

        public string GetLogID()
        {
            return LogID ?? string.Empty;
        }

        public void SetLogID(string logID)
        {
            LogID = logID;
        }
    }
}
