using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DocExp.Attributes;
using winPPTDemo;

namespace DocExp.AbstractClasses
{
    
    public abstract class Action
    {
        public abstract void DoAction(string path,FileType parFileType,frmMain frm);
        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }
        public bool FileExists(string path)
        {
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                ShowError("File is not accessable");
                return false;
            }
        }
        public bool DirectoryExists(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }
            else
            {
                ShowError("Directory is not accessable");
                return false;
            }
        }
    }
}
