
namespace ErrorHelper.Core.Model.LogHelper.IISLog
{
    public class IISLogQueryCondition : LogQueryCondition
    {
        public IISLogQueryCondition(string logSourceFolderPath) : base(logSourceFolderPath)
        {
            StartTime = DateTime.Today.Date;
            EndTime = StartTime.AddDays(1);
        }
    }
}
