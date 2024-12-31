using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace json2Excel
{
    public partial class ShowForm : Form
    {

        // 自定义事件，用来通过事件传递参数
        public delegate void ExportExcelClickEventHandler(object sender, ExportExcelEventArgs e);
        public event ExportExcelClickEventHandler ExportExcelClick;

        public ShowForm(JToken json)
        {
            InitializeComponent();

            // 构建根节点
            TreeNode rootNode = new TreeNode("JSON");
            BuildTreeNode(json, rootNode);

            // 装载到treeView
            treeView1.Nodes.Add(rootNode);
            treeView1.ExpandAll();

            // 订阅ExportExcelClick事件
            ExportExcelClick += ExportExcelMenuItem_Click;
        }


        // 递归构建树节点
        private void BuildTreeNode(JToken token, TreeNode parentNode)
        {
            if (token.Type == JTokenType.Object)
            {
                foreach (JProperty property in token.Children<JProperty>())
                {
                    string key = property.Name;
                    TreeNode node = new TreeNode($"{key}: ");

                    JToken valueJtoken = property.Value;
                    if (valueJtoken.HasValues)
                    {
                        // 如果是对象，父节点展示属性个数（包含数组类型）
                        node.Text += "(" + GetJTokenLength(valueJtoken) + ")";
                        // 如果是数组，在node上增加右键导出excel按钮功能
                        if (valueJtoken.Type == JTokenType.Array)
                        {
                            node.ForeColor = Color.Green;
                            AndExportExcelButton(node, valueJtoken);
                        }

                        // 如果是对象，则递归构建子节点
                        BuildTreeNode(valueJtoken, node);
                    }
                    else
                    {
                        // 如果是基本类型，则直接显示值
                        node.Text += valueJtoken.ToString();
                    }
                    parentNode.Nodes.Add(node);
                }
            }
            else if (token.Type == JTokenType.Array)
            {
                int index = 0;
                foreach (JToken item in token.Children())
                {
                    TreeNode node = null;
                    if (item.HasValues) // 如果是子对象，则展示索引和展开值
                    {
                        node = new TreeNode($"[{index}]: ");
                        BuildTreeNode(item, node);

                    }
                    else // 如果是基本类型，则直接显示值
                    {
                        node = new TreeNode(item.ToString());
                    }
                    parentNode.Nodes.Add(node);
                    index++;
                }

            }
        }


        // 获取json长度
        private string GetJTokenLength(JToken jToken)
        {
            switch (jToken.Type)
            {
                case JTokenType.Array:
                    return "arr:" + ((JArray)jToken).Count;
                case JTokenType.Object:
                    return "obj:" + ((JObject)jToken).Properties().Count();
                default:
                    return "";
            }
        }


        // 增加导出excel按钮
        private void AndExportExcelButton(TreeNode node, JToken jtoken)
        {

            // 创建ContextMenuStrip实例
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            // 创建菜单项
            ToolStripMenuItem exportExcelMenuItem = new ToolStripMenuItem("导出excel");

            // 为按钮绑定事件，触发自定义事件并传递参数
            exportExcelMenuItem.Click += (s, args) =>
            {
                if (ExportExcelClick != null)
                {
                    ExportExcelEventArgs customArgs = new ExportExcelEventArgs
                    {
                        JsonToken = jtoken,
                        RelatedTreeNode = node
                    };
                    ExportExcelClick(s, customArgs);
                }
            };
            contextMenuStrip.Items.Add(exportExcelMenuItem);
            node.ContextMenuStrip = contextMenuStrip;
        }


        // 导出excel按钮点击事件
        private void ExportExcelMenuItem_Click(object sender, EventArgs e)
        {
            if (e is ExportExcelEventArgs customArgs)
            {
                JToken jtoken = customArgs.JsonToken;

                // 创建Excel工作簿（这里创建的是.xlsx格式的工作簿）
                IWorkbook workbook = new XSSFWorkbook();
                // 创建工作表
                ISheet sheet = workbook.CreateSheet("Sheet1");

                if (jtoken[0].Type == JTokenType.Object)
                {
                    // 做成一个二维表格
                    // 用于存储表头信息（属性名列表）
                    List<string> headerRow = new List<string>();
                    // 用于存储转换后的二维表格数据（这里用List<List<string>>模拟二维表格，实际可根据需求替换为更合适的数据结构）
                    List<List<string>> tableData = new List<List<string>>();
                    
                    // 假设JSON是数组形式，获取数组中第一个对象的属性名作为表头
                    JObject firstObject = (JObject)jtoken[0];
                    foreach (JProperty property in firstObject.Properties())
                    {
                        headerRow.Add(property.Name);
                    }
                    tableData.Add(headerRow);

                    // 遍历JSON数组中的每个对象，提取对应属性的值添加到表格数据中
                    JArray jArray = (JArray)jtoken;
                    foreach (JObject obj in jArray)
                    {
                        List<string> dataRow = new List<string>();
                        foreach (string propertyName in headerRow)
                        {
                            dataRow.Add(obj[propertyName]?.ToString() ?? "");
                        }
                        tableData.Add(dataRow);
                    }

                    // 写入到表格
                    for (int rowIndex = 0; rowIndex < tableData.Count; rowIndex++) 
                    {
                        IRow dataExcelRow = sheet.CreateRow(rowIndex);
                        List<string> currentRow = tableData[rowIndex];
                        for (int colIndex = 0; colIndex < currentRow.Count; colIndex++)
                        {
                            dataExcelRow.CreateCell(colIndex).SetCellValue(currentRow[colIndex]);
                        }
                    }
                }
                else if (jtoken[0].Type == JTokenType.Array)
                {
                    // 遍历数组，将数据逐行写入Excel,从第一行开始，不加标题
                    for (int i = 0; i < ((JArray)jtoken).Count; i++)
                    {
                        IRow dataRow = sheet.CreateRow(i);
                        for(int j=0; j < ((JArray)jtoken[i]).Count; j++)
                        {
                            ICell cell = dataRow.CreateCell(j);
                            cell.SetCellValue(jtoken[i][j].ToString());
                        }
                    }
                }
                else
                {
                    // 正文输出
                    for (int i = 0; i < ((JArray)jtoken).Count; i++)
                    {
                        IRow dataRow = sheet.CreateRow(i);
                        // 写入第一列
                        ICell cell = dataRow.CreateCell(0);
                        cell.SetCellValue(jtoken[i].ToString());
                    }
                }


                // 创建保存文件对话框
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel文件|*.xlsx";
                saveFileDialog.Title = "保存Excel文件";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 获取用户选择的路径和文件名
                    string filePath = saveFileDialog.FileName;
                    // 将Excel文件保存到用户选择的路径
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        workbook.Write(fileStream);
                    }
                }

            }
        }
    }
}
