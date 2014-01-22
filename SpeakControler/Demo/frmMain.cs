using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using httpHelper;
using rfidCheck;
using System.Diagnostics;
using System.Speech.Synthesis;
using Ledsoft.ls.dll;

namespace Demo
{
    public partial class frmMain : Form
    {
        SpeechSynthesizer _synth = new SpeechSynthesizer();
        List<string> listToSpeak = new List<string>();//存放待播放的文本
        public frmMain()
        {
            InitializeComponent();

            __timer = new Timer();
            __timer.Interval = 300;
            __timer.Tick += new EventHandler(__timer_Tick_getCommand);


            __httpHelper.RequestCompleted += new deleGetRequestObject(__httpHelper_RequestCompleted_getCommand);

            _synth.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(_synth_SpeakCompleted);
            _synth.SelectVoice("Microsoft Lili");
            //_synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult, 1, new System.Globalization.CultureInfo("zh-CHS"));
            _synth.Rate = -2;
        }

        void _synth_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            if (listToSpeak.Count > 0 && _synth.State != SynthesizerState.Speaking)
            {
                string str = listToSpeak[0];
                listToSpeak.RemoveAt(0);
                _synth.SpeakAsync(str);
            }
        }

        bool bRunning = false;//是否正在运行
        Timer __timer = null;//控制器运作的引擎
        string lastUpdateTimeStamp = string.Empty;
        HttpWebConnect __httpHelper = new HttpWebConnect();
        void __timer_Tick_getCommand(object sender, EventArgs e)
        {
            string url = sysConfig.getRestUrl();
            scanTagPara para = new scanTagPara("inventory", this.lastUpdateTimeStamp);
            //LedInfo li = new LedInfo(string.Empty, this.lastUpdateTimeStamp, string.Empty);
            string jsonString = fastJSON.JSON.Instance.ToJSON(para);
            HttpWebConnect httpHelper = new HttpWebConnect();
            httpHelper.RequestCompleted += new deleGetRequestObject(__httpHelper_RequestCompleted_getCommand);
            httpHelper.TryPostData(url, jsonString);
        }
        //通过网络读取到命令后，对命令进行解析
        void __httpHelper_RequestCompleted_getCommand(object o)
        {

            string strTags = (string)o;
            object olist = fastJSON.JSON.Instance.ToObjectList(strTags, typeof(List<tagID>), typeof(tagID));
            foreach (tagID c in (List<tagID>)olist)
            {
                if (string.Compare(this.lastUpdateTimeStamp, c.startTime, true) < 0)//如果命令的时间较晚
                {
                    this.lastUpdateTimeStamp = c.startTime;
                }
                Debug.WriteLine(c.tag + "      " + c.startTime);
            }

            /*
            deleControlInvoke dele = delegate(object oc)
            {
                string cmds = (string)oc;
                Debug.WriteLine(
                    string.Format("frmMain.__httpHelper_RequestCompleted_getCommand  ->  = {0}"
                    , cmds));
                int index = cmds.IndexOf("[");
                if (index >= 0)
                {
                    cmds = cmds.Substring(index);
                }
                else
                {
                    return;
                }
                //this.appendLog(cmds);
                // return;
                object olist = fastJSON.JSON.Instance.ToObjectList(cmds, typeof(List<CommandInfo>), typeof(CommandInfo));
                foreach (CommandInfo c in (List<CommandInfo>)olist)
                {
                    if (string.Compare(this.lastUpdateTimeStamp, c.startTime, true) < 0)//如果命令的时间较晚
                    {
                        this.lastUpdateTimeStamp = c.startTime;
                    }
                    switch (c.commandName)
                    {
                        case "tts":
                            Debug.WriteLine(
                                            string.Format("frmMain.__httpHelper_RequestCompleted_getCommand  -> tts = {0}"
                                                            , c.info));

                            this.appendLog(string.Format("获取到tts发布信息，内容为 {0} {1}", c.info,c.startTime));
                            this.listToSpeak.Add(c.info);
                            //_synth.SpeakAsync(c.info);
                            _synth_SpeakCompleted(null, null);
                            break;
                        case "led":
                            //发现新的led信息，需要发送到相应的led屏上
                            Debug.WriteLine(
                                string.Format("frmMain.__httpHelper_RequestCompleted_getCommand  -> led = {0}    {1}"
                                , c.ledIP, c.info));
                            this.appendLog(string.Format("获取到led发布信息，ip为 {0}   内容为 {1}  {2}", c.ledIP, c.info,c.startTime));
                            try
                            {
                                this.sendMsgToLED(c.ledIP, c.info);
                            }
                            catch (System.Exception ex)
                            {
                                //appendLog("发送到LED屏时发生异常：" + ex.Message);
                            }
                            break;

                    }

                }
                #region old code
                //object olist = fastJSON.JSON.Instance.ToObjectList(cmds, typeof(List<LedInfo>), typeof(LedInfo));
                //foreach (LedInfo c in (List<LedInfo>)olist)
                //{
                //    if (string.Compare(this.lastUpdateTimeStamp, c.startTime, true) < 0)//如果命令的时间较晚
                //    {
                //        this.lastUpdateTimeStamp = c.startTime;
                //    }
                //    //发现新的led信息，需要发送到相应的led屏上
                //    Debug.WriteLine(
                //        string.Format("frmMain.__httpHelper_RequestCompleted_getCommand  ->  = {0}    {1}"
                //        , c.ledIP, c.info));
                //    this.appendLog(string.Format("获取到led发布信息，ip为 {0}   内容为 {1}", c.ledIP, c.info));

                //    this.sendMsgToLED(c.ledIP, c.info);
                //}
                #endregion

            };
            this.Invoke(dele, o);
             * */
        }

