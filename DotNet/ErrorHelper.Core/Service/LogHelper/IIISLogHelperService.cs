using ErrorHelper.Core.Service.LogHelper;

namespace ErrorHelper.Infrastructure.Service.LogHelper
{
    public interface IIISLogHelperService<TLogFile, TLogInfo, TLogQueryCondition> : ILogHelperService<TLogFile, TLogInfo, TLogQueryCondition>
    {
    }
}