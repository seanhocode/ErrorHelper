using ErrorHelper.App.Service.FormControl;
using ErrorHelper.App.View.Common;
using ErrorHelper.App.View.LogViewer;
using ErrorHelper.App.ViewModel.Viewer.LogViewer;
using ErrorHelper.Core.Model.Common.Configuration;
using ErrorHelper.Core.Model.LogHelper;
using ErrorHelper.Infrastructure.Common.Configuration;
using SeanTool.Tools;
using System.Diagnostics;

namespace ErrorHelper.App.Control.LogViewer
{
    public partial class LogViewerControl : UserControl
    {
        # region VAR
        protected IList<LogFile<LogInfo>> LogFileList { get; set; }
        protected IList<LogInfo> LogInfoList { get; set; }
        protected List<int> VisibleIndexes { get; set; }
        protected int MaxVisibleRows = 200000;
        protected virtual LogQueryConditionViewModel<LogQueryCondition> _LogQueryConditionViewModel { get; set; }

        protected FormControlService controlSrv = new FormControlService();

        protected DateTimePicker StartTimePicker;
        protected DateTimePicker EndTimePicker;
        protected TextBox LogQueryCondition1TextBox;
        protected TextBox LogQueryCondition2TextBox;
        protected TextBox LogQueryCondition3TextBox;
        protected Label StartTimeConditionLable;
        protected Label EndTimeConditionLabel;
        protected Label LogQueryCondition1Label;
        protected Label LogQueryCondition2Label;
        protected Label LogQueryCondition3Label;
        protected Label FolderPathConditionLabel;
        protected Button QueryLogBtn;
        protected Button ChangeLogFolderBtn;
        protected TableLayoutPanel LogViewerTableLayoutPanel;
        protected DataGridView LogInfoDataGridView;
        protected Label ErrorSourceFolderPathLabel;
        protected LogDetailForm LogDetailForm;
        protected Button SaveFolderPathBtn;

        protected virtual (string FieldName, string HeaderText)[] ColumnOrderAndHeader { get; set; }

        /// <summary>
        /// 點QueryBtn後執行的Method
        /// </summary>
        /// <remarks>傳入LogQueryCondition並回傳LogFileList(Log查詢結果)</remarks>
        public Func<LogQueryCondition, IList<LogFile<LogInfo>>> ClickQueryLogBtn;

        # endregion

        # region 建構元及初始化
        /// <summary>
        /// 給繼承Control使用的無參建構子
        /// </summary>
        /// <remarks>
        /// 若衍生類別的建構子沒有在初始子（constructor initializer）明確呼叫 base(...),
        /// 編譯器會嘗試呼叫基底類別的無參建構子（前提是該無參建構子存在）。
        /// 若基底僅提供有參建構子，衍生類必須顯式呼叫 base(... )，否則會編譯錯誤。
        /// 注意：避免在基底建構子呼叫 virtual 方法，因為衍生類欄位還未初始化，可能導致 NullReference 或未預期行為。
        /// </remarks>
        protected LogViewerControl()
        {
            Initialize();
        }

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="viewModel"></param>
        public LogViewerControl(LogQueryConditionViewModel<LogQueryCondition> viewModel)
        {
            Initialize();
            _LogQueryConditionViewModel = viewModel;
            SetQueryConditionViewModel();
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
            LogQueryCondition1Label.Text = "FileName:";
            LogQueryCondition2Label.Text = "Title:";
            LogQueryCondition3Label.Text = "Detail:";
            LogDetailForm = new LogDetailForm();
            //設計工具常常覆蓋DateTimePickerFormat設定，手動設定
            StartTimePicker.Format = DateTimePickerFormat.Custom;
            EndTimePicker.Format = DateTimePickerFormat.Custom;
            StartTimePicker.CustomFormat = AppSettings.SystemSetting.TimePickerFormatStr;
            EndTimePicker.CustomFormat = AppSettings.SystemSetting.TimePickerFormatStr;

            //DGV
            LogInfoDataGridView.DataBindings.Clear();
            LogInfoDataGridView.AutoGenerateColumns = false;
            LogInfoDataGridView.VirtualMode = true;
            LogInfoDataGridView.DataSource = null;
            LogInfoDataGridView.CellValueNeeded += LogInfoDGV_CellValueNeeded;
            LogInfoDataGridView.CellContentClick += LogInfoDGV_CellContentClick;
            LoadDGVColumn();
        }

