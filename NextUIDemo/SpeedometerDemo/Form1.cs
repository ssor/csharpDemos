using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NextUI.Collection;

namespace SpeedometerDemo
{
    public partial class Form1 : Form
    {
        private Timer _timer = new Timer();
        public Form1()
        {
            InitializeComponent();
            MeterLabel m0 = new MeterLabel(0, "0");
            m0.MainColor = Color.Blue;
            MeterLabel m1 = new MeterLabel(1000,"1");
            m1.MainColor = Color.Blue;
            MeterLabel m2 = new MeterLabel(2000,"2");
            m2.MainColor = Color.Blue;
            MeterLabel m3 = new MeterLabel(3000,"3");
            m3.MainColor = Color.Blue;
            MeterLabel m4 = new MeterLabel(4000,"4");
            m4.MainColor = Color.Blue;
            MeterLabel m5 = new MeterLabel(5000, "5");
            m5.MainColor = Color.Blue;
            MeterLabel m6 = new MeterLabel(6000,"6");
            MeterLabel m7 = new MeterLabel(7000,"7");
            MeterLabel m8 = new MeterLabel(8000,"8");
            MeterLabel m9 = new MeterLabel(9000,"9");
            this.pointerMeter1.Labels.Add(m0);
             this.pointerMeter1.Labels.Add(m1);
             this.pointerMeter1.Labels.Add(m2);
             this.pointerMeter1.Labels.Add(m3);
             this.pointerMeter1.Labels.Add(m4);
             this.pointerMeter1.Labels.Add(m5);
             this.pointerMeter1.Labels.Add(m6);
             this.pointerMeter1.Labels.Add(m7);
             this.pointerMeter1.Labels.Add(m8);
             this.pointerMeter1.Labels.Add(m9);
            
            this.pointerMeter2.Labels.Add(m0);
             this.pointerMeter2.Labels.Add(m1);
             this.pointerMeter2.Labels.Add(m2);
             this.pointerMeter2.Labels.Add(m3);
             this.pointerMeter2.Labels.Add(m4);
             this.pointerMeter2.Labels.Add(m5);
             this.pointerMeter2.Labels.Add(m6);
             this.pointerMeter2.Labels.Add(m7);
             this.pointerMeter2.Labels.Add(m8);
             this.pointerMeter2.Labels.Add(m9);

             this.pointerMeter4.Labels.Add(m0);
             this.pointerMeter4.Labels.Add(m1);
             this.pointerMeter4.Labels.Add(m2);
             this.pointerMeter4.Labels.Add(m3);
             this.pointerMeter4.Labels.Add(m4);
             this.pointerMeter4.Labels.Add(m5);
             this.pointerMeter4.Labels.Add(m6);
             this.pointerMeter4.Labels.Add(m7);
             this.pointerMeter4.Labels.Add(m8);
             this.pointerMeter4.Labels.Add(m9);
             _timer.Interval = 1;
             _timer.Tick += new EventHandler(_timer_Tick);
             _timer.Start();
            
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if (this.pointerMeter2.Number == 8000)
            {
                this.pointerMeter2.Number = this.pointerMeter2.Number - 500;
                this.pointerMeter1.Number = this.pointerMeter1.Number - 500;
                this.pointerMeter4.Number = this.pointerMeter4.Number - 500;
            }
            else
            {
                this.pointerMeter2.Number += 50;
                this.pointerMeter1.Number += 50;
                this.pointerMeter4.Number += 50;
            }
        }
    }
}