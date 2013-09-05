using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace winFormWithFlash
{
    public partial class frmInfo4Student : Form
    {
        public frmInfo4Student()
        {
            InitializeComponent();
        }
        public void playswf(string path)
        {
            this.axShockwaveFlash1.Movie = path;
            this.axShockwaveFlash1.Play();
        }
        public void playNextFrame()
        {
            this.axShockwaveFlash1.Forward();
        }
        public void playSpecifiedFrame(string path,int iframe)
        {
            this.axShockwaveFlash1.Movie = path;
            this.axShockwaveFlash1.FrameNum = iframe;
            //this.axShockwaveFlash1.Play();
            
        }
        public void playPreviousFrame()
        {
            this.axShockwaveFlash1.Back();
        }
        public void stopPlay()
        {
            this.axShockwaveFlash1.Stop();
        }

    }
}
