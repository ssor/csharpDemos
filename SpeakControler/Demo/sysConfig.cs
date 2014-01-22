using System;
using System.Collections.Generic;
using System.Text;

namespace rfidCheck
{
    public static class sysConfig
    {

        //public static string comportName="com1";

        //public static string baudRate = "57600";

        public static int tcp_port = 9002;
        public static string restIp = "192.168.1.100";

        public static string restUrl =
            "http://" + restIp + ":" + tcp_port + "/index.php/LED/led/getLedInfos";

        public static string getRestUrl()
        {
            return "http://" + restIp + ":" + tcp_port + "/index.php/RFIDReader/Reader/getScanTags";
            //return "http://" + restIp + ":" + tcp_port + "/index.php/LED/led/getLedInfos";
        }
        public static string getCommandRestUrl()
        {
            return "http://" + restIp + ":" + tcp_port + "/index.php/LED/CommandInfo/getCommandInfos";
        }

    }
}
