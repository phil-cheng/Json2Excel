using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace json2Excel
{
    public partial class IndexForm : Form
    {
        public IndexForm()
        {
            InitializeComponent();
        }

        private void IndexForm_DragDrop(object sender, DragEventArgs e)
        {
            // 获取拖放的文件路径数组
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                // 这里假设只处理第一个文件，你可以根据需求扩展处理多个文件
                string filePath = files[0];
                try
                {
                    // 读取文件内容并显示到文本框中
                    string fileContent = File.ReadAllText(filePath);
                    jsonTextBox.Text = fileContent;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("读取文件出错: " + ex.Message);
                }

            }
        }

        private void IndexForm_DragEnter(object sender, DragEventArgs e)
        {
            // 检查拖放的数据是否包含文件
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

      
        private void TransferButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(jsonTextBox.Text))
            {
                MessageBox.Show("必须输入要转换的内容");
                return;
            }

            try
            {
                // 使用JsonConvert.DeserializeObject方法尝试反序列化，这里先反序列化为JToken类型，
                // 它可以表示任意JSON结构，并且不会实际去解析具体的对象类型，只是做结构合法性校验
                JToken token = JsonConvert.DeserializeObject<JToken>(jsonTextBox.Text);
                ShowForm showForm = new ShowForm(token);
                showForm.Show();

            }
            catch (JsonException ex)
            {
                MessageBox.Show($"JSON格式错误，错误信息：{ex.Message}");
                return;
            }

        }
    }
}
