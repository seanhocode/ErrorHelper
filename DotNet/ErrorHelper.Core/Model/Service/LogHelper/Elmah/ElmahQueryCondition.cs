
namespace ErrorHelper.Core.Model.Service.LogHelper.Elmah
{
    public class ElmahQueryCondition : LogQueryCondition
    {
        public ElmahQueryCondition(string logSourceFolderPath) : base(logSourceFolderPath) 
        {
            StartTime = DateTime.Today.Date;
            EndTime = StartTime.AddDays(1);
        }
    }
}
