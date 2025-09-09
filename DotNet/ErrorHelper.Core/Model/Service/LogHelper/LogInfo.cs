
namespace ErrorHelper.Core.Model.Service.LogHelper
{
    public class LogInfo
    {
        /// <summary>
        /// ErrorID
        /// </summary>
        public string? ErrorID { get; set; }

        /// <summary>
        /// 發生時間
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// 詳細錯誤訊息
        /// </summary>
        protected string? Detail { get; set; }

        /// <summary>
        /// 取得詳細錯誤訊息
        /// </summary>
        /// <returns>詳細錯誤訊息</returns>
        public string GetDetail()
        {
            return Detail ?? string.Empty;
        }
    }
}
