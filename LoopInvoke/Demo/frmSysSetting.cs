using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using nsConfigDB;
using rfidCheck;

namespace Demo
{
    public partial class frmSysSetting : Form
    {
        public frmSysSetting()
        {
            InitializeComponent();

            this.loadConfig();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        bool checkValidation()
        {
            bool bR = true;

            string strPort = this.txtPort.Text;
            try
            {
                int iport = int.Parse(strPort);
                if (iport < 80)
                {
                    bR = false;
                    MessageBox.Show("端口设置不符合规定，请重新设置！");
                    goto end;
                }
                strPort = iport.ToString();
            }
            catch (System.Exception ex)
            {
                bR = false;
                MessageBox.Show("端口设置不符合规定，请重新设置！");
                goto end;
            }
            string strIP = this.txtIP.Text;
            try
            {
                IPAddress _ip = IPAddress.Parse(strIP);

            }
            catch (System.Exception ex)
            {
                bR = false;
                MessageBox.Show("IP地址设置不符合规定，请重新设置！");
                goto end;
            }
        end:
            return bR;
        }
        void loadConfig()
        {
            this.txtPort.Text = sysConfig.tcp_port.ToString();
            this.txtIP.Text = sysConfig.restIp;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.checkValidation())
            {
                ConfigDB.saveConfig("ip", this.txtIP.Text);
                ConfigDB.saveConfig("port", this.txtPort.Text);

                sysConfig.restIp = this.txtIP.Text;
                sysConfig.tcp_port = int.Parse(this.txtPort.Text);

                this.Close();
            }
        }
    }
}
