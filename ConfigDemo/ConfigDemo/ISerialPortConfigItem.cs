using System;
using System.Collections.Generic;
using System.Text;

namespace Config
{
    public interface ISerialPortConfigItem
    {
        string GetItemValue(enumSerialPortConfigItem itemName);

        bool SaveConfigItem(string configFile);

        bool SaveConfigItem(string configFile, string portName, string baudRate);
    }
}
