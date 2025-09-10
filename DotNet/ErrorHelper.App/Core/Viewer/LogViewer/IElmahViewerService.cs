namespace ErrorHelper.App.Core.Viewer.LogViewer
{
    public interface IElmahViewerService : ILogViewerService
    {
        ToolStripMenuItem GetOpenElmahFolderMenuItem();
    }
}