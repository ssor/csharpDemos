using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NextUI.Collection;

namespace KnobDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MeterLabel m1 = new MeterLabel(0, "0");
            m1.MainColor = Color.LightYellow;
            MeterLabel m2 = new MeterLabel(1, "1");
            m2.MainColor = Color.LightSteelBlue;
            MeterLabel m3 = new MeterLabel(2, "2");
            m3.MainColor = Color.Green;
            MeterLabel m4 = new MeterLabel(3, "3");
            m4.MainColor = Color.Lavender;
            MeterLabel m5 = new MeterLabel(4, "4");
            m5.MainColor = Color.Khaki;
            MeterLabel m6 = new MeterLabel(5, "5");
            this.controlKnob1.Labels.Add(m1);
            this.controlKnob1.Labels.Add(m2);
            this.controlKnob1.Labels.Add(m3);
            this.controlKnob1.Labels.Add(m4);
            this.controlKnob1.Labels.Add(m5);
            this.controlKnob1.Labels.Add(m6);
            this.controlKnob2.Labels.Add(m1);
            this.controlKnob2.Labels.Add(m2);
            this.controlKnob2.Labels.Add(m3);
            this.controlKnob2.Labels.Add(m4);
            this.controlKnob2.Labels.Add(m5);
            this.controlKnob2.Labels.Add(m6);

            MeterLabel m11 = new MeterLabel(0, "0");
            m11.Image = global::KnobDemo.Properties.Resources.themo3;
            MeterLabel m21 = new MeterLabel(1, "1");
            MeterLabel m31 = new MeterLabel(2, "2");
            m31.Image =  global::KnobDemo.Properties.Resources.themo4;
            MeterLabel m41 = new MeterLabel(3, "3");
            MeterLabel m51 = new MeterLabel(4, "4");
            m51.Image = global::KnobDemo.Properties.Resources.themo5;
            MeterLabel m61 = new MeterLabel(5, "5");
            this.controlKnob3.Labels.Add(m11);
            this.controlKnob3.Labels.Add(m21);
            this.controlKnob3.Labels.Add(m31);
            this.controlKnob3.Labels.Add(m41);
            this.controlKnob3.Labels.Add(m51);
            this.controlKnob3.Labels.Add(m61);

            this.controlKnob1.Rotate += new NextUI.Bar.OnRotate(controlKnob1_Rotate);
            this.controlKnob2.Rotate += new NextUI.Bar.OnRotate(controlKnob2_Rotate);
            this.controlKnob3.Rotate += new NextUI.Bar.OnRotate(controlKnob3_Rotate);
        }

        void controlKnob3_Rotate(object sender, int value)
        {
            this.label3.Text = "Control 3 = " + value;
        }

        void controlKnob2_Rotate(object sender, int value)
        {
            this.label2.Text = "Control 2 = " +  value;
        }

        void controlKnob1_Rotate(object sender, int value)
        {
            this.label1.Text = "Control 1 = " +  value;
        }
    }
}