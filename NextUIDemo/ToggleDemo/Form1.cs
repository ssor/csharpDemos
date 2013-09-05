using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ToggleDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.switchbutton1.SwitchOff += new NextUI.Bar.OnSwitchOff(switchbutton1_SwitchOff);
            this.switchbutton1.SwitchOn += new NextUI.Bar.OnSwitchOn(switchbutton1_SwitchOn);

            this.switchbutton2.SwitchOff += new NextUI.Bar.OnSwitchOff(switchbutton2_SwitchOff);
            this.switchbutton2.SwitchOn += new NextUI.Bar.OnSwitchOn(switchbutton2_SwitchOn);

            this.switchbutton3.SwitchOff += new NextUI.Bar.OnSwitchOff(switchbutton3_SwitchOff);
            this.switchbutton3.SwitchOn += new NextUI.Bar.OnSwitchOn(switchbutton3_SwitchOn);
        }

        void switchbutton3_SwitchOn(object sender)
        {
            this.label3.Text = "Switch on";
        }

        void switchbutton3_SwitchOff(object sender)
        {
            this.label3.Text = "Switch off";
        }

        void switchbutton2_SwitchOn(object sender)
        {
            this.label2.Text = "Switch on";
        }

        void switchbutton2_SwitchOff(object sender)
        {
            this.label2.Text = "Switch off";
        }

        void switchbutton1_SwitchOn(object sender)
        {
            this.label1.Text = "Switch on";
        }

        void switchbutton1_SwitchOff(object sender)
        {
            this.label1.Text = "Switch off";
        }
    }
}