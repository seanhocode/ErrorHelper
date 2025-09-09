using ErrorHelper.Core.Model.Service.LogHelper;
using ErrorHelper.Core.Service.Log;
using System.Xml.Linq;

namespace ErrorHelper.Core.Service.LogHelper
{
    public interface IElmahService<TLogFile, TLogInfo, TLogQueryCondition> : ILogHelperService<TLogFile, TLogInfo, TLogQueryCondition>
    {
        (DateTime? ElmahTime, string GUID)? GetElmahFileNameData(string elmahName);
        TLogInfo GetElmahInfo(XDocument info);
        DateTime? GetElmahZipDateTime(string zipFileName);
        TLogFile GetLogFile(string logName, string logZipPath);
    }
}
