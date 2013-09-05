using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ResSync;
using httpHelper;
using System.Diagnostics;
using System.Net;

namespace ResSyncDemo
{
    public delegate void deleControlInvoke(object o);

    public partial class Form1 : Form
    {
        ResSyncer syncer = new ResSyncer();
        public Form1()
        {
            InitializeComponent();

            this.__lastTagTimeStamp = syncer.lastTagTimeStamp;
        }
        List<string> file_name_list = new List<string>();
        string _filename;
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "all files (*.*)|*.*";
                DialogResult res = dlg.ShowDialog(this);
                if (res == DialogResult.OK)
                {
                    _filename = dlg.FileName;
                    this.textBox1.Text = _filename;
                }
                else
                    return;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            syncer.start_sync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string uploadfile = this._filename;
            string url = ResSyncer.server_ip + "/index.php/ResSync/resourceSync/upload_file";
            UploadFile.UploadFileEx(uploadfile, url, null, null,
                null, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string url = ResSyncer.server_ip + "/index.php/ResSync/resourceSync/get_syc_list";
            HttpWebConnect helper = new HttpWebConnect();
            helper.RequestCompleted += new deleGetRequestObject(helper_RequestCompleted_get_syc_list);
            helper.TryPostData(url, __lastTagTimeStamp);
        }
        string __lastTagTimeStamp = string.Empty;
        void helper_RequestCompleted_get_syc_list(object o)
        {
            string strres = (string)o;
            Debug.WriteLine(
                string.Format("helper_RequestCompleted_get_syc_list  ->  = {0}"
                , strres));
            object olist = fastJSON.JSON.Instance.ToObjectList(strres, typeof(List<res>), typeof(res));
            deleControlInvoke dele = delegate(object ol)
            {
                List<res> resList = (List<res>)ol;
                if (resList.Count > 0)
                {
                    this.__lastTagTimeStamp = resList[0].time_stamp;
                    for (int i = 0; i < resList.Count; i++)
                    {
                        res temp = resList[i];
                        this.listBox1.Items.Add(temp.resID);
                        this.file_name_list.Add(temp.resID);
                    }
                }
            };
            this.Invoke(dele, olist);
        }
    }
}
