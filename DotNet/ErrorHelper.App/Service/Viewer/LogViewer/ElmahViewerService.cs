using ErrorHelper.App.Control.LogViewer.Elmah;
using ErrorHelper.App.Core;
using ErrorHelper.App.Core.Viewer.LogViewer;
using ErrorHelper.Core.Model.LogHelper.Elmah;
using ErrorHelper.Core.Service.LogHelper;
using ErrorHelper.Infrastructure.Common.Configuration;

namespace ErrorHelper.App.Service.Viewer.LogViewer
{
    public class ElmahViewerService : ViewerServiceBase, IElmahViewerService
    {
        private IElmahHelperService<ElmahFile, ElmahInfo, ElmahQueryCondition> elmahHelperService { get { return DIHelper.GetService<IElmahHelperService<ElmahFile, ElmahInfo, ElmahQueryCondition>>(); } }

        public ToolStripMenuItem GetOpenNewLogViewerTabPageMenuItem()
        {
            return controlSrv.NewToolStripMenuItem("OpenNewElmahViewerTabPageMenuItem", "新增Elmah查詢頁面");
        }

        public void NewLogQueryPage(TabControl tabControl)
        {
            ElmahViewerTabPage elmahViewerTabPage = new ElmahViewerTabPage(AppSettings.LogSetting.DefaultLogFolderPath);
            elmahViewerTabPage.ElmahViewerControl.ClickQueryLogBtn += (elmahQueryCondition) => ClickQueryLogBtn(elmahQueryCondition);
            tabControl.TabPages.Add(elmahViewerTabPage);
            tabControl.SelectedTab = elmahViewerTabPage;
        }

        private IList<ElmahFile> ClickQueryLogBtn(ElmahQueryCondition elmahQueryCondition)
        {
            return elmahHelperService.GetLogFileList(elmahQueryCondition);
        }
    }
}
