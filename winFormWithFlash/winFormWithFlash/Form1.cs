using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace winFormWithFlash
{
    public partial class Form1 : Form
    {
        frmInfo4Student frmStudent = null;
        bool teachingState = false;
        string path = null;
        public Form1()
        {
            InitializeComponent();
            //this.TransparencyKey = Color.Red;
            //this.axShockwaveFlash1.BackgroundColor = 65536 * 255;
            //this.axShockwaveFlash1.BGColor = "ff0000";
            // this.axShockwaveFlash1.BGColor = "000000";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.axShockwaveFlash1.Play();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.path = this.textBox1.Text;
            this.axShockwaveFlash1.Movie = path;
            this.axShockwaveFlash1.FrameNum = 0;
            //this.axShockwaveFlash1.Play();
            if (null != this.frmStudent)
            {
                frmStudent.playSpecifiedFrame(path, 0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.axShockwaveFlash1.Stop();

        }

        private void axShockwaveFlash1_Enter(object sender, EventArgs e)
        {
            // MessageBox.Show("flash被点击了！");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.axShockwaveFlash1.Forward();
            if (null != this.frmStudent)
            {
                //frmStudent.playNextFrame();
                frmStudent.playSpecifiedFrame(path, this.axShockwaveFlash1.FrameNum);

            }
            Debug.WriteLine(
                string.Format("Form1.button3_Click  -> FrameNum  = {0}"
                , this.axShockwaveFlash1.FrameNum));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.axShockwaveFlash1.Back();
            if (null != this.frmStudent)
            {
                //frmStudent.playPreviousFrame();
                frmStudent.playSpecifiedFrame(path, this.axShockwaveFlash1.FrameNum);

            }
            Debug.WriteLine(
    string.Format("Form1.button4_Click  -> FrameNum  = {0}"
    , this.axShockwaveFlash1.FrameNum));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int height = int.Parse(this.textBox2.Text);
            this.groupBox1.Height = height;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Screen[] screens = System.Windows.Forms.Screen.AllScreens;
            for (int i = 0; i < screens.Length; i++)
            {
                Screen sc = screens[i];
                //sc.Bounds
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.teachingState == false)
            {
                if (this.frmStudent == null)
                {
                    this.frmStudent = new frmInfo4Student();

                }

                Screen[] screens = System.Windows.Forms.Screen.AllScreens;
                for (int i = 0; i < screens.Length; i++)
                {
                    Screen sc = screens[i];
                    if (sc.Primary == false)
                    {
                        Rectangle rect = sc.WorkingArea;
                        this.frmStudent.Left = rect.Left;
                        this.frmStudent.Top = rect.Top;
                        this.frmStudent.Width = rect.Width;
                        this.frmStudent.Height = rect.Height;

                        frmStudent.Show();
                        if (this.axShockwaveFlash1.FrameNum >= 0)
                        {
                            frmStudent.playSpecifiedFrame(path, this.axShockwaveFlash1.FrameNum);
                        }
                    }
                    //sc.Bounds
                }
                this.button7.Text = "退出上课模式";
                teachingState = true;
            }
            else
            {
                if (this.frmStudent != null)
                {
                    this.frmStudent.Close();
                    this.frmStudent = null;
                }
                teachingState = false;
                this.button7.Text = "进入上课模式";
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "flash文件|*.swf";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog1.FileName;
                this.button1_Click(null, null);
            }
        }
    }
}
