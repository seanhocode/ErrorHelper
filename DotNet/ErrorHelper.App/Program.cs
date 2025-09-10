using ErrorHelper.App.Core;
using ErrorHelper.Core.Model.Service.LogHelper.Elmah;
using ErrorHelper.Core.Service.LogHelper;
using ErrorHelper.Infrastructure.Service.LogHelper;
using Microsoft.Extensions.DependencyInjection;

namespace ErrorHelper.App
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            DIHelper.Init();

            Application.Run(new ErrorHelperForm());
        }
    }
}