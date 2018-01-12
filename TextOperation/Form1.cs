using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextOperation
{
    public partial class Form1 : Form
    {

        private List<int> source;
        private List<int> result;
        private int myBase;
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;//禁止调整窗口大小
            this.AllowDrop = true;//允许拖放文件

            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            source = new List<int>();
            result = new List<int>();
            myBase = 0;
        }

        /// <summary>
        /// 主窗口拖放响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            //MessageBox.Show(path);   
        }

        /// <summary>
        /// 获取拖放的文件路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilePath_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            FilePath.Text = path;
        }

        /// <summary>
        /// 拖入的是文件则响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilePath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FilePath.Text == "")
            {
                MessageBox.Show("路径为空！");
                return;
            }

            if (IsNumeric(InBase.Text))
            {
                myBase = int.Parse(InBase.Text);
            }
            else
            {
                MessageBox.Show("基数不对！");
                return;
            }
            source.Clear();
            result.Clear();

            Read(FilePath.Text);
            foreach (var i in source)
            {
                result.Add(myBase - i);
            }
            int sourceSum = 0;
            int resultSum = 0;
            var time = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            for (int j = 0; j < result.Count; j++)
            {
                sourceSum += source[j];
                resultSum += result[j];
                Write(@"c:\" + time + ".txt", source[j].ToString() + "        " + result[j].ToString());
            }
            Write(@"c:\" + time + ".txt", sourceSum + "        " + resultSum);

            MessageBox.Show("OK " + time);
        }

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        private void Write(string path, string data)
        {
            StreamWriter sw;
            if (!File.Exists(path))
            {
                sw = File.CreateText(path);//创建一个用于写入 UTF-8 编码的文本  
                //MessageBox.Show("文件不存在");
            }
            else
            {
                sw = File.AppendText(path);//打开现有 UTF-8 编码文本文件以进行读取  
                //MessageBox.Show("AppendText文件打开成功！");

            }
            sw.WriteLine(data);//以行为单位写入字符串  
            sw.Close();
            sw.Dispose();//文件流释放  
        }

        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="path"></param>
        private void Read(string path)
        {
            StreamReader readfile = new StreamReader(path);
            string temp;
            bool flag = true;
            while ((temp = readfile.ReadLine()) != null)
            {
                if (IsNumeric(temp))
                {
                    source.Add(int.Parse(temp));
                }
                else
                {
                    source.Add(0);
                }
            }
        }

        /// <summary>
        /// 判读是否是数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

        private void add()
        {
            MessageBox.Show("GitHub修改");
        }

        private void addT()
        {
            MessageBox.Show("瞎搞一下看看能不能版本回退");
        }

        private void TestGit()
        {
            MessageBox.Show("测试分支之间的联系");
            MessageBox.Show("这是TestBranch分支");
        }
    }
}
