using ErrorHelper.App.ViewModel.Viewer.LogViewer;
using ErrorHelper.Core.Model.LogHelper.IISLog;

namespace ErrorHelper.App.Control.LogViewer.IISLog
{
    public class IISLogViewerTabPage : TabPage
    {
        public IISLogViewerControl IISLogViewerControl;
        public IISLogQueryConditionViewModel IISLogQueryConditionViewModel;

        public IISLogViewerTabPage(string folderPath)
        {
            Text = $"IISLogViewer";

            // Model + ViewModel
            var model = new IISLogQueryCondition(folderPath);
            IISLogQueryConditionViewModel = new IISLogQueryConditionViewModel(model);

            // View
            IISLogViewerControl = new IISLogViewerControl(IISLogQueryConditionViewModel)
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(IISLogViewerControl);
        }
    }
}
