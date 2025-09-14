using ErrorHelper.App.Core.Model;
using ErrorHelper.App.Service.FormControl;
using ErrorHelper.App.ViewModel.Viewer.LogViewer;
using ErrorHelper.Core.Model.Service.LogHelper;
using ErrorHelper.Infrastructure.Common.Configuration;
using System.Diagnostics;

namespace ErrorHelper.App.View.Control.Viewer.LogViewer
{
    public partial class LogViewerControl : UserControl
    {
        protected readonly LogQueryConditionViewModel _LogQueryConditionViewModel;
        protected FormControlService controlSrv = new FormControlService();

        public IList<LogFile<LogInfo>> LogFileList { get; set; }
        public IList<LogInfo> LogInfoList => LogFileList.Select(logFile => logFile.LogInfo).ToList<LogInfo>() ?? [];

        protected DateTimePicker StartTimePicker;
        protected DateTimePicker EndTimePicker;
        protected TextBox FileNameTextBox;
        protected TextBox MessageTextBox;
        protected TextBox DetailTextBox;
        protected Label StartTimeConditionLable;
        protected Label EndTimeConditionLabel;
        protected Label FileNameConditionLabel;
        protected Label MessageConditionLabel;
        protected Label DetailConditionLabel;
        protected Label FolderPathConditionLabel;
        protected Button QueryLogBtn;
        protected Button ChangeLogFolderBtn;
        protected TableLayoutPanel LogViewerTableLayoutPanel;
        protected DataGridView LogInfoDataGridView;
        protected Label ErrorSourceFolderPathLabel;
        protected LogDetailForm LogDetailForm;

        public Func<LogQueryCondition, IList<LogFile<LogInfo>>> ClickQueryLogBtn;

        public LogViewerControl()
        {
            InitializeComponent();
            InitializeOtherControl();
        }

        public LogViewerControl(LogQueryConditionViewModel viewModel)
        {
            InitializeComponent();
            InitializeOtherControl();
            _LogQueryConditionViewModel = viewModel;
            SetViewModel();
        }

        protected virtual void InitializeOtherControl()
        {
            LogDetailForm = new LogDetailForm();
        }

