using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Config;

namespace ConfigDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Load += new EventHandler(Form1_Load);
        }

        void Form1_Load(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////////////////////////
            // 初始化用，非必须
            for (int i=1;i<=8;i++)
            {
                string str = Enum.GetName(typeof(SerialPortConfigItemName),i);
                this.comboBox1.Items.Add(str);
            }
            this.comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            this.comboBox1.SelectedIndex = 0;
           ///////////////////////////////////////////////////////////////////////////
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取保存的参数
            SerialPortConfigItem item = ConfigManager.GetConfigItem(comboBox1.Text);
            this.textBox1.Text = item.GetItemValue(enumSerialPortConfigItem.串口名称);
            this.textBox2.Text = item.GetItemValue(enumSerialPortConfigItem.波特率);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //保存参数
            SerialPortConfigItem item = ConfigManager.GetConfigItem(comboBox1.Text);
            item.SpName = this.textBox1.Text;
            item.SpBaudRate = this.textBox2.Text;
            if (ConfigManager.SaveConfigItem(item))
            {
                MessageBox.Show("保存完毕");
            }
            else
            {
                MessageBox.Show("保存失败");
            }
        }
    }
}
