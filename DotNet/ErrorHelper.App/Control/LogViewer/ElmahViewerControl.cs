using ErrorHelper.App.Service.FormControl;
using ErrorHelper.App.ViewModel.Viewer.LogViewer;
using ErrorHelper.Core.Model.LogHelper;
using ErrorHelper.Core.Model.LogHelper.Elmah;
using System.Diagnostics;

namespace ErrorHelper.App.Control.LogViewer
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
            //資料Binding完後生成Grid按鈕
            LogInfoDataGridView.DataBindingComplete += (sender, e) => { GenGridAction(); };
            LogInfoDataGridView.Columns["LogID"].Visible = false;
            LogInfoDataGridView.Columns["Message"].Visible = false;
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
            _ElmahQueryConditionViewModel.ElmahQueryCondition.IgnoreMessageList = new List<string>();
            QueryLog();
        }

        protected override void QueryLog()
        {
            ElmahFileList = ClickQueryLogBtn?.Invoke(_ElmahQueryConditionViewModel.ElmahQueryCondition);
            LogInfoDataGridView.DataSource = ElmahInfoList;
        }

        protected override void ChangeLogFolderBtn_Click(object sender, EventArgs e)
        {
            _ElmahQueryConditionViewModel.LogSourceFolderPath = FormControlService.GetSelectFolderPath(_ElmahQueryConditionViewModel.LogSourceFolderPath);
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

        protected override void AddTitleToIgnoreList(LogInfo logInfo)
        {
            _ElmahQueryConditionViewModel.ElmahQueryCondition.IgnoreMessageList.Add(logInfo.Title);
            QueryLog();
        }
    }
}
