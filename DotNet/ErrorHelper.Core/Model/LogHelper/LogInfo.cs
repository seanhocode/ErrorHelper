using ErrorHelper.Core.Model.LogHelper.Elmah;

namespace ErrorHelper.Core.Model.LogHelper
{
    public class LogInfo
    {
        /// <summary>
        /// LogID
        /// </summary>
        public string? LogID { get; set; } = string.Empty;

        /// <summary>
        /// 發生時間
        /// </summary>
        public DateTime Time { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string? Message { get; set; } = string.Empty;

        /// <summary>
        /// 錯誤訊息標頭
        /// </summary>
        /// <remarks>第一行前100字</remarks>
        public string? Title => Message.Split('\n')[0].Length > 100 ? Message.Split('\n')[0].Substring(0, 100) : Message.Split('\n')[0];

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
    }
}
