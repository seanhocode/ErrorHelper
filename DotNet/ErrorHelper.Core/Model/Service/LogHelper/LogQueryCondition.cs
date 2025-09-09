
namespace ErrorHelper.Core.Model.Service.LogHelper
{
    public class LogQueryCondition
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? FileName { get; set; }
        public string? Message { get; set; }
        public string? Detail { get; set; }
        public string? LogSourceFolderPath { get; set; }
    }
}
