using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using Config;
using System.IO.Ports;

namespace SGSclient
{
    //The commands for interaction between the server and the client
    enum Command
    {
        Login,      //Log into the server
        Logout,     //Logout of the server
        Message,    //Send a text message to all the chat clients
        List,       //Get a list of users in the chat room from the server
        Null        //No command
    }

    public partial class SGSClient : Form
    {
        public Socket clientSocket = null; //The main client socket
        public SerialPort comport = null;
        enumSendDataType sendDataType = enumSendDataType.UDP;
        public string strName;      //Name by which the user logs into the room
        public EndPoint epServer;   //The EndPoint of the server
        public string data2Send = string.Empty;
        int maxLength = 0;
        int currentLength = 0;
        Timer _timer = null;
        int timerInterval = 5000;
        long dataPackageIndex = 1;

        byte[] byteData = new byte[1024];

        public SGSClient()
        {
            InitializeComponent();
            _timer = new Timer();
            _timer.Interval = this.timerInterval;
            _timer.Tick += new EventHandler(_timer_Tick);


            this.comboBox1.Items.Clear();
            IList<SavedData> list = SavedData.getAllSavedData();
            if (list != null && list.Count > 0)
            {
                foreach (SavedData sd in list)
                {
                    this.comboBox1.Items.Add(sd.name);
                }
            }
            this.comboBox1.SelectedIndex = -1;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            string msgToSend = string.Empty;

            Random r = new Random();
            int maxValue = this.maxLength;
            if (maxValue > 1024)
            {
                maxValue = 1024;
            }
            int randomInt = r.Next(0, maxValue);
            int startIndex = this.currentLength;

            if (this.currentLength + randomInt <= this.maxLength)
            {

                msgToSend = this.data2Send.Substring(startIndex, randomInt);
                this.currentLength = this.currentLength + randomInt;
            }
            else
            {
                int leftLength = randomInt - (this.maxLength - this.currentLength);
                msgToSend = this.data2Send.Substring(startIndex) + this.data2Send.Substring(0, leftLength);
                this.currentLength = leftLength;

            }
            if (msgToSend.Length > 0)
            {

                this.txtlog.Text += this.dataPackageIndex.ToString() + "    ******************************" + DateTime.Now.ToShortTimeString() + "\r\n" + " " + msgToSend + "\r\n";
                //                this.txtlog.Text = this.dataPackageIndex.ToString() + "    ******************************" + DateTime.Now.ToShortTimeString() + "\r\n" + " " + msgToSend + "\r\n" + this.txtlog.Text;
                this.dataPackageIndex++;
                if (this.sendDataType == enumSendDataType.UDP)
                {
                    //Send it to the server
                    byte[] byteData = Encoding.UTF8.GetBytes(msgToSend);
                    clientSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, epServer, new AsyncCallback(OnSend), null);
                }
                else
                {
                    this.comport.Write(msgToSend);
                }
            }

        }
        private void OnSend(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SGSclient: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Broadcast the message typed by the user to everyone
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSend.Text == "开始")
                {
                    data2Send = txtMessage.Text;
                    this.maxLength = data2Send.Length;
                    this.currentLength = 0;
                    btnSend.Text = "停止";
                    this.button2.Enabled = false;

                    //根据配置确定udp还是串口
                    appConfig appc = (appConfig)genericConfig.getDefaultConfig(new appConfig());
                    if (appc != null)
                    {
                        this._timer.Interval = int.Parse(appc.sendDataInterval);
                        this.sendDataType = appc.sendDataType;
                        if (appc.sendDataType == enumSendDataType.UDP)
                        {
                            //打开UDP连接
                            if (this.clientSocket == null)
                            {
                                UDPConfig config = (UDPConfig)genericConfig.getDefaultConfig(new UDPConfig());
                                if (config != null)
                                {
                                    //IP address of the server machine
                                    IPAddress ipAddress = IPAddress.Parse(config.ip);
                                    int port = int.Parse(config.port);
                                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
                                    epServer = (EndPoint)ipEndPoint;
                                }
                                //Using UDP sockets
                                clientSocket = new Socket(AddressFamily.InterNetwork,
                                    SocketType.Dgram, ProtocolType.Udp);
                            }
                        }
                        else
                            if (appc.sendDataType == enumSendDataType.SerialPort)
                            {
                                //打开串口
                                if (this.comport == null)
                                {
                                    serialPortConfig config = (serialPortConfig)genericConfig.getDefaultConfig(new serialPortConfig());
                                    if (config != null)
                                    {
                                        this.comport = new SerialPort();
                                        comport.PortName = config.portName;
                                        comport.BaudRate = int.Parse(config.baudRate);
                                        comport.DataBits = int.Parse(config.dataBits);
                                        comport.StopBits = (StopBits)Enum.Parse(typeof(StopBits), config.stopBits);
                                        comport.Parity = (Parity)Enum.Parse(typeof(Parity), config.parity);

                                        comport.Open();
                                    }
                                }
                            }
                    }

                    this._timer.Enabled = true;
                }
                else
                {
                    this._timer.Enabled = false;
                    this.button2.Enabled = true;

                    btnSend.Text = "开始";
                }


