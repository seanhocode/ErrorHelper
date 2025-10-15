using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErrorHelper.App.View.Common
{
    public partial class SelectForm : Form
    {
        /// <summary>
        /// 選項清單
        /// </summary>
        //public List<string> Items { get; set; } = new List<string>();

        public Dictionary<string, string> Items { get; set; }

        /// <summary>
        /// 選擇值
        /// </summary>
        public string? SelectedValue { get; private set; }

        /// <summary>
        /// 請設定Items(Dictionary<string-key, string-value>)
        /// </summary>
        /// <param name="formTitle"></param>
        public SelectForm(string formTitle = "Select")
        {
            InitializeComponent();
            Text = formTitle;
        }

        private void SelectProjectInfoForm_Load(object sender, EventArgs e)
        {
            SelectComboBox.DataSource = Items.Keys.ToList();
        }
        private ComboBox SelectComboBox;
        private Button SelectBtn;

        private void SelectBtn_Click(object sender, EventArgs e)
        {
            string selectedValue = string.Empty;
            Items.TryGetValue(SelectComboBox.SelectedItem?.ToString(), out selectedValue);
            // 設定選擇結果
            SelectedValue = selectedValue;

            // 設定 DialogResult，讓 ShowDialog() 結束
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
