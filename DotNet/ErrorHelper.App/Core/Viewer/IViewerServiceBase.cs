namespace ErrorHelper.App.Core.Viewer
{
    public interface IViewerServiceBase
    {
        event Action<Control>? OnAddControlRequested;
    }
}