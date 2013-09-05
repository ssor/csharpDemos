using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace mp3Player
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strPath = @"C:\Users\Public\Music\Sample Music\Kalimba.mp3";
            this.axWindowsMediaPlayer1.URL = strPath;

        }
    }
}
