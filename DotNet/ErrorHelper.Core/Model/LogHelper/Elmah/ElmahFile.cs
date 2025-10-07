namespace ErrorHelper.Core.Model.LogHelper.Elmah
{
    public class ElmahFile : LogFile<ElmahInfo>
    {
        public string? GUID { get; set; } = string.Empty;
    }
}
