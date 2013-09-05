using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace wfNewView
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Program.setScreenPara(this.button1.Width, this.button1.Height);
            this.Shown += new EventHandler(Form1_Shown);
        }
        void show_position(int top, int left)
        {
            this.label1.Text = string.Format("left =>  {0}  top => {1}", left, top);
        }
        void show_size(int height, int width)
        {
            this.label2.Text = string.Format("width =>  {0}  height => {1}", width, height);

        }
        void Form1_Shown(object sender, EventArgs e)
        {

            this.label1.Text = string.Format("width =>  {0}  height => {1}", this.pictureBox1.Width, this.pictureBox1.Height);

            this.button2.Width = Program.getRealWidth(this.button2.Width);
            this.button2.Height = Program.getRealHeight(this.button2.Height);

            this.button2.Text = this.button2.Width.ToString() + " " + this.button2.Height.ToString();


            Button button3 = new System.Windows.Forms.Button();
            button3.Location = new System.Drawing.Point(30, 30);
            button3.Location = Program.getRealPoint(button3.Location);

            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(75, 140);
            button3.Text = string.Format("{0} {1}", button3.Width, button3.Height);
            button3.UseVisualStyleBackColor = true;

            Button button4 = new System.Windows.Forms.Button();
            button4.Location = new System.Drawing.Point(130, 30);
            button4.Location = Program.getRealPoint(button4.Location);
            button4.Name = "button3";
            button4.Size = new System.Drawing.Size(75, 144);
            button4.Size = Program.getRealSize(button4.Size);
            button4.Text = string.Format("{0}  {1}", button4.Width, button4.Height);
            button4.UseVisualStyleBackColor = true;
            button4.Click += new EventHandler(button4_Click);

            this.pictureBox1.Controls.Add(button3);
            this.pictureBox1.Controls.Add(button4);
        }

        void button4_Click(object sender, EventArgs e)
        {
            this.show_position(((Button)sender).Top, ((Button)sender).Left);
            this.show_size(((Button)sender).Height, ((Button)sender).Width);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.show_position(button3.Top, this.button3.Left);
            this.show_size(button3.Height, button3.Width);
        }
    }
}
