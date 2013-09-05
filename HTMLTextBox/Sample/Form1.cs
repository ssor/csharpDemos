using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Sample
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
            this.LoadFile(@"C:\Users\ssor\Desktop\sun.htm");
        }
        private void LoadFile(string filename)
        {
            using (StreamReader reader = File.OpenText(filename))
            {
                string html = reader.ReadToEnd();
                reader.Close();
                string file_path = Path.GetDirectoryName(filename);
                html = html.Replace("image_path:", "file://" + file_path);
                this.webBrowser1.DocumentText = html;

                //Text = editor.DocumentTitle;
            }
        }
    }
}
