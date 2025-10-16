using ErrorHelper.App.ViewModel.Viewer.LogViewer;
using ErrorHelper.Core.Model.LogHelper.IISLog;

namespace ErrorHelper.App.Control.LogViewer
{
    public partial class IISLogViewerControl : LogViewerControl
    {
        protected readonly IISLogQueryConditionViewModel _IISLogQueryConditionViewModel;

        protected IList<IISLogFile> IISLogFileList { get; set; }
        protected IList<IISLogInfo> IISLogInfoList => IISLogFileList.Select(iisLogFile => iisLogFile.LogInfo).ToList<IISLogInfo>() ?? [];

        public new Func<IISLogQueryCondition, IList<IISLogFile>> ClickQueryLogBtn;

        public IISLogViewerControl()
        {
            InitializeComponent();
        }

        protected override void SetViewModel()
        {
            StartTimePicker.DataBindings.Add("Value", _IISLogQueryConditionViewModel, nameof(_IISLogQueryConditionViewModel.StartTime));
            EndTimePicker.DataBindings.Add("Value", _IISLogQueryConditionViewModel, nameof(_IISLogQueryConditionViewModel.EndTime));
            FileNameTextBox.DataBindings.Add("Text", _IISLogQueryConditionViewModel, nameof(_IISLogQueryConditionViewModel.FileName));
            MessageTextBox.DataBindings.Add("Text", _IISLogQueryConditionViewModel, nameof(_IISLogQueryConditionViewModel.Message));
            DetailTextBox.DataBindings.Add("Text", _IISLogQueryConditionViewModel, nameof(_IISLogQueryConditionViewModel.Detail));
            ErrorSourceFolderPathLabel.DataBindings.Add("Text", _IISLogQueryConditionViewModel, nameof(_IISLogQueryConditionViewModel.LogSourceFolderPath));
        }

        public IISLogViewerControl(IISLogQueryConditionViewModel viewModel)
        {
            _IISLogQueryConditionViewModel = viewModel;
            SetViewModel();

            IISLogFileList = new List<IISLogFile>();

            LogInfoDataGridView.DataSource = IISLogInfoList;
            //資料Binding完後生成Grid按鈕
            LogInfoDataGridView.DataBindingComplete += (sender, e) => { GenGridAction(); };
            LogInfoDataGridView.Columns["LogID"].Visible = false;
            LogInfoDataGridView.Columns["Message"].Visible = false;

            ChangeLogFolder();
        }
    }
}
