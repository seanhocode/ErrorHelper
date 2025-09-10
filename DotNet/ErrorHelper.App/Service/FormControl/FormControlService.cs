using ErrorHelper.App.Core.FormControl;

namespace ErrorHelper.App.Service.FormControl
{
    public class FormControlService : IFormControlService
    {
        /// <summary>
        /// NewTableLayoutPanel
        /// </summary>
        /// <param name="tableLayoutPanelName"></param>
        /// <param name="rowCount"></param>
        /// <param name="columnCount"></param>
        /// <returns></returns>
        public TableLayoutPanel NewTableLayoutPanel(string tableLayoutPanelName, int rowCount, int columnCount)
        {
            return new TableLayoutPanel()
            {
                Name = tableLayoutPanelName,
                Dock = DockStyle.Fill,
                RowCount = rowCount,
                ColumnCount = columnCount
            };
        }

        /// <summary>
        /// NewMenuStrip
        /// </summary>
        /// <param name="menuStripName"></param>
        /// <returns></returns>
        public MenuStrip NewMenuStrip(string menuStripName)
        {
            MenuStrip menuStrip = new MenuStrip()
            {
                Name = menuStripName,
                AutoSize = true,
                Dock = DockStyle.Top
            };

            return menuStrip;
        }

        /// <summary>
        /// NewToolStripMenuItem
        /// </summary>
        /// <param name="toolStripMenuItemName"></param>
        /// <param name="toolStripMenuItemText"></param>
        /// <param name="clickHandler"></param>
        /// <returns></returns>
        public ToolStripMenuItem NewToolStripMenuItem(string toolStripMenuItemName, string toolStripMenuItemText, EventHandler clickHandler = null)
        {
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem()
            {
                Name = toolStripMenuItemName,
                Text = toolStripMenuItemText
            };
            if (clickHandler != null)
                toolStripMenuItem.Click += clickHandler;

            return toolStripMenuItem;
        }

        /// <summary>
        /// NewToolStripMenuItemDropDownList
        /// </summary>
        /// <param name="toolStripMenuItemName"></param>
        /// <param name="toolStripMenuItemText"></param>
        /// <param name="dorpDownList"></param>
        /// <returns></returns>
        public ToolStripMenuItem NewToolStripMenuItemDropDownList(string toolStripMenuItemName, string toolStripMenuItemText, ToolStripMenuItem[] dorpDownList)
        {
            ToolStripMenuItem dropdownList = NewToolStripMenuItem(toolStripMenuItemName, toolStripMenuItemText);

            dropdownList.DropDownItems.AddRange(dorpDownList);

            return dropdownList;
        }
    }
}
