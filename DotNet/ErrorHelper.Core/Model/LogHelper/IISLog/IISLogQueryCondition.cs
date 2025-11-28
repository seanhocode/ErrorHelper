
using System.Net.NetworkInformation;

namespace ErrorHelper.Core.Model.LogHelper.IISLog
{
    public class IISLogQueryCondition : LogQueryCondition
    {
        public string SCStatus { get; set; }
        public string CSUriStem { get; set; }
        public int TimeTaken { get; set; }
        public IList<string> IgnoreUriList { get; set; }

        public IISLogQueryCondition(string logSourceFolderPath) : base(logSourceFolderPath)
        {
            SCStatus = string.Empty;
            CSUriStem = string.Empty;
            TimeTaken = 0;
            IgnoreUriList = new List<string>();
            StartTime = DateTime.Today.Date;
            EndTime = StartTime.AddDays(1);
        }
    }
}
