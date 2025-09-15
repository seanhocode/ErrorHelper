using ErrorHelper.App.Service.FormControl;
using ErrorHelper.App.ViewModel.Viewer.LogViewer;
using ErrorHelper.Core.Model.Service.LogHelper;
using ErrorHelper.Core.Model.Service.LogHelper.Elmah;
using ErrorHelper.Infrastructure.Common.Configuration;
using System.Diagnostics;

namespace ErrorHelper.App.Control.Viewer.LogViewer
{
    public partial class ElmahViewerControl : LogViewerControl
    {
        protected readonly ElmahQueryConditionViewModel _ElmahQueryConditionViewModel;
        public IList<ElmahFile> ElmahFileList { get; set; }
        public IList<LogInfo> ElmahInfoList => ElmahFileList.Select(elmahFile => elmahFile.LogInfo).ToList<LogInfo>() ?? [];

        public new Func<ElmahQueryCondition, IList<ElmahFile>> ClickQueryLogBtn;

        public ElmahViewerControl(ElmahQueryConditionViewModel viewModel)
        {
            _ElmahQueryConditionViewModel = viewModel;
            SetViewModel();

            ElmahFileList = new List<ElmahFile>();

            LogInfoDataGridView.DataSource = ElmahInfoList;
            //資料Binding完後生成Grid按鈕
            LogInfoDataGridView.DataBindingComplete += (sender, e) => { GenGridAction(); };
        }

        protected override void SetViewModel()
        {
            // 綁定 UI 和 ViewModel
            StartTimePicker.DataBindings.Add("Value", _ElmahQueryConditionViewModel, nameof(_ElmahQueryConditionViewModel.StartTime));
            EndTimePicker.DataBindings.Add("Value", _ElmahQueryConditionViewModel, nameof(_ElmahQueryConditionViewModel.EndTime));
            FileNameTextBox.DataBindings.Add("Text", _ElmahQueryConditionViewModel, nameof(_ElmahQueryConditionViewModel.FileName));
            MessageTextBox.DataBindings.Add("Text", _ElmahQueryConditionViewModel, nameof(_ElmahQueryConditionViewModel.Message));
            DetailTextBox.DataBindings.Add("Text", _ElmahQueryConditionViewModel, nameof(_ElmahQueryConditionViewModel.Detail));
            ErrorSourceFolderPathLabel.DataBindings.Add("Text", _ElmahQueryConditionViewModel, nameof(_ElmahQueryConditionViewModel.LogSourceFolderPath));
        }

        protected override void QueryLogBtn_Click(object sender, EventArgs e)
        {
            ElmahFileList = ClickQueryLogBtn?.Invoke(_ElmahQueryConditionViewModel.ElmahQueryCondition) ?? new List<ElmahFile>();
            LogInfoDataGridView.DataSource = ElmahInfoList;
        }

        protected override void ChangeLogFolderBtn_Click(object sender, EventArgs e)
        {
            _ElmahQueryConditionViewModel.LogSourceFolderPath = FormControlService.GetSelectFolderPath(_ElmahQueryConditionViewModel.LogSourceFolderPath);
            QueryLogBtn_Click(null, null);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // MessageTextBox
            // 
            LogViewerTableLayoutPanel.SetColumnSpan(MessageTextBox, 3);
            // 
            // DetailTextBox
            // 
            LogViewerTableLayoutPanel.SetColumnSpan(DetailTextBox, 3);
            // 
            // ElmahViewerControl
            // 
            Name = "ElmahViewerControl";
            ResumeLayout(false);

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
    }
}
