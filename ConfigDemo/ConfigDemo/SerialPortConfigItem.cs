using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace Config
{
    public enum SerialPortConfigItemName
    {
        GPS串口设置 = 1,
        超高频RFID串口设置 = 2,
        GSM模块串口设置 = 3,
        Zigbee模块串口设置 = 4,
        常用串口设置 = 5,
        实验 = 6,
        条码模块 = 7,
        高频RFID串口设置 = 8
    }
    public enum enumSerialPortConfigItem
    {
        串口名称 = 1,
        波特率 = 2
    }
    /// <summary>
    /// 串口参数的内容类，对串口的参数以及必要的操作进行封装
    /// </summary>
    public class SerialPortConfigItem : ISerialPortConfigItem
    {
        //static string configTableName = "config.xml";


        string _configName;
        public string ConfigName
        {
            get { return _configName; }
            set { _configName = value; }
        }
        string _spName;
        public string SpName
        {
            get { return _spName; }
            set { _spName = value; }
        }
        string _spBaudRate = "57600";
        public string SpBaudRate
        {
            get { return _spBaudRate; }
            set { _spBaudRate = value; }
        }
        //string _spDataBits = "8";
        //public string SpDataBits
        //{
        //    get { return _spDataBits; }
        //    set { _spDataBits = value; }
        //}
        //string _spStopBits = "0";
        //public string SpStopBits
        //{
        //    get { return _spStopBits; }
        //    set { _spStopBits = value; }
        //}
        //string _spParity = "0";
        //public string SpParity
        //{
        //    get { return _spParity; }
        //    set { _spParity = value; }
        //}
        public string GetItemValue(enumSerialPortConfigItem itemName)
        {
            string strR = null;
            if (itemName == enumSerialPortConfigItem.串口名称)
            {
                strR = this._spName;
                return strR;
            }
            if (itemName == enumSerialPortConfigItem.波特率)
            {
                strR = this._spBaudRate.ToString();
                return strR;
            }
            //if (itemName == "Parity")
            //{
            //    strR = this._spParity.ToString();
            //    return strR;
            //}
            //if (itemName == "StopBits")
            //{
            //    strR = this._spStopBits;
            //    return strR;
            //}
            //if (itemName == "DataBits")
            //{
            //    strR = this._spDataBits;
            //    return strR;
            //}
            return strR;
        }
        //void SaveConfigItem()
        //{
        //    ConfigManager.SaveSerialPortConfigurnation(this);
        //}
        public bool SaveConfigItem(string configFile)
        {
            return nsConfigDB.ConfigDB.saveConfig(configFile, this._configName,
                                       new string[2] { this.SpName, this.SpBaudRate });
        }
        public bool SaveConfigItem(string configFile,
                   string portName, string baudRate)
        {
            this.SpName = portName;
            this.SpBaudRate = baudRate;
            //this.SpParity = parity;
            //this.SpDataBits = dataBits;
            //this.SpStopBits = stopBits;
            //ConfigManager.SaveSerialPortConfigurnation(this);
            return nsConfigDB.ConfigDB.saveConfig(configFile, this._configName,
                                                    new string[2] { this.SpName, this.SpBaudRate });
        }
        public SerialPortConfigItem(string name, string spName, string spBaudRate)
        //, string spDataBits, string spStopBits, string spParity)
        {
            this._configName = name;
            this._spName = spName;
            this._spBaudRate = spBaudRate;
            //this._spDataBits = spDataBits;
            //this._spStopBits = spStopBits;
            //this._spParity = spParity;
        }

        public SerialPortConfigItem(SerialPortConfigItemName itemName)
        {
            this._configName = Enum.GetName(typeof(SerialPortConfigItemName)
                                    , itemName);
            switch (itemName)
            {
                case SerialPortConfigItemName.GPS串口设置:
                    this._spBaudRate = "9600";
                    break;
                case SerialPortConfigItemName.GSM模块串口设置:
                    this._spBaudRate = "57600";
                    break;
                case SerialPortConfigItemName.Zigbee模块串口设置:
                    this._spBaudRate = "19200";
                    break;
                case SerialPortConfigItemName.常用串口设置:
                    this._spBaudRate = "9600";
                    break;
                case SerialPortConfigItemName.超高频RFID串口设置:
                    this._spBaudRate = "57600";
                    break;
                case SerialPortConfigItemName.高频RFID串口设置:
                    this._spBaudRate = "115200";
                    break;
                case SerialPortConfigItemName.条码模块:
                    this._spBaudRate = "9600";
                    break;
            }
        }
        public string toString()
        {
            string strR = null;
            strR = string.Format(@"name = {0},spName = {1},BaudRate = {2}",
                                  this._configName, this._spName, this._spBaudRate);
            return strR;
        }



    }
}
