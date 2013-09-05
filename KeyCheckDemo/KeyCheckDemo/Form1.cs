using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using httpHelper;
using System.Diagnostics;
using System.Security.Cryptography;
using KeyChec;

namespace KeyCheckDemo
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = PayCheck.getMD5(this.textBox1.Text);
        }
    }
}