                //string msgToSend = txtMessage.Text;
                //byte[] byteData = Encoding.UTF8.GetBytes(msgToSend);
                ////Send it to the server
                //clientSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, epServer, new AsyncCallback(OnSend), null);

                //txtMessage.Text = null;
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to send message to the server.", "SGSclientUDP: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndReceive(ar);

                //Convert the bytes received into an object of type Data
                Data msgReceived = new Data(byteData);


                if (msgReceived.strMessage != null && msgReceived.cmdCommand != Command.List)
                    txtlog.Text += msgReceived.strMessage + "\r\n";

                byteData = new byte[1024];

                //Start listening to receive more data from the user
                clientSocket.BeginReceiveFrom(byteData, 0, byteData.Length, SocketFlags.None, ref epServer,
                                           new AsyncCallback(OnReceive), null);
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SGSclient: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            if (txtMessage.Text.Length == 0)
                btnSend.Enabled = false;
            else
                btnSend.Enabled = true;
        }

        private void SGSClient_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                if (clientSocket != null)
                {
                    clientSocket.Close();
                }
                if (comport != null)
                {
                    comport.Close();
                }
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SGSclient: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend_Click(sender, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.txtlog.Text = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmConfig frm = new frmConfig();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string name = string.Empty;
            if (this.comboBox1.Text == null || this.comboBox1.Text.Length <= 0)
            {
                if (this.txtMessage.Text != null && this.txtMessage.Text.Length > 0)
                {
                    name = this.txtMessage.Text.Substring(0, 10);
                }
                else
                {
                    name = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                }
            }
            SavedData sd = new SavedData(name, this.txtMessage.Text);
            SavedData.saveData(sd);
            MessageBox.Show("保存完成", "提示");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex != -1)
            {
                string name = this.comboBox1.Text;
                SavedData sd = new SavedData(name, null);
                SavedData sdR = SavedData.getSaveData(sd);
                if (sdR != null)
                {
                    this.txtMessage.Text = sdR.message;
                }
            }
        }
    }

    //The data structure by which the server and the client interact with 
    //each other
    class Data
    {
        //Default constructor
        public Data()
        {
            this.cmdCommand = Command.Null;
            this.strMessage = null;
            this.strName = null;
        }

        //Converts the bytes into an object of type Data
        public Data(byte[] data)
        {
            //The first four bytes are for the Command
            this.cmdCommand = (Command)BitConverter.ToInt32(data, 0);

            //The next four store the length of the name
            int nameLen = BitConverter.ToInt32(data, 4);

            //The next four store the length of the message
            int msgLen = BitConverter.ToInt32(data, 8);

            //This check makes sure that strName has been passed in the array of bytes
            if (nameLen > 0)
                this.strName = Encoding.UTF8.GetString(data, 12, nameLen);
            else
                this.strName = null;

            //This checks for a null message field
            if (msgLen > 0)
                this.strMessage = Encoding.UTF8.GetString(data, 12 + nameLen, msgLen);
            else
                this.strMessage = null;
        }

        //Converts the Data structure into an array of bytes
        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();

            //First four are for the Command
            result.AddRange(BitConverter.GetBytes((int)cmdCommand));

            //Add the length of the name
            if (strName != null)
                result.AddRange(BitConverter.GetBytes(strName.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));

            //Length of the message
            if (strMessage != null)
                result.AddRange(BitConverter.GetBytes(strMessage.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));

            //Add the name
            if (strName != null)
                result.AddRange(Encoding.UTF8.GetBytes(strName));

            //And, lastly we add the message text to our array of bytes
            if (strMessage != null)
                result.AddRange(Encoding.UTF8.GetBytes(strMessage));

            return result.ToArray();
        }

        public string strName;      //Name by which the client logs into the room
        public string strMessage;   //Message text
        public Command cmdCommand;  //Command type (login, logout, send message, etcetera)
    }
}