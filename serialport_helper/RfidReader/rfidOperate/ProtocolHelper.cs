using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace intelligentMiddleWare
{
    public class ProtocolHelper
    {
        public string type = string.Empty;//命令类型
        public string localDeviceID = string.Empty;
        public string epcID = string.Empty;
        public string questionID = string.Empty;
        public string questionValue = string.Empty;
        public string remoteDeviceID = string.Empty;
        public string guid = string.Empty;
        public ProtocolHelper()
        {

        }
        public ProtocolHelper(string _type, string _deviceID, string _remoteDeviceID, string _epc, string _questionID, string _value)
        {
            this.type = _type;
            this.localDeviceID = _deviceID;
            this.remoteDeviceID = _remoteDeviceID;
            this.epcID = _epc;
            this.questionID = _questionID;
            this.questionValue = _value;
        }
        /// <summary>
        /// 通过协议数据生成类
        /// </summary>
        /// <param name="data">需要解析的数据</param>
        public static ProtocolHelper getProtocolHelper(string data)
        {
            ProtocolHelper helper = null;
            if (data == null || data.Length < 2)
            {
                return helper;
            }
            else
            {
                Debug.WriteLine(
                	string.Format("ProtocolHelper.getProtocolHelper  ->  = {0}"
                	, data));

                int indexLeft = data.IndexOf("[");
                int indexRight = data.IndexOf("]");
                if (indexLeft == -1 || indexRight == -1)
                {
                    return null;
                }
                try
                {
                    data = data.Replace("[", "");
                    data = data.Replace("]", "");

                    helper = new ProtocolHelper();
                    string[] arrays = data.Split(',');
                    helper.type = arrays[0];
                    helper.localDeviceID = arrays[1];
                    helper.remoteDeviceID = arrays[2];
                    helper.epcID = arrays[3];
                    helper.questionID = arrays[4];
                    helper.questionValue = arrays[5].ToUpper();
                    //helper.guid = arrays[6];
                }
                catch (System.Exception ex)
                {

                }
            }
            return helper;
        }
    }
}
