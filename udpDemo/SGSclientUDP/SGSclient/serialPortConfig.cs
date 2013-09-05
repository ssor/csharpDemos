using System;
using System.Collections.Generic;
using System.Text;
using Db4objects.Db4o;
using SGSclient;

namespace Config
{
    public interface IConfig
    {
        string getConfigName();
        void copy(IConfig configSrc);
    }
    public class genericConfig
    {
        public static IConfig getDefaultConfig(IConfig config)
        {
            IConfig configR = null;
            IObjectContainer db = Db4oFactory.OpenFile(staticClass.configFilePath);
            try
            {
                IList<IConfig> list = db.Query<IConfig>(delegate(IConfig cf)
                {
                    return cf.getConfigName() == config.getConfigName();
                }
                                                          );
                if (list.Count > 0)
                {
                    configR = list[0];
                }
            }
            finally
            {
                db.Close();
            }
            return configR;
        }
        public static void saveConfig(IConfig config)
        {
            IObjectContainer db = Db4oFactory.OpenFile(staticClass.configFilePath);
            try
            {
                IList<IConfig> list = db.Query<IConfig>(delegate(IConfig cf)
                {
                    return cf.getConfigName() == config.getConfigName();
                }
                                                          );
                if (list.Count <= 0)
                {
                    db.Store(config);
                }
                else
                {
                    list[0].copy(config);
                    db.Store(list[0]);
                }

            }
            finally
            {
                db.Close();
            }
        }
    }
    public class UDPConfig : IConfig
    {
        public string configName = "UDPConfig";
        public string ip = "127.0.0.1";
        public string port = "13000";
        public UDPConfig() { }
        public UDPConfig(string ip, string port)
        {
            this.ip = ip;
            this.port = port;
        }
        public UDPConfig(string name, string ip, string port)
            : this(ip, port)
        {
            this.configName = name;
        }
        public void copy(IConfig src)
        {
            UDPConfig source = (UDPConfig)src;
            this.configName = source.configName;
            this.ip = source.ip;
            this.port = source.port;
        }
        public string getConfigName()
        {
            return this.configName;
        }

    }
    public class serialPortConfig : IConfig
    {
        public string configName = "serialPortConfig";
        public string portName = string.Empty;
        public string baudRate = string.Empty;
        public string parity = string.Empty;
        public string dataBits = string.Empty;
        public string stopBits = string.Empty;

        public serialPortConfig() { }
        public serialPortConfig(string port, string rate, string parity, string dataBits, string stopBits)
        {
            this.portName = port;
            this.baudRate = rate;
            this.parity = parity;
            this.dataBits = dataBits;
            this.stopBits = stopBits;
        }
        public serialPortConfig(string configName, string port, string rate, string parity, string dataBits, string stopBits)
        {
            this.configName = configName;
            this.portName = port;
            this.baudRate = rate;
            this.parity = parity;
            this.dataBits = dataBits;
            this.stopBits = stopBits;
        }
        public void copy(IConfig src)
        {
            serialPortConfig source = (serialPortConfig)src;
            this.configName = source.configName;
            this.portName = source.portName;
            this.baudRate = source.baudRate;
            this.parity = source.parity;
            this.dataBits = source.dataBits;
            this.stopBits = source.stopBits;
        }
        public string getConfigName()
        {
            return this.configName;
        }
    }
}
