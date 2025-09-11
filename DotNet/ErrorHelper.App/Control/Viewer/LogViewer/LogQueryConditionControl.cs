using ErrorHelper.App.ViewModel.Viewer.LogViewer;

namespace ErrorHelper.App.Control.Viewer.LogViewer
{
    public partial class LogQueryConditionControl : UserControl
    {
        private readonly LogQueryConditionViewModel _LogQueryConditionViewModel;

        private DateTimePicker StartTimePicker;
        private DateTimePicker EndTimePicker;
        private TextBox FileNameTextBox;
        private TextBox MessageTextBox;
        private TextBox DetailTextBox;
        private Label StartTimeConditionLable;
        private Label EndTimeConditionLabel;
        private Label FileNameConditionLabel;
        private Label MessageConditionLabel;
        private Label DetailConditionLabel;
        private Label FolderPathConditionLabel;
        private Label ErrorSourceFolderPathLabel;

        public LogQueryConditionControl(LogQueryConditionViewModel viewModel)
        {
            InitializeComponent();
            _LogQueryConditionViewModel = viewModel;

            // 綁定 UI 和 ViewModel
            StartTimePicker.DataBindings.Add("Value", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.StartTime));
            EndTimePicker.DataBindings.Add("Value", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.EndTime));
            FileNameTextBox.DataBindings.Add("Text", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.FileName));
            MessageTextBox.DataBindings.Add("Text", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.Message));
            DetailTextBox.DataBindings.Add("Text", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.Detail));
            ErrorSourceFolderPathLabel.DataBindings.Add("Text", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.LogSourceFolderPath));
        }

        private void InitializeComponent()
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
            SuspendLayout();
            // 
            // StartTimePicker
            // 
            StartTimePicker.Location = new Point(128, 42);
            StartTimePicker.Name = "StartTimePicker";
            StartTimePicker.Size = new Size(190, 23);
            StartTimePicker.TabIndex = 0;
            // 
            // EndTimePicker
            // 
            EndTimePicker.Location = new Point(440, 42);
            EndTimePicker.Name = "EndTimePicker";
            EndTimePicker.Size = new Size(190, 23);
            EndTimePicker.TabIndex = 1;
            // 
            // FileNameTextBox
            // 
            FileNameTextBox.Location = new Point(128, 86);
            FileNameTextBox.Name = "FileNameTextBox";
            FileNameTextBox.Size = new Size(100, 23);
            FileNameTextBox.TabIndex = 2;
            // 
            // MessageTextBox
            // 
            MessageTextBox.Location = new Point(340, 86);
            MessageTextBox.Name = "MessageTextBox";
            MessageTextBox.Size = new Size(100, 23);
            MessageTextBox.TabIndex = 3;
            // 
            // DetailTextBox
            // 
            DetailTextBox.Location = new Point(530, 86);
            DetailTextBox.Name = "DetailTextBox";
            DetailTextBox.Size = new Size(100, 23);
            DetailTextBox.TabIndex = 4;
            // 
            // ErrorSourceFolderPathLabel
            // 
            ErrorSourceFolderPathLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            ErrorSourceFolderPathLabel.Location = new Point(128, 2);
            ErrorSourceFolderPathLabel.Name = "ErrorSourceFolderPathLabel";
            ErrorSourceFolderPathLabel.Size = new Size(502, 23);
            ErrorSourceFolderPathLabel.TabIndex = 5;
            // 
            // StartTimeConditionLable
            // 
            StartTimeConditionLable.AutoSize = true;
            StartTimeConditionLable.Font = new Font("Microsoft JhengHei UI", 15F);
            StartTimeConditionLable.Location = new Point(16, 42);
            StartTimeConditionLable.Name = "StartTimeConditionLable";
            StartTimeConditionLable.Size = new Size(106, 25);
            StartTimeConditionLable.TabIndex = 6;
            StartTimeConditionLable.Text = "StartTime:";
            // 
            // EndTimeConditionLabel
            // 
            EndTimeConditionLabel.AutoSize = true;
            EndTimeConditionLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            EndTimeConditionLabel.Location = new Point(335, 42);
            EndTimeConditionLabel.Name = "EndTimeConditionLabel";
            EndTimeConditionLabel.Size = new Size(99, 25);
            EndTimeConditionLabel.TabIndex = 7;
            EndTimeConditionLabel.Text = "EndTime:";
            // 
            // FileNameConditionLabel
            // 
            FileNameConditionLabel.AutoSize = true;
            FileNameConditionLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            FileNameConditionLabel.Location = new Point(16, 84);
            FileNameConditionLabel.Name = "FileNameConditionLabel";
            FileNameConditionLabel.Size = new Size(106, 25);
            FileNameConditionLabel.TabIndex = 8;
            FileNameConditionLabel.Text = "FileName:";
            // 
            // MessageConditionLabel
            // 
            MessageConditionLabel.AutoSize = true;
            MessageConditionLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            MessageConditionLabel.Location = new Point(234, 84);
            MessageConditionLabel.Name = "MessageConditionLabel";
            MessageConditionLabel.Size = new Size(100, 25);
            MessageConditionLabel.TabIndex = 9;
            MessageConditionLabel.Text = "Message:";
            // 
            // DetailConditionLabel
            // 
            DetailConditionLabel.AutoSize = true;
            DetailConditionLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            DetailConditionLabel.Location = new Point(453, 84);
            DetailConditionLabel.Name = "DetailConditionLabel";
            DetailConditionLabel.Size = new Size(71, 25);
            DetailConditionLabel.TabIndex = 10;
            DetailConditionLabel.Text = "Detail:";
            // 
            // FolderPathConditionLabel
            // 
            FolderPathConditionLabel.AutoSize = true;
            FolderPathConditionLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            FolderPathConditionLabel.Location = new Point(3, 0);
            FolderPathConditionLabel.Name = "FolderPathConditionLabel";
            FolderPathConditionLabel.Size = new Size(119, 25);
            FolderPathConditionLabel.TabIndex = 11;
            FolderPathConditionLabel.Text = "FolderPath:";
            // 
            // LogViewerControl
            // 
            Controls.Add(FolderPathConditionLabel);
            Controls.Add(DetailConditionLabel);
            Controls.Add(MessageConditionLabel);
            Controls.Add(FileNameConditionLabel);
            Controls.Add(EndTimeConditionLabel);
            Controls.Add(StartTimeConditionLable);
            Controls.Add(StartTimePicker);
            Controls.Add(EndTimePicker);
            Controls.Add(FileNameTextBox);
            Controls.Add(MessageTextBox);
            Controls.Add(DetailTextBox);
            Controls.Add(ErrorSourceFolderPathLabel);
            Name = "LogViewerControl";
            Size = new Size(640, 118);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
