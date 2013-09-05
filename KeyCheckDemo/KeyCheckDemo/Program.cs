using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KeyChec;

namespace KeyCheckDemo
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
            PayCheck.start_loop_check("yingkou2012");
            Application.Run(new Form1());
        }
    }
}
