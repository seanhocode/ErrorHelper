
namespace ErrorHelper.Core.Model.Service.LogHelper.Elmah
{
    public class ElmahInfo : LogInfo
    {
        public string? Application { get; set; }

        public string? Host { get; set; }

        public string? Type { get; set; }

        public string? Source { get; set; }
    }
}
