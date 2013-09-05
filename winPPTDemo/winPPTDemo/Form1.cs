using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace winPPTDemo
{
    public partial class Form1 : Form
    {
        private Object oDocument; 

        public Form1()
        {
            InitializeComponent();
            this.webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(webBrowser1_Navigated);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closed += new System.EventHandler(this.Form1_Closed);
        }

        void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            //Object o = e.Url;

            //oDocument = o.GetType().InvokeMember("Document", BindingFlags.GetProperty, null, o, null);

            //Object oApplication = o.GetType().InvokeMember("Application", BindingFlags.GetProperty, null, oDocument, null);

            //Object oName = o.GetType().InvokeMember("Name", BindingFlags.GetProperty, null, oApplication, null);

            //MessageBox.Show("File opened by: " + oName.ToString()); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.webBrowser1.Navigate(this.textBox1.Text);
            String strFileName;

            //Find the Office document.
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            strFileName = openFileDialog1.FileName;

            //If the user does not cancel, open the document.
            if (strFileName.Length != 0)
            {
                Object refmissing = System.Reflection.Missing.Value;
                oDocument = null;
                webBrowser1.Navigate(strFileName);
            }
        }
        public void Form1_Load(object sender, System.EventArgs e)
        {
            button1.Text = "Browse";
            openFileDialog1.Filter = "Office Documents(*.doc, *.xls, *.ppt)|*.doc;*.xls;*.ppt";
            openFileDialog1.FilterIndex = 1;
        }

        public void Form1_Closed(object sender, System.EventArgs e)
        {
            oDocument = null;
        }
    }
}
