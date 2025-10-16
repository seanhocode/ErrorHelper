using ErrorHelper.App.ViewModel.Viewer.LogViewer;
using ErrorHelper.Core.Model.LogHelper.Elmah;

namespace ErrorHelper.App.Control.LogViewer.Elmah
{
    public class ElmahViewerTabPage : TabPage
    {
        public ElmahViewerControl ElmahViewerControl;
        public ElmahQueryConditionViewModel ElmahQueryConditionViewModel;

        public ElmahViewerTabPage(string folderPath)
        {
            Text = $"ElmahViewer";

            // Model + ViewModel
            var model = new ElmahQueryCondition(folderPath);
            ElmahQueryConditionViewModel = new ElmahQueryConditionViewModel(model);

            // View
            ElmahViewerControl = new ElmahViewerControl(ElmahQueryConditionViewModel)
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(ElmahViewerControl);
        }
    }
}
