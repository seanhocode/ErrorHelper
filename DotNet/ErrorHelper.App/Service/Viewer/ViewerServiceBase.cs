using ErrorHelper.App.Core;
using ErrorHelper.App.Core.FormControl;
using ErrorHelper.App.Core.Viewer;

namespace ErrorHelper.App.Service.Viewer
{
    public class ViewerServiceBase : IViewerServiceBase
    {
        public event Action<System.Windows.Forms.Control>? OnAddControlRequested;

        protected IFormControlService controlService { get { return DIHelper.GetService<IFormControlService>(); } }

        protected void RequestAddControl(System.Windows.Forms.Control control)
        {
            OnAddControlRequested?.Invoke(control);
        }
    }
}
