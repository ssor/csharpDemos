using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO.Ports;
using barcode_helper;

namespace barcode_demo
{
    public delegate void deleUpdateContorl(string s);
    public partial class frmReadBar : Form, Ibarcode_reader_listener
    {

        private System.IO.Ports.SerialPort comport = new System.IO.Ports.SerialPort();//定义串口
        bool isPortOpen = false;
        bool isReadingBarcode = false;

        DataTable dataTable = null;
        public frmReadBar()
        {
            InitializeComponent();

            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            cmbPortName.Items.AddRange(ports);

            dataTable = new DataTable();
            dataTable.Columns.Add("条码", typeof(string));
            dataTable.Columns.Add("时间", typeof(string));

            this.Shown += new EventHandler(frmReadBar_Shown);

            //this.pictureBox1.ImageLocation = "http://pica.nipic.com/2008-03-03/20083310379189_2.jpg";
        }

        void frmReadBar_Shown(object sender, EventArgs e)
        {
            this.updateText(string.Empty);
        }
        string buffer = string.Empty;

        void updateText(string str)
        {
            Debug.WriteLine(
                string.Format("frmReadBar.updateText  ->  = {0}"
                , str));
            if (str != string.Empty)
            {
                DataRow dr = this.dataTable.NewRow();
                dr["条码"] = str;
                dr["时间"] = DateTime.Now.ToString("");
                this.dataTable.Rows.InsertAt(dr, 0);
                //this.dataTable.Rows.Add(new object[] { str, DateTime.Now.ToString("") });
            }
            this.dataGridView1.DataSource = dataTable;
            DataGridViewColumnCollection columns = this.dataGridView1.Columns;
            columns[0].Width = 240;
            columns[1].Width = 150;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.isPortOpen == true)
            {
                MessageBox.Show("请先关闭串口！", "提示");
                return;
            }
            this.Close();
        }

        #region 打开关闭串口操作

        bool open_serialport()
        {
            bool bR = true;
            try
            {
                if (this.isPortOpen == false)
                {
                    // 设置串口参数
                    if (!comport.IsOpen)
                    {
                        if (this.cmbPortName.Text == string.Empty)
                        {
                            MessageBox.Show("请先指定串口！");
                            return false;
                        }
                        comport.PortName = this.cmbPortName.Text;
                        comport.Open();//尝试打开串口
                        this.isPortOpen = true;
                    }
                }
            }
            catch (Exception ex)//进行异常捕获
            {
                MessageBox.Show(ex.Message);
                bR = false;
            }
            return bR;
        }
        bool close_serialport()
        {
            bool bR = true;
            try
            {
                if (this.isPortOpen == true)
                {

                    bool bOk = false;
                    if (comport.IsOpen)
                    {
                        // 如果没有全部完成，则要将消息处理让出，使Invoke有机会完成
                        while (!bOk)
                        {
                            Application.DoEvents();
                        }
                        //打开时点击，则关闭串口
                        comport.Close();
                        this.isPortOpen = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bR = false;
            }
            return bR;
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.isReadingBarcode == false)//开始读取条码
            {
                if (this.isPortOpen == false)
                {
                    if (this.open_serialport() == true)
                    {
                        this.beginToReadBarcode();
                    }
                }
                else
                {
                    this.beginToReadBarcode();
                }
            }
            else//停止读取条码
            {
                barcode_reader_helper.stop();
                this.isReadingBarcode = false;
                this.button1.Text = "开始";
                //this._timer.Enabled = false;
            }
        }
        void beginToReadBarcode()
        {
            this.isReadingBarcode = true;
            this.button1.Text = "停止";
            // 循环发送命令
            //this._timer.Enabled = true;
            barcode_reader_helper.start(this.comport, this);
        }
        public void new_message(string barcode)
        {
            this.Invoke(new deleUpdateContorl(updateText), barcode);

        }
    }
}
