using ErrorHelper.App.Control.Viewer.LogViewer;
using ErrorHelper.App.Core.Viewer.LogViewer;
using ErrorHelper.App.Service.Viewer;
using ErrorHelper.Infrastructure.Common.Configuration;

namespace ErrorHelper.App.Service.Control.LogViewer
{
    public class LogViewerService : ViewerServiceBase, ILogViewerService
    {
        public void NewLogQueryPage(TabControl tabControl)
        {
            //tabControl.Controls.Add(new TabPage() { Text = "test" });
            var logTab = new LogViewerTabPage(AppSettings.LogSetting.DefaultLogFolderPath);
            tabControl.TabPages.Add(logTab);
            tabControl.SelectedTab = logTab;
        }
    }
}
