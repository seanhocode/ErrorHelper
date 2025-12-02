using ErrorHelper.App.Service.FormControl;
using ErrorHelper.App.View.Common;
using ErrorHelper.App.ViewModel.Viewer.LogViewer;
using ErrorHelper.Core.Model.Common.Configuration;
using ErrorHelper.Core.Model.LogHelper;
using ErrorHelper.Core.Model.LogHelper.Elmah;
using SeanTool.Tools;
using System.Diagnostics;

namespace ErrorHelper.App.Control.LogViewer.Elmah
{
    public partial class ElmahViewerControl : LogViewerControl
    {
        # region VAR
        protected readonly ElmahQueryConditionViewModel _ElmahQueryConditionViewModel;
        protected IList<ElmahFile> ElmahFileList { get; set; }
        protected IList<LogInfo> ElmahInfoList { get; set; }

        public new Func<ElmahQueryCondition, IList<ElmahFile>> ClickQueryLogBtn;
        # endregion

        # region 建構元及初始化
        public ElmahViewerControl(ElmahQueryConditionViewModel viewModel)
        {
            _ElmahQueryConditionViewModel = viewModel;
            SetQueryConditionViewModel();

            ElmahFileList = new List<ElmahFile>();

            ChangeLogFolder();
        }

        protected override void InitializeOtherControl()
        {
            base.InitializeOtherControl();
            LogQueryCondition1Label.Text = "檔案名稱:";
            LogQueryCondition2Label.Text = "錯誤說明:";
            LogQueryCondition3Label.Text = "錯誤資訊:";
        }

        protected override void SetQueryConditionViewModel()
        {
            StartTimePicker.DataBindings.Add("Value", _ElmahQueryConditionViewModel, nameof(_ElmahQueryConditionViewModel.StartTime));
            EndTimePicker.DataBindings.Add("Value", _ElmahQueryConditionViewModel, nameof(_ElmahQueryConditionViewModel.EndTime));
            LogQueryCondition1TextBox.DataBindings.Add("Text", _ElmahQueryConditionViewModel, nameof(_ElmahQueryConditionViewModel.FileName));
            LogQueryCondition2TextBox.DataBindings.Add("Text", _ElmahQueryConditionViewModel, nameof(_ElmahQueryConditionViewModel.Message));
            LogQueryCondition3TextBox.DataBindings.Add("Text", _ElmahQueryConditionViewModel, nameof(_ElmahQueryConditionViewModel.Detail));
            ErrorSourceFolderPathLabel.DataBindings.Add("Text", _ElmahQueryConditionViewModel, nameof(_ElmahQueryConditionViewModel.LogSourceFolderPath));
        }
        # endregion

        # region DGV相關設定
        protected override void LogInfoDGV_CellValueNeeded(object? sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex < 0) return;

            LogInfo log = ElmahInfoList[e.RowIndex];
            string col = LogInfoDataGridView.Columns[e.ColumnIndex].Name;

            switch (col)
            {
                case nameof(LogInfo.Time):
                    e.Value = log.Time;
                    break;
                case nameof(LogInfo.Title):
                    e.Value = log.Title;
                    break;
                case "OpenErrorDetailBtnCol":
                    e.Value = "細節";
                    break;
                case "OpenElmahFolderBtnCol":
                    e.Value = "檔案總管顯示";
                    break;
                case "AddTitleToIgnoreListBtnCol":
                    e.Value = "忽略此類型";
                    break;
            }
        }

        protected override void LogInfoDGV_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var colName = LogInfoDataGridView.Columns[e.ColumnIndex].Name;
            var log = ElmahInfoList[e.RowIndex];

            if (colName == "OpenErrorDetailBtnCol")
                OpenLogDetail(log);

            else if (colName == "OpenElmahFolderBtnCol")
                OpenLogSourceFolder(log);

            else if (colName == "AddTitleToIgnoreListBtnCol")
                AddTitleToIgnoreList(log);
        }

        protected override void DefineDGVColumn()
        {
            ColumnOrderAndHeader = new[]{
                ("OpenErrorDetailBtnCol", "操作", 50, DataGridViewContentAlignment.MiddleCenter),
                ("OpenElmahFolderBtnCol", "操作", 100, DataGridViewContentAlignment.MiddleCenter),
                ("AddTitleToIgnoreListBtnCol", "操作", 100, DataGridViewContentAlignment.MiddleCenter),
                (nameof(LogInfo.Time), "時間", 175, DataGridViewContentAlignment.MiddleCenter),
                (nameof(LogInfo.Title), "錯誤說明", 1000, DataGridViewContentAlignment.MiddleLeft)
            };
        }
        # endregion

        # region BtnClick
        protected override void QueryLogBtn_Click(object sender, EventArgs e)
        {
            _ElmahQueryConditionViewModel.LogQueryCondition.IgnoreMessageList = new List<string>();
            _ = QueryLog();
        }

        protected override void SaveFolderPathBtn_Click(object sender, EventArgs e)
        {
            TextForm folderPathAliasForm = new TextForm("Input", "Please enter folder path alias.");

            if (folderPathAliasForm.ShowDialog() == DialogResult.OK)
            {
                string configFilePath = Path.Combine(FileTool.ThisExeDir, "Config", "ElmahFolderList.json");
                SelectItem item = new SelectItem()
                {
                    Key = folderPathAliasForm.InputText,
                    Value = _ElmahQueryConditionViewModel.LogSourceFolderPath
                };

                JsonTool.SaveSinglePropertyToListJson<SelectItem>(configFilePath, "ElmahFolderList", item.Key, item);

                MessageBox.Show("Save successfully.");
            }
        }
        # endregion

        # region Service
        protected override async Task QueryLog()
        {
            QueryLogBtn.Text = "Loading...";
            QueryLogBtn.Enabled = false;
            LogInfoDataGridView.RowCount = 0;

            ElmahInfoList = await Task.Run(() =>
            {
                ElmahFileList = ClickQueryLogBtn?.Invoke((ElmahQueryCondition)_ElmahQueryConditionViewModel.LogQueryCondition) ?? new List<ElmahFile>();
                return ElmahFileList.Select(elmahFile => elmahFile.LogInfo ?? new LogInfo()).ToList() ?? [];
            });

            if (ElmahInfoList != null && ElmahInfoList.Count > 0)
                LogInfoDataGridView.RowCount = ElmahInfoList.Count;
            else
                MessageBox.Show("No log found.");

            LogInfoDataGridView.Invalidate();
            QueryLogBtn.Text = "Query";
            QueryLogBtn.Enabled = true;
        }

        protected override void OpenLogSourceFolder(LogInfo logInfo)
        {
            ElmahFile? selectedErrorFile = ElmahFileList.FirstOrDefault(file => file.LogInfo?.LogID == logInfo.LogID);

            if (selectedErrorFile != null)
            {
                if (string.IsNullOrEmpty(selectedErrorFile.SourceZIPPath))
                    Process.Start("explorer.exe", $"/select,\"{Path.Combine(selectedErrorFile.ParentFolderPath ?? string.Empty, selectedErrorFile.FileName ?? string.Empty)}\"");
                else
                    Process.Start("explorer.exe", $"/select,\"{Path.Combine(selectedErrorFile.SourceZIPPath, selectedErrorFile.FileName ?? string.Empty)}\"");
            }
        }

        protected override void ChangeLogFolder()
        {
            SelectForm selectConfigForm = new SelectForm("SelectFolder");
            Dictionary<string, string> logFolderItems = new Dictionary<string, string>();
            string configFilePath = Path.Combine(FileTool.ThisExeDir, "Config", "ElmahFolderList.json");

            foreach (string key in JsonTool.GetJsonSubPropertyList(configFilePath, "ElmahFolderList"))
                logFolderItems.Add(key, key);

            selectConfigForm.Items = logFolderItems;

            if (selectConfigForm.ShowDialog() == DialogResult.OK)
            {
                SelectItem selectedItem = JsonTool.GetSinglePropertyByListJson<SelectItem>(configFilePath, "ElmahFolderList", (selectConfigForm.SelectedValue ?? string.Empty));
                if (selectedItem.Value == "Select")
                    _ElmahQueryConditionViewModel.LogSourceFolderPath = FormControlService.GetSelectFolderPath(_ElmahQueryConditionViewModel.LogSourceFolderPath);
                else
                    _ElmahQueryConditionViewModel.LogSourceFolderPath = selectedItem.Value;
            }
            else
            {
                _ElmahQueryConditionViewModel.LogSourceFolderPath = FormControlService.GetSelectFolderPath(_ElmahQueryConditionViewModel.LogSourceFolderPath);
            }
        }

        protected override void AddTitleToIgnoreList(LogInfo logInfo)
        {
            _ElmahQueryConditionViewModel.LogQueryCondition.IgnoreMessageList.Add(logInfo.Title ?? string.Empty);
            _ = QueryLog();
        }
        # endregion
    }
}