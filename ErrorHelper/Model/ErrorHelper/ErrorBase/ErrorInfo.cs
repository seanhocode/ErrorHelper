namespace ErrorHelper.Model.ErrorHelper.ErrorBase
{
    public class ErrorInfo
    {
        public DateTime Time { get; set; }

        private string? Application { get; set; }

        private string? Host { get; set; }

        private string? Type { get; set; }

        public required string ErrorID { get; set; }

        public required string Message { get; set; }

        private string? Source { get; set; }

        private string? Detail { get; set; }
    }
}
