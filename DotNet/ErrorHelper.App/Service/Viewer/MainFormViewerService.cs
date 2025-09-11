
using ErrorHelper.App.Core;
using ErrorHelper.App.Core.Viewer;
using ErrorHelper.App.Core.Viewer.LogViewer;

namespace ErrorHelper.App.Service.Viewer
{
    public class MainFormViewerService : ViewerServiceBase, IMainFormViewerService
    {
        protected IElmahViewerService elmahViewerService { get { return DIHelper.GetService<IElmahViewerService>(); } }

        /// <summary>
        /// 生成主要畫面
        /// </summary>
        /// <returns></returns>
        public TableLayoutPanel GetMainLayout()
        {
            TableLayoutPanel mainLayout = controlService.NewTableLayoutPanel("MainLayout", 2, 1);
            TabControl tabControl = controlService.NewTabControl("MainTabControl");

            // =======================================MainLayout=======================================
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));      //Row 0: 菜單
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));  //Row 1: MainTabControl

            mainLayout.Controls.Add(GenMainMenuStrip(tabControl), 1, 0);    //菜單
            mainLayout.Controls.Add(tabControl, 1, 1);                      //MainTabControl
            // =======================================MainLayout=======================================

            return mainLayout;
        }

        /// <summary>
        /// 生成主選單區域
        /// </summary>
        /// <returns></returns>
        private MenuStrip GenMainMenuStrip(TabControl tabControl)
        {
            MenuStrip mainMenuStrip = controlService.NewMenuStrip("MainMenuStrip");
            ToolStripMenuItem openElmahFolderMenuItem = elmahViewerService.GetOpenElmahFolderMenuItem();

            openElmahFolderMenuItem.Click += (sender, e) =>
            {
                elmahViewerService.NewElmahQueryPage(tabControl);
            };

            ToolStripMenuItem fileDropDownList = controlService.NewToolStripMenuItemDropDownList(
                        "FileToolStripMenuItem", "功能",
                        new ToolStripMenuItem[] {
                            openElmahFolderMenuItem
                        });

            //, doBackupItem = controlService.NewToolStripMenuItem("DoBackupStripMenuItem", "Backup", DoBackup)

            mainMenuStrip.Items.Add(fileDropDownList);

            return mainMenuStrip;
        }
    }
}
