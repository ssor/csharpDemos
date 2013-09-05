using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace wfNewView
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            //this.Shown += new EventHandler(Form2_Shown);
            this.pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);
            this.Load += new EventHandler(Form2_Load);
            this.Shown += new EventHandler(Form2_Shown);
            //this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
        }

        void Form2_Load(object sender, EventArgs e)
        {
            int i = 0;
        }
        private void MainForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //Graphics g = this.CreateGraphics();
            //// Make a big red pen.
            //Pen p = new Pen(Color.Red, 7);
            //g.DrawLine(p, 1, 1, 100, 100);

            Graphics g = this.CreateGraphics();

            // Create string to draw.
            String drawString = "Sample Text";

            // Create font and brush.
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF(150.0F, 150.0F);

            // Draw string to screen.
            g.DrawString(drawString, drawFont, drawBrush, drawPoint);
            //g.Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="number_of_row">货架的层数</param>
        void paint_carbinet_background(Graphics g, int number_of_row)
        {

            int width = this.pictureBox1.Width;//全部宽度
            int height = this.pictureBox1.Height;//全部高度
            //设定顶层、底层以及中间的隔层高度相同
            int top_height = 30;
            int bottom_height = 30;
            int gap_height = 20;
            //侧边厚度
            int broadslide_width = 20;
            // Make a big red pen.
            Pen p_top = new Pen(Color.FromArgb(160, 160, 160), top_height);
            Pen p_bottom = new Pen(Color.FromArgb(160, 160, 160), bottom_height);
            Pen p = new Pen(Color.FromArgb(160, 160, 160), gap_height);
            Pen p_slide = new Pen(Color.FromArgb(180, 180, 180), broadslide_width);
            g.DrawLine(p_slide, broadslide_width / 2, 1, broadslide_width / 2, height);//左边
            g.DrawLine(p_slide, width - broadslide_width / 2, 1, width - broadslide_width / 2, height);//右边
            g.DrawLine(p_top, 0, gap_height / 2, width, gap_height / 2);//顶
            g.DrawLine(p_bottom, 1, height - gap_height / 2, width, height - gap_height / 2);//底

            //可用宽度
            int work_width = width - broadslide_width * 2;
            //可用高度
            int work_height = height - top_height - bottom_height;
            //柜子的层数
            //int number_of_row = 3;
            //需要在中间绘制  number_of_row -1 个隔层
            // 计算隔层的位置
            // 首先从可用高度中减去隔层所占的高度
            work_height = work_height - (number_of_row - 1) * gap_height;
            //每层的高度
            int row_height = work_height / number_of_row;
            //层的top属性，第二层的就是加上每层的高度和隔层的高度
            int row_top = top_height + row_height + gap_height / 2;//每层的top属性
            //中间隔层
            for (int i = 1; i < number_of_row; i++, row_top = row_top + row_height + gap_height)
            {
                g.DrawLine(p, broadslide_width, row_top, width - broadslide_width, row_top);
            }
        }
        Graphics g_picbox = null;
        void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //g_picbox = e.Graphics;
            paint_carbinet_background(g, 3);

            if (this.pictureBox1.Controls.Count <= 0)
            {
                Button button4 = new System.Windows.Forms.Button();
                button4.Location = new System.Drawing.Point(10, 10);
                button4.Location = Program.getRealPoint(button4.Location);
                button4.Name = "button3";
                button4.Size = new System.Drawing.Size(75, 144);
                //button4.Size = Program.getRealSize(button4.Size);
                button4.Text = string.Format("{0}  {1}", button4.Width, button4.Height);
                button4.UseVisualStyleBackColor = true;

                this.pictureBox1.Controls.Add(button4);
            }
        }

        void Form2_Shown(object sender, EventArgs e)
        {
            int i = 0;
            //Brush brush = new SolidBrush(Color.Black);

            //Pen pen = new Pen(brush);
            //pen.Width = 10;
            //g.DrawLine(pen, new Point(1, 1), new Point(100, 100));
        }
    }
}
