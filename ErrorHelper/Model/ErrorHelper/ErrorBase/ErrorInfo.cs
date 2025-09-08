namespace ErrorHelper.Model.ErrorHelper.ErrorBase
{
    public class ErrorInfo : IErrorInfo
    {
        public required string ErrorID { get; set; }
        public DateTime Time { get; set; }

        public required string Message { get; set; }

        private string? Detail { get; set; }

        public string GetDetail()
        {
            return Detail;
        }
    }
}
