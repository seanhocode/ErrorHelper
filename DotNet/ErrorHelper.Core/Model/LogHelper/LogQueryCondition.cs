namespace ErrorHelper.Core.Model.LogHelper
{
    public class LogQueryCondition
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string FileName { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public string LogSourceFolderPath { get; set; }
        public IList<string> IgnoreMessageList { get; set; }

        public LogQueryCondition(string logSourceFolderPath = "")
        {
            StartTime = new DateTime(1900, 1, 1);
            EndTime = new DateTime(2099, 12, 31);
            FileName = string.Empty;
            Message = string.Empty;
            Detail = string.Empty;
            LogSourceFolderPath = logSourceFolderPath;
            IgnoreMessageList = new List<string>();
        }
    }
}
