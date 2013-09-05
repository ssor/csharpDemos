using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Server;

namespace zigbee_monitor_demo
{
    public delegate void deleUpdateControl(string s1, string s2, string s3, string s4);
    public partial class Form1 : Form, IUDPServerListener
    {
        bool bBusy = false;
        public Form1()
        {
            InitializeComponent();

            this.lblHum.Text = string.Empty;
            this.lblTemp.Text = string.Empty;

            this.txtPort.Text = "5000";

            UDPServer.listener = this;
        }

        void parse_command(string data)
        {
            //data = "[huminity_too_low,00158D0000002893,0001,1][interval_huminity_notify,00158D0000002893,0001,1]";
            MatchCollection mc = Regex.Matches(data, @"\[\w{1,},\w{1,},\w{1,},\w{1,}\]");
            foreach (Match m in mc)
            {
                Debug.WriteLine(m.ToString());
                string[] command = m.ToString().Substring(1, m.ToString().Length - 2).Split(',');
                Debug.WriteLine("event -> " + command[0]);
                Debug.WriteLine("adress -> " + command[1]);
                Debug.WriteLine("nodeID -> " + command[2]);
                Debug.WriteLine("value -> " + command[3]);

                dispose_command(command, m.ToString());
            }
        }

        private void dispose_command(string[] command, string raw_data)
        {
            string tip_text = string.Empty;
            string hum_text = string.Empty;
            string temp_text = string.Empty;
            if (bBusy == true)
            {
                return;
            }
            switch (command[0])
            {
                //湿度信息
                case "interval_humidity_notify":
                    tip_text = "湿度信息更新 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    hum_text = command[3].ToString();
                    break;
                case "humidity_too_low":
                    tip_text = "湿度太低，请注意 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    hum_text = command[3].ToString();
                    break;
                case "humidity_too_high":
                    tip_text = "湿度太高，请注意 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    hum_text = command[3].ToString();
                    break;
                case "humidity_lower":
                    tip_text = "湿度降低，请注意 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    hum_text = command[3].ToString();
                    break;
                case "humidity_higher":
                    tip_text = "湿度升高，请注意 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    hum_text = command[3].ToString();
                    break;

                //温度信息
                case "interval_temperature_notify":
                    tip_text = "温度信息更新 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    temp_text = command[3].ToString();
                    break;
                case "temperature_lower":
                    tip_text = "温度降低，请注意 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    temp_text = command[3].ToString();
                    break;
                case "temperature_higher":
                    tip_text = "温度升高，请注意 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    temp_text = command[3].ToString();
                    break;
                case "temperature_too_low":
                    tip_text = "温度太低，请注意 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    temp_text = command[3].ToString();
                    break;
                case "temperature_too_high":
                    tip_text = "温度太高，请注意 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    temp_text = command[3].ToString();
                    break;
            }

            update_control(tip_text, hum_text, temp_text, raw_data);
        }

        private void update_control(string tip_text, string hum_text, string temp_text, string raw_data)
        {
            deleUpdateControl dele = delegate(string s1, string s2, string s3, string s4)
            {
                if (s1 != string.Empty)
                {
                    this.txtTipList.Text = tip_text + "\r\n" + this.txtTipList.Text;
                }
                if (s2 != string.Empty)
                {
                    this.lblHum.Text = s2;
                }
                if (s3 != string.Empty)
                {
                    this.lblTemp.Text = s3;
                }
                if (s4 != string.Empty)
                {
                    this.txtDataList.Text = raw_data + "\r\n" + this.txtDataList.Text;
                }
                this.bBusy = false;
            };
            this.bBusy = true;
            this.Invoke(dele, tip_text, hum_text, temp_text, raw_data);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //parse_command(null);
            //new_message();
            try
            {
                int port = int.Parse(this.txtPort.Text);
                UDPServer.startUDPListening(port);
                this.btnStart.Enabled = false;
                //this.btnStart.Text = "停止监听(&S)";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("串口填写错误！", "提示");
                return;
            }
        }

        public void new_message()
        {

            //UDPServer.sbuilder.Append("[huminity_too_low,00158D0000002893,0001,1]");
            //UDPServer.Manualstate.Set();

            string data = UDPServer.sbuilder.ToString();
            int last_right_flag = data.LastIndexOfAny(new char[] { ']' });
            if (last_right_flag > 0)
            {
                string data_to_dispose = data.Substring(0, last_right_flag + 1);
                UDPServer.sbuilder.Remove(0, last_right_flag + 1);
                parse_command(data_to_dispose);
                //string temp = UDPServer.sbuilder.ToString();
            }

            //UDPServer.Manualstate.Reset();
        }
    }
}
