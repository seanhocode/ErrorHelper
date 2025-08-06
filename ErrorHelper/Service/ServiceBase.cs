using ErrorHelper.Tools;

namespace ErrorHelper.Service
{
    public class ServiceBase
    {
        protected ZipTool zipTool = new ZipTool();
        protected FileTool fileTool = new FileTool();
        protected FormControlTool controlTool = new FormControlTool();
    }
}
