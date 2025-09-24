
namespace ErrorHelper.Core.Model.Service.LogHelper.Elmah
{
    public class ElmahInfo : LogInfo
    {
        public string? Application { get; set; } = string.Empty;

        public string? Host { get; set; } = string.Empty;

        public string? Type { get; set; } = string.Empty;

        public string? Source { get; set; } = string.Empty;
    }
}
