namespace ErrorHelper.App.View.LogViewer
{
    partial class LogDetailForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            LogDetailTextBox = new TextBox();
            LogMessageTextBox = new TextBox();
            LogIDTextBox = new TextBox();
            LogTimeTextBox = new TextBox();
            LogIDLabel = new Label();
            LogMessageLabel = new Label();
            LogTimeLabel = new Label();
            LogDetailLabel = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90F));
            tableLayoutPanel1.Controls.Add(LogDetailLabel, 0, 3);
            tableLayoutPanel1.Controls.Add(LogTimeLabel, 0, 2);
            tableLayoutPanel1.Controls.Add(LogDetailTextBox, 1, 3);
            tableLayoutPanel1.Controls.Add(LogMessageTextBox, 1, 1);
            tableLayoutPanel1.Controls.Add(LogIDTextBox, 1, 0);
            tableLayoutPanel1.Controls.Add(LogTimeTextBox, 1, 2);
            tableLayoutPanel1.Controls.Add(LogIDLabel, 0, 0);
            tableLayoutPanel1.Controls.Add(LogMessageLabel, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            tableLayoutPanel1.Size = new Size(1920, 1061);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // LogDetailTextBox
            // 
            LogDetailTextBox.Dock = DockStyle.Fill;
            LogDetailTextBox.Location = new Point(195, 162);
            LogDetailTextBox.Multiline = true;
            LogDetailTextBox.Name = "LogDetailTextBox";
            LogDetailTextBox.ScrollBars = ScrollBars.Both;
            LogDetailTextBox.Size = new Size(1722, 896);
            LogDetailTextBox.TabIndex = 0;
            // 
            // LogMessageTextBox
            // 
            LogMessageTextBox.Dock = DockStyle.Fill;
            LogMessageTextBox.Location = new Point(195, 56);
            LogMessageTextBox.Multiline = true;
            LogMessageTextBox.Name = "LogMessageTextBox";
            LogMessageTextBox.ScrollBars = ScrollBars.Both;
            LogMessageTextBox.Size = new Size(1722, 47);
            LogMessageTextBox.TabIndex = 3;
            // 
            // LogIDTextBox
            // 
            LogIDTextBox.Dock = DockStyle.Fill;
            LogIDTextBox.Location = new Point(195, 3);
            LogIDTextBox.Multiline = true;
            LogIDTextBox.Name = "LogIDTextBox";
            LogIDTextBox.ScrollBars = ScrollBars.Both;
            LogIDTextBox.Size = new Size(1722, 47);
            LogIDTextBox.TabIndex = 2;
            // 
            // LogTimeTextBox
            // 
            LogTimeTextBox.Dock = DockStyle.Fill;
            LogTimeTextBox.Location = new Point(195, 109);
            LogTimeTextBox.Multiline = true;
            LogTimeTextBox.Name = "LogTimeTextBox";
            LogTimeTextBox.Size = new Size(1722, 47);
            LogTimeTextBox.TabIndex = 4;
            // 
            // LogIDLabel
            // 
            LogIDLabel.AutoSize = true;
            LogIDLabel.Dock = DockStyle.Fill;
            LogIDLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            LogIDLabel.Location = new Point(3, 0);
            LogIDLabel.Name = "LogIDLabel";
            LogIDLabel.Size = new Size(186, 53);
            LogIDLabel.TabIndex = 5;
            LogIDLabel.Text = "LogID";
            LogIDLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LogMessageLabel
            // 
            LogMessageLabel.AutoSize = true;
            LogMessageLabel.Dock = DockStyle.Fill;
            LogMessageLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            LogMessageLabel.Location = new Point(3, 53);
            LogMessageLabel.Name = "LogMessageLabel";
            LogMessageLabel.Size = new Size(186, 53);
            LogMessageLabel.TabIndex = 6;
            LogMessageLabel.Text = "Message";
            LogMessageLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LogTimeLabel
            // 
            LogTimeLabel.AutoSize = true;
            LogTimeLabel.Dock = DockStyle.Fill;
            LogTimeLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            LogTimeLabel.Location = new Point(3, 106);
            LogTimeLabel.Name = "LogTimeLabel";
            LogTimeLabel.Size = new Size(186, 53);
            LogTimeLabel.TabIndex = 7;
            LogTimeLabel.Text = "LogTime";
            LogTimeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LogDetailLabel
            // 
            LogDetailLabel.AutoSize = true;
            LogDetailLabel.Dock = DockStyle.Fill;
            LogDetailLabel.Font = new Font("Microsoft JhengHei UI", 15F);
            LogDetailLabel.Location = new Point(3, 159);
            LogDetailLabel.Name = "LogDetailLabel";
            LogDetailLabel.Size = new Size(186, 902);
            LogDetailLabel.TabIndex = 8;
            LogDetailLabel.Text = "Detail";
            LogDetailLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LogDetailForm
            // 
            AutoSize = true;
            ClientSize = new Size(1920, 1061);
            Controls.Add(tableLayoutPanel1);
            Name = "LogDetailForm";
            Text = "LogDetail";
            WindowState = FormWindowState.Maximized;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TextBox LogDetailTextBox;
        private TextBox LogMessageTextBox;
        private TextBox LogIDTextBox;
        private TextBox LogTimeTextBox;
        private Label LogDetailLabel;
        private Label LogTimeLabel;
        private Label LogIDLabel;
        private Label LogMessageLabel;
    }
}