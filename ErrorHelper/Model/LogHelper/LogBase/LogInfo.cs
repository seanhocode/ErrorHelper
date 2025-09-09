namespace ErrorHelper.Model.LogHelper.ErrorBase
{
    public class LogInfo
    {
        public string ErrorID { get; set; }
        public DateTime Time { get; set; }

        public string Message { get; set; }

        protected string? Detail { get; set; }

        public string GetDetail()
        {
            return Detail;
        }
    }
}
