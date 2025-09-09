using ErrorHelper.Model.Common.Config;
using ErrorHelper.Model.LogHelper.ErrorBase;
using ErrorHelper.Service.LogHelper.Elmah;
using System.ComponentModel;

namespace ErrorHelper.Model.LogHelper.Elmah
{
    public class ElmahPage : LogPage
    {
        private ElmahService elmahSrv = new ElmahService();

        /// <summary>
        /// 查詢Elmah
        /// </summary>
        /// <remarks>一併更新_elmahList</remarks>
        public void QueryError()
        {
            //LoadElmahList
            ErrorList = elmahSrv.GetElmahList(ErrorQueryCondition.ErrorSourceFolderPath
                                             , ErrorQueryCondition.StartTime
                                             , ErrorQueryCondition.EndTime
                                             , ErrorQueryCondition.FileName
                                             , ErrorQueryCondition.Message
                                             , ErrorQueryCondition.Detail);

            ErrorDataGridView.DataSource =  new BindingList<LogInfo>(ErrorList.Select(elmah => elmah.ErrorInfo).ToList());
        }

        /// <summary>
        /// 刪除Grid上的Elmah
        /// </summary>
        /// <remarks>如檔案在zip裡，會刪除整個zip。會備份至BackUp\yyyyMMdd-HHmmss</remarks>
        public void DeleteElmah()
        {
            elmahSrv.DeleteElmah((IList<LogInfo>)ErrorDataGridView.DataSource, ErrorList);

            QueryError();
        }

        /// <summary>
        /// 建立Elmah Page
        /// </summary>
        public ElmahPage()
        {
            ErrorList = new List<LogFile>();

            ErrorQueryCondition.ErrorSourceFolderPath = AppSettings.Elmah.DefaultElmahFolderPath;

            QueryError();
        }

        public override IList<Button> GetCustomizeAction(ref int currLeft)
        {
            IList<Button> btnList = new List<Button>();

            Button queryBtn = new Button
            {
                Text = "查詢",
                Location = new Point(currLeft + 10, 10),
                Width = 50
            };
            queryBtn.Click += (sender, e) =>
            { QueryError(); MessageBox.Show("Done!"); };
            currLeft += queryBtn.Width + 10;
            btnList.Add(queryBtn);

            Button deleteBtn = new Button
            {
                Text = "刪除",
                Location = new Point(currLeft + 10, 10),
                Width = 50
            };
            deleteBtn.Click += (sender, e) =>
            { DeleteElmah(); };
            currLeft += deleteBtn.Width + 10;
            btnList.Add(deleteBtn);

            return btnList;
        }
    }
}
