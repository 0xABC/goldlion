using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoldLion_Project
{
    public partial class Form1 : Form
    {
        private const string OrderFolderPath =
            @"\\10.60.1.213\Department\PY CS\Jeffrey Team\2- RBO#Li Ning, ANTA, Fila\3.3 Goldlion\3.1 订单整理";

        private const string ReportFolderPath = @"\\FPPDPYU09\ShareDisk2$\CS & Macro\Open SO Report\SO Report Backup\";

        /// <summary>
        /// 初始化
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            RemoteFolderBox.Text = OrderFolderPath;
            OpenSoPathBox.Text = ReportFolderPath;
        }

        /// <summary>
        /// 点击按钮 开始运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void start_btn_Click(object sender, EventArgs e)
        {
            List<string> orderFileList = new List<string>();
            foreach (var item in orderListView.Items.Cast<ListViewItem>())
            {
                orderFileList.Add(item.Tag.ToString());
            }

            if (orderFileList.Count == 0)
            {
                MessageBox.Show("请选择至少一份Order Form！", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            List<string> reportFileList = new List<string>();
            foreach (var item in reportListView.Items.Cast<ListViewItem>())
            {
                reportFileList.Add(item.Tag.ToString());
            }

            if (reportFileList.Count == 0)
            {
                MessageBox.Show("请选择至少一份Open SO Report！", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Disable_Button();

            LogBoxWriteLine("正在读取选择的Order Form 文件......", true);
            //异步 读取根目录下的 order form
            var orderList = await ReadOrderFormData(orderFileList);

            if (orderList.Count == 0)
            {
                LogBoxWriteLine("选择的文件没有读取到需要的数据！", true);
                MessageBox.Show("选择的文件没有读取到需要的数据！，退出程序！", "Alert", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                Enable_Button();
                return;
            }

            LogBoxWriteLine($"读取完成，一共有 {orderList.Count} 条订单记录！\r\n", true);

            //开始写入数据
            LogBoxWriteLine("正在匹配SOL并写入订单数据 ......", true);

            var savePath = await Common.WriteData(orderList, reportFileList,
                new Progress<string>(strText => LogBoxWriteLine(strText, true)));

            LogBoxWriteLine($"写入完成，Excel文件已保存 -> {savePath}", true);

            Enable_Button();
            MessageBox.Show("操作完成！", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 异步读取Order Form数据
        /// </summary>
        /// <param name="fileList"></param>
        /// <returns></returns>
        private Task<List<OrderData>> ReadOrderFormData(List<string> fileList)
        {
            var t = Task.Run(async () =>
            {
                List<Task<OrderData[]>> taskList = new List<Task<OrderData[]>>();
                foreach (var strPath in fileList)
                {
                    taskList.Add(Common.ReadOrderFormDataAsync(strPath,
                        new Progress<string>(strText => LogBoxWriteLine(strText, true))));
                }

                await Task.WhenAll(taskList.ToArray());

                List<OrderData> resList = new List<OrderData>();
                foreach (var tsk in taskList)
                {
                    resList.AddRange(tsk.Result);
                }

                return resList;
            });
            return t;
        }


        /// <summary>
        /// 启动Form的所有Button
        /// </summary>
        private void Enable_Button()
        {
            Action act = () =>
            {
                start_btn.Enabled = true;
                clear_log_btn.Enabled = true;
                reset_btn.Enabled = true;
                slt_report_btn.Enabled = true;
                slt_orderForm_btn.Enabled = true;
            };
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(act));
            }
            else
            {
                act();
            }
        }

        /// <summary>
        /// 禁用Form的所有button
        /// </summary>
        private void Disable_Button()
        {
            Action act = () =>
            {
                start_btn.Enabled = false;
                clear_log_btn.Enabled = false;
                reset_btn.Enabled = false;
                slt_report_btn.Enabled = false;
                slt_orderForm_btn.Enabled = false;
            };
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(act));
            }
            else
            {
                act();
            }
        }

        /// <summary>
        /// 向LogTextBox写入一行文本
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="timeRequired"></param>
        private void LogBoxWriteLine(string strText, bool timeRequired = false)
        {
            Action act = () =>
            {
                if (timeRequired)
                    strText = "[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") + "] " + strText;

                logBox.Text += strText + "\r\n";
                logBox.SelectionStart = logBox.Text.Length;
                logBox.ScrollToCaret();
            };
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(act));
            }
            else
            {
                act();
            }
        }


        /// <summary>
        /// 点击按钮选择order form文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slt_orderForm_btn_Click(object sender, EventArgs e)
        {
            var fileList = Common.GetFiles("Excel File(*.xlsx)|*.xlsx", true, "请选择Order Form 文件 可多选", OrderFolderPath);

            foreach (var strPath in fileList)
            {
                ListViewItem lvi = new ListViewItem
                {
                    Tag = strPath,
                    Text = Path.GetFileName(Path.GetDirectoryName(strPath))
                };

                lvi.SubItems.Add(Path.GetFileName(strPath));
                orderListView.Items.Add(lvi);
            }
        }

        /// <summary>
        /// 点击按钮选择open so report文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slt_report_btn_Click(object sender, EventArgs e)
        {
            var fileList = Common.GetFiles("Excel File(*.xlsb)|*.xlsb", true, "请选择Report文件 可多选", ReportFolderPath);
            foreach (var strPath in fileList)
            {
                ListViewItem lvi = new ListViewItem
                {
                    Tag = strPath,
                    Text = Path.GetFileName(Path.GetDirectoryName(strPath))
                };

                lvi.SubItems.Add(Path.GetFileName(strPath));
                reportListView.Items.Add(lvi);
            }
        }

        /// <summary>
        /// 点击重置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void reset_btn_Click(object sender, EventArgs e)
        {
            reportListView.Items.Clear();
            orderListView.Items.Clear();
            logBox.Text = string.Empty;
        }

        /// <summary>
        /// 点击按钮清空LogBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clear_log_btn_Click(object sender, EventArgs e)
        {
            logBox.Text = string.Empty;
        }
    }
}