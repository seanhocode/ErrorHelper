using ErrorHelper.Model.ErrorHelper.Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHelper.Model.ErrorHelper.ErrorBase
{
    public class ErrorFile
    {
        /// <summary>
        /// ErrorInfo
        /// </summary>
        public ErrorInfo ErrorInfo { get; set; }

        /// <summary>
        /// ErrorFileName
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 檔案產生時間
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 來自哪個ZIP(完整路徑)
        /// </summary>
        public string SourceZIPPath { get; set; }

        /// <summary>
        /// 上層資料夾
        /// </summary>
        public string ParentFolderPath { get; set; }
    }
}
