
namespace ErrorHelper.Model.ErrorHelper.ErrorBase
{
    public interface IErrorFile
    {
        IErrorInfo ErrorInfo { get; set; }
        string FileName { get; set; }
        string ParentFolderPath { get; set; }
        string? SourceZIPPath { get; set; }
        DateTime FileTime { get; set; }
    }
}