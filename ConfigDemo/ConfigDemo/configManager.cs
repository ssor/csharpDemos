using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;

namespace Config
{
    /// <summary>
    /// 提取，更新，设置各个设置选项
    /// </summary>
    public class ConfigManager
    {
        public static string configTableName = "serial_port_config_table";
        static bool initialed = InitialConfigManager();


        public static bool InitialConfigManager()
        {
            nsConfigDB.ConfigItem item = new nsConfigDB.ConfigItem(ConfigManager.configTableName);
            item.AddColumn("port_name");
            item.AddColumn("baut");
            nsConfigDB.ConfigDB.addConfigItem(item);
            return true;
        }

        public static SerialPortConfigItem GetConfigItem(string itemName)
        {
            SerialPortConfigItemName item = (SerialPortConfigItemName)Enum.Parse(typeof(SerialPortConfigItemName), itemName);
            return GetConfigItem(item);
        }
        public static SerialPortConfigItem GetConfigItem(SerialPortConfigItemName itemName)
        {
            SerialPortConfigItem spci = null;
            string name = Enum.GetName(typeof(SerialPortConfigItemName)
                                    , itemName);
            string[] items = nsConfigDB.ConfigDB.getConfig(configTableName, name);
            if (items == null)
            {
                spci = new SerialPortConfigItem(itemName);
            }
            else
            {
                spci = new SerialPortConfigItem(name, items[1], items[2]);
            }
            return spci;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="spci"></param>
        /// <returns></returns>
        public static bool SetSerialPort(ref SerialPort sp, ISerialPortConfigItem spci)
        {
            if (null == spci)
            {
                return false;
            }

            try
            {
                if (null == spci.GetItemValue(enumSerialPortConfigItem.串口名称))//尚未初始化设置/
                {
                    MessageBox.Show("请先设置串口参数");
                    return false;
                }
                sp.PortName = spci.GetItemValue(enumSerialPortConfigItem.串口名称);
                sp.BaudRate = int.Parse(spci.GetItemValue(enumSerialPortConfigItem.波特率));
                sp.DataBits = 8;//int.Parse(spci.GetItemValue("DataBits"));
                sp.StopBits = StopBits.One;//(StopBits)Enum.Parse(typeof(StopBits), spci.GetItemValue("StopBits"));
                sp.Parity = Parity.None;//(Parity)Enum.Parse(typeof(Parity), spci.GetItemValue("Parity"));

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("配置文件出现错误！" + ex.Message);
                return false;
            }
            return true;
        }
        public static bool SaveConfigItem(ISerialPortConfigItem spci)
        {
            return spci.SaveConfigItem(configTableName);
        }
    }
}
