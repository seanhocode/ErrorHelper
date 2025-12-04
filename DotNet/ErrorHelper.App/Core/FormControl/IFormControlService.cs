namespace ErrorHelper.App.Core.FormControl
{
    public interface IFormControlService
    {
        MenuStrip NewMenuStrip(string menuStripName);
        TableLayoutPanel NewTableLayoutPanel(string tableLayoutPanelName, int rowCount, int columnCount);
        ToolStripMenuItem NewToolStripMenuItem(string toolStripMenuItemName, string toolStripMenuItemText, EventHandler clickHandler = null);
        ToolStripMenuItem NewToolStripMenuItemDropDownList(string toolStripMenuItemName, string toolStripMenuItemText, ToolStripMenuItem[] dorpDownList);
        TabControl NewTabControl(string tabControlName);
    }
}