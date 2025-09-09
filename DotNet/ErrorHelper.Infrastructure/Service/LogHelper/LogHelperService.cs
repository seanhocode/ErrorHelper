using ErrorHelper.Core.Model.Service.LogHelper;
using ErrorHelper.Core.Service.Log;

namespace ErrorHelper.Infrastructure.Service.LogHelper
{
    public class LogHelperService : ServiceBase, ILogHelperService
    {
        public IList<LogFile> GetLogList(LogQueryCondition logQueryCondition)
        {
            return new List<LogFile>();
        }

        public string Test(){
            return "HelloWorld";
        }
    }
}
