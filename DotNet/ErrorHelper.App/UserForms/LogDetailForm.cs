using ErrorHelper.Core.Model.Service.LogHelper;

namespace ErrorHelper.App.UserForms
{
    public class LogDetailForm : Form
    {
        public LogDetailForm()
        {
            InitializeComponent();
        }

        public void SetLogDetail(LogInfo logInfo)
        {
            LogDetailTextBox.Text = logInfo.GetDetail();
        }

        private void InitializeComponent()
        {
            LogDetailTextBox = new TextBox();
            SuspendLayout();
            // 
            // LogDetailTextBox
            // 
            LogDetailTextBox.Dock = DockStyle.Fill;
            LogDetailTextBox.Location = new Point(0, 0);
            LogDetailTextBox.Multiline = true;
            LogDetailTextBox.Name = "LogDetailTextBox";
            LogDetailTextBox.TabIndex = 0;
            // 
            // LogDetailForm
            // 
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowOnly;
            ClientSize = new Size(1920, 1080);
            Controls.Add(LogDetailTextBox);
            Name = "LogDetailForm";
            Load += LogDetailForm_Load;
            ResumeLayout(false);
            PerformLayout();

        }
        private TextBox LogDetailTextBox;

        private void LogDetailForm_Load(object sender, EventArgs e)
        {

        }
    }
}
