using ErrorHelper.App.ViewModel.Viewer.LogViewer;
using ErrorHelper.Core.Model.Service.LogHelper;

namespace ErrorHelper.App.Control.Viewer.LogViewer
{
    public class LogViewerTabPage : TabPage
    {
        public LogQueryConditionControl _LogQueryConditionControl;
        public LogQueryConditionViewModel _LogQueryConditionViewModel;

        public LogViewerTabPage(string folderPath)
        {
            Text = $"Log Viewer";

            // Model + ViewModel
            var model = new LogQueryCondition(folderPath);
            _LogQueryConditionViewModel = new LogQueryConditionViewModel(model);

            // View
            _LogQueryConditionControl = new LogQueryConditionControl(_LogQueryConditionViewModel)
            {
                Dock = DockStyle.Top
            };

            Controls.Add(_LogQueryConditionControl);
        }
    }
}
