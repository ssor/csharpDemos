using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace jsonTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class command
    {
        public string name;
        public string para;
        public command() { }
        public command(string _name, string _para)
        {
            this.name = _name;
            this.para = _para;
        }
    }
}
