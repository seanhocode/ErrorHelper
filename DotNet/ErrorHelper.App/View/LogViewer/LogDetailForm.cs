using ErrorHelper.Core.Model.LogHelper;

namespace ErrorHelper.App.View.LogViewer
{
    public partial class LogDetailForm : Form
    {
        public LogDetailForm()
        {
            InitializeComponent();
        }

        public void SetLogDetail(LogInfo logInfo)
        {
            LogIDTextBox.Text = logInfo.LogID;
            LogTimeTextBox.Text = logInfo.Time.ToString("yyyy/MM/dd dddd tt hh:mm:ss");
            LogMessageTextBox.Text = logInfo.Message;
            LogDetailTextBox.Text = logInfo.GetDetail();
            if (string.IsNullOrEmpty(LogDetailTextBox.Text)){
                LogMessageTextBox.Text = logInfo.Title;
                LogDetailTextBox.Text = logInfo.Message;
            }
        }
    }
}
