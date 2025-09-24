using ErrorHelper.App.ViewModel.Viewer.LogViewer;
using ErrorHelper.Core.Model.Service.LogHelper.Elmah;

namespace ErrorHelper.App.UserControls.Viewer.LogViewer
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
                Dock = DockStyle.Top
            };

            this.Controls.Add(ElmahViewerControl);
        }
    }
}
