using ErrorHelper.App.Service.FormControl;
using ErrorHelper.App.View.Common;
using ErrorHelper.App.ViewModel.Viewer.LogViewer;
using ErrorHelper.Core.Model.Common.Configuration;
using ErrorHelper.Core.Model.LogHelper;
using ErrorHelper.Core.Model.LogHelper.Elmah;
using ErrorHelper.Infrastructure.Common.Configuration;
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
            SetViewModel();

            ElmahFileList = new List<ElmahFile>();

            LogInfoDataGridView.DataSource = ElmahInfoList;
            
            

            ChangeLogFolder();
        }

        protected override void CustomizeDGVColumn()
        {
            base.CustomizeDGVColumn();

            LogInfoDataGridView.Columns["LogID"].Visible = false;
            LogInfoDataGridView.Columns["Message"].Visible = false;
            LogInfoDataGridView.Columns["Time"].DefaultCellStyle.Format = AppSettings.SystemSetting.TimeFormatStr;
        }

        protected override void SetViewModel()
        {
            StartTimePicker.DataBindings.Add("Value", _ElmahQueryConditionViewModel, nameof(_ElmahQueryConditionViewModel.StartTime));
            EndTimePicker.DataBindings.Add("Value", _ElmahQueryConditionViewModel, nameof(_ElmahQueryConditionViewModel.EndTime));
            FileNameTextBox.DataBindings.Add("Text", _ElmahQueryConditionViewModel, nameof(_ElmahQueryConditionViewModel.FileName));
            MessageTextBox.DataBindings.Add("Text", _ElmahQueryConditionViewModel, nameof(_ElmahQueryConditionViewModel.Message));
            DetailTextBox.DataBindings.Add("Text", _ElmahQueryConditionViewModel, nameof(_ElmahQueryConditionViewModel.Detail));
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
                string configFilePath = Path.Combine(FileTool.ThisExeDir, "Config", "LogFolderList.json");
                SelectItem item = new SelectItem()
                {
                    Key = folderPathAliasForm.InputText,
                    Value = _ElmahQueryConditionViewModel.LogSourceFolderPath
                };

                JsonTool.SaveSinglePropertyToListJson<SelectItem>(configFilePath, "LogFolderList", item.Key, item);

                MessageBox.Show("Save successfully.");
            }
        }

        protected override void QueryLog()
        {
            ElmahFileList = ClickQueryLogBtn?.Invoke((ElmahQueryCondition)_ElmahQueryConditionViewModel.LogQueryCondition);
            LogInfoDataGridView.DataSource = ElmahInfoList;
        }

        protected override void ChangeLogFolderBtn_Click(object sender, EventArgs e)
        {
            ChangeLogFolder();

            QueryLog();
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
            string configFilePath = Path.Combine(FileTool.ThisExeDir, "Config", "LogFolderList.json");

            foreach (string key in JsonTool.GetJsonSubPropertyList(configFilePath, "LogFolderList"))
                logFolderItems.Add(key, key);

            selectConfigForm.Items = logFolderItems;

            if (selectConfigForm.ShowDialog() == DialogResult.OK)
            {
                SelectItem selectedItem = JsonTool.GetSinglePropertyByListJson<SelectItem>(configFilePath, "LogFolderList", (selectConfigForm.SelectedValue ?? string.Empty));
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