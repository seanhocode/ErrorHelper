using ErrorHelper.Core.Model.Service.LogHelper.Elmah;
using ErrorHelper.Core.Service.Log;
using ErrorHelper.Core.Service.LogHelper;
using ErrorHelper.Infrastructure.Common.Configuration;
using ErrorHelper.Infrastructure.Service.LogHelper;
using Microsoft.Extensions.DependencyInjection;

namespace ErrorHelper.App
{
    public partial class ErrorHelperForm : Form
    {
        private readonly IElmahService<ElmahFile, ElmahInfo, ElmahQueryCondition> elmahService = new ElmahService();

        public ErrorHelperForm()
        {
            InitializeComponent();
        }

        private void ErrorHelperForm_Load(object sender, EventArgs e)
        {
            ElmahQueryCondition elmahQueryCondition = new ElmahQueryCondition(AppSettings.LogSetting.DefaultLogFolderPath);
            MessageBox.Show(elmahService.GetLogFileList(elmahQueryCondition).FirstOrDefault().LogInfo.Message);
        }
    }
}
