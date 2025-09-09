using ErrorHelper.Core.Service.Log;
using ErrorHelper.Infrastructure.Service.LogHelper;
using Microsoft.Extensions.DependencyInjection;

namespace ErrorHelper.App
{
    public partial class ErrorHelperForm : Form
    {
        private readonly ILogHelperService LogHelperService = new LogHelperService();

        public ErrorHelperForm()
        {
            InitializeComponent();
        }

        private void ErrorHelperForm_Load(object sender, EventArgs e)
        {
            MessageBox.Show(LogHelperService.Test());
        }
    }
}
