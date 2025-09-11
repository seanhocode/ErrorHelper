using ErrorHelper.App.Core;
using ErrorHelper.App.Core.FormControl;
using ErrorHelper.App.Core.Viewer;

namespace ErrorHelper.App.Service.Viewer
{
    public class ViewerServiceBase : IViewerServiceBase
    {
        protected IFormControlService controlService { get { return DIHelper.GetService<IFormControlService>(); } }
    }
}
