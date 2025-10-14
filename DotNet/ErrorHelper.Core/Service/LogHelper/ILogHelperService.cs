
namespace ErrorHelper.Core.Service.LogHelper
{
    public interface ILogHelperService<TLogFile, TLogInfo, TLogQueryCondition> : IServiceBase
    {
        /// <summary>
        /// 查詢LogFile清單
        /// </summary>
        /// <param name="logQueryCondition"></param>
        /// <returns></returns>
        abstract IList<TLogFile> GetLogFileList(TLogQueryCondition logQueryCondition);

        /// <summary>
        /// 取得單個LogFile
        /// </summary>
        /// <param name="logPath"></param>
        /// <returns></returns>
        abstract TLogFile GetLogFile(string logPath);
    }
}
