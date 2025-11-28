using ErrorHelper.App.Service.FormControl;
using ErrorHelper.App.View.Common;
using ErrorHelper.App.ViewModel.Viewer.LogViewer;
using ErrorHelper.Core.Model.Common.Configuration;
using ErrorHelper.Core.Model.LogHelper;
using ErrorHelper.Core.Model.LogHelper.IISLog;
using ErrorHelper.Tool;
using System.Diagnostics;

namespace ErrorHelper.App.Control.LogViewer
{
    public partial class IISLogViewerControl : LogViewerControl
    {
        protected readonly IISLogQueryConditionViewModel _IISLogQueryConditionViewModel;

        protected IList<IISLogFile> IISLogFileList { get; set; }
        protected List<IISLogInfo> IISLogInfoList => IISLogFileList
                                                        .SelectMany(iisLogFile => iisLogFile.LogList)
                                                        .OrderByDescending(logInfo => logInfo.Time)
                                                        .ToList() ?? [];

        public new Func<IISLogQueryCondition, IList<IISLogFile>> ClickQueryLogBtn;

        public IISLogViewerControl(IISLogQueryConditionViewModel viewModel)
        {
            _IISLogQueryConditionViewModel = viewModel;
            SetQueryConditionViewModel();

            IISLogFileList = new List<IISLogFile>();

            LogInfoDataGridView.DataSource = IISLogInfoList;

            ChangeLogFolder();
        }

        protected override void DefineDGVColumn()
        {
            ColumnOrderAndHeader = new[]{
                ("OpenIISErrorDetailCol", "操作"),
                ("OpenIISLogFolderCol", "操作"),
                ("AddUriToIgnoreList", "操作"),
                (nameof(IISLogInfo.Time), "時間"),
                (nameof(IISLogInfo.SCStatus), "狀態碼"),
                (nameof(IISLogInfo.TimeTaken), "耗時 (ms)"),
                (nameof(IISLogInfo.ClientIP), "客戶端 IP"),
                (nameof(IISLogInfo.ClientPort), "客戶端 Port"),
                (nameof(IISLogInfo.ServerIP), "伺服器 IP"),
                (nameof(IISLogInfo.ServerPort), "伺服器 Port"),
                (nameof(IISLogInfo.CSUriStem), "請求路徑")
            };
        }

        protected override void GenGridAction()
        {
            if (!LogInfoDataGridView.Columns.Contains("OpenIISErrorDetailCol"))
            {
                controlSrv.GenDataGridViewActionColumn<IISLogInfo>(LogInfoDataGridView
                , "OpenIISErrorDetailCol"
                , "操作", "細節"
                , 0
                , (logInfo) => { OpenLogDetail(logInfo); });
            }

            if (!LogInfoDataGridView.Columns.Contains("OpenIISLogFolderCol"))
            {
                controlSrv.GenDataGridViewActionColumn<IISLogInfo>(LogInfoDataGridView
                , "OpenIISLogFolderCol"
                , "操作", "檔案總管顯示"
                , 0
                , (logInfo) => { OpenIISLogSourceFolder(logInfo); });
            }

            if (!LogInfoDataGridView.Columns.Contains("AddUriToIgnoreList"))
            {
                controlSrv.GenDataGridViewActionColumn<IISLogInfo>(LogInfoDataGridView
                , "AddUriToIgnoreList"
                , "操作", "忽略此類型"
                , 0
                , (logInfo) => { AddUriToIgnoreList(logInfo); });
            }
        }

        protected virtual void AddUriToIgnoreList(IISLogInfo logInfo)
        {
            _IISLogQueryConditionViewModel.LogQueryCondition.IgnoreUriList.Add(logInfo.CSUriStem);
            QueryLog();
        }

        protected override void InitializeOtherControl()
        {
            base.InitializeOtherControl();
            LogQueryCondition1Label.Text = "狀態碼:";
            LogQueryCondition2Label.Text = "請求路徑:";
            LogQueryCondition3Label.Text = "耗時大於:";
        }

