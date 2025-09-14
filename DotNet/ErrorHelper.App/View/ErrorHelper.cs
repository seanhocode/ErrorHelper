using ErrorHelper.App.Core;
using ErrorHelper.App.Core.Viewer;

namespace ErrorHelper.App
{
    public partial class ErrorHelperForm : Form
    {
        private IMainFormViewerService mainFormViewerSrv { get { return DIHelper.GetService<IMainFormViewerService>(); } }
        private TableLayoutPanel mainLayout;

        public ErrorHelperForm()
        {
            InitializeComponent();
        }

        private void ErrorHelperForm_Load(object sender, EventArgs e)
        {
            mainLayout = mainFormViewerSrv.GetMainLayout();
            this.Controls.Add(mainLayout);
        }
    }
}
