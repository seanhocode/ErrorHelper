using ErrorHelper.Core.Service;

namespace ErrorHelper.Infrastructure.Service
{
    public class ServiceBase : IServiceBase
    {
        public bool CheckService(){
            return true;
        }
    }
}
