using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace RfidReader
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
            Application.Run(new Form1());                                                                                           
            //rexTest();
        }

        static void rexTest()
        {
            string s = "ip=127.0.0.1&port=9002&other=123";
            string pattern = @"(?<key>\w{1,128})=([.\w]{0,128})";//?<key> 是为匹配的group设置别名，可以不使用索引而直接使用别名提取，如gc["address"]
            string patternIP = @"ip=(?<value>[.\w]{0,128})";
            Match mIP = Regex.Match(s, patternIP);
            string IP = mIP.Groups["value"].Value;
            MatchCollection mc = Regex.Matches(s, pattern);
            foreach (Match match in mc)
            {
                GroupCollection gc = match.Groups;
                //string outputText = "URL:" + match.Value + "；Protocol:" + gc["protocol"].Value + "；Address:" + gc["address"].Value;
                //Console.WriteLine(outputText);
            }

        }
    }

    /*
     对于rmu900读写器的操作，主要操作方式为：写入数据，等待返回，返回数据的解析
   * 因此对于这种情况，将操作单位（操作单位是指完成一个功能所必须的一系列操作，
    比如，写入读取标签ID，那就需要首先写入读取标签命令，接收返回的数据并解析，如果
    含有标签，则引发相应事件）作为基础，比如读标签，写标签或者停止读标签等等。
    
     */
}
