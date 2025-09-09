
namespace ErrorHelper.Model.ErrorHelper.ErrorBase
{
    public interface IErrorInfo
    {
        string ErrorID { get; set; }
        string Message { get; set; }
        DateTime Time { get; set; }

        string GetDetail();
    }
}