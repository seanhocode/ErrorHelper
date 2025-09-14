using ErrorHelper.App.Core;
using ErrorHelper.App.Core.Viewer.LogViewer;
using ErrorHelper.App.Service.Viewer;
using ErrorHelper.App.View.Control.Viewer.LogViewer;
using ErrorHelper.Core.Model.Service.LogHelper.Elmah;
using ErrorHelper.Core.Service.LogHelper;
using ErrorHelper.Infrastructure.Common.Configuration;

namespace ErrorHelper.App.Service.Control.LogViewer
{
    public class ElmahViewerService : ViewerServiceBase, IElmahViewerService
    {
        private IElmahHelperService<ElmahFile, ElmahInfo, ElmahQueryCondition> elmahHelperService { get { return DIHelper.GetService<IElmahHelperService<ElmahFile, ElmahInfo, ElmahQueryCondition>>(); } }

        public ToolStripMenuItem GetOpenNewLogViewerTabPageMenuItem()
        {
            return controlService.NewToolStripMenuItem("OpenNewElmahViewerTabPageMenuItem", "新增Elmah查詢頁面");
        }

        /// <summary>
        /// 新增查詢Elmah頁面
        /// </summary>
        /// <param name="tabControl"></param>
        /// <returns></returns>
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
