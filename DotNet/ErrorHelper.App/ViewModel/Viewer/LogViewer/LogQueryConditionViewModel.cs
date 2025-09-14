
using ErrorHelper.Core.Model.Service.LogHelper;
using System.ComponentModel;

namespace ErrorHelper.App.ViewModel.Viewer.LogViewer
{
    public class LogQueryConditionViewModel : INotifyPropertyChanged
    {
        private readonly LogQueryCondition _LogQueryCondition;

        public LogQueryConditionViewModel(LogQueryCondition logQueryCondition)
        {
            _LogQueryCondition = logQueryCondition;
        }

        public DateTime StartTime
        {
            get => _LogQueryCondition.StartTime;
            set { if (_LogQueryCondition.StartTime != value) { _LogQueryCondition.StartTime = value; OnPropertyChanged(nameof(StartTime)); } }
        }

        public DateTime EndTime
        {
            get => _LogQueryCondition.EndTime;
            set { if (_LogQueryCondition.EndTime != value) { _LogQueryCondition.EndTime = value; OnPropertyChanged(nameof(EndTime)); } }
        }

        public string FileName
        {
            get => _LogQueryCondition.FileName;
            set { if (_LogQueryCondition.FileName != value) { _LogQueryCondition.FileName = value?.Trim(); OnPropertyChanged(nameof(FileName)); } }
        }

        public string Message
        {
            get => _LogQueryCondition.Message;
            set { if (_LogQueryCondition.Message != value) { _LogQueryCondition.Message = value; OnPropertyChanged(nameof(Message)); } }
        }

        public string Detail
        {
            get => _LogQueryCondition.Detail;
            set { if (_LogQueryCondition.Detail != value) { _LogQueryCondition.Detail = value; OnPropertyChanged(nameof(Detail)); } }
        }

        public string LogSourceFolderPath
        {
            get => _LogQueryCondition.LogSourceFolderPath;
            set { if (_LogQueryCondition.LogSourceFolderPath != value) { _LogQueryCondition.LogSourceFolderPath = value; OnPropertyChanged(nameof(LogSourceFolderPath)); } }
        }

        public LogQueryCondition LogQueryCondition => _LogQueryCondition;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
