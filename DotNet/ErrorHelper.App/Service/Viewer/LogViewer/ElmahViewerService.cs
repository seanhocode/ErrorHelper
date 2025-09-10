using ErrorHelper.App.Core;
using ErrorHelper.App.Core.Viewer.LogViewer;
using ErrorHelper.Core.Model.Service.LogHelper.Elmah;
using ErrorHelper.Core.Service.LogHelper;

namespace ErrorHelper.App.Service.Control.LogViewer
{
    public class ElmahViewerService : LogViewerService, IElmahViewerService
    {
        private IElmahHelperService<ElmahFile, ElmahInfo, ElmahQueryCondition> elmahHelperService { get { return DIHelper.GetService<IElmahHelperService<ElmahFile, ElmahInfo, ElmahQueryCondition>>(); } }

        public ToolStripMenuItem GetOpenElmahFolderMenuItem()
        {
            return controlService.NewToolStripMenuItem("OpenElmahFolderStripMenuItem", "新增Elmah查詢頁面", NewElmahQueryPage);
        }

        /// <summary>
        /// 新增查詢Elmah頁面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewElmahQueryPage(object? sender, EventArgs e)
        {
            RequestAddControl(new Button() { Text = "Test", Location = new Point(10, 10) });
        }
    }
}
