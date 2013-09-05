using System;
using System.Collections.Generic;
using System.Text;

namespace invokePhpRestDemo
{
    public class RestUrl
    {
        //public static string RestAddress = "http://192.168.1.100:9002/index.php/";
        //public static string RestAddress = "http://182.18.26.127:80/index.php/";
        public static string RestAddress = "http://localhost:9002/index.php/";


        public static string post_res = RestAddress + "ResSync/resourceSync/upload_file";
        public static string download_res = RestAddress + "ResSync/resourceSync/download_file";
        public static string get_res_list = RestAddress + "ResSync/resourceSync/get_syc_list";

    }
}
