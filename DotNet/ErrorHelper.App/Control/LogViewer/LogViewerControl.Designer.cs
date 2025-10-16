namespace ErrorHelper.App.Control.LogViewer
{
    partial class LogViewerControl
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
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
            SaveFolderPathBtn = new Button();
            LogViewerTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LogInfoDataGridView).BeginInit();
            SuspendLayout();
            // 
            // StartTimePicker
            // 
            StartTimePicker.Anchor = AnchorStyles.Left;
            StartTimePicker.Location = new Point(134, 53);
            StartTimePicker.Name = "StartTimePicker";
            StartTimePicker.Size = new Size(200, 23);
            StartTimePicker.TabIndex = 3;
            // 
            // EndTimePicker
            // 
            EndTimePicker.Anchor = AnchorStyles.Left;
            EndTimePicker.Location = new Point(494, 53);
            EndTimePicker.Name = "EndTimePicker";
            EndTimePicker.Size = new Size(200, 23);
            EndTimePicker.TabIndex = 5;
            // 
            // FileNameTextBox
            // 
            FileNameTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            LogViewerTableLayoutPanel.SetColumnSpan(FileNameTextBox, 3);
            FileNameTextBox.Location = new Point(134, 93);
            FileNameTextBox.Name = "FileNameTextBox";
            FileNameTextBox.Size = new Size(890, 23);
            FileNameTextBox.TabIndex = 7;
            // 
            // MessageTextBox
            // 
            MessageTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            LogViewerTableLayoutPanel.SetColumnSpan(MessageTextBox, 3);
            MessageTextBox.Location = new Point(134, 133);
            MessageTextBox.Name = "MessageTextBox";
            MessageTextBox.Size = new Size(890, 23);
            MessageTextBox.TabIndex = 9;
            // 
            // DetailTextBox
            // 
            DetailTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            LogViewerTableLayoutPanel.SetColumnSpan(DetailTextBox, 3);
            DetailTextBox.Location = new Point(134, 173);
            DetailTextBox.Name = "DetailTextBox";
            DetailTextBox.Size = new Size(890, 23);
            DetailTextBox.TabIndex = 11;
            // 
            // ErrorSourceFolderPathLabel
            // 
            ErrorSourceFolderPathLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            LogViewerTableLayoutPanel.SetColumnSpan(ErrorSourceFolderPathLabel, 3);
            ErrorSourceFolderPathLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            ErrorSourceFolderPathLabel.Location = new Point(134, 11);
            ErrorSourceFolderPathLabel.Name = "ErrorSourceFolderPathLabel";
            ErrorSourceFolderPathLabel.Size = new Size(890, 23);
            ErrorSourceFolderPathLabel.TabIndex = 1;
            // 
            // StartTimeConditionLable
            // 
            StartTimeConditionLable.Anchor = AnchorStyles.Left;
            StartTimeConditionLable.AutoSize = true;
            StartTimeConditionLable.Font = new Font("Microsoft JhengHei UI", 15F);
            StartTimeConditionLable.Location = new Point(3, 52);
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
            EndTimeConditionLabel.Location = new Point(363, 52);
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
            FileNameConditionLabel.Location = new Point(3, 92);
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
            MessageConditionLabel.Location = new Point(3, 132);
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
            DetailConditionLabel.Location = new Point(3, 172);
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
            FolderPathConditionLabel.Location = new Point(3, 10);
            FolderPathConditionLabel.Name = "FolderPathConditionLabel";
            FolderPathConditionLabel.Size = new Size(119, 25);
            FolderPathConditionLabel.TabIndex = 0;
            FolderPathConditionLabel.Text = "FolderPath:";
            // 
            // QueryLogBtn
            // 
            QueryLogBtn.Anchor = AnchorStyles.None;
            QueryLogBtn.Location = new Point(3, 215);
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
            ChangeLogFolderBtn.Location = new Point(165, 215);
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
            LogViewerTableLayoutPanel.Controls.Add(SaveFolderPathBtn, 2, 5);
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
            LogViewerTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
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
            LogInfoDataGridView.AllowUserToOrderColumns = true;
            LogInfoDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            LogInfoDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LogViewerTableLayoutPanel.SetColumnSpan(LogInfoDataGridView, 4);
            LogInfoDataGridView.Dock = DockStyle.Fill;
            LogInfoDataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            LogInfoDataGridView.Location = new Point(3, 268);
            LogInfoDataGridView.Name = "LogInfoDataGridView";
            LogInfoDataGridView.Size = new Size(1021, 500);
            LogInfoDataGridView.TabIndex = 14;
            // 
            // SaveFolderPathBtn
            // 
            SaveFolderPathBtn.Anchor = AnchorStyles.None;
            SaveFolderPathBtn.Location = new Point(363, 215);
            SaveFolderPathBtn.Name = "SaveFolderPathBtn";
            SaveFolderPathBtn.Size = new Size(125, 40);
            SaveFolderPathBtn.TabIndex = 15;
            SaveFolderPathBtn.Text = "SaveFolderPath";
            SaveFolderPathBtn.UseVisualStyleBackColor = true;
            SaveFolderPathBtn.Click += SaveFolderPathBtn_Click;
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

        #endregion
    }
}
