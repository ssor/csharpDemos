using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using InventoryMSystem;

namespace Server
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
    public partial class SGSserverForm : Form
    {
        Timer _timer;
        //The ClientInfo structure holds the required information about every
        //client connected to the server
        struct ClientInfo
        {
            public EndPoint endpoint;   //Socket of the client
            public string strName;      //Name by which the user logged into the chat room
        }

        //The collection of all clients logged into the room (an array of type ClientInfo)
        ArrayList clientList;

        //The main socket on which the server listens to the clients
        Socket serverSocket;

        byte[] byteData = new byte[1024];

        TDJ_RFIDHelper helper = new TDJ_RFIDHelper();

        public SGSserverForm()
        {
            clientList = new ArrayList();
            _timer = new Timer();
            _timer.Interval = 500;
            _timer.Tick += new EventHandler(_timer_Tick);
            InitializeComponent();

            this.Shown += new EventHandler(SGSserverForm_Shown);
            this.FormClosing += new FormClosingEventHandler(SGSserverForm_FormClosing);

        }

        void SGSserverForm_Shown(object sender, EventArgs e)
        {
            _timer.Enabled = true;
        }

        void SGSserverForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _timer.Tick -= _timer_Tick;
            _timer.Enabled = false;
        }

        void _timer_Tick(object sender, EventArgs e)
        {

            UDPServer.Manualstate.WaitOne();
            UDPServer.Manualstate.Reset();
            string str = UDPServer.sbuilder.ToString();
            UDPServer.sbuilder.Remove(0, str.Length);
            helper.ParseDataToTag(str);
            if (str != null && str.Length > 0)
            {
                this.txtLog.Text = this.txtLog.Text  + str;
                //this.txtLog.Text = str + "\r\n" + this.txtLog.Text;
                Debug.WriteLine(
                    string.Format(".  _timer_Tick -> string = {0}"
                    , str));
            }
            UDPServer.Manualstate.Set();



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //UDPServer.startUDPListening();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SGSServerUDP",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                IPEndPoint ipeSender = new IPEndPoint(IPAddress.Any, 0);
                EndPoint epSender = (EndPoint)ipeSender;

                serverSocket.EndReceiveFrom(ar, ref epSender);

                string strReceived = Encoding.UTF8.GetString(byteData);
                Array.Clear(byteData, 0, byteData.Length);
                int i = strReceived.IndexOf("\0");

                this.txtLog.Text += strReceived.Substring(0, i) + "\r\n";
                //Start listening to the message send by the user
                serverSocket.BeginReceiveFrom(byteData, 0, byteData.Length, SocketFlags.None, ref epSender,
                    new AsyncCallback(OnReceive), epSender);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SGSServerUDP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OnSend(IAsyncResult ar)
        {
            try
            {
                serverSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SGSServerUDP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.txtLog.Text = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.button2.Enabled = false;
            staticClass.strServerIP = this.textBox2.Text;
            staticClass.iServePort = int.Parse(this.textBox1.Text);
            UDPServer.startUDPListening();

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