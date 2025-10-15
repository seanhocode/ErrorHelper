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
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
    }
}