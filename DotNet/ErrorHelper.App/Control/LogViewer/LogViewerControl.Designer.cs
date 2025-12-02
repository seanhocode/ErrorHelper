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
            LogQueryCondition1TextBox = new TextBox();
            LogQueryCondition2TextBox = new TextBox();
            LogQueryCondition3TextBox = new TextBox();
            ErrorSourceFolderPathLabel = new Label();
            StartTimeConditionLable = new Label();
            EndTimeConditionLabel = new Label();
            LogQueryCondition1Label = new Label();
            LogQueryCondition2Label = new Label();
            LogQueryCondition3Label = new Label();
            FolderPathConditionLabel = new Label();
            QueryLogBtn = new Button();
            ChangeLogFolderBtn = new Button();
            LogViewerTableLayoutPanel = new TableLayoutPanel();
            SaveFolderPathBtn = new Button();
            LogInfoDataGridView = new DataGridView();
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
            // LogQueryCondition1TextBox
            // 
            LogQueryCondition1TextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            LogViewerTableLayoutPanel.SetColumnSpan(LogQueryCondition1TextBox, 3);
            LogQueryCondition1TextBox.Location = new Point(134, 93);
            LogQueryCondition1TextBox.Name = "LogQueryCondition1TextBox";
            LogQueryCondition1TextBox.Size = new Size(890, 23);
            LogQueryCondition1TextBox.TabIndex = 7;
            // 
            // LogQueryCondition2TextBox
            // 
            LogQueryCondition2TextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            LogViewerTableLayoutPanel.SetColumnSpan(LogQueryCondition2TextBox, 3);
            LogQueryCondition2TextBox.Location = new Point(134, 133);
            LogQueryCondition2TextBox.Name = "LogQueryCondition2TextBox";
            LogQueryCondition2TextBox.Size = new Size(890, 23);
            LogQueryCondition2TextBox.TabIndex = 9;
            // 
            // LogQueryCondition3TextBox
            // 
            LogQueryCondition3TextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            LogViewerTableLayoutPanel.SetColumnSpan(LogQueryCondition3TextBox, 3);
            LogQueryCondition3TextBox.Location = new Point(134, 173);
            LogQueryCondition3TextBox.Name = "LogQueryCondition3TextBox";
            LogQueryCondition3TextBox.Size = new Size(890, 23);
            LogQueryCondition3TextBox.TabIndex = 11;
            // 
            // ErrorSourceFolderPathLabel
            // 
            ErrorSourceFolderPathLabel.Anchor = AnchorStyles.Left;
            LogViewerTableLayoutPanel.SetColumnSpan(ErrorSourceFolderPathLabel, 3);
            ErrorSourceFolderPathLabel.Font = new Font("Microsoft JhengHei UI", 10F);
            ErrorSourceFolderPathLabel.Location = new Point(134, 10);
            ErrorSourceFolderPathLabel.Name = "ErrorSourceFolderPathLabel";
            ErrorSourceFolderPathLabel.Size = new Size(890, 24);
            ErrorSourceFolderPathLabel.TabIndex = 1;
            // 
            // StartTimeConditionLable
            // 
            StartTimeConditionLable.Anchor = AnchorStyles.None;
            StartTimeConditionLable.AutoSize = true;
            StartTimeConditionLable.Font = new Font("Microsoft JhengHei UI", 15F);
            StartTimeConditionLable.Location = new Point(12, 52);
            StartTimeConditionLable.Name = "StartTimeConditionLable";
            StartTimeConditionLable.Size = new Size(106, 25);
            StartTimeConditionLable.TabIndex = 2;
            StartTimeConditionLable.Text = "StartTime:";
            // 
            // EndTimeConditionLabel
            // 
            EndTimeConditionLabel.Anchor = AnchorStyles.None;
            EndTimeConditionLabel.AutoSize = true;
            EndTimeConditionLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            EndTimeConditionLabel.Location = new Point(376, 52);
            EndTimeConditionLabel.Name = "EndTimeConditionLabel";
            EndTimeConditionLabel.Size = new Size(99, 25);
            EndTimeConditionLabel.TabIndex = 4;
            EndTimeConditionLabel.Text = "EndTime:";
            // 
            // LogQueryCondition1Label
            // 
            LogQueryCondition1Label.Anchor = AnchorStyles.None;
            LogQueryCondition1Label.AutoSize = true;
            LogQueryCondition1Label.Font = new Font("Microsoft JhengHei UI", 15F);
            LogQueryCondition1Label.Location = new Point(34, 92);
            LogQueryCondition1Label.Name = "LogQueryCondition1Label";
            LogQueryCondition1Label.Size = new Size(63, 25);
            LogQueryCondition1Label.TabIndex = 6;
            LogQueryCondition1Label.Text = "LQC1";
            // 
            // LogQueryCondition2Label
            // 
            LogQueryCondition2Label.Anchor = AnchorStyles.None;
            LogQueryCondition2Label.AutoSize = true;
            LogQueryCondition2Label.Font = new Font("Microsoft JhengHei UI", 15F);
            LogQueryCondition2Label.Location = new Point(34, 132);
            LogQueryCondition2Label.Name = "LogQueryCondition2Label";
            LogQueryCondition2Label.Size = new Size(63, 25);
            LogQueryCondition2Label.TabIndex = 8;
            LogQueryCondition2Label.Text = "LQC2";
            // 
            // LogQueryCondition3Label
            // 
            LogQueryCondition3Label.Anchor = AnchorStyles.None;
            LogQueryCondition3Label.AutoSize = true;
            LogQueryCondition3Label.Font = new Font("Microsoft JhengHei UI", 15F);
            LogQueryCondition3Label.Location = new Point(34, 172);
            LogQueryCondition3Label.Name = "LogQueryCondition3Label";
            LogQueryCondition3Label.Size = new Size(63, 25);
            LogQueryCondition3Label.TabIndex = 10;
            LogQueryCondition3Label.Text = "LQC3";
            // 
            // FolderPathConditionLabel
            // 
            FolderPathConditionLabel.Anchor = AnchorStyles.None;
            FolderPathConditionLabel.AutoSize = true;
            FolderPathConditionLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            FolderPathConditionLabel.Location = new Point(6, 10);
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
            LogViewerTableLayoutPanel.Controls.Add(LogQueryCondition1Label, 0, 2);
            LogViewerTableLayoutPanel.Controls.Add(LogQueryCondition1TextBox, 1, 2);
            LogViewerTableLayoutPanel.Controls.Add(LogQueryCondition2Label, 0, 3);
            LogViewerTableLayoutPanel.Controls.Add(LogQueryCondition2TextBox, 1, 3);
            LogViewerTableLayoutPanel.Controls.Add(LogQueryCondition3Label, 0, 4);
            LogViewerTableLayoutPanel.Controls.Add(LogQueryCondition3TextBox, 1, 4);
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
            LogViewerTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            LogViewerTableLayoutPanel.Size = new Size(1027, 771);
            LogViewerTableLayoutPanel.TabIndex = 0;
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
            // LogInfoDataGridView
            // 
            LogInfoDataGridView.AllowUserToOrderColumns = true;
            LogInfoDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            LogInfoDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LogViewerTableLayoutPanel.SetColumnSpan(LogInfoDataGridView, 4);
            LogInfoDataGridView.Dock = DockStyle.Fill;
            LogInfoDataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            LogInfoDataGridView.Location = new Point(3, 268);
            LogInfoDataGridView.Name = "LogInfoDataGridView";
            LogInfoDataGridView.Size = new Size(1021, 500);
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

        #endregion
    }
}
