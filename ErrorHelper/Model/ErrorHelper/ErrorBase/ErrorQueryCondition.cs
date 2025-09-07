using ErrorHelper.Model.Common.Config;
using ErrorHelper.Model.ErrorHelper.Elmah;
using ErrorHelper.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHelper.Model.ErrorHelper.ErrorBase
{
    public class ErrorQueryCondition
    {
        private FormControlTool controlTool = new FormControlTool();

        private const string ErrorFolderPrefix = "Log資料夾:";

        #region 查詢條件
        public DateTime StartTime
        {
            get => StartTimePicker.Value;
            set => StartTimePicker.Value = value;
        }
        public DateTime EndTime
        {
            get => EndTimePicker.Value;
            set => EndTimePicker.Value = value;
        }
        public string FileName
        {
            get => FileNameTextBox.Text;
            set => FileNameTextBox.Text = value;
        }
        public string Message
        {
            get => MessageTextBox.Text;
            set => MessageTextBox.Text = value;
        }
        public string Detail
        {
            get => DetailTextBox.Text;
            set => DetailTextBox.Text = value;
        }
        public string ErrorSourceFolderPath
        {
            get => ErrorSourceFolderPathLabel.Text.Replace(ErrorFolderPrefix, string.Empty);
            set => ErrorSourceFolderPathLabel.Text = $"{ErrorFolderPrefix}{value}";
        }
        #endregion

        #region 畫面
        private Label ErrorSourceFolderPathLabel { get; set; }
        public DateTimePicker StartTimePicker { get; set; }
        public DateTimePicker EndTimePicker { get; set; }
        public TextBox FileNameTextBox { get; set; }
        public TextBox MessageTextBox { get; set; }
        public TextBox DetailTextBox { get; set; }
        private Panel InfoPanel { get; set; }
        private Panel TimeQueryPanel { get; set; }
        private Panel QueryPanel { get; set; }
        public TableLayoutPanel QueryConditionLayout { get; set; }
        #endregion

        public ErrorQueryCondition()
        {
            InitialData();

            GenErrorQueryConditionLayout();
        }

        /// <summary>
        /// 更改Log資料夾 for button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChangeErrorFolder(object? sender, EventArgs e)
        {
            ChangeErrorFolder();
        }

        /// <summary>
        /// 更改Log資料夾
        /// </summary>
        public void ChangeErrorFolder()
        {
            ErrorSourceFolderPath =
                FormControlTool.GetSelectFolderPath(ErrorSourceFolderPath);
        }

        /// <summary>
        /// 載入預設查詢條件
        /// </summary>
        private void InitialData()
        {
            string dateTimeFormat = "yyyy/MM/dd HH:mm";

            StartTimePicker =
                controlTool.NewDateTimePicker("ErrorQueryConditionStartTime", DateTime.Today);
            StartTimePicker.CustomFormat = dateTimeFormat;

            if (AppSettings.Elmah.DefaultElmahQueryDays >= 0)
            {
                StartTimePicker.ValueChanged += (sender, e) =>
                {
                    //EndDateTime = StartDateTime + XXX Days
                    DateTimePicker senderPicker = (DateTimePicker)sender;
                    EndTime = senderPicker.Value.AddDays(AppSettings.Elmah.DefaultElmahQueryDays);
                };
            }

            EndTimePicker =
                controlTool.NewDateTimePicker("ErrorQueryConditionEndTime", DateTime.Today.AddDays(1));
            EndTimePicker.CustomFormat = dateTimeFormat;

            FileNameTextBox = controlTool.NewTextBox("ErrorQueryConditionFileName", 120);
            MessageTextBox = controlTool.NewTextBox("ErrorQueryConditionMessage", 120);
            DetailTextBox = controlTool.NewTextBox("ErrorQueryConditionDetail", 120);

            ErrorSourceFolderPathLabel = new Label
            {
                Name = "ErrorQueryConditionElmahSourceFolderPath",
                AutoSize = true
            };

            ErrorSourceFolderPath = AppSettings.Elmah.DefaultElmahFolderPath;
        }

        /// <summary>
        /// 生成查詢控制項區域
        /// </summary>
        private void GenErrorQueryConditionLayout()
        {
            QueryConditionLayout = controlTool.NewTableLayoutPanel("QueryConditionLayout", 3, 1);

            QueryConditionLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     //Row 0: 資訊區
            QueryConditionLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     //Row 1: 查詢區(Time)
            QueryConditionLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     //Row 2: 查詢區(Contain)

            GenErrorInfoPanel();
            GenErrorTimeQueryPanel();
            GenErrorContainQueryPanel();

            QueryConditionLayout.Controls.Add(InfoPanel, 0, 0);    //資訊區
            QueryConditionLayout.Controls.Add(TimeQueryPanel, 0, 1);    //查詢區(Time)
            QueryConditionLayout.Controls.Add(QueryPanel, 0, 2);    //查詢區(Contain)
        }

        /// <summary>
        /// 資訊控制項區域
        /// </summary>
        private void GenErrorInfoPanel()
        {
            int currLeft = 0;
            InfoPanel = new Panel
            {
                Name = "ErrorInfoPanel",
                Dock = DockStyle.Fill,
                Height = 40 // 或其他適合高度
            };

            ErrorSourceFolderPathLabel.Location = new Point(currLeft, 15);

            InfoPanel.Controls.Add(ErrorSourceFolderPathLabel);
        }

        /// <summary>
        /// 時間條件控制項區域
        /// </summary>
        private void GenErrorTimeQueryPanel()
        {
            int currLeft = 0;

            TimeQueryPanel = new Panel
            {
                Name = "ElmahTimeQueryPanel",
                Dock = DockStyle.Fill,
                Height = 40 // 或其他適合高度
            };

            Label lable = new Label
            {
                Text = "查詢區間:",
                Location = new Point(currLeft, 15),
                Width = 60
            };
            currLeft += lable.Width;
            TimeQueryPanel.Controls.Add(lable);

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
            TimeQueryPanel.Controls.Add(lable);

            EndTimePicker.Location = new Point(currLeft, 10);
            currLeft += EndTimePicker.Width + 10;
            TimeQueryPanel.Controls.Add(EndTimePicker);
        }

        /// <summary>
        /// 條件控制項區域
        /// </summary>
        private void GenErrorContainQueryPanel()
        {
            int currLeft = 0;
            QueryPanel = new Panel
            {
                Name = "ErrorContainQueryPanel",
                Dock = DockStyle.Fill,
                Height = 40 // 或其他適合高度
            };

            GenErrorContainQueryItem(QueryPanel, ref currLeft, "檔名:", 35, FileNameTextBox);
            GenErrorContainQueryItem(QueryPanel, ref currLeft, "Message:", 50, MessageTextBox);
            GenErrorContainQueryItem(QueryPanel, ref currLeft, "Detail:", 45, DetailTextBox);
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
