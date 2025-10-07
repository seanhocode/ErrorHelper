namespace ErrorHelper.App.Core.Viewer
{
    public interface IErrorHelperViewerService : IViewerServiceBase
    {
        /// <summary>
        /// 生成主要畫面
        /// </summary>
        /// <returns></returns>
        TableLayoutPanel GetMainLayout();
    }
}