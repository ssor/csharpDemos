using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DocExp.Actions;
using DocExp;
using DocExp.PreviewControls;

namespace winPPTDemo
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        internal void LoadSubFilesAndFolders(string path)
        {
        }

        internal void SetPreviewPanelButtonsVisibility()
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String strFileName;

            //Find the Office document.
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            strFileName = openFileDialog1.FileName;

            //If the user does not cancel, open the document.
            if (strFileName.Length != 0)
            {
                Preview preview = new Preview();
                FileType ft = new FileType("Ppt Files", "*.Ppt", typeof(PowerPointPreview), null);
                preview.DoAction(strFileName, ft, this);
            }
        }
    }
}
