using ErrorHelper.Core.Model.Service.LogHelper;

namespace ErrorHelper.Core.Service.Log
{
    public interface ILogHelperService : IServiceBase
    {
        IList<LogFile> GetLogList(LogQueryCondition logQueryCondition);

        string Test();
    }
}