        protected override void SetQueryConditionViewModel()
        {
            StartTimePicker.DataBindings.Add("Value", _IISLogQueryConditionViewModel, nameof(_IISLogQueryConditionViewModel.StartTime));
            EndTimePicker.DataBindings.Add("Value", _IISLogQueryConditionViewModel, nameof(_IISLogQueryConditionViewModel.EndTime));
            LogQueryCondition1TextBox.DataBindings.Add("Text", _IISLogQueryConditionViewModel, nameof(_IISLogQueryConditionViewModel.SCStatus));
            LogQueryCondition2TextBox.DataBindings.Add("Text", _IISLogQueryConditionViewModel, nameof(_IISLogQueryConditionViewModel.CSUriStem));
            LogQueryCondition3TextBox.DataBindings.Add("Text", _IISLogQueryConditionViewModel, nameof(_IISLogQueryConditionViewModel.TimeTaken));
            ErrorSourceFolderPathLabel.DataBindings.Add("Text", _IISLogQueryConditionViewModel, nameof(_IISLogQueryConditionViewModel.LogSourceFolderPath));
        }

        protected override void QueryLogBtn_Click(object sender, EventArgs e)
        {
            _IISLogQueryConditionViewModel.LogQueryCondition.IgnoreUriList = new List<string>();
            QueryLog();
        }

        protected override void QueryLog()
        {
            IISLogFileList = ClickQueryLogBtn?.Invoke((IISLogQueryCondition)_IISLogQueryConditionViewModel.LogQueryCondition);
            LogInfoDataGridView.DataSource = IISLogInfoList;
        }

        protected override void SaveFolderPathBtn_Click(object sender, EventArgs e)
        {
            TextForm folderPathAliasForm = new TextForm("Input", "Please enter folder path alias.");

            if (folderPathAliasForm.ShowDialog() == DialogResult.OK)
            {
                string configFilePath = Path.Combine(FileTool.ThisExeDir, "Config", "IISLogFolderList.json");
                SelectItem item = new SelectItem()
                {
                    Key = folderPathAliasForm.InputText,
                    Value = _IISLogQueryConditionViewModel.LogSourceFolderPath
                };

                JsonTool.SaveSinglePropertyToListJson<SelectItem>(configFilePath, "IISLogFolderList", item.Key, item);

                MessageBox.Show("Save successfully.");
            }
        }

        protected virtual void OpenLogDetail(IISLogInfo logInfo)
        {
            //ToDo: Implement IIS log detail view
        }

        protected virtual void OpenIISLogSourceFolder(IISLogInfo logInfo)
        {
            IISLogFile? selectedErrorFile = IISLogFileList.FirstOrDefault(file => file.FileName == logInfo.LogID);

            if (selectedErrorFile != null)
            {
                if (string.IsNullOrEmpty(selectedErrorFile.SourceZIPPath))
                    Process.Start("explorer.exe", $"/select,\"{Path.Combine(selectedErrorFile.ParentFolderPath, selectedErrorFile.FileName)}\"");
                else
                    Process.Start("explorer.exe", $"/select,\"{Path.Combine(selectedErrorFile.SourceZIPPath, selectedErrorFile.FileName)}\"");
            }
        }

        protected override void ChangeLogFolder()
        {
            SelectForm selectConfigForm = new SelectForm("SelectFolder");
            Dictionary<string, string> logFolderItems = new Dictionary<string, string>();
            string configFilePath = Path.Combine(FileTool.ThisExeDir, "Config", "IISLogFolderList.json");

            foreach (string key in JsonTool.GetJsonSubPropertyList(configFilePath, "IISLogFolderList"))
                logFolderItems.Add(key, key);

            selectConfigForm.Items = logFolderItems;

            if (selectConfigForm.ShowDialog() == DialogResult.OK)
            {
                SelectItem selectedItem = JsonTool.GetSinglePropertyByListJson<SelectItem>(configFilePath, "IISLogFolderList", (selectConfigForm.SelectedValue ?? string.Empty));
                if (selectedItem.Value == "Select")
                    _IISLogQueryConditionViewModel.LogSourceFolderPath = FormControlService.GetSelectFolderPath(_IISLogQueryConditionViewModel.LogSourceFolderPath);
                else
                    _IISLogQueryConditionViewModel.LogSourceFolderPath = selectedItem.Value;
            }
            else
            {
                _IISLogQueryConditionViewModel.LogSourceFolderPath = FormControlService.GetSelectFolderPath(_IISLogQueryConditionViewModel.LogSourceFolderPath);
            }
        }

        protected override void AddTitleToIgnoreList(LogInfo logInfo)
        {
            _IISLogQueryConditionViewModel.LogQueryCondition.IgnoreMessageList.Add(logInfo.Title);
            QueryLog();
        }
    }
}
