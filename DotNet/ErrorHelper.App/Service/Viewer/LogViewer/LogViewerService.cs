using ErrorHelper.App.Core.Viewer.LogViewer;
using ErrorHelper.App.Service.Viewer;

namespace ErrorHelper.App.Service.Control.LogViewer
{
    public class LogViewerService : ViewerServiceBase, ILogViewerService
    {
        /// <summary>
        /// 生成查詢控制項區域
        /// </summary>
        private TableLayoutPanel GetLogQueryConditionLayout()
        {
            TableLayoutPanel queryConditionLayout = controlService.NewTableLayoutPanel("ErrorQueryConditionLayout", 3, 1);

            queryConditionLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     //Row 0: 資訊區
            queryConditionLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     //Row 1: 查詢區(Time)
            queryConditionLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     //Row 2: 查詢區(Contain)

            queryConditionLayout.Controls.Add(GenErrorInfoPanel(), 0, 0);    //資訊區
            queryConditionLayout.Controls.Add(GenErrorTimeQueryPanel(), 0, 1);    //查詢區(Time)
            queryConditionLayout.Controls.Add(GenErrorContainQueryPanel(), 0, 2);    //查詢區(Contain)

            return queryConditionLayout;
        }

        /// <summary>
        /// 資訊控制項區域
        /// </summary>
        private Panel GenErrorInfoPanel()
        {
            int currLeft = 0;
            Panel infoPanel = new Panel
            {
                Name = "ErrorInfoPanel",
                Dock = DockStyle.Top,
                Height = 40 // 或其他適合高度
            };

            ErrorSourceFolderPathLabel.Location = new Point(currLeft, 15);

            infoPanel.Controls.Add(ErrorSourceFolderPathLabel);
        }

        /// <summary>
        /// 時間條件控制項區域
        /// </summary>
        private Panel GenErrorTimeQueryPanel()
        {
            int currLeft = 0;

            Panel timeQueryPanel = new Panel
            {
                Name = "ErrorTimeQueryPanel",
                Dock = DockStyle.Top,
                Height = 40 // 或其他適合高度
            };

            Label lable = new Label
            {
                Text = "查詢區間:",
                Location = new Point(currLeft, 15),
                Width = 60
            };
            currLeft += lable.Width;
            timeQueryPanel.Controls.Add(lable);

            StartTimePicker.Location = new Point(currLeft, 10);
            currLeft += StartTimePicker.Width;
            TimeQueryPanel.Controls.Add(StartTimePicker);

            lable = new Label
            {
                Text = "~",
                Location = new Point(currLeft + 10, 15),
                Width = 20
            };
            currLeft += lable.Width + 10;
            timeQueryPanel.Controls.Add(lable);

            EndTimePicker.Location = new Point(currLeft, 10);
            currLeft += EndTimePicker.Width + 10;
            timeQueryPanel.Controls.Add(EndTimePicker);

            return timeQueryPanel;
        }

        /// <summary>
        /// 條件控制項區域
        /// </summary>
        private Panel GenErrorContainQueryPanel()
        {
            int currLeft = 0;
            Panel queryPanel = new Panel
            {
                Name = "ErrorContainQueryPanel",
                Dock = DockStyle.Top,
                Height = 40 // 或其他適合高度
            };

            GenErrorContainQueryItem(queryPanel, ref currLeft, "檔名:", 35, FileNameTextBox);
            GenErrorContainQueryItem(queryPanel, ref currLeft, "Message:", 50, MessageTextBox);
            GenErrorContainQueryItem(queryPanel, ref currLeft, "Detail:", 45, DetailTextBox);

            return queryPanel;
        }

        /// <summary>
        /// 生成條件控制項
        /// </summary>
        /// <param name="queryPanel"></param>
        /// <param name="currLeft"></param>
        /// <param name="labelText"></param>
        /// <param name="labelWidth"></param>
        /// <param name="textBox"></param>
        private void GenErrorContainQueryItem(Panel queryPanel, ref int currLeft, string labelText, int labelWidth, TextBox textBox)
        {
            Label lable = new Label
            {
                Text = labelText,
                Location = new Point(currLeft, 15),
                Width = labelWidth
            };
            currLeft += lable.Width;
            queryPanel.Controls.Add(lable);

            textBox.Location = new Point(currLeft, 10);
            currLeft += textBox.Width;
            queryPanel.Controls.Add(textBox);
        }
    }
}
