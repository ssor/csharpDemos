using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NextUI.Collection;

namespace MeterDemo
{
    public partial class Form1 : Form
    {
        private Timer _timer = new Timer();
        public Form1()
        {
            InitializeComponent();
            MeterLabel m1 = new MeterLabel(-10, "-10");
            m1.MainColor = Color.AliceBlue;
            MeterLabel m2 = new MeterLabel(-5, "-5");
            m2.MainColor = Color.LightBlue;
            MeterLabel m3 = new MeterLabel(0, "0");
            m3.MainColor = Color.Blue;
            MeterLabel m4 = new MeterLabel(5, "5");
            MeterLabel m5 = new MeterLabel(10, "10");
            MeterLabel m6 = new MeterLabel(15, "15");
            MeterLabel m7 = new MeterLabel(20, "20");
            MeterLabel m8 = new MeterLabel(30, "30");
            MeterLabel m9 = new MeterLabel(40, "40");
            MeterLabel m10 = new MeterLabel(50, "50");
            m10.MainColor = Color.LightYellow;
            MeterLabel m11 = new MeterLabel(60, "60");
            m11.MainColor = Color.Yellow;
            MeterLabel m12 = new MeterLabel(70, "70");
            m12.MainColor = Color.Orange;
            this.thermoDisplay1.Label.Add(m1);
            this.thermoDisplay1.Label.Add(m2);
            this.thermoDisplay1.Label.Add(m3);
            this.thermoDisplay1.Label.Add(m4);
            this.thermoDisplay1.Label.Add(m5);
            this.thermoDisplay1.Label.Add(m6);
            this.thermoDisplay1.Label.Add(m7);
            this.thermoDisplay1.Label.Add(m8);
            this.thermoDisplay1.Label.Add(m9);
            this.thermoDisplay1.Label.Add(m10);
            this.thermoDisplay1.Label.Add(m11);
            this.thermoDisplay1.Label.Add(m12);


            MeterLabel m1111 = new MeterLabel(-10, "-10");
            m1111.MainColor = Color.Blue;
            MeterLabel m1211 = new MeterLabel(-5, "-5");
            m1211.MainColor = Color.Blue;
            MeterLabel m13 = new MeterLabel(0, "0");
            m13.MainColor = Color.Blue;
            MeterLabel m14 = new MeterLabel(5, "5");
            MeterLabel m15 = new MeterLabel(10, "10");
            MeterLabel m16 = new MeterLabel(15, "15");
            MeterLabel m17 = new MeterLabel(20, "20");
            MeterLabel m18 = new MeterLabel(30, "30");
            MeterLabel m19 = new MeterLabel(40, "40");
            MeterLabel m110 = new MeterLabel(50, "50");
            MeterLabel m111 = new MeterLabel(60, "60");
            MeterLabel m112 = new MeterLabel(70, "70");
            this.thermoDisplay2.Label.Add(m1111);
            this.thermoDisplay2.Label.Add(m1211);
            this.thermoDisplay2.Label.Add(m13);
            this.thermoDisplay2.Label.Add(m14);
            this.thermoDisplay2.Label.Add(m15);
            this.thermoDisplay2.Label.Add(m16);
            this.thermoDisplay2.Label.Add(m17);
            this.thermoDisplay2.Label.Add(m18);
            this.thermoDisplay2.Label.Add(m19);
            this.thermoDisplay2.Label.Add(m110);
            this.thermoDisplay2.Label.Add(m111);
            this.thermoDisplay2.Label.Add(m112);


            MeterLabel m11111 = new MeterLabel(-10, "-10");
            m11111.MainColor = Color.Blue;
            MeterLabel m12111 = new MeterLabel(-5, "-5");
            m12111.MainColor = Color.Blue;
            MeterLabel m131 = new MeterLabel(0, "  0");
            m131.MainColor = Color.Blue;
            MeterLabel m141 = new MeterLabel(5, "5");
            MeterLabel m151 = new MeterLabel(10, " 10");
            MeterLabel m161 = new MeterLabel(15, "15");
            MeterLabel m171 = new MeterLabel(20, " 20");
            MeterLabel m181 = new MeterLabel(30, "30");
            m11111.Image = global::MeterDemo.Properties.Resources.themo2;
            this.thermoDisplay3.Label.Add(m11111);
            this.thermoDisplay3.Label.Add(m12111);
            this.thermoDisplay3.Label.Add(m131);
            m131.Image = global::MeterDemo.Properties.Resources.themo3;
            this.thermoDisplay3.Label.Add(m141);
            this.thermoDisplay3.Label.Add(m151);
            m151.Image = global::MeterDemo.Properties.Resources.themo4;
            this.thermoDisplay3.Label.Add(m161);
            this.thermoDisplay3.Label.Add(m171);
            m171.Image = global::MeterDemo.Properties.Resources.themo5;
            this.thermoDisplay3.Label.Add(m181);
            _timer.Interval = 100;
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Start();


        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if (this.thermoDisplay1.Number == 25)
            {
               this.thermoDisplay1.Number -= 15;
                this.thermoDisplay2.Number -= 15;
                this.thermoDisplay3.Number -= 15;
                
            }
            this.thermoDisplay1.Number++;
            this.thermoDisplay2.Number++;
            this.thermoDisplay3.Number++;
        }
    }
}