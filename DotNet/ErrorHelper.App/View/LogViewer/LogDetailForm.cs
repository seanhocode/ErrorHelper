using ErrorHelper.Core.Model.LogHelper;
using ErrorHelper.Infrastructure.Common.Configuration;

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
            LogTimeTextBox.Text = logInfo.Time.ToString(AppSettings.SystemSetting.TimeFormatStr);
            LogMessageTextBox.Text = logInfo.Message;
            LogDetailTextBox.Text = logInfo.GetDetail();
            if (string.IsNullOrEmpty(LogDetailTextBox.Text)){
                LogMessageTextBox.Text = logInfo.Title;
                LogDetailTextBox.Text = logInfo.Message;
            }
        }
    }
}
