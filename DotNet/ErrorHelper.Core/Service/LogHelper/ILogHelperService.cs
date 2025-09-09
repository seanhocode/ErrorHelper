
namespace ErrorHelper.Core.Service.Log
{
    public interface ILogHelperService<TLogFile, TLogInfo, TLogQueryCondition> : IServiceBase
    {
        abstract IList<TLogFile> GetLogFileList(TLogQueryCondition logQueryCondition);
        abstract TLogFile GetLogFile(string logPath);
    }
}
