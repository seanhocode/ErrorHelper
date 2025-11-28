using ErrorHelper.Core.Model.LogHelper.Elmah;
using ErrorHelper.Infrastructure.Common.Configuration;

namespace ErrorHelper.App.ViewModel.Viewer.LogViewer
{
    public class ElmahQueryConditionViewModel : LogQueryConditionViewModel<ElmahQueryCondition>
    {
        public ElmahQueryConditionViewModel(ElmahQueryCondition elmahQueryCondition) : base(elmahQueryCondition) 
        {
        }

        public override DateTime StartTime
        {
            get => _LogQueryCondition.StartTime;
            set { 
                if (_LogQueryCondition.StartTime != value) {
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
    }
}
