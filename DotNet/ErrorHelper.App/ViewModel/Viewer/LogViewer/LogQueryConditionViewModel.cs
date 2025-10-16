using ErrorHelper.Core.Model.LogHelper;

namespace ErrorHelper.App.ViewModel.Viewer.LogViewer
{
    public class LogQueryConditionViewModel : ViewModelBase
    {
        protected readonly LogQueryCondition _LogQueryCondition;

        public LogQueryConditionViewModel(LogQueryCondition logQueryCondition)
        {
            _LogQueryCondition = logQueryCondition;
        }

        public virtual DateTime StartTime
        {
            get => _LogQueryCondition.StartTime;
            set { if (_LogQueryCondition.StartTime != value) { _LogQueryCondition.StartTime = value; OnPropertyChanged(nameof(StartTime)); } }
        }

        public virtual DateTime EndTime
        {
            get => _LogQueryCondition.EndTime;
            set { if (_LogQueryCondition.EndTime != value) { _LogQueryCondition.EndTime = value; OnPropertyChanged(nameof(EndTime)); } }
        }

        public virtual string FileName
        {
            get => _LogQueryCondition.FileName;
            set { if (_LogQueryCondition.FileName != value) { _LogQueryCondition.FileName = value?.Trim(); OnPropertyChanged(nameof(FileName)); } }
        }

        public virtual string Message
        {
            get => _LogQueryCondition.Message;
            set { if (_LogQueryCondition.Message != value) { _LogQueryCondition.Message = value; OnPropertyChanged(nameof(Message)); } }
        }

        public virtual string Detail
        {
            get => _LogQueryCondition.Detail;
            set { if (_LogQueryCondition.Detail != value) { _LogQueryCondition.Detail = value; OnPropertyChanged(nameof(Detail)); } }
        }

        public virtual string LogSourceFolderPath
        {
            get => _LogQueryCondition.LogSourceFolderPath;
            set { if (_LogQueryCondition.LogSourceFolderPath != value) { _LogQueryCondition.LogSourceFolderPath = value; OnPropertyChanged(nameof(LogSourceFolderPath)); } }
        }

        public virtual LogQueryCondition LogQueryCondition => _LogQueryCondition;
    }
}
