
namespace ErrorHelper.App.Core.Viewer.LogViewer
{
    public interface ILogViewerService : IViewerServiceBase
    {
        /// <summary>
        /// 產生開啟LogViewer功能的ToolStripMenuItem
        /// </summary>
        /// <returns></returns>
        ToolStripMenuItem GetOpenNewLogViewerTabPageMenuItem();

        /// <summary>
        /// 產生編輯畫面TabPage進TabControl
        /// </summary>
        /// <param name="tabControl">要將TabPage加入的TabControl</param>
        void NewLogQueryPage(TabControl tabControl);
    }
}
