using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDBDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nsConfigDB.ConfigDB.saveConfig("tb1", this.textBox1.Text, new string[] {
                this.textBox2.Text,
                this.textBox3.Text,
                this.textBox4.Text});

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = string.Empty;
            this.textBox2.Text = string.Empty;
            this.textBox3.Text = string.Empty;
            this.textBox4.Text = string.Empty;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] strs = nsConfigDB.ConfigDB.getConfig("tb1", this.textBox1.Text);
            if (strs.Length != null)
            {
                this.textBox2.Text = strs[1];
                this.textBox3.Text = strs[2];
                this.textBox4.Text = strs[3];
            }
        }
    }
}
