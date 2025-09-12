
namespace ErrorHelper.App.Core.Viewer.LogViewer
{
    public interface ILogViewerService : IViewerServiceBase
    {
        ToolStripMenuItem GetOpenNewLogViewerTabPageMenuItem();
        void NewLogQueryPage(TabControl tabControl);
    }
}
