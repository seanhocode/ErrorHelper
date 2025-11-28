using ErrorHelper.Core.Model.LogHelper.IISLog;
using ErrorHelper.Infrastructure.Common.Configuration;

namespace ErrorHelper.App.ViewModel.Viewer.LogViewer
{
    public class IISLogQueryConditionViewModel : LogQueryConditionViewModel<IISLogQueryCondition>
    {
        public virtual string SCStatus
        {
            get => _LogQueryCondition.SCStatus;
            set { if (_LogQueryCondition.SCStatus != value) { _LogQueryCondition.SCStatus = value; OnPropertyChanged(nameof(SCStatus)); } }
        }

        public virtual string CSUriStem
        {
            get => _LogQueryCondition.CSUriStem;
            set { if (_LogQueryCondition.CSUriStem != value) { _LogQueryCondition.CSUriStem = value; OnPropertyChanged(nameof(CSUriStem)); } }
        }

        public virtual string TimeTaken
        {
            get => _LogQueryCondition.TimeTaken.ToString();
            set { 
                if (int.TryParse(value, out int intTimeToken))
                    _LogQueryCondition.TimeTaken = intTimeToken; 
                else
                    _LogQueryCondition.TimeTaken = 0;

                OnPropertyChanged(nameof(TimeTaken));
            }
        }

        public IISLogQueryConditionViewModel(IISLogQueryCondition iisLogQueryCondition) : base(iisLogQueryCondition)
        { 
        }

        public override DateTime StartTime
        {
            get => _LogQueryCondition.StartTime;
            set
            {
                if (_LogQueryCondition.StartTime != value)
                {
                    _LogQueryCondition.StartTime = value;
                    if (AppSettings.LogSetting.DefaultLogQueryDays >= 0)
                    {
                        //EndDateTime = StartDateTime + XXX Days
                        EndTime = StartTime.AddDays(AppSettings.LogSetting.DefaultLogQueryDays);
                    }
                    OnPropertyChanged(nameof(StartTime));
                }
            }
        }

        public virtual IISLogQueryCondition IISLogQueryCondition => _LogQueryCondition;
    }
}
