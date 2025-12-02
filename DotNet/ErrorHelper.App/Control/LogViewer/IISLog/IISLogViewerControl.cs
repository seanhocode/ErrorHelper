using ErrorHelper.App.Service.FormControl;
using ErrorHelper.App.View.Common;
using ErrorHelper.App.ViewModel.Viewer.LogViewer;
using ErrorHelper.Core.Model.Common.Configuration;
using ErrorHelper.Core.Model.LogHelper.IISLog;
using SeanTool.Tools;
using System.Diagnostics;
using System.Windows.Forms;

namespace ErrorHelper.App.Control.LogViewer
{
    public partial class IISLogViewerControl : LogViewerControl
    {
        # region VAR
        protected readonly IISLogQueryConditionViewModel _IISLogQueryConditionViewModel;

        protected IList<IISLogFile> IISLogFileList { get; set; }

        protected List<IISLogInfo> IISLogInfoList { get; set; }

        public new Func<IISLogQueryCondition, IList<IISLogFile>> ClickQueryLogBtn;
        # endregion

        # region 建構元及初始化
        public IISLogViewerControl(IISLogQueryConditionViewModel viewModel)
        {
            _IISLogQueryConditionViewModel = viewModel;
            SetQueryConditionViewModel();

            IISLogFileList = new List<IISLogFile>();

            ChangeLogFolder();
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
        # endregion

        # region DGV相關設定
        protected override void LogInfoDGV_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int modelIndex = VisibleIndexes[e.RowIndex];
            IISLogInfo log = IISLogInfoList[modelIndex];
            string col = LogInfoDataGridView.Columns[e.ColumnIndex].Name;

            switch (col)
            {
                case nameof(IISLogInfo.Time):
                    e.Value = log.Time;
                    break;

                case nameof(IISLogInfo.SCStatus):
                    e.Value = log.SCStatus;
                    break;

                case nameof(IISLogInfo.TimeTaken):
                    e.Value = log.TimeTaken;
                    break;

                case nameof(IISLogInfo.CSUriStem):
                    e.Value = log.CSUriStem;
                    break;
                case "OpenIISErrorDetailCol":
                    e.Value = "細節";
                    break;

                case "OpenIISLogFolderCol":
                    e.Value = "檔案總管顯示";
                    break;

                case "AddUriToIgnoreList":
                    e.Value = "忽略此類型";
                    break;
            }
        }

        protected override void LogInfoDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var colName = LogInfoDataGridView.Columns[e.ColumnIndex].Name;
            var log = IISLogInfoList[e.RowIndex];

            if (colName == "OpenIISErrorDetailCol")
                OpenLogDetail(log);

            else if (colName == "OpenIISLogFolderCol")
                OpenIISLogSourceFolder(log);

            else if (colName == "AddUriToIgnoreList")
                AddUriToIgnoreList(log);
        }

        protected override void DefineDGVColumn()
        {
            ColumnOrderAndHeader = new[]{
                ("OpenIISErrorDetailCol", "操作"),
                ("OpenIISLogFolderCol", "操作"),
                ("AddUriToIgnoreList", "操作"),
                (nameof(IISLogInfo.Time), "時間"),
                (nameof(IISLogInfo.SCStatus), "狀態碼"),
                (nameof(IISLogInfo.TimeTaken), "耗時(ms)"),
                (nameof(IISLogInfo.CSUriStem), "請求路徑")
            };
        }
        # endregion

        # region BtnClick
        protected override void QueryLogBtn_Click(object sender, EventArgs e)
        {
            _IISLogQueryConditionViewModel.LogQueryCondition.IgnoreUriList = new List<string>();
            QueryLog();
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
        # endregion

        # region Service
        protected virtual void AddUriToIgnoreList(IISLogInfo logInfo)
        {
            _IISLogQueryConditionViewModel.LogQueryCondition.IgnoreUriList.Add(logInfo.CSUriStem);
            QueryLog();
        }
        
        protected override void QueryLog()
        {
            LogInfoDataGridView.Rows.Clear();

            IISLogFileList = ClickQueryLogBtn?.Invoke((IISLogQueryCondition)_IISLogQueryConditionViewModel.LogQueryCondition);

            IISLogInfoList = IISLogFileList
                                    .SelectMany(iisLogFile => iisLogFile.LogList)
                                    .OrderByDescending(logInfo => logInfo.Time)
                                    .ToList() ?? [];

            VisibleIndexes = Enumerable.Range(0, IISLogInfoList.Count)
                            .Take(MaxVisibleRows)
                            .ToList();

            LogInfoDataGridView.Invalidate();
            LogInfoDataGridView.Refresh();

            if (IISLogInfoList.Count > 0)
                LogInfoDataGridView.RowCount = VisibleIndexes.Count;
        }

        protected virtual void OpenLogDetail(IISLogInfo logInfo)
        {
            IISLogInfoViewModel viewModel = new IISLogInfoViewModel(logInfo);
            viewModel.OpenViewWindow();
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
        # endregion
    }
}