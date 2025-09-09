using ErrorHelper.Core.Model.Service.LogHelper;
using ErrorHelper.Core.Service.Log;

namespace ErrorHelper.Infrastructure.Service.LogHelper
{
    public class LogHelperService : ServiceBase, ILogHelperService<LogFile<LogInfo>, LogInfo, LogQueryCondition>
    {
        public LogFile<LogInfo> GetLogFile(string logPath)
        {
            throw new NotImplementedException();
        }

        public IList<LogFile<LogInfo>> GetLogFileList(LogQueryCondition logQueryCondition)
        {
            throw new NotImplementedException();
        }
    }
}
