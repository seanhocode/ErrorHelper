using ErrorHelper.App.View.LogViewer;
using ErrorHelper.App.ViewModel.Viewer.LogViewer;
using ErrorHelper.Core.Model.Common.Configuration;
using ErrorHelper.Core.Model.LogHelper;
using ErrorHelper.Infrastructure.Common.Configuration;
using SeanTool.CSharp;
using SeanTool.CSharp.Forms;
using System.Diagnostics;

namespace ErrorHelper.App.Control.LogViewer
{
    public partial class LogViewerControl : UserControl
    {
        # region VAR
        protected IList<LogFile<LogInfo>> LogFileList { get; set; }
        protected IList<LogInfo> LogInfoList { get; set; }
        protected virtual LogQueryConditionViewModel<LogQueryCondition> _LogQueryConditionViewModel { get; set; }

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

        protected virtual (string FieldName, string HeaderText, int Width, DataGridViewContentAlignment locate)[] ColumnOrderAndHeader { get; set; }

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

            // DGV
            // 設定DataGridViewAutoSizeColumnsMode.AllCells會導致讀取非常慢
            LogInfoDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            // 避免最後一列消失
            LogInfoDataGridView.AllowUserToAddRows = false;
            // 欄位標題置中
            LogInfoDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // ==========VirtualMode設定==========
            LogInfoDataGridView.DataBindings.Clear();
            LogInfoDataGridView.AutoGenerateColumns = false;
            LogInfoDataGridView.DataSource = null;
            LogInfoDataGridView.VirtualMode = true;
            LogInfoDataGridView.CellValueNeeded += LogInfoDGV_CellValueNeeded;
            LogInfoDataGridView.CellContentClick += LogInfoDGV_CellContentClick;
            // ==========VirtualMode設定==========

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
        /// <summary>
        /// DGV VirtualMode Event
        /// </summary>
        /// <remarks>
        /// 當 DGV 需要某個儲存格的資料時觸發
        /// </remarks>
        /// <param name="sender">觸發事件的 DataGridView 控制項</param>
        /// <param name="e">包含行索引 RowIndex 和列索引 ColumnIndex 的事件參數</param>
        protected virtual void LogInfoDGV_CellValueNeeded(object? sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex < 0) return;

            LogInfo log = LogInfoList[e.RowIndex];
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
                case "OpenErrorDetailBtnCol":
                    e.Value = "細節";
                    break;
                case "OpenLogFolderBtnCol":
                    e.Value = "檔案總管顯示";
                    break;
                case "AddTitleToIgnoreListBtnCol":
                    e.Value = "忽略此類型";
                    break;
            }
        }

        /// <summary>
        /// DataGridView 儲存格內容點擊事件
        /// </summary>
        /// <remarks>用於處理使用者點擊按鈕欄位 Button Columns 的操作</remarks>
        /// <param name="sender">觸發事件的 DataGridView 控制項</param>
        /// <param name="e">包含點擊位置的行索引 RowIndex 和列索引 ColumnIndex 的事件參數</param>
        protected virtual void LogInfoDGV_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            LogInfo log = LogInfoList[e.RowIndex];
            string colName = LogInfoDataGridView.Columns[e.ColumnIndex].Name;

            if (colName == "OpenErrorDetailBtnCol")
                OpenLogDetail(log);

            else if (colName == "OpenLogFolderBtnCol")
                OpenLogSourceFolder(log);

            else if (colName == "AddTitleToIgnoreListBtnCol")
                AddTitleToIgnoreList(log);
        }

        /// <summary>
        /// 自訂欄位樣式
        /// </summary>
        /// <remarks>
        /// (屬性名稱, 欄位標題) - 這裡同時定義了順序和標題，
        /// 按鈕欄位命名用BtnCol結尾
        /// </remarks>
        protected virtual void DefineDGVColumn()
        {
            ColumnOrderAndHeader = new[]{
                ("OpenErrorDetailBtnCol", "操作", 50, DataGridViewContentAlignment.MiddleCenter),
                ("OpenLogFolderBtnCol", "操作", 100, DataGridViewContentAlignment.MiddleCenter),
                ("AddTitleToIgnoreListBtnCol", "操作", 100, DataGridViewContentAlignment.MiddleCenter),
                (nameof(LogInfo.Time), "時間", 175, DataGridViewContentAlignment.MiddleCenter),
                (nameof(LogInfo.LogID), "ID", 100, DataGridViewContentAlignment.MiddleCenter),
                (nameof(LogInfo.Title), "Title", 1000, DataGridViewContentAlignment.MiddleCenter)
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
        /// 載入欄位、設定樣式
        /// </summary>
        protected virtual void LoadDGVColumn()
        {
            DefineDGVColumn();

            DataGridViewColumnCollection columns = LogInfoDataGridView.Columns;

            // 加入缺少的欄位
            foreach ((string fieldName, string headerText, int width, DataGridViewContentAlignment locate) in ColumnOrderAndHeader)
            {
                if (!columns.Contains(fieldName))
                {
                    if (fieldName.EndsWith("BtnCol"))
                    {
                        // 建立按鈕欄位
                        DataGridViewButtonColumn btn = new DataGridViewButtonColumn
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
                        DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn
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
                column.Visible = visibleFields.Contains(column.Name);

            // 設定排序與標題
            foreach ((string fieldName, string headerText, int width, DataGridViewContentAlignment locate) in ColumnOrderAndHeader)
            {
                if (columns.Contains(fieldName))
                {
                    columns[fieldName].Visible = true;
                    columns[fieldName].HeaderText = headerText;
                    columns[fieldName].Width = width;
                    columns[fieldName].DefaultCellStyle.Alignment = locate;
                    columns[fieldName].DisplayIndex = displayIndex++;
                }
            }

            SetDGVColumnFormat();
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
            _ = QueryLog();
        }

        /// <summary>
        /// 更改Log資料夾按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ChangeLogFolderBtn_Click(object sender, EventArgs e)
        {
            ChangeLogFolder();
            _ = QueryLog();
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
                Key = Path.GetDirectoryName(_LogQueryConditionViewModel.LogSourceFolderPath) ?? string.Empty,
                Value = _LogQueryConditionViewModel.LogSourceFolderPath
            };

            JsonTool.SaveSinglePropertyToListJson<SelectItem>(configFilePath, "LogFolderList", item.Key, item);
        }
        # endregion

        # region Service
        /// <summary>
        /// 查詢Log
        /// </summary>
        protected virtual async Task QueryLog()
        {
            QueryLogBtn.Text = "Loading...";
            QueryLogBtn.Enabled = false;
            LogInfoDataGridView.RowCount = 0;

            LogInfoList = await Task.Run(() =>
            {
                LogFileList = ClickQueryLogBtn?.Invoke(_LogQueryConditionViewModel.LogQueryCondition) ?? new List<LogFile<LogInfo>>();
                return LogFileList.Select(logFile => logFile.LogInfo ?? new LogInfo()).ToList() ?? [];
            });

            if (LogInfoList != null && LogInfoList.Count > 0)
                LogInfoDataGridView.RowCount = LogInfoList.Count;
            else
                MessageBox.Show("No log found.");

            LogInfoDataGridView.Invalidate();
            QueryLogBtn.Text = "Query";
            QueryLogBtn.Enabled = true;
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
                    _LogQueryConditionViewModel.LogSourceFolderPath = FormControlTool.GetSelectFolderPath(_LogQueryConditionViewModel.LogSourceFolderPath);
                else
                    _LogQueryConditionViewModel.LogSourceFolderPath = selectedItem.Value;
            }
            else
            {
                _LogQueryConditionViewModel.LogSourceFolderPath = FormControlTool.GetSelectFolderPath(_LogQueryConditionViewModel.LogSourceFolderPath);
            }
        }

        /// <summary>
        /// 將Log Title加入忽略清單
        /// </summary>
        /// <param name="logInfo"></param>
        protected virtual void AddTitleToIgnoreList(LogInfo logInfo)
        {
            _LogQueryConditionViewModel.LogQueryCondition.IgnoreMessageList.Add(logInfo.Title ?? string.Empty);
            _ = QueryLog();
        }
        # endregion
    }
}
