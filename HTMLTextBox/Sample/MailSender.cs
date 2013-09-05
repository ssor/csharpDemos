using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace Sample
{
    public partial class MailSender : Form
    {

        public MailSender()
        {
            InitializeComponent();

            string body = string.Empty;
            string path = "template.html";
            if (File.Exists(path))
            {
                using (FileStream fs = File.OpenRead(path))
                {
                    byte[] b = new byte[1024];
                    UTF8Encoding temp = new UTF8Encoding(true);

                    while (fs.Read(b, 0, b.Length) > 0)
                    {
                        body += temp.GetString(b);
                    }
                }
                //htmlTextBoxBody.Text = "1111";
                //htmlTextBoxBody.reset_document_text(body);
            }
            this.htmlTextBoxBody = new McDull.Windows.Forms.HTMLTextBox();
            // 
            // htmlTextBoxBody
            // 
            this.htmlTextBoxBody.Location = new System.Drawing.Point(14, 78);
            this.htmlTextBoxBody.Name = "htmlTextBoxBody";
            this.htmlTextBoxBody.Size = new System.Drawing.Size(605, 389);
            this.htmlTextBoxBody.TabIndex = 6;
            this.Controls.Add(this.htmlTextBoxBody);

            this.htmlTextBoxBody.Text = body;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            this.save_as_html();

            //string host = "192.168.22.12";
            //int port = 25;
            //string userid = "jay.liu";
            //string password = "1111";

            //MailMessage message = BuildMessage();

            //SmtpClient smtp = new SmtpClient(host, port);
            //smtp.Credentials = new NetworkCredential(userid, password);
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            //try
            //{
            //    smtp.Send(message);

            //    MessageBox.Show("Success!", "Sample", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Failed! \r\n" + ex.Message, "Sample", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }
        void save_as_html()
        {
            string body = htmlTextBoxBody.Text;
            StreamWriter html_file = File.CreateText(string.Format("html_{0:000}.html", 1));
            html_file.Write(body);
            html_file.Close();
        }
        private MailMessage BuildMessage()
        {
            string from = "sample@sample.sample";
            string to = textBoxTo.Text;
            string subproject = textBoxSubject.Text;
            string[] images = htmlTextBoxBody.Images;
            string body = htmlTextBoxBody.Text;

            MailMessage message = new MailMessage();

            message.From = new MailAddress(from);
            message.To.Add(new MailAddress(to));
            message.Subject = subproject;

            //message.IsBodyHtml = true;

            if (images.Length != 0)
            {
                for (int i = 0, count = images.Length; i < count; ++i)
                {
                    string image = images[i];

                    if (image.Trim() == "")

                        if (!image.StartsWith("file"))
                        {
                            continue;
                        }

                    string path = Path.GetFullPath(image.Replace("%20", " ").Replace("file:///", ""));
                    string cid = string.Format("image_{0:00}", i);

                    Attachment attach = new Attachment(path);
                    attach.Name = Path.GetFileNameWithoutExtension(path);
                    attach.ContentId = cid;
                    message.Attachments.Add(attach);

                    body = body.Replace(path, string.Format("cid:{0}", cid));
                }
            }

            message.Body = body;

            return message;
        }
    }
}