using System;
using System.Collections.Generic;
using System.Text;

namespace Config
{
    public enum enumSendDataType
    {
        UDP, SerialPort,None
    }
    public class appConfig : IConfig
    {
        public string sendDataInterval = string.Empty;//发送时间间隔
        public enumSendDataType sendDataType = enumSendDataType.UDP;
        public string configName = "appConfig";

        public appConfig(enumSendDataType sendDataType, string sendDataInterval)
        {
            this.sendDataType = sendDataType;
            this.sendDataInterval = sendDataInterval;
        }
        public appConfig()
        {

        }
        public void copy(IConfig src)
        {
            appConfig source = (appConfig)src;

            this.configName = source.configName;
            this.sendDataInterval = source.sendDataInterval;
            this.sendDataType = source.sendDataType;
        }
        public string getConfigName()
        {
            return this.configName;
        }

    }

}
