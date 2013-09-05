using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DigitalDisplayDemo
{
    public partial class Form1 : Form
    {
        private Timer _timer = new Timer();
        public Form1()
        {
            InitializeComponent();
            _timer.Interval = 100;
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Start();
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            this.digitalDisplay1.Number++;
            this.digitalDisplay3.Number -= 20;
            this.digitalDisplay2.Number += 20;
        }
    }
}