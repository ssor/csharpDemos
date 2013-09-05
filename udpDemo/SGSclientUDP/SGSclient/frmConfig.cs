using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using Config;

namespace SGSclient
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();

            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            cmbPortName.Items.AddRange(ports);

            appConfig appc = (appConfig)genericConfig.getDefaultConfig(new appConfig());
            if (appc != null)
            {
                this.txtInterval.Text = appc.sendDataInterval;
                if (appc.sendDataType == enumSendDataType.UDP)
                {
                    this.radioUDP.Checked = true;
                }
                else
                    if (appc.sendDataType == enumSendDataType.SerialPort)
                    {
                        this.radioSerialPort.Checked = true;
                    }
            }

            this.loadUdpConfig();
            this.LoadSerialConfig();
        }
        void loadUdpConfig()
        {
            try
            {
                UDPConfig config = (UDPConfig)genericConfig.getDefaultConfig(new UDPConfig());
                if (config!=null)
                {
                    this.txtPort.Text = config.port;
                    this.txtServerIP.Text = config.ip;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            	
            }
        }
        void LoadSerialConfig()
        {
            try
            {
                serialPortConfig config = (serialPortConfig)genericConfig.getDefaultConfig(new serialPortConfig());
                if (config == null)
                {
                    return;
                }
                string portname = config.portName;
                if (string.Empty == portname)
                {
                    cmbPortName.SelectedIndex = -1;
                }
                else
                {

                    cmbPortName.SelectedIndex = cmbPortName.Items.IndexOf(portname);
                }
                string baudRate = config.baudRate;
                if (string.Empty != baudRate)
                {
                    cmbBaudRate.SelectedIndex = cmbBaudRate.Items.IndexOf(baudRate);
                }
                else
                {
                    cmbBaudRate.SelectedIndex = -1;
                }
                string parity = config.parity;
                if (string.Empty != parity)
                {
                    cmbParity.SelectedIndex = cmbParity.Items.IndexOf(parity);
                }
                else
                {
                    cmbParity.SelectedIndex = -1;
                }
                string stopbites = config.stopBits;
                if (string.Empty != stopbites)
                {
                    cmbStopBits.SelectedIndex = cmbStopBits.Items.IndexOf(stopbites);
                }
                else
                {
                    cmbStopBits.SelectedIndex = -1;
                }
                string databits = config.dataBits;
                if (string.Empty != databits)
                {
                    cmbDataBits.SelectedIndex = cmbDataBits.Items.IndexOf(databits);
                }
                else
                {
                    cmbDataBits.SelectedIndex = -1;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioUDP_CheckedChanged(object sender, EventArgs e)
        {
            this.groupUDP.Visible = true;
            this.groupSerialPort.Visible = false;
        }

        private void radioSerialPort_CheckedChanged(object sender, EventArgs e)
        {
            this.groupUDP.Visible = false;
            this.groupSerialPort.Visible = true;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtInterval.Text == null || this.txtInterval.Text.Length <= 0)
            {
                MessageBox.Show("请填写一个时间间隔，单位为微秒", "提示");
                return;
            }
            string interval = this.txtInterval.Text;

            enumSendDataType type = enumSendDataType.None;
            try
            {
                int i = int.Parse(interval);
            }
            catch
            {
                MessageBox.Show("填写的时间间隔不正确", "提示");
                return;
            }
            if (this.radioSerialPort.Checked == true)
            {
                type = enumSendDataType.SerialPort;
            }
            else if (this.radioUDP.Checked == true)
            {
                type = enumSendDataType.UDP;
            }

            appConfig config = new appConfig(type, interval);
            genericConfig.saveConfig(config);

            if (type == enumSendDataType.SerialPort)
            {
                serialPortConfig serialConfig = new serialPortConfig(cmbPortName.Text,
                                 cmbBaudRate.Text,
                                 cmbParity.Text,
                                 cmbDataBits.Text,
                                 cmbStopBits.Text);
                genericConfig.saveConfig(serialConfig);
            }
            else
                if (type == enumSendDataType.UDP)
                {
                    UDPConfig udpConfig = new UDPConfig(this.txtServerIP.Text, this.txtPort.Text);
                    genericConfig.saveConfig(udpConfig);
                }


            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
