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
        public IList<ElmahFile> ElmahFileList { get; set; }
        /// <summary>
        /// Message只取第一行前100字
        /// </summary>
        public IList<LogInfo> ElmahInfoList => 
            ElmahFileList.Select(
                elmahFile => new LogInfo { 
                    Message = elmahFile.LogInfo.Message.Split('\n')[0].Length > 100 ? elmahFile.LogInfo.Message.Split('\n')[0].Substring(0, 100) : elmahFile.LogInfo.Message.Split('\n')[0],
                    Time = elmahFile.LogInfo.Time
                } ).ToList<LogInfo>() ?? [];

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

        protected override void OpenLogSourceFolder(LogInfo logInfo)
        {
            ElmahFile? selectedErrorFile = ElmahFileList.FirstOrDefault(file => file.LogInfo.GetLogID() == logInfo.GetLogID());

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
