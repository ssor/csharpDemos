using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NextUI.Collection;

namespace DragScrollDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MeterLabel m1 = new MeterLabel(0, "0");
            m1.MainColor = Color.LightYellow;
            MeterLabel m2 = new MeterLabel(10, "10");
            m2.MainColor = Color.LightSteelBlue;
            MeterLabel m3 = new MeterLabel(20, "20");
            m3.MainColor = Color.Green;
            MeterLabel m4 = new MeterLabel(30, "30");
            m4.MainColor = Color.Lavender;
            MeterLabel m5 = new MeterLabel(40, "40");
            m5.MainColor = Color.Khaki;
            MeterLabel m6 = new MeterLabel(300, "300");

            this.selectControl1.Labels.Add(m1);
            this.selectControl1.Labels.Add(m2);
            this.selectControl1.Labels.Add(m3);
            this.selectControl1.Labels.Add(m4);
            this.selectControl1.Labels.Add(m5);
            this.selectControl1.Labels.Add(m6);


            MeterLabel m11 = new MeterLabel(0, "0");
            m11.MainColor = Color.LightYellow;
            MeterLabel m21 = new MeterLabel(10, "10");
            m21.MainColor = Color.LightSteelBlue;
            MeterLabel m31 = new MeterLabel(20, "20");
            m31.MainColor = Color.Green;
            MeterLabel m41 = new MeterLabel(30, "30");
            m41.MainColor = Color.Lavender;
            MeterLabel m51 = new MeterLabel(40, "40");
            m51.MainColor = Color.Khaki;
            MeterLabel m61 = new MeterLabel(50, "50");

            this.selectControl2.Labels.Add(m11);
            this.selectControl2.Labels.Add(m21);
            this.selectControl2.Labels.Add(m31);
            this.selectControl2.Labels.Add(m41);
            this.selectControl2.Labels.Add(m51);
            this.selectControl2.Labels.Add(m61);


            this.selectControl3.Labels.Add(m11);
            this.selectControl3.Labels.Add(m21);
            this.selectControl3.Labels.Add(m31);
            this.selectControl3.Labels.Add(m41);
            this.selectControl3.Labels.Add(m51);
            this.selectControl3.Labels.Add(m61);

            this.selectControl1.Slide += new NextUI.Bar.OnSlide(selectControl1_Slide);
            this.selectControl2.Slide += new NextUI.Bar.OnSlide(selectControl2_Slide);
            this.selectControl3.Slide += new NextUI.Bar.OnSlide(selectControl3_Slide);
        }

        void selectControl3_Slide(object sender, int val)
        {
            this.label3.Text = "SelectControl3 value = " + val;
        }

        void selectControl2_Slide(object sender, int val)
        {
            this.label2.Text = "SelectControl2 value = " + val;
        }

        void selectControl1_Slide(object sender, int val)
        {
            this.label1.Text = "SelectControl1 value = " + val;
        }
    }
}