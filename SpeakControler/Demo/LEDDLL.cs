using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Configuration;

namespace Ledsoft.ls.dll
{
    /// <summary>
    /// 灵信LED显示动态链接库声明
    /// </summary>
    class LEDDLL
    {

        //设置通讯方式
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetTransMode")]
        public static extern int SetTransMode(int TransMode, int ConType);

        //设置网络通讯参数
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetNetworkPara")]
        public static extern int SetNetworkPara(int pno, char[] ip);

        //设置串口通讯参数
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetSerialPortPara")]
        public static extern int SetSerialPortPara(int pno, int port, int rate);


        //屏参设置
        [DllImport("ListenPlayDll.dll", EntryPoint = "SendScreenPara")]
        public static extern int SendScreenPara(int pno, int DBColor, int width, int height);

        //开关屏
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetPower")]
        public static extern int SetPower(int pno, int power);

        //设置控制器定时开关屏
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetPowerTimer")]
        public static extern int SetPowerTimer(int pno, int flag, int startHour1, int startMinute1, int endHour1, int endMinute1,
                                                int startHour2, int startMinute2, int endHour2, int endMinute2,
                                                int startHour3, int startMinute3, int endHour3, int endMinute3);

        //校时
        [DllImport("ListenPlayDll.dll", EntryPoint = "AdjustTime")]
        public static extern int AdjustTime(int pno);

        //设置控制器网络参数
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetRemoteNetwork")]
        public static extern int SetRemoteNetwork(int pno, string macAddress, string ip, string gateway, string subnetmask);


        //调节亮度
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetBrightness")]
        public static extern int SetBrightness(int pno, int value);

        //设置控制器定时亮度调节
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetBrightnessTimer")]
        public static extern int SetBrightnessTimer(int pno, int flag,
                                         int startHour1, int startMinute1, int endHour1, int endMinute1, int brightness1,
                                         int startHour2, int startMinute2, int endHour2, int endMinute2, int brightness2,
                                         int startHour3, int startMinute3, int endHour3, int endMinute3, int brightness3);

        //时间限制
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetTimingLimit")]
        public static extern int SetTimingLimit(int pno, int FSecond, int FMinute, int FHour, int FDay, int FMonth, int FWeek, int FYear);
        //取消限制
        [DllImport("ListenPlayDll.dll", EntryPoint = "CancelTimingLimit")]
        public static extern int CancelTimingLimit(int pno);


        //查询//

        //网络搜索控制器
        [DllImport("ListenPlayDll.dll", EntryPoint = "SearchController")]
        public static extern int SearchController(string filePath);
        //串口搜素
        [DllImport("ListenPlayDll.dll", EntryPoint = "ComSearchController")]
        public static extern int ComSearchController(int PNO, int ComNo, int BaudRate, string filePath);


        ////////
        //内容//

        //初始化数据结构
        [DllImport("ListenPlayDll.dll", EntryPoint = "StartSend")]
        public static extern int StartSend();

        //添加控制器资源
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddControl")]
        public static extern int AddControl(int pno, int DBColor);

        //添加节目
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddProgram")]
        public static extern int AddProgram(int pno, int jno, int playTime);

        //设置节目定时播放属性
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetProgramTimer")]
        public static extern int SetProgramTimer(int pno, int jno, int TimingModel, int WeekSelect,
                                                 int startSecond, int startMinute, int startHour, int startDay, int startMonth, int startWeek, int startYear,
                                                 int endSecond, int endMinute, int endHour, int endDay, int endMonth, int endWeek, int endYear);

        //转换rgb值为颜色值
        //int _stdcall GetRGB( int r, int g, int b);

        //添加单行文本区域
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddLnTxtArea")]
        public static extern int AddLnTxtArea(int pno, int jno, int qno, int left, int top, int width, int height, string LnFileName, int PlayStyle, int Playspeed, int times);

        //添加静止文本区域
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddQuitText")]
        public static extern int AddQuitText(int pno, int jno, int qno, int left, int top, int width, int height, int FontColor, string fontName, int fontSize, int fontBold, int Italic, int Underline, string text);

        //添加图文区域
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddFileArea")]
        public static extern int AddFileArea(int pno, int jno, int qno, int left, int top, int width, int height);

        //添加文件
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddFile")]
        public static extern int AddFile(int pno, int jno, int qno, int mno, string fileName, int width, int height, int playstyle, int QuitStyle, int playspeed, int delay, int MidText);

        //添加动画区域
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddFlashArea")]
        public static extern int AddFlashArea(int pno, int jno, int qno, int left, int top, int width, int height);

        //添加动画帧图片
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddFlashBmpFile")]
        public static extern int AddFlashBmpFile(int pno, int jno, int qno, int mno, string fileName, int width, int height, int playspeed);

        //添加数字时钟区域
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddDClockArea")]
        public static extern int AddDClockArea(int pno, int jno, int qno, int left, int top, int width, int height,
                                               int fontColor, string fontName, int fontSize, int fontBold, int Italic, int Underline,
                                               int year, int week, int month, int day, int hour, int minute, int second, int TwoOrFourYear,
                                               int HourShow, int format, int spanMode, int Advacehour, int Advaceminute);


        //添加模拟时钟区域
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddModelClock")]
        public static extern int AddModelClock(int pno, int jno, int qno, int left, int top, int width, int height, int BeforOrDelay,
                                               int TimeHour, int TimeMin, int HourColor, int MinColor, int SecColor);

        //添加计时区域
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddTimerArea")]
        public static extern int AddTimerArea(int pno, int jno, int qno, int left, int top, int width, int height,
                                              int fontColor, string fontName, int fontSize, int fontBold, int Italic, int Underline,
                                              int mode, int DayShow, int CulWeek, int CulDay, int CulHour, int CulMin, int CulSec,
                                              int year, int week, int month, int day, int hour, int minute, int second);


        //添加温度区域
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddTemperature")]
        public static extern int AddTemperature(int pno, int jno, int qno, int left, int top, int width, int height,
                                                int fontColor, string fontName, int fontSize, int fontBold, int Italic, int Underline,
                                                int Temp_Mode, int Temp_digit);

        //添加噪声区
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddNoise")]
        public static extern int AddNoise(int pno, int jno, int qno, int left, int top, int width, int height,
                                          int fontColor, string fontName, int fontSize, int fontBold, int Italic, int Underline,
                                          int Noise_digit, int NAdjust);


        //发送内容
        [DllImport("ListenPlayDll.dll", EntryPoint = "SendControl")]
        public static extern int SendControl(int PNO, int SendType, IntPtr hwnd);



        //////////////////
        //户外配置与查询//
        //////////////////

        //设置硬件参数
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetHardPara")]
        public static extern int SetHardPara(int PNO, int Sign, int Mirror, int ScanStyle, int LineOrder, int cls, int RGChange, int zhangKong);
        //查询硬件参数
        [DllImport("ListenPlayDll.dll", EntryPoint = "GetHardPara")]
        public static extern int GetHardPara(int PNO, string FilePath);


        //显示测试
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetTest")]
        public static extern int SetTest(int pno, int value);
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddFileString")]
        public static extern int AddFileString(int pno, int jno, int qno, int mno, string str, string fontname, int fontsize, int fontcolor, bool bold, bool italic, bool underline,int align, int width, int height, int playstyle, int QuitStyle, int playspeed, int delay, int MidText);

        [DllImport("ListenPlayDll.dll", EntryPoint = "AddLnTxtString")]
        public static extern int AddLnTxtString(int pno, int jno, int qno, int left, int top, int width, int height,
                string text,
                string fontname,
                int fontsize,
                int fontcolor,
                bool bold,
                bool italic,
                bool underline,
                int PlayStyle,
                int Playspeed,
                int times);



    }
}





