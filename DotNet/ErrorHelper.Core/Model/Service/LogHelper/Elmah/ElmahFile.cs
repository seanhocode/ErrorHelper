
namespace ErrorHelper.Core.Model.Service.LogHelper.Elmah
{
    public class ElmahFile : LogFile<ElmahInfo>
    {
        public string? GUID { get; set; } = string.Empty;
    }
}
