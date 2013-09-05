using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.Media;

namespace RfidReader
{
    public partial class Form1 : Form, IRFIDHelperSubscriber
    {
        Rmu900RFIDHelper rmu900Helper = null;
        IDataTransfer dataTransfer = null;
        SerialPort comport = null;

        public Form1()
        {
            InitializeComponent();
            string strPortName = "com25";
            this.comboBox1.Items.Clear();
            this.comboBox1.Items.Add("RMU900");
            this.comboBox1.Items.Add("2600");
            this.comboBox1.SelectedIndex = 0;
            dataTransfer = new SerialPortDataTransfer();
            comport = new SerialPort(strPortName, 57600, Parity.None, 8, StopBits.One);
            ((SerialPortDataTransfer)dataTransfer).Comport = comport;
            rmu900Helper = new Rmu900RFIDHelper(dataTransfer);
            rmu900Helper.Subscribe(this);
            dataTransfer.AddParser(rmu900Helper);

            this.label2.Text = "当前使用的端口为 " + strPortName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rmu900Helper.StartInventoryOnce();
        }
        void UpdateEpcList(object o)
        {
            //把读取到的标签epc与产品的进行关联
            deleControlInvoke dele = delegate(object oTag)
            {
                string value = oTag as string;
                this.textBox1.Text = value;
                Debug.WriteLine(
                    string.Format("Form1.UpdateEpcList  ->  = {0}"
                    , value));
            };
            this.Invoke(dele, o);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.rmu900Helper.StartWriteEpc(this.textBox1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            rmu900Helper.StartCheckRmuStatus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            rmu900Helper.StartInventory();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            rmu900Helper.StopInventory();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = string.Empty;
        }

        #region IRFIDHelperSubscriber 成员

        public void NewMessageArrived()
        {
            string r3 = rmu900Helper.CheckWriteEpc();
            if (r3 != string.Empty)
            {

                Debug.WriteLine("写入标签成功 " + r3);
            }
            //string r2 = rmu900Helper.CheckInventory();
            //if (r2 != string.Empty)
            //{
            //    this.UpdateEpcList(r2);
            //    //AudioAlert.PlayAlert();
            //    Debug.WriteLine("读取到标签 " + r2);

            //}
            string r1 = rmu900Helper.ChekcInventoryOnce();
            if (r1 != string.Empty)
            {
                Debug.WriteLine("读取到标签 " + r1);
                AudioAlert.PlayAlert();
                this.UpdateEpcList(r1);
            }
            //string r = rmu900Helper.CheckRmuStatus();
            //if (r == "ok")
            //{
            //    MessageBox.Show("设备状态良好！");
            //}
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            AudioAlert.PlayAlert();
        }
    }
    public delegate void deleControlInvoke(object o);

}
