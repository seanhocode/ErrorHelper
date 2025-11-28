using ErrorHelper.App.Service.FormControl;
using ErrorHelper.App.View.Common;
using ErrorHelper.App.ViewModel.Viewer.LogViewer;
using ErrorHelper.Core.Model.Common.Configuration;
using ErrorHelper.Core.Model.LogHelper;
using ErrorHelper.Core.Model.LogHelper.Elmah;
using ErrorHelper.Core.Model.LogHelper.IISLog;
using ErrorHelper.Tool;
using System.Diagnostics;

namespace ErrorHelper.App.Control.LogViewer.Elmah
{
    public partial class ElmahViewerControl : LogViewerControl
    {
        protected readonly ElmahQueryConditionViewModel _ElmahQueryConditionViewModel;
        protected IList<ElmahFile> ElmahFileList { get; set; }
        protected IList<LogInfo> ElmahInfoList => ElmahFileList.Select(elmahFile => elmahFile.LogInfo).ToList<LogInfo>() ?? [];

        public new Func<ElmahQueryCondition, IList<ElmahFile>> ClickQueryLogBtn;

        public ElmahViewerControl(ElmahQueryConditionViewModel viewModel)
        {
            _ElmahQueryConditionViewModel = viewModel;
            SetQueryConditionViewModel();

            ElmahFileList = new List<ElmahFile>();

            LogInfoDataGridView.DataSource = ElmahInfoList;

            ChangeLogFolder();
        }

        protected override void DefineDGVColumn()
        {
            ColumnOrderAndHeader = new[]{
                ("OpenErrorDetailCol", "操作"),
                ("OpenElmahFolderCol", "操作"),
                ("AddTitleToIgnoreList", "操作"),
                (nameof(LogInfo.Time), "時間"),
                (nameof(IISLogInfo.Title), "錯誤說明")
            };
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

        protected override void QueryLogBtn_Click(object sender, EventArgs e)
        {
            _ElmahQueryConditionViewModel.LogQueryCondition.IgnoreMessageList = new List<string>();
            QueryLog();
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

        protected override void QueryLog()
        {
            ElmahFileList = ClickQueryLogBtn?.Invoke((ElmahQueryCondition)_ElmahQueryConditionViewModel.LogQueryCondition);
            LogInfoDataGridView.DataSource = ElmahInfoList;
        }

        protected override void OpenLogSourceFolder(LogInfo logInfo)
        {
            ElmahFile? selectedErrorFile = ElmahFileList.FirstOrDefault(file => file.LogInfo.LogID == logInfo.LogID);

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
            _ElmahQueryConditionViewModel.LogQueryCondition.IgnoreMessageList.Add(logInfo.Title);
            QueryLog();
        }
    }
}