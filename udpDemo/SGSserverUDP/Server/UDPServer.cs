using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Threading;

namespace Server
{
    //public delegate void invokeCtlString(string str);
    
    public class UDPServer
    {
        public static ManualResetEvent Manualstate = new ManualResetEvent(true);
        public static StringBuilder sbuilder = new StringBuilder();
        public static Socket serverSocket;
        static byte[] byteData = new byte[1024];
        public static void startUDPListening()
        {
            try
            {


                //We are using UDP sockets
                serverSocket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Dgram, ProtocolType.Udp);
                IPAddress ip = IPAddress.Parse(InventoryMSystem.staticClass.strServerIP);
                IPEndPoint ipEndPoint = new IPEndPoint(ip, InventoryMSystem.staticClass.iServePort);
//                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, port);

                //Bind this address to the server
                serverSocket.Bind(ipEndPoint);
                //防止客户端强行中断造成的异常
                long IOC_IN = 0x80000000;
                long IOC_VENDOR = 0x18000000;
                long SIO_UDP_CONNRESET = IOC_IN | IOC_VENDOR | 12;

                byte[] optionInValue = { Convert.ToByte(false) };
                byte[] optionOutValue = new byte[4];
                serverSocket.IOControl((int)SIO_UDP_CONNRESET, optionInValue, optionOutValue);

                IPEndPoint ipeSender = new IPEndPoint(IPAddress.Any, 0);
                //The epSender identifies the incoming clients
                EndPoint epSender = (EndPoint)ipeSender;

                //Start receiving data
                serverSocket.BeginReceiveFrom(byteData, 0, byteData.Length,
                    SocketFlags.None, ref epSender, new AsyncCallback(OnReceive), epSender);


                //TcpListener server = null;
                //try
                //{


                //    // Set the TcpListener on port 13000.
                //    //IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                //    IPAddress localAddr = IPAddress.Parse("192.168.36.168");

                //    // TcpListener server = new TcpListener(port);
                //    server = new TcpListener(localAddr, port);
                    
                //    // Start listening for client requests.
                //    server.Start();

                //    // Buffer for reading data
                //    Byte[] bytes = new Byte[256];
                //    String data = null;

                //    // Enter the listening loop.
                //    while (true)
                //    {
                //        Console.Write("Waiting for a connection... ");

                //        // Perform a blocking call to accept requests.
                //        // You could also user server.AcceptSocket() here.
                //        TcpClient client = server.AcceptTcpClient();
                //        Console.WriteLine("Connected!");

                //        data = null;

                //        // Get a stream object for reading and writing
                //        NetworkStream stream = client.GetStream();

                //        int i;

                //        // Loop to receive all the data sent by the client.
                //        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                //        {
                //            // Translate data bytes to a ASCII string.
                //            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                //            Debug.WriteLine("Received: {0}", data);

                //            // Process the data sent by the client.
                //            data = data.ToUpper();

                //            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                //            // Send back a response.
                //            stream.Write(msg, 0, msg.Length);
                //            Debug.WriteLine("Sent: {0}", data);
                //        }

                //        // Shutdown and end connection
                //        client.Close();
                //    }
                //}
                //catch (SocketException e)
                //{
                //    Console.WriteLine("SocketException: {0}", e);
                //}
                //finally
                //{
                //    // Stop listening for new clients.
                //    server.Stop();
                //}




                //**********************************************************************

            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    string.Format("UDPServer.startUDPListening  -> error = {0}"
                    , ex.Message));
            }
        }
        public static void OnReceive(IAsyncResult ar)
        {
            try
            {
                IPEndPoint ipeSender = new IPEndPoint(IPAddress.Any, 0);
                EndPoint epSender = (EndPoint)ipeSender;

                serverSocket.EndReceiveFrom(ar, ref epSender);

                string strReceived = Encoding.UTF8.GetString(byteData);
                //////////////////////////////////////////////////////////////////////////
                //针对 reader1000 读写器的解析
                //byte[] bytesEpc=new byte[24];
                //Array.Copy(byteData, 3, bytesEpc, 0, 24);
                //string epc = Encoding.UTF8.GetString(bytesEpc);
                //Debug.WriteLine("epc = " + epc);
                //////////////////////////////////////////////////////////////////////////

                Debug.WriteLine(
                	string.Format("UDPServer.OnReceive  -> received = {0}"
                	, strReceived));

                Array.Clear(byteData, 0, byteData.Length);
                int i = strReceived.IndexOf("\0");
                Manualstate.WaitOne();
                Manualstate.Reset();
                //todo here should deal with the received string
                sbuilder.Append(strReceived.Substring(0, i));
                Manualstate.Set();

                //Start listening to the message send by the user
                serverSocket.BeginReceiveFrom(byteData, 0, byteData.Length, SocketFlags.None, ref epSender,
                    new AsyncCallback(OnReceive), epSender);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    string.Format("UDPServer.OnReceive  -> error = {0}"
                    , ex.Message));
            }
        }
    }
}
