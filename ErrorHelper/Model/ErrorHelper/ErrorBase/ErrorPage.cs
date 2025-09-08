using ErrorHelper.Tools;
using System.ComponentModel;
using System.Diagnostics;

namespace ErrorHelper.Model.ErrorHelper.ErrorBase
{
    public class ErrorPage
    {
        private FormControlTool controlTool = new FormControlTool();

        public TabPage ErrorTabPage { get; set; }

        public ErrorQueryCondition ErrorQueryCondition { get; set; }

        public IList<IErrorFile> ErrorList { get; set; }

        private TableLayoutPanel ErrorTabPageLayout { get; set; }

        public DataGridView ErrorDataGridView { get; set; }

        private Form ErrorDetailForm { get; set; }

        private Panel ErrorActionPanel { get; set; }

        public ErrorPage()
        {
            InitialData();
            GetErrorTabPage();
        }

        public void GetErrorTabPage()
        {
            ErrorTabPage = controlTool.NewTabPage("QueryErrorTabPage", "查詢Error");

            GenErrorTabPageLayout();

            ErrorTabPage.Controls.Add(ErrorTabPageLayout);
        }

        public virtual IList<Button> GetCustomizeAction(ref int currLeft)
        {
            return new List<Button>();
        }

        private void InitialData()
        {
            ErrorQueryCondition = new ErrorQueryCondition();
            GenErrorDetailForm();
            GenErrorDataGridView();
        }

        private void GenErrorTabPageLayout()
        {
            ErrorTabPageLayout = controlTool.NewTableLayoutPanel("QueryErrorTabPageLayout", 3, 1);

            ErrorTabPageLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25));     //Row 0: 查詢區
            ErrorTabPageLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     //Row 1: 動作區
            ErrorTabPageLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); //Row 2: Grid區

            GenErrorActionPanel();

            ErrorTabPageLayout.Controls.Add(ErrorQueryCondition.QueryConditionLayout, 0, 0);    //資訊區
            ErrorTabPageLayout.Controls.Add(ErrorActionPanel, 0, 1);    //動作區
            ErrorTabPageLayout.Controls.Add(ErrorDataGridView, 0, 2);    //Grid區
        }

        private void GenErrorDataGridView()
        {
            ErrorDataGridView = controlTool.NewDataGridView("ErrorDataGridView");

            ErrorDataGridView.DataSource = new BindingList<IErrorInfo>();

            //資料Binding完後生成Grid按鈕
            ErrorDataGridView.DataBindingComplete += (sender, e) => { GenGridAction(sender); };
        }

        private void GenGridAction(object sender)
        {
            DataGridView dataGridView = (DataGridView)sender;

            if (!dataGridView.Columns.Contains("OpenErrorDetailCol"))
            {
                controlTool.GenDataGridViewActionColumn<IErrorInfo>(dataGridView
                , "OpenErrorDetailCol"
                , "操作", "細節"
                , 0
                , (error) => { OpenErrorDetail(error); });
            }

            if (!dataGridView.Columns.Contains("OpenElmahFolderCol"))
            {
                controlTool.GenDataGridViewActionColumn<IErrorInfo>(dataGridView
                , "OpenElmahFolderCol"
                , "操作", "檔案總管顯示"
                , 0
                , (error) => { OpenElmahFolder(error); });
            }
        }

        private void OpenErrorDetail(IErrorInfo errorInfo)
        {
            ErrorDetailForm.Controls["ErrorDetailTextBox"].Text = errorInfo.GetDetail();

            ErrorDetailForm.ShowDialog(); // 模態顯示
        }

        private void GenErrorDetailForm()
        {
            ErrorDetailForm = new Form
            {
                Dock = DockStyle.Fill,
                Text = "Detail",
                AutoScroll = true,
                Size = new Size(1200, 600)
            };

            ErrorDetailForm.FormClosing += (s, e) =>
            {
                e.Cancel = true;    // 不關閉
                ((Form)s).Hide();   // 改為隱藏
            };

            GenErrorDetailFormArea();
        }

        private void GenErrorDetailFormArea()
        {
            TextBox textBox = new TextBox
            {
                Name = "ErrorDetailTextBox",
                Multiline = true,
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Both,
                AutoSize = true
            };

            ErrorDetailForm.Controls.Add(textBox);
        }

        private void OpenElmahFolder(IErrorInfo errorInfo)
        {
            IErrorFile? selectedErrorFile = ErrorList.FirstOrDefault(file => file.ErrorInfo.ErrorID == errorInfo.ErrorID);

            if (selectedErrorFile != null)
            {
                if (string.IsNullOrEmpty(selectedErrorFile.SourceZIPPath))
                    Process.Start("explorer.exe", $"/select,\"{Path.Combine(selectedErrorFile.ParentFolderPath, selectedErrorFile.FileName)}\"");
                else 
                    Process.Start("explorer.exe", $"/select,\"{Path.Combine(selectedErrorFile.SourceZIPPath, selectedErrorFile.FileName)}\"");
            }
        }

        private void GenErrorActionPanel()
        {
            int currLeft = 0;

            ErrorActionPanel = new Panel
            {
                Name = "ElmahActionPanel",
                Dock = DockStyle.Fill,
                Height = 40
            };

            foreach(Button customizeBtn in GetCustomizeAction(ref currLeft))
                ErrorActionPanel.Controls.Add(customizeBtn);

            Button changeElmahFolderBtn = new Button
            {
                Text = "更改Log資料夾",
                Location = new Point(currLeft + 10, 10),
                Width = 150
            };
            changeElmahFolderBtn.Click += (sender, e) => { ErrorQueryCondition.ChangeErrorFolder(); };
            currLeft += changeElmahFolderBtn.Width + 10;

            ErrorActionPanel.Controls.Add(changeElmahFolderBtn);
        }
    }
}
