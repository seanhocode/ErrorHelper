
namespace ErrorHelper.Core.Model.LogHelper.IISLog
{
    public class IISLogFile : LogFile<IISLogInfo>
    {
        public List<IISLogInfo> LogList { get; set; } = new List<IISLogInfo>();
    }
}
