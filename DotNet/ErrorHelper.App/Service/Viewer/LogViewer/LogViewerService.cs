using ErrorHelper.App.Control.Viewer.LogViewer;
using ErrorHelper.App.Core.Viewer.LogViewer;
using ErrorHelper.App.Service.Viewer;
using ErrorHelper.Core.Model.Service.LogHelper;
using ErrorHelper.Infrastructure.Common.Configuration;

namespace ErrorHelper.App.Service.Control.LogViewer
{
    public class LogViewerService : ViewerServiceBase, ILogViewerService
    {
        public ToolStripMenuItem GetOpenNewLogViewerTabPageMenuItem()
        {
            return controlService.NewToolStripMenuItem("OpenNewLogViewerTabPageMenuItem", "新增Log查詢頁面");
        }

        public void NewLogQueryPage(TabControl tabControl)
        {
            var logTab = new LogViewerTabPage(AppSettings.LogSetting.DefaultLogFolderPath);
            logTab.LogViewerControl.ClickQueryLogBtn += QueryLogInfoList; 
            tabControl.TabPages.Add(logTab);
            tabControl.SelectedTab = logTab;
        }

        public IList<LogFile<LogInfo>> QueryLogInfoList(LogQueryCondition logQueryCondition)
        {
            return new List<LogFile<LogInfo>>();
        }
    }
}
