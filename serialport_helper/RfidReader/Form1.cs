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
using intelligentMiddleWare;

namespace RfidReader
{
    public partial class Form1 : Form
    {
        SerialPort comport = null;
        StringBuilder sbuilder = new StringBuilder();

        public Form1()
        {
            InitializeComponent();

            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            portNames.Items.Clear();
            portNames.Items.AddRange(ports);

            this.lblEPC.Text = string.Empty;
            this.lblEquip.Text = string.Empty;
        }

        #region IRFIDHelperSubscriber 成员

        public void NewMessageArrived()
        {

        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            AudioAlert.PlayAlert();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            comport = new SerialPort(this.portNames.Text, 19200, Parity.None, 8, StopBits.One);
            comport.DataReceived += new SerialDataReceivedEventHandler(comport_DataReceived);
            comport.Open();
            this.btnStop.Visible = true;
            this.btnStart.Visible = false;
        }

        void comport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string temp = comport.ReadExisting();
                sbuilder.Append(temp);
                while (true)
                {
                    temp = sbuilder.ToString();
                    if (temp == null || temp == string.Empty)
                    {
                        break;
                    }

                    int indexLeft = temp.IndexOf("[");
                    int indexRight = temp.IndexOf("]");
                    if (indexRight == -1 || indexLeft == -1)
                    {
                        break;
                        //return;
                    }
                    if (indexLeft >= indexRight)
                    {
                        //前面有数据错误
                        sbuilder.Remove(0, indexLeft);
                    }
                    else
                    {
                        string data = temp.Substring(indexLeft, indexRight - indexLeft + 1);
                        sbuilder.Remove(0, indexRight + 1);
                        //Data dataTemp = new Data(data);
                        ProtocolHelper p = ProtocolHelper.getProtocolHelper(data);
                        if (p != null)
                        {
                            if (p.epcID == "0000000000")
                            {
                                AudioAlert.Msg();
                            }
                            else
                            {
                                AudioAlert.PlayAlert();

                            }
                            Action invoke = () =>
                            {

                                this.lblEquip.Text = p.localDeviceID.Substring(9);
                                this.lblEPC.Text = p.epcID;
                                // FFFFFFFFFF00000F
                            };
                            this.Invoke(invoke);

                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (comport != null)
            {
                comport.Close();
            }
            this.btnStop.Visible = false;
            this.btnStart.Visible = true;

        }
    }
    public delegate void deleControlInvoke(object o);

}
