using ErrorHelper.Model.Common.Config;
using ErrorHelper.Tools;
using System.Configuration;

namespace ErrorHelper.Model.Elmah
{
    public class ElmahQueryCondition
    {
        private FormControlTool controlTool = new FormControlTool();

        private const string ElmahFolderPrefix = "Elmah資料夾:";
        public DateTimePicker StartTimePicker { get; set; }
        public DateTimePicker EndTimePicker { get; set; }
        public TextBox FileNameTextBox { get; set; }
        public TextBox MessageTextBox { get; set; }
        public TextBox DetailTextBox { get; set; }
        public Label ElmahSourceFolderPathLabel { get; set; }
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
        public string ElmahSourceFolderPath
        {
            get => ElmahSourceFolderPathLabel.Text.Replace(ElmahFolderPrefix, string.Empty);
            set => ElmahSourceFolderPathLabel.Text = $"{ElmahFolderPrefix}{value}";
        }

        /// <summary>
        /// 初始化ElmahQueryCondition
        /// </summary>
        public ElmahQueryCondition()
        {
            //ElmahQueryCondition queryCondition = new ElmahQueryCondition();
            string dateTimeFormat = "yyyy/MM/dd HH:mm";

            StartTimePicker =
                controlTool.NewDateTimePicker("QueryCondition_StartTime", DateTime.Today);
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
                controlTool.NewDateTimePicker("QueryCondition_EndTime", DateTime.Today.AddDays(1));
            EndTimePicker.CustomFormat = dateTimeFormat;

            FileNameTextBox = controlTool.NewTextBox("QueryCondition_FileName", 120);
            MessageTextBox = controlTool.NewTextBox("QueryCondition_Message", 120);
            DetailTextBox = controlTool.NewTextBox("QueryCondition_Detail", 120);

            ElmahSourceFolderPathLabel = new Label
            {
                Name = "QueryCondition_ElmahSourceFolderPath"
                ,
                AutoSize = true
            };
            ElmahSourceFolderPath = AppSettings.Elmah.DefaultElmahFolderPath;
        }

        /// <summary>
        /// 更改ElmahFolder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChangeElmahFolder(object? sender, EventArgs e)
        {
            ChangeElmahFolder();
        }

        /// <summary>
        /// 更改ElmahFolder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChangeElmahFolder()
        {
            ElmahSourceFolderPath =
                controlTool.GetSelectFolderPath(ElmahSourceFolderPath);
        }
    }
}
