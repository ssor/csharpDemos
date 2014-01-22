using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using httpHelper;

namespace jsonTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Shown += new EventHandler(Form1_Shown);
        }

        void Form1_Shown(object sender, EventArgs e)
        {
            //jsonClass jc = new jsonClass("wei", "24");
            //string jsonString = string.Empty;
            //jsonString = fastJSON.JSON.Instance.ToJSON(jc);
            //Debug.WriteLine(jsonString);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "http://localhost:9002/index.php/Inventory/Demo/demo1";
            string jsonString = string.Empty;

            HttpWebConnect helper = new HttpWebConnect();
            helper.RequestCompleted += new deleGetRequestObject(helper_RequestCompleted_getAllOrders);
            helper.TryPostData(url, jsonString);
        }
        void helper_RequestCompleted_getAllOrders(object o)
        {
            string strProduct = (string)o;
            object olist = fastJSON.JSON.Instance.ToObject(strProduct, typeof(jsonClass));
            jsonClass jc = (jsonClass)olist;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string url = "http://localhost:9002/index.php/Inventory/Demo/demo2";
            jsonClass jc = new jsonClass("wei", "24");
            string jsonString = string.Empty;
            jsonString = fastJSON.JSON.Instance.ToJSON(jc);
            HttpWebConnect helper = new HttpWebConnect();
            helper.RequestCompleted += new deleGetRequestObject(helper_RequestCompleted_getAllOrders);
            helper.TryPostData(url, jsonString);
        }
        int speaktest = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            string url = "http://192.168.57.105:3003/newcmd";
            command cmd = new command("tts", speaktest.ToString());
            speaktest++;
            string jsonString = string.Empty;
            jsonString = fastJSON.JSON.Instance.ToJSON(cmd);
            HttpWebConnect helper = new HttpWebConnect();
            helper.RequestCompleted += (obj) =>
            {
                string str = (string)obj;
                Debug.WriteLine(str);
            };
            helper.TryPostData(url, jsonString);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string url = "http://192.168.57.105:3003/getcmds";
            command cmd = new command("cmd2", "");
            string jsonString = string.Empty;
            jsonString = fastJSON.JSON.Instance.ToJSON(cmd);
            HttpWebConnect helper = new HttpWebConnect();
            helper.RequestCompleted += (obj) =>
            {
                string str = (string)obj;
                Debug.WriteLine(str);
            };
            helper.TryPostData(url, jsonString);
        }
    }
}
