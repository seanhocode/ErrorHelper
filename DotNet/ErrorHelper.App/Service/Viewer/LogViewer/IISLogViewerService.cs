using ErrorHelper.App.Control.LogViewer.IISLog;
using ErrorHelper.App.Core;
using ErrorHelper.App.Core.Viewer.LogViewer;
using ErrorHelper.Core.Model.LogHelper.IISLog;
using ErrorHelper.Infrastructure.Common.Configuration;
using ErrorHelper.Infrastructure.Service.LogHelper;

namespace ErrorHelper.App.Service.Viewer.LogViewer
{
    public class IISLogViewerService : ViewerServiceBase, IIISLogViewerService
    {
        private IIISLogHelperService<IISLogFile, IISLogInfo, IISLogQueryCondition> iisLogHelperSrv { get { return DIHelper.GetService<IIISLogHelperService<IISLogFile, IISLogInfo, IISLogQueryCondition>>(); } }

        public ToolStripMenuItem GetOpenNewLogViewerTabPageMenuItem()
        {
            return controlSrv.NewToolStripMenuItem("OpenNewIISLogViewerTabPageMenuItem", "新增IISLog查詢頁面");
        }

        public void NewLogQueryPage(TabControl tabControl)
        {
            IISLogViewerTabPage iisLogViewerTabPage = new IISLogViewerTabPage(AppSettings.LogSetting.DefaultLogFolderPath);
            iisLogViewerTabPage.IISLogViewerControl.ClickQueryLogBtn += (iisLogQueryCondition) => ClickQueryLogBtn(iisLogQueryCondition);
            tabControl.TabPages.Add(iisLogViewerTabPage);
            tabControl.SelectedTab = iisLogViewerTabPage;
        }

        private IList<IISLogFile> ClickQueryLogBtn(IISLogQueryCondition iisLogQueryCondition)
        {
            return iisLogHelperSrv.GetLogFileList(iisLogQueryCondition);
        }
    }
}