        protected virtual void SetViewModel()
        {
            // 綁定 UI 和 ViewModel
            StartTimePicker.DataBindings.Add("Value", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.StartTime));
            EndTimePicker.DataBindings.Add("Value", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.EndTime));
            FileNameTextBox.DataBindings.Add("Text", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.FileName));
            MessageTextBox.DataBindings.Add("Text", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.Message));
            DetailTextBox.DataBindings.Add("Text", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.Detail));
            ErrorSourceFolderPathLabel.DataBindings.Add("Text", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.LogSourceFolderPath));
        }

        protected void InitializeComponent()
        {
            StartTimePicker = new DateTimePicker();
            EndTimePicker = new DateTimePicker();
            FileNameTextBox = new TextBox();
            MessageTextBox = new TextBox();
            DetailTextBox = new TextBox();
            ErrorSourceFolderPathLabel = new Label();
            StartTimeConditionLable = new Label();
            EndTimeConditionLabel = new Label();
            FileNameConditionLabel = new Label();
            MessageConditionLabel = new Label();
            DetailConditionLabel = new Label();
            FolderPathConditionLabel = new Label();
            QueryLogBtn = new Button();
            ChangeLogFolderBtn = new Button();
            LogViewerTableLayoutPanel = new TableLayoutPanel();
            LogInfoDataGridView = new DataGridView();
            LogViewerTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LogInfoDataGridView).BeginInit();
            SuspendLayout();
            // 
            // StartTimePicker
            // 
            StartTimePicker.Anchor = AnchorStyles.Left;
            StartTimePicker.Location = new Point(134, 48);
            StartTimePicker.Name = "StartTimePicker";
            StartTimePicker.Size = new Size(200, 23);
            StartTimePicker.TabIndex = 3;
            // 
            // EndTimePicker
            // 
            EndTimePicker.Anchor = AnchorStyles.Left;
            EndTimePicker.Location = new Point(476, 48);
            EndTimePicker.Name = "EndTimePicker";
            EndTimePicker.Size = new Size(200, 23);
            EndTimePicker.TabIndex = 5;
            // 
            // FileNameTextBox
            // 
            FileNameTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            LogViewerTableLayoutPanel.SetColumnSpan(FileNameTextBox, 3);
            FileNameTextBox.Location = new Point(134, 88);
            FileNameTextBox.Name = "FileNameTextBox";
            FileNameTextBox.Size = new Size(890, 23);
            FileNameTextBox.TabIndex = 7;
            // 
            // MessageTextBox
            // 
            MessageTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            LogViewerTableLayoutPanel.SetColumnSpan(MessageTextBox, 3);
            MessageTextBox.Location = new Point(134, 128);
            MessageTextBox.Name = "MessageTextBox";
            MessageTextBox.Size = new Size(890, 23);
            MessageTextBox.TabIndex = 9;
            // 
            // DetailTextBox
            // 
            DetailTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            LogViewerTableLayoutPanel.SetColumnSpan(DetailTextBox, 3);
            DetailTextBox.Location = new Point(134, 168);
            DetailTextBox.Name = "DetailTextBox";
            DetailTextBox.Size = new Size(890, 23);
            DetailTextBox.TabIndex = 11;
            // 
            // ErrorSourceFolderPathLabel
            // 
            ErrorSourceFolderPathLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            LogViewerTableLayoutPanel.SetColumnSpan(ErrorSourceFolderPathLabel, 3);
            ErrorSourceFolderPathLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            ErrorSourceFolderPathLabel.Location = new Point(134, 8);
            ErrorSourceFolderPathLabel.Name = "ErrorSourceFolderPathLabel";
            ErrorSourceFolderPathLabel.Size = new Size(890, 23);
            ErrorSourceFolderPathLabel.TabIndex = 1;
            // 
            // StartTimeConditionLable
            // 
            StartTimeConditionLable.Anchor = AnchorStyles.Left;
            StartTimeConditionLable.AutoSize = true;
            StartTimeConditionLable.Font = new Font("Microsoft JhengHei UI", 15F);
            StartTimeConditionLable.Location = new Point(3, 47);
            StartTimeConditionLable.Name = "StartTimeConditionLable";
            StartTimeConditionLable.Size = new Size(106, 25);
            StartTimeConditionLable.TabIndex = 2;
            StartTimeConditionLable.Text = "StartTime:";
            // 
            // EndTimeConditionLabel
            // 
            EndTimeConditionLabel.Anchor = AnchorStyles.Left;
            EndTimeConditionLabel.AutoSize = true;
            EndTimeConditionLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            EndTimeConditionLabel.Location = new Point(371, 47);
            EndTimeConditionLabel.Name = "EndTimeConditionLabel";
            EndTimeConditionLabel.Size = new Size(99, 25);
            EndTimeConditionLabel.TabIndex = 4;
            EndTimeConditionLabel.Text = "EndTime:";
            // 
            // FileNameConditionLabel
            // 
            FileNameConditionLabel.Anchor = AnchorStyles.Left;
            FileNameConditionLabel.AutoSize = true;
            FileNameConditionLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            FileNameConditionLabel.Location = new Point(3, 87);
            FileNameConditionLabel.Name = "FileNameConditionLabel";
            FileNameConditionLabel.Size = new Size(106, 25);
            FileNameConditionLabel.TabIndex = 6;
            FileNameConditionLabel.Text = "FileName:";
            // 
            // MessageConditionLabel
            // 
            MessageConditionLabel.Anchor = AnchorStyles.Left;
            MessageConditionLabel.AutoSize = true;
            MessageConditionLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            MessageConditionLabel.Location = new Point(3, 127);
            MessageConditionLabel.Name = "MessageConditionLabel";
            MessageConditionLabel.Size = new Size(100, 25);
            MessageConditionLabel.TabIndex = 8;
            MessageConditionLabel.Text = "Message:";
            // 
            // DetailConditionLabel
            // 
            DetailConditionLabel.Anchor = AnchorStyles.Left;
            DetailConditionLabel.AutoSize = true;
            DetailConditionLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            DetailConditionLabel.Location = new Point(3, 167);
            DetailConditionLabel.Name = "DetailConditionLabel";
            DetailConditionLabel.Size = new Size(71, 25);
            DetailConditionLabel.TabIndex = 10;
            DetailConditionLabel.Text = "Detail:";
            // 
            // FolderPathConditionLabel
            // 
            FolderPathConditionLabel.Anchor = AnchorStyles.Left;
            FolderPathConditionLabel.AutoSize = true;
            FolderPathConditionLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            FolderPathConditionLabel.Location = new Point(3, 7);
            FolderPathConditionLabel.Name = "FolderPathConditionLabel";
            FolderPathConditionLabel.Size = new Size(119, 25);
            FolderPathConditionLabel.TabIndex = 0;
            FolderPathConditionLabel.Text = "FolderPath:";
            // 
            // QueryLogBtn
            // 
            QueryLogBtn.Anchor = AnchorStyles.None;
            QueryLogBtn.Location = new Point(3, 210);
            QueryLogBtn.Name = "QueryLogBtn";
            QueryLogBtn.Size = new Size(125, 40);
            QueryLogBtn.TabIndex = 12;
            QueryLogBtn.Text = "Query";
            QueryLogBtn.UseVisualStyleBackColor = true;
            QueryLogBtn.Click += QueryLogBtn_Click;
            // 
            // ChangeLogFolderBtn
            // 
            ChangeLogFolderBtn.Anchor = AnchorStyles.None;
            ChangeLogFolderBtn.Location = new Point(169, 210);
            ChangeLogFolderBtn.Name = "ChangeLogFolderBtn";
            ChangeLogFolderBtn.Size = new Size(160, 40);
            ChangeLogFolderBtn.TabIndex = 13;
            ChangeLogFolderBtn.Text = "ChangeLogFolder";
            ChangeLogFolderBtn.UseVisualStyleBackColor = true;
            ChangeLogFolderBtn.Click += ChangeLogFolderBtn_Click;
            // 
            // LogViewerTableLayoutPanel
            // 
            LogViewerTableLayoutPanel.ColumnCount = 4;
            LogViewerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            LogViewerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            LogViewerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            LogViewerTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            LogViewerTableLayoutPanel.Controls.Add(FolderPathConditionLabel, 0, 0);
            LogViewerTableLayoutPanel.Controls.Add(ErrorSourceFolderPathLabel, 1, 0);
            LogViewerTableLayoutPanel.Controls.Add(StartTimeConditionLable, 0, 1);
            LogViewerTableLayoutPanel.Controls.Add(StartTimePicker, 1, 1);
            LogViewerTableLayoutPanel.Controls.Add(EndTimeConditionLabel, 2, 1);
            LogViewerTableLayoutPanel.Controls.Add(EndTimePicker, 3, 1);
            LogViewerTableLayoutPanel.Controls.Add(FileNameConditionLabel, 0, 2);
            LogViewerTableLayoutPanel.Controls.Add(FileNameTextBox, 1, 2);
            LogViewerTableLayoutPanel.Controls.Add(MessageConditionLabel, 0, 3);
            LogViewerTableLayoutPanel.Controls.Add(MessageTextBox, 1, 3);
            LogViewerTableLayoutPanel.Controls.Add(DetailConditionLabel, 0, 4);
            LogViewerTableLayoutPanel.Controls.Add(DetailTextBox, 1, 4);
            LogViewerTableLayoutPanel.Controls.Add(QueryLogBtn, 0, 5);
            LogViewerTableLayoutPanel.Controls.Add(ChangeLogFolderBtn, 1, 5);
            LogViewerTableLayoutPanel.Controls.Add(LogInfoDataGridView, 0, 6);
            LogViewerTableLayoutPanel.Dock = DockStyle.Fill;
            LogViewerTableLayoutPanel.Location = new Point(0, 0);
            LogViewerTableLayoutPanel.Name = "LogViewerTableLayoutPanel";
            LogViewerTableLayoutPanel.RowCount = 7;
            LogViewerTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            LogViewerTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            LogViewerTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            LogViewerTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            LogViewerTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            LogViewerTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            LogViewerTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            LogViewerTableLayoutPanel.Size = new Size(1027, 771);
            LogViewerTableLayoutPanel.TabIndex = 0;
            // 
            // LogInfoDataGridView
            // 
            LogInfoDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            LogInfoDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LogViewerTableLayoutPanel.SetColumnSpan(LogInfoDataGridView, 4);
            LogInfoDataGridView.Dock = DockStyle.Fill;
            LogInfoDataGridView.Location = new Point(3, 263);
            LogInfoDataGridView.Name = "LogInfoDataGridView";
            LogInfoDataGridView.Size = new Size(1021, 505);
            LogInfoDataGridView.TabIndex = 14;
            // 
            // LogViewerControl
            // 
            Controls.Add(LogViewerTableLayoutPanel);
            Name = "LogViewerControl";
            Size = new Size(1027, 771);
            LogViewerTableLayoutPanel.ResumeLayout(false);
            LogViewerTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LogInfoDataGridView).EndInit();
            ResumeLayout(false);
        }

        protected virtual void QueryLogBtn_Click(object sender, EventArgs e)
        {
            ClickQueryLogBtn?.Invoke(_LogQueryConditionViewModel.LogQueryCondition);
        }

        protected virtual void ChangeLogFolderBtn_Click(object sender, EventArgs e)
        {

        }

        protected virtual void GenGridAction()
        {
            if (!LogInfoDataGridView.Columns.Contains("OpenErrorDetailCol"))
            {
                controlSrv.GenDataGridViewActionColumn<LogInfo>(LogInfoDataGridView
                , "OpenErrorDetailCol"
                , "操作", "細節"
                , 0
                , (logInfo) => { OpenLogDetail(logInfo); });
            }

            if (!LogInfoDataGridView.Columns.Contains("OpenElmahFolderCol"))
            {
                controlSrv.GenDataGridViewActionColumn<LogInfo>(LogInfoDataGridView
                , "OpenElmahFolderCol"
                , "操作", "檔案總管顯示"
                , 0
                , (logInfo) => { OpenLogSourceFolder(logInfo); });
            }
        }

        protected virtual void OpenLogDetail(LogInfo logInfo)
        {
            LogDetailForm.SetLogDetail(logInfo);
            LogDetailForm.ShowDialog();
        }

        protected virtual void OpenLogSourceFolder(LogInfo logInfo)
        {
            LogFile<LogInfo>? selectedErrorFile = LogFileList.FirstOrDefault(file => file.LogInfo.LogID == logInfo.LogID);

            if (selectedErrorFile != null)
            {
                if (string.IsNullOrEmpty(selectedErrorFile.SourceZIPPath))
                    Process.Start("explorer.exe", $"/select,\"{Path.Combine(selectedErrorFile.ParentFolderPath, selectedErrorFile.FileName)}\"");
                else
                    Process.Start("explorer.exe", $"/select,\"{Path.Combine(selectedErrorFile.SourceZIPPath, selectedErrorFile.FileName)}\"");
            }
        }
    }
}
