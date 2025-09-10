namespace ErrorHelper.App.Core.Viewer
{
    public interface IMainFormViewerService : IViewerServiceBase
    {
        TableLayoutPanel GetMainLayout();
        void Test(Action<System.Windows.Forms.Control> e);
    }
}