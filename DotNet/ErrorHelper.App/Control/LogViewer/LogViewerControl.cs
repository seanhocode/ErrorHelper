using ErrorHelper.App.Service.FormControl;
using ErrorHelper.App.View.Common;
using ErrorHelper.App.View.LogViewer;
using ErrorHelper.App.ViewModel.Viewer.LogViewer;
using ErrorHelper.Core.Model.Common.Configuration;
using ErrorHelper.Core.Model.LogHelper;
using ErrorHelper.Tool;
using System.Diagnostics;

namespace ErrorHelper.App.Control.LogViewer
{
    public partial class LogViewerControl : UserControl
    {
        private const string CustomDateTimePickerFormat = "yyyy/MM/dd HH:mm:ss";
        protected IList<LogFile<LogInfo>> LogFileList { get; set; }
        protected IList<LogInfo> LogInfoList => LogFileList.Select(logFile => logFile.LogInfo).ToList<LogInfo>() ?? [];

        protected LogQueryConditionViewModel _LogQueryConditionViewModel;
        protected FormControlService controlSrv = new FormControlService();

        protected DateTimePicker StartTimePicker;
        protected DateTimePicker EndTimePicker;
        protected TextBox FileNameTextBox;
        protected TextBox MessageTextBox;
        protected TextBox DetailTextBox;
        protected Label StartTimeConditionLable;
        protected Label EndTimeConditionLabel;
        protected Label FileNameConditionLabel;
        protected Label MessageConditionLabel;
        protected Label DetailConditionLabel;
        protected Label FolderPathConditionLabel;
        protected Button QueryLogBtn;
        protected Button ChangeLogFolderBtn;
        protected TableLayoutPanel LogViewerTableLayoutPanel;
        protected DataGridView LogInfoDataGridView;
        protected Label ErrorSourceFolderPathLabel;
        protected LogDetailForm LogDetailForm;
        protected Button SaveFolderPathBtn;

        /// <summary>
        /// 點QueryBtn後執行的Method
        /// </summary>
        /// <remarks>傳入LogQueryCondition並回傳LogFileList(Log查詢結果)</remarks>
        public Func<LogQueryCondition, IList<LogFile<LogInfo>>> ClickQueryLogBtn;

        /// <summary>
        /// 給繼承Control用之建構子
        /// </summary>
        protected LogViewerControl()
        {
            Initialize();
        }

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="viewModel"></param>
        public LogViewerControl(LogQueryConditionViewModel viewModel)
        {
            Initialize();
            _LogQueryConditionViewModel = viewModel;
            SetViewModel();
        }

        /// <summary>
        /// 初始化Control
        /// </summary>
        protected virtual void Initialize()
        {
            InitializeComponent();
            InitializeOtherControl();
        }

        /// <summary>
        /// 處理非設計工具定義之Control設定
        /// </summary>
        protected virtual void InitializeOtherControl()
        {
            LogDetailForm = new LogDetailForm();
            //設計工具常常覆蓋DateTimePickerFormat設定，手動設定
            StartTimePicker.Format = DateTimePickerFormat.Custom;
            EndTimePicker.Format = DateTimePickerFormat.Custom;
            StartTimePicker.CustomFormat = CustomDateTimePickerFormat;
            EndTimePicker.CustomFormat = CustomDateTimePickerFormat;
        }

        /// <summary>
        /// 綁定 UI 和 ViewModel
        /// </summary>
        protected virtual void SetViewModel()
        {
            StartTimePicker.DataBindings.Add("Value", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.StartTime));
            EndTimePicker.DataBindings.Add("Value", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.EndTime));
            FileNameTextBox.DataBindings.Add("Text", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.FileName));
            MessageTextBox.DataBindings.Add("Text", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.Message));
            DetailTextBox.DataBindings.Add("Text", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.Detail));
            ErrorSourceFolderPathLabel.DataBindings.Add("Text", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.LogSourceFolderPath));
        }

        /// <summary>
        /// 查詢按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void QueryLogBtn_Click(object sender, EventArgs e)
        {
            _LogQueryConditionViewModel.LogQueryCondition.IgnoreMessageList = new List<string>();
            QueryLog();
        }

        /// <summary>
        /// 更改Log資料夾按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ChangeLogFolderBtn_Click(object sender, EventArgs e)
        {
            ChangeLogFolder();
            QueryLog();
        }

        /// <summary>
        /// 儲存資料夾路徑按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void SaveFolderPathBtn_Click(object sender, EventArgs e)
        {
            string configFilePath = Path.Combine(FileTool.ThisExeDir, "Config", "LogFolderList.json");
            SelectItem item = new SelectItem()
            {
                Key = Path.GetDirectoryName(_LogQueryConditionViewModel.LogSourceFolderPath),
                Value = _LogQueryConditionViewModel.LogSourceFolderPath
            };

            JsonTool.SaveSinglePropertyToListJson<SelectItem>(configFilePath, "LogFolderList", item.Key, item);
        }

        /// <summary>
        /// 查詢Log
        /// </summary>
        protected virtual void QueryLog()
        {
            LogFileList = ClickQueryLogBtn?.Invoke(_LogQueryConditionViewModel.LogQueryCondition);
            LogInfoDataGridView.DataSource = LogInfoList;
        }

        /// <summary>
        /// 產生DGV自訂Action欄位
        /// </summary>
        protected virtual void GenGridAction()
        {
            if (!LogInfoDataGridView.Columns.Contains("OpenErrorDetailCol"))
            {
                controlSrv.GenDataGridViewActionColumn<LogInfo>(LogInfoDataGridView
                , "OpenErrorDetailCol"
                , "操作", "細節"
                , 0
                , (logInfo) => { OpenLogDetail(logInfo); });
            }

            if (!LogInfoDataGridView.Columns.Contains("OpenElmahFolderCol"))
            {
                controlSrv.GenDataGridViewActionColumn<LogInfo>(LogInfoDataGridView
                , "OpenElmahFolderCol"
                , "操作", "檔案總管顯示"
                , 0
                , (logInfo) => { OpenLogSourceFolder(logInfo); });
            }

            if (!LogInfoDataGridView.Columns.Contains("AddTitleToIgnoreList"))
            {
                controlSrv.GenDataGridViewActionColumn<LogInfo>(LogInfoDataGridView
                , "AddTitleToIgnoreList"
                , "操作", "忽略此類型"
                , 0
                , (logInfo) => { AddTitleToIgnoreList(logInfo); });
            }
        }

        /// <summary>
        /// DGV自訂欄位-打開Detail視窗
        /// </summary>
        /// <param name="logInfo"></param>
        protected virtual void OpenLogDetail(LogInfo logInfo)
        {
            LogDetailForm.SetLogDetail(logInfo);
            LogDetailForm.ShowDialog();
        }

        /// <summary>
        /// DGV自訂欄位-打開Log所在資料夾
        /// </summary>
        /// <param name="logInfo"></param>
        protected virtual void OpenLogSourceFolder(LogInfo logInfo)
        {
            LogFile<LogInfo>? selectedErrorFile = LogFileList.FirstOrDefault(file => file.LogInfo.LogID == logInfo.LogID);

            if (selectedErrorFile != null)
            {
                if (string.IsNullOrEmpty(selectedErrorFile.SourceZIPPath))
                    Process.Start("explorer.exe", $"/select,\"{Path.Combine(selectedErrorFile.ParentFolderPath, selectedErrorFile.FileName)}\"");
                else
                    Process.Start("explorer.exe", $"/select,\"{Path.Combine(selectedErrorFile.SourceZIPPath, selectedErrorFile.FileName)}\"");
            }
        }

        /// <summary>
        /// 更改Log資料夾
        /// </summary>
        protected virtual void ChangeLogFolder()
        {
            SelectForm selectConfigForm = new SelectForm("SelectFolder");
            Dictionary<string, string> logFolderItems = new Dictionary<string, string>();
            string configFilePath = Path.Combine(FileTool.ThisExeDir, "Config", "LogFolderList.json");

            foreach (string key in JsonTool.GetJsonSubPropertyList(configFilePath, "LogFolderList"))
                logFolderItems.Add(key, key);

            selectConfigForm.Items = logFolderItems;

            if (selectConfigForm.ShowDialog() == DialogResult.OK)
            {
                SelectItem selectedItem = JsonTool.GetSinglePropertyByListJson<SelectItem>(configFilePath, "LogFolderList", (selectConfigForm.SelectedValue ?? string.Empty));
                if (selectedItem.Value == "Select")
                    _LogQueryConditionViewModel.LogSourceFolderPath = FormControlService.GetSelectFolderPath(_LogQueryConditionViewModel.LogSourceFolderPath);
                else
                    _LogQueryConditionViewModel.LogSourceFolderPath = selectedItem.Value;
            }
            else
            {
                _LogQueryConditionViewModel.LogSourceFolderPath = FormControlService.GetSelectFolderPath(_LogQueryConditionViewModel.LogSourceFolderPath);
            }
        }

        protected virtual void AddTitleToIgnoreList(LogInfo logInfo)
        {
            _LogQueryConditionViewModel.LogQueryCondition.IgnoreMessageList.Add(logInfo.Title);
            QueryLog();
        }
    }
}
