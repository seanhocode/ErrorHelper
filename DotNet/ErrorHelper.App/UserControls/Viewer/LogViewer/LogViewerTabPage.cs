using ErrorHelper.App.ViewModel.Viewer.LogViewer;
using ErrorHelper.Core.Model.Service.LogHelper;

namespace ErrorHelper.App.UserControls.Viewer.LogViewer
{
    public class LogViewerTabPage : TabPage
    {
        public LogViewerControl LogViewerControl;
        public LogQueryConditionViewModel LogQueryConditionViewModel;

        public LogViewerTabPage(string folderPath)
        {
            Text = $"LogViewer";

            // Model + ViewModel
            var model = new LogQueryCondition(folderPath);
            LogQueryConditionViewModel = new LogQueryConditionViewModel(model);

            // View
            LogViewerControl = new LogViewerControl(LogQueryConditionViewModel)
            {
                Dock = DockStyle.Top
            };

            Controls.Add(LogViewerControl);
        }
    }
}
