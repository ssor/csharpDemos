using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NextUI.Collection;

namespace AnalogControlSample
{
    public partial class Form1 : Form
    {
        private Timer _timer1;
        private Timer _timer2;
        private Timer _timer3;
        public Form1()
        {
            
            InitializeComponent();
            _timer1 = new Timer();
            _timer1.Interval = 1000;
            _timer2 = new Timer();
            _timer2.Interval = 1000;
            _timer3 = new Timer();
            _timer3.Interval = 1000;
            _timer1.Tick += new EventHandler(_timer1_Tick);
            _timer2.Tick += new EventHandler(_timer2_Tick);
            _timer3.Tick += new EventHandler(_timer3_Tick);
            analogCounter1.Number = 100;

            analogCounter2.Number = 10000;
            analogCounter3.Number = 100000;
            analogCounter1.ScrollEffect = false;
            analogCounter3.ScrollEffect = true;
            CounterCollection c = analogCounter2.Panels;
            c[c.Count - 1].FontColor = Color.White;
            c[c.Count - 1].MainColor = Color.Black;
            c[c.Count - 1].Type = NextUI.Display.RollingCounter.FillType.Solid;
            c[c.Count - 2].FontColor = Color.White;
            c[c.Count - 2].MainColor = Color.Black;
            c[c.Count - 2].Type = NextUI.Display.RollingCounter.FillType.Solid;
            c[c.Count - 3].FontColor = Color.White;
            c[c.Count - 3].MainColor = Color.Black;
            c[c.Count - 3].Type = NextUI.Display.RollingCounter.FillType.Solid;
            _timer1.Start();
            _timer2.Start();
            _timer3.Start();
        }

        void _timer3_Tick(object sender, EventArgs e)
        {
            analogCounter3.Number++;
        }

        void _timer2_Tick(object sender, EventArgs e)
        {
            analogCounter2.Number++;
        }

        void _timer1_Tick(object sender, EventArgs e)
        {

            analogCounter1.Number --;
        }
    }
}