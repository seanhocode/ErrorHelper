using ErrorHelper.App.Control.Viewer.LogViewer;
using ErrorHelper.App.Core;
using ErrorHelper.App.Core.Viewer.LogViewer;
using ErrorHelper.Core.Model.Service.LogHelper.Elmah;
using ErrorHelper.Core.Service.LogHelper;
using ErrorHelper.Infrastructure.Common.Configuration;

namespace ErrorHelper.App.Service.Control.LogViewer
{
    public class ElmahViewerService : LogViewerService, IElmahViewerService
    {
        private IElmahHelperService<ElmahFile, ElmahInfo, ElmahQueryCondition> elmahHelperService { get { return DIHelper.GetService<IElmahHelperService<ElmahFile, ElmahInfo, ElmahQueryCondition>>(); } }

        public ToolStripMenuItem GetOpenElmahFolderMenuItem()
        {
            return controlService.NewToolStripMenuItem("OpenElmahFolderStripMenuItem", "新增Elmah查詢頁面");
        }

        /// <summary>
        /// 新增查詢Elmah頁面
        /// </summary>
        /// <param name="tabControl"></param>
        /// <returns></returns>
        public void NewElmahQueryPage(TabControl tabControl)
        {
            //tabControl.Controls.Add(new TabPage() { Text = "test" });
            var logTab = new LogViewerTabPage(AppSettings.LogSetting.DefaultLogFolderPath);
            tabControl.TabPages.Add(logTab);
            tabControl.SelectedTab = logTab;
        }
    }
}
