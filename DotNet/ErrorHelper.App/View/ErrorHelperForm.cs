using ErrorHelper.App.Core;
using ErrorHelper.App.Core.Viewer;

namespace ErrorHelper.App.View
{
    public partial class ErrorHelperForm : Form
    {
        private IErrorHelperViewerService mainFormViewerSrv { get { return DIHelper.GetService<IErrorHelperViewerService>(); } }
        private TableLayoutPanel mainLayout;

        public ErrorHelperForm()
        {
            InitializeComponent();

            // 設置表單在螢幕中央開啟
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ErrorHelperForm_Load(object sender, EventArgs e)
        {
            mainLayout = mainFormViewerSrv.GetMainLayout();
            this.Controls.Add(mainLayout);
        }
    }
}
