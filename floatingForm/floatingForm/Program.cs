using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace floatingForm
{
    static class Program
    {
        public static Form1 frm1;
        public static Form2 frm2;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frm1 = new Form1();
            frm2 = new Form2();
            Application.Run(frm1);
        }

        public static void show_float_form()
        {
            frm1.Show();
            frm2.Hide();
        }
        public static void show_main_form()
        {
            frm2.Show();
            frm1.Hide();
        }
    }
}