        void sendMsgToLED(string IP, string Text)
        {
            try
            {

                Debug.WriteLine(
                    string.Format("frmMain.sendMsgToLED  -> ip = {0}   msg = {1}"
                    , IP, Text));
                int result;


                //LEDDLL.SetTransMode(3, 3);"192.168.1.99"
                //LEDDLL.SetSerialPortPara(2, 1, 115200);
                string ip = IP;
                LEDDLL.SetNetworkPara(1, ip.ToCharArray());

                //   result = LEDDLL.SendScreenPara(2, 1, 64, 32);
                //result = LEDDLL.SendScreenPara(2, 1, 96, 16);
                LEDDLL.StartSend();  //初始化数据结构

                LEDDLL.AddControl(1, 1);        //参数依次为：屏号，单双色
                LEDDLL.AddProgram(1, 1, 0);     //参数依次为：屏号，节目号，节目播放时间

                LEDDLL.AddFileArea(1, 1, 1, 0, 0, 96, 16);
                LEDDLL.AddFileString(1, 1, 1, 1, Text, "宋体", 12, 255, false, false, false, 1, 64, 32, 32, 255, 100, 2, 1);
                LEDDLL.SendControl(1, 1, IntPtr.Zero);
                //string temp;
                //switch (result)
                //{
                //    case 1: temp = "发送成功"; break;
                //    case 2: temp = "通讯失败"; break;
                //    case 3: temp = "发送过程中出错"; break;
                //    default: temp = ""; break;
                //}
                //    MessageBox.Show(temp);

            }
            catch (Exception ex)
            {
                //appendLog("发送到LED屏时发生异常：" + ex.Message);

            }

        }

        void appendLog(string log)
        {
            if (this.txtLog.Text != null && this.txtLog.Text.Length > 4096)
            {
                this.txtLog.Text = string.Empty;
            }
            this.txtLog.Text = log + "\r\n" + this.txtLog.Text;// +" " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //启动控制器
            //启动以后，控制器会不断的访问网络以获取最新的命令
            if (this.bRunning)
            {
                this.__timer.Enabled = false;
                this.btnStart.Text = "运行";
                this.bRunning = false;
                this.matrixCircularProgressControl1.Stop();
            }
            else
            {
                this.bRunning = true;
                this.btnStart.Text = "停止";
                //重置状态
                this.lastUpdateTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                this.__timer.Enabled = true;
                this.matrixCircularProgressControl1.Start();
            }
        }

        private void 设置TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSysSetting frm = new frmSysSetting();
            frm.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.sendMsgToLED("192.168.0.98", "98aaaaaaaaaaaaaaaaaa");
        }
    }
    public class scanTagPara
    {
        public string cmd = string.Empty;
        public string startTime = string.Empty;
        public scanTagPara(string _cmd, string _startTime)
        {
            this.cmd = _cmd;
            this.startTime = _startTime;
        }
    }
    public class tagID
    {
        public string tag = string.Empty;
        public string startTime = string.Empty;
        public string cmd = string.Empty;
        public string state;
        public tagID() { }
        //public tagID(string _tag, string _time)
        public tagID(string _tag, string _time, string _cmd)
        {
            this.tag = _tag;
            this.startTime = _time;
            this.cmd = _cmd;
            this.state = string.Empty;
        }
    }
}
