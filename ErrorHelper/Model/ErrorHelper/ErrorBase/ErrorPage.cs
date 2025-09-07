using ErrorHelper.Model.ErrorHelper.Elmah;
using ErrorHelper.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHelper.Model.ErrorHelper.ErrorBase
{
    public class ErrorPage
    {
        private FormControlTool controlTool = new FormControlTool();

        public TabPage ErrorTabPage { get; set; }

        public ErrorQueryCondition ErrorQueryCondition { get; set; }

        public IList<ErrorFile> ErrorList { get; set; }

        private TableLayoutPanel ErrorTabPageLayout { get; set; }

        public ErrorPage()
        {
            InitialData();
            GetErrorTabPage();
        }

        public void GetErrorTabPage()
        {
            ErrorTabPage = controlTool.NewTabPage("QueryErrorTabPage", "查詢Error");

            GenQueryErrorTabPageLayout();

            ErrorTabPage.Controls.Add(ErrorTabPageLayout);
        }

        private void InitialData()
        {
            ErrorQueryCondition = new ErrorQueryCondition();
        }

        private void GenQueryErrorTabPageLayout()
        {
            ErrorTabPageLayout = controlTool.NewTableLayoutPanel("QueryErrorTabPageLayout", 1, 1);

            ErrorTabPageLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     //Row 0: 查詢區
            //ErrorTabPageLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     //Row 3: 動作區
            //ErrorTabPageLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); //Row 4: Grid區

            ErrorTabPageLayout.Controls.Add(ErrorQueryCondition.QueryConditionLayout, 0, 0);    //資訊區
            //queryElmahTabPageLayout.Controls.Add(GenElmahActionPanel(), 1, 3);    //動作區
            //queryElmahTabPageLayout.Controls.Add(ElmahDataGridView, 1, 4);    //Grid區
        }
    }
}