        /// <summary>
        /// 綁定 UI 和 ViewModel
        /// </summary>
        /// <remarks>需在QueryConditionViewModel初始化後呼叫</remarks>
        protected virtual void SetQueryConditionViewModel()
        {
            StartTimePicker.DataBindings.Add("Value", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.StartTime));
            EndTimePicker.DataBindings.Add("Value", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.EndTime));
            LogQueryCondition1TextBox.DataBindings.Add("Text", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.FileName));
            LogQueryCondition2TextBox.DataBindings.Add("Text", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.Message));
            LogQueryCondition3TextBox.DataBindings.Add("Text", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.Detail));
            ErrorSourceFolderPathLabel.DataBindings.Add("Text", _LogQueryConditionViewModel, nameof(_LogQueryConditionViewModel.LogSourceFolderPath));
        }
        #endregion

        # region DGV相關設定
        protected virtual void LogInfoDGV_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int modelIndex = VisibleIndexes[e.RowIndex];
            LogInfo log = LogInfoList[modelIndex];
            string col = LogInfoDataGridView.Columns[e.ColumnIndex].Name;

            switch (col)
            {
                case nameof(LogInfo.Time):
                    e.Value = log.Time;
                    break;
                case nameof(LogInfo.LogID):
                    e.Value = log.LogID;
                    break;
                case nameof(LogInfo.Title):
                    e.Value = log.Title;
                    break;
                case "OpenErrorDetailCol":
                    e.Value = "細節";
                    break;
                case "OpenLogFolderCol":
                    e.Value = "檔案總管顯示";
                    break;
                case "AddTitleToIgnoreList":
                    e.Value = "忽略此類型";
                    break;
            }
        }

        protected virtual void LogInfoDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var colName = LogInfoDataGridView.Columns[e.ColumnIndex].Name;
            var log = LogInfoList[e.RowIndex];

            if (colName == "OpenErrorDetailCol")
                OpenLogDetail(log);

            else if (colName == "OpenLogFolderCol")
                OpenLogSourceFolder(log);

            else if (colName == "AddTitleToIgnoreList")
                AddTitleToIgnoreList(log);
        }

        /// <summary>
        /// 自訂欄位樣式
        /// </summary>
        /// <remarks>(屬性名稱, 欄位標題) - 這裡同時定義了順序和標題</remarks>
        protected virtual void DefineDGVColumn()
        {
            ColumnOrderAndHeader = new[]{
                ("OpenErrorDetailCol", "操作"),
                ("OpenLogFolderCol", "操作"),
                ("AddTitleToIgnoreList", "操作"),
                (nameof(LogInfo.Time), "時間"),
                (nameof(LogInfo.LogID), "ID"),
                (nameof(LogInfo.Title), "Title")
            };
        }

        /// <summary>
        /// 設定欄位格式
        /// </summary>
        protected virtual void SetDGVColumnFormat()
        {
            LogInfoDataGridView.Columns[nameof(LogInfo.Time)].DefaultCellStyle.Format = AppSettings.SystemSetting.TimeFormatStr;
        }

        /// <summary>
        /// 自訂欄位樣式
        /// </summary>
        protected virtual void LoadDGVColumn()
        {
            DefineDGVColumn();

            DataGridViewColumnCollection columns = LogInfoDataGridView.Columns;

            // 先加入缺少的欄位（按鈕欄位 + 普通欄位）
            foreach (var (fieldName, headerText) in ColumnOrderAndHeader)
            {
                if (!columns.Contains(fieldName))
                {
                    // 判斷是否為按鈕欄位
                    if (fieldName.StartsWith("Open") || fieldName.StartsWith("Add"))
                    {
                        // 建立按鈕欄位
                        var btn = new DataGridViewButtonColumn
                        {
                            Name = fieldName,
                            HeaderText = headerText,
                            UseColumnTextForButtonValue = false
                        };
                        columns.Add(btn);
                    }
                    else
                    {
                        // 一般文字欄位
                        var col = new DataGridViewTextBoxColumn
                        {
                            Name = fieldName,
                            HeaderText = headerText
                        };
                        columns.Add(col);
                    }
                }
            }

            // 依照 ColumnOrderAndHeader 控制可見/排序
            int displayIndex = 0;
            var visibleFields = new HashSet<string>(
                ColumnOrderAndHeader.Select(c => c.FieldName),
                StringComparer.OrdinalIgnoreCase
            );

            // 隱藏非白名單欄位
            foreach (DataGridViewColumn column in columns)
            {
                column.Visible = visibleFields.Contains(column.Name);
            }

            // 設定排序與標題
            foreach (var (fieldName, headerText) in ColumnOrderAndHeader)
            {
                if (columns.Contains(fieldName))
                {
                    columns[fieldName].Visible = true;
                    columns[fieldName].HeaderText = headerText;
                    columns[fieldName].DisplayIndex = displayIndex++;
                }
            }

            //SetDGVColumnFormat(); // optional
        }
        # endregion

        # region BtnClick
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
        # endregion

        # region Service
        /// <summary>
        /// 查詢Log
        /// </summary>
        protected virtual void QueryLog()
        {
            LogInfoDataGridView.Rows.Clear();

            LogFileList = ClickQueryLogBtn?.Invoke(_LogQueryConditionViewModel.LogQueryCondition);

            LogInfoList = LogFileList.Select(logFile => logFile.LogInfo).ToList<LogInfo>() ?? [];

            VisibleIndexes = Enumerable.Range(0, LogInfoList.Count)
                            .Take(MaxVisibleRows)
                            .ToList();

            LogInfoDataGridView.Invalidate();
            LogInfoDataGridView.Refresh();

            if (LogInfoList.Count > 0)
                LogInfoDataGridView.RowCount = VisibleIndexes.Count;
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

        /// <summary>
        /// 將Log Title加入忽略清單
        /// </summary>
        /// <param name="logInfo"></param>
        protected virtual void AddTitleToIgnoreList(LogInfo logInfo)
        {
            _LogQueryConditionViewModel.LogQueryCondition.IgnoreMessageList.Add(logInfo.Title);
            QueryLog();
        }
        # endregion
    }
}
