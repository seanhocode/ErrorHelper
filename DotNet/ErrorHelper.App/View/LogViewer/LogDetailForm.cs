using ErrorHelper.Core.Model.LogHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErrorHelper.App.View.LogViewer
{
    public partial class LogDetailForm : Form
    {
        public LogDetailForm()
        {
            InitializeComponent();
        }

        public void SetLogDetail(LogInfo logInfo)
        {
            LogDetailTextBox.Text = logInfo.GetDetail();
        }

        
        private TextBox LogDetailTextBox;
    }
}
