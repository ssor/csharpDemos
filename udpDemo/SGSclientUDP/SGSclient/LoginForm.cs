using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace SGSclient
{
    public partial class LoginForm : Form
    {
        public Socket clientSocket;
        public EndPoint epServer;
        public string strName;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //Using UDP sockets
                clientSocket = new Socket(AddressFamily.InterNetwork, 
                    SocketType.Dgram, ProtocolType.Udp);

                //IP address of the server machine
                IPAddress ipAddress = IPAddress.Parse(txtServerIP.Text);
                int port = int.Parse(txtPort.Text);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

                epServer = (EndPoint)ipEndPoint;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message, "SGSclient", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error); 
            } 
        }

        private void OnSend(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);                
                strName = txtPort.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SGSclient", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtPort.Text.Length > 0 && txtServerIP.Text.Length > 0)
                btnOK.Enabled = true;
            else
                btnOK.Enabled = false;
        }

        private void txtServerIP_TextChanged(object sender, EventArgs e)
        {
            if (txtPort.Text.Length > 0 && txtServerIP.Text.Length > 0)
                btnOK.Enabled = true;
            else
                btnOK.Enabled = false;
        }
    }
}