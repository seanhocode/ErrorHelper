
using ErrorHelper.Core.Model.LogHelper.Elmah;
using ErrorHelper.Infrastructure.Common.Configuration;
using System.ComponentModel;

namespace ErrorHelper.App.ViewModel.Viewer.LogViewer
{
    public class ElmahQueryConditionViewModel : INotifyPropertyChanged
    {
        private readonly ElmahQueryCondition _ElmahQueryCondition;

        public ElmahQueryConditionViewModel(ElmahQueryCondition elmahQueryCondition)
        {
            _ElmahQueryCondition = elmahQueryCondition;
        }

        public DateTime StartTime
        {
            get => _ElmahQueryCondition.StartTime;
            set { 
                if (_ElmahQueryCondition.StartTime != value) { 
                    _ElmahQueryCondition.StartTime = value;
                    if (AppSettings.LogSetting.DefaultLogQueryDays >= 0)
                    {
                        //EndDateTime = StartDateTime + XXX Days
                        EndTime = StartTime.AddDays(AppSettings.LogSetting.DefaultLogQueryDays);
                    }
                    OnPropertyChanged(nameof(StartTime)); 
                } 
            }
        }

        public DateTime EndTime
        {
            get => _ElmahQueryCondition.EndTime;
            set { if (_ElmahQueryCondition.EndTime != value) { _ElmahQueryCondition.EndTime = value; OnPropertyChanged(nameof(EndTime)); } }
        }

        public string FileName
        {
            get => _ElmahQueryCondition.FileName;
            set { if (_ElmahQueryCondition.FileName != value) { _ElmahQueryCondition.FileName = value?.Trim(); OnPropertyChanged(nameof(FileName)); } }
        }

        public string Message
        {
            get => _ElmahQueryCondition.Message;
            set { if (_ElmahQueryCondition.Message != value) { _ElmahQueryCondition.Message = value; OnPropertyChanged(nameof(Message)); } }
        }

        public string Detail
        {
            get => _ElmahQueryCondition.Detail;
            set { if (_ElmahQueryCondition.Detail != value) { _ElmahQueryCondition.Detail = value; OnPropertyChanged(nameof(Detail)); } }
        }

        public string LogSourceFolderPath
        {
            get => _ElmahQueryCondition.LogSourceFolderPath;
            set { if (_ElmahQueryCondition.LogSourceFolderPath != value) { _ElmahQueryCondition.LogSourceFolderPath = value; OnPropertyChanged(nameof(LogSourceFolderPath)); } }
        }

        public ElmahQueryCondition ElmahQueryCondition => _ElmahQueryCondition;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
