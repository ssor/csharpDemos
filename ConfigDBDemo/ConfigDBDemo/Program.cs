using System;
using System.Collections.Generic;
using System.Windows.Forms;
using nsConfigDB;

namespace ConfigDBDemo
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

            ProgramInital();
            Application.Run(new Form1());
        }

        private static void ProgramInital()
        {
            ConfigItem item = new ConfigItem("tb1");
            item.AddColumn("c1");
            item.AddColumn("c2");
            item.AddColumn("c3");

            nsConfigDB.ConfigDB.addConfigItem(item);


            //nsConfigDB.ConfigDB.saveConfig("tb1", "r1", new string[]{"value2c1","value2c2","value2c3"});
            //nsConfigDB.ConfigDB.getConfig("tb1", "r1");
        }
    }
}
