using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ledsoft.ls.dll;


namespace Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }





        private void SendScreenPara_Click(object sender, EventArgs e)
        {
            try
            {
                int result;
                
      
                LEDDLL.SetTransMode(3, 3);
                LEDDLL.SetSerialPortPara(1, 1, 115200);
                //string ip = "192.168.2.120";
               //LEDDLL.SetNetworkPara(1, ip.ToCharArray());

                result = LEDDLL.SendScreenPara(1, 1, 64, 32);
               
                string temp;
                switch (result)
                {
                    case 1: temp = "发送成功"; break;
                    case 2: temp = "通讯失败"; break;
                    case 3: temp = "发送过程中出错"; break;
                    default: temp = ""; break;
                }
                MessageBox.Show(temp);

            }
            catch (Exception ex)
            {


            }

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            LEDDLL.StartSend();  //初始化数据结构

            LEDDLL.AddControl(1, 1);        //参数依次为：屏号，单双色
            LEDDLL.AddProgram(1, 1, 0);     //参数依次为：屏号，节目号，节目播放时间
            LEDDLL.AddLnTxtString(1, 1, 1, 0, 0, 128, 32, "2011意义", "宋体", 12, 255, false, false, false, 32, 10, 0);

            LEDDLL.SendControl(1, 1, IntPtr.Zero);
        }


     

        private void button2_Click(object sender, EventArgs e)
        {
            
            LEDDLL.StartSend();  //初始化数据结构

           LEDDLL.AddControl(1, 1);        //参数依次为：屏号，单双色
            LEDDLL.AddProgram(1, 1, 0);     //参数依次为：屏号，节目号，节目播放时间

            LEDDLL.AddFileArea(1, 1, 1, 0, 0, 64, 32);

         

            LEDDLL.AddFileString(1, 1, 1, 1, "2011意义1号屏", "宋体", 12, 255, false, false, false,1, 64, 32, 32, 255, 10, 2, 1);
  
               
            LEDDLL.SendControl(1, 1, IntPtr.Zero);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LEDDLL.StartSend();  //初始化数据结构

            LEDDLL.AddControl(2, 1);        //参数依次为：屏号，单双色
            LEDDLL.AddProgram(2, 1, 0);     //参数依次为：屏号，节目号，节目播放时间

            LEDDLL.AddFileArea(2, 1, 1, 0, 0, 64, 32);



            LEDDLL.AddFileString(2, 1, 1, 1, "2011意义2号屏", "宋体", 12, 255, false, false, false, 1, 64, 32, 32, 255, 100, 2, 1);


            LEDDLL.SendControl(2, 1, IntPtr.Zero);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int result;


                LEDDLL.SetTransMode(3, 3);
                LEDDLL.SetSerialPortPara(2, 1, 115200);
                //string ip = "192.168.2.120";
                //LEDDLL.SetNetworkPara(1, ip.ToCharArray());

                result = LEDDLL.SendScreenPara(2 , 1, 64, 32);

                string temp;
                switch (result)
                {
                    case 1: temp = "发送成功"; break;
                    case 2: temp = "通讯失败"; break;
                    case 3: temp = "发送过程中出错"; break;
                    default: temp = ""; break;
                }
                MessageBox.Show(temp);

            }
            catch (Exception ex)
            {


            }
        }

      
   
     
     
    
    }


}
