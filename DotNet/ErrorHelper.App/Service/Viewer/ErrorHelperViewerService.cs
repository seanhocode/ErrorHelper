using ErrorHelper.App.Core;
using ErrorHelper.App.Core.Viewer;
using ErrorHelper.App.Core.Viewer.LogViewer;
using ErrorHelper.App.ViewModel.Backup;
using ErrorHelper.Core.Model.BackupHelper;
using ErrorHelper.Infrastructure.Service.BackupHelper;

namespace ErrorHelper.App.Service.Viewer
{
    public class ErrorHelperViewerService : ViewerServiceBase, IErrorHelperViewerService
    {
        protected IElmahViewerService elmahViewerSrv { get { return DIHelper.GetService<IElmahViewerService>(); } }
        protected IBackupHelperService backupHelperSrv { get { return DIHelper.GetService<IBackupHelperService>(); } }

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
            ToolStripMenuItem openElmahFolderMenuItem = elmahViewerSrv.GetOpenNewLogViewerTabPageMenuItem();
            ToolStripMenuItem backupFolderMenuItem = controlService.NewToolStripMenuItem("BackupFolderMenuItem", "備份資料夾");

            openElmahFolderMenuItem.Click += (sender, e) =>
            {
                elmahViewerSrv.NewLogQueryPage(tabControl);
            };

            backupFolderMenuItem.Click += (sender, e) =>
            {
                BackupFolderViewModel backupFolder = new BackupFolderViewModel(new BackupFolder());
                if (backupFolder.OpenEditWindow())
                {
                    backupHelperSrv.BackupFolderByTemp(backupFolder.BackupFolder);
                    MessageBox.Show("Done");
                }
            };

            ToolStripMenuItem fileDropDownList = controlService.NewToolStripMenuItemDropDownList(
                        "FileToolStripMenuItem", "功能",
                        new ToolStripMenuItem[] {
                            openElmahFolderMenuItem
                            , backupFolderMenuItem
                        });

            mainMenuStrip.Items.Add(fileDropDownList);

            return mainMenuStrip;
        }
    }
}
