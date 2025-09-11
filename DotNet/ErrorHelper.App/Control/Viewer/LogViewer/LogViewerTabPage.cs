using ErrorHelper.App.ViewModel.Viewer.LogViewer;
using ErrorHelper.Core.Model.Service.LogHelper;

namespace ErrorHelper.App.Control.Viewer.LogViewer
{
    public class LogViewerTabPage : TabPage
    {
        private readonly LogQueryConditionControl _LogQueryConditionControl;
        private readonly LogQueryConditionViewModel _LogQueryConditionViewModel;

        public LogViewerTabPage(string folderPath)
        {
            Text = $"Log Viewer";

            // Model + ViewModel
            var model = new LogQueryCondition(folderPath);
            _LogQueryConditionViewModel = new LogQueryConditionViewModel(model);

            // View
            _LogQueryConditionControl = new LogQueryConditionControl(_LogQueryConditionViewModel)
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(_LogQueryConditionControl);
        }

        // 如果要從外面存取 ViewModel 或 Model，提供屬性
        public LogQueryConditionViewModel ViewModel => _LogQueryConditionViewModel;
    }
}
