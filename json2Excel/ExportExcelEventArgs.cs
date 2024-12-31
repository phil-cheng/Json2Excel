using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace json2Excel
{
    public class ExportExcelEventArgs : EventArgs
    {
        public JToken JsonToken { get; set; }
        public TreeNode RelatedTreeNode { get; set; }
    }
}
