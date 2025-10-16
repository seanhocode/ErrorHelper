using ErrorHelper.App.Core.FormControl;
using ErrorHelper.App.Core.Viewer;
using ErrorHelper.App.Core.Viewer.LogViewer;
using ErrorHelper.App.Service.FormControl;
using ErrorHelper.App.Service.Viewer;
using ErrorHelper.App.Service.Viewer.LogViewer;
using ErrorHelper.Core.Model.LogHelper.Elmah;
using ErrorHelper.Core.Model.LogHelper.IISLog;
using ErrorHelper.Core.Service.LogHelper;
using ErrorHelper.Infrastructure.Service.BackupHelper;
using ErrorHelper.Infrastructure.Service.LogHelper;
using Microsoft.Extensions.DependencyInjection;

namespace ErrorHelper.App.Core
{
    public static class DIHelper
    {
        private static IServiceProvider? _provider;

        // 初始化，只能呼叫一次
        public static void Init()
        {
            if (_provider != null)
                throw new InvalidOperationException("DIHelper 已經初始化過了");

            var services = new ServiceCollection();

            // 註冊服務
            services.AddSingleton<IFormControlService, FormControlService>();
            services.AddSingleton<IElmahHelperService<ElmahFile, ElmahInfo, ElmahQueryCondition>, ElmahHelperService>();
            services.AddSingleton<IIISLogHelperService<IISLogFile, IISLogInfo, IISLogQueryCondition>, IISLogHelperService>();
            services.AddSingleton<IElmahViewerService, ElmahViewerService>();
            services.AddSingleton<IIISLogViewerService, IISLogViewerService>();
            services.AddSingleton<IErrorHelperViewerService, ErrorHelperViewerService>();
            services.AddSingleton<IBackupHelperService, BackupHelperService>();

            _provider = services.BuildServiceProvider();
        }

        public static T GetService<T>() where T : notnull
        {
            if (_provider == null)
                throw new InvalidOperationException("DIHelper 尚未初始化");

            return _provider.GetRequiredService<T>();
        }

        public static object GetService(Type t)
        {
            if (_provider == null)
                throw new InvalidOperationException("DIHelper 尚未初始化");

            return _provider.GetRequiredService(t);
        }
    }
}
