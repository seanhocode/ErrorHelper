using ErrorHelper.Core.Model.LogHelper.IISLog;

namespace ErrorHelper.Infrastructure.Service.LogHelper
{
    public class IISLogHelperService : ServiceBase, IIISLogHelperService<IISLogFile, IISLogInfo, IISLogQueryCondition>
    {
        public IList<IISLogFile> GetLogFileList(IISLogQueryCondition elmahQueryCondition)
        {
            return new List<IISLogFile>();
        }

        public IISLogFile GetLogFile(string logPath)
        {
            return new IISLogFile();
        }
    }
}
