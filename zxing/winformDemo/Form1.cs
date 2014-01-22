using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataMatrix.net;
using ZXing;
using ZXing.Common;

namespace winformDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            MultiFormatWriter mutiWriter = new MultiFormatWriter();
            BitMatrix bm = mutiWriter.encode("123", BarcodeFormat.PDF_417, 150, 150);
            BarcodeWriter bw = new BarcodeWriter { Format = BarcodeFormat.PDF_417 };
            pictureBox1.Image = bw.Write(bm);
            //Bitmap img = bm.ToBitmap(BarcodeFormat.PDF_417, "123");
            //pictureBox1.Image = img;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.pictureBox1.Image == null)
            {
                return;
            }
            Image img = this.pictureBox1.Image;
            Bitmap bmap;
            try
            {
                bmap = new Bitmap(img);
            }
            catch (System.IO.IOException ioe)
            {
                MessageBox.Show(ioe.ToString());
                return;
            }
            if (bmap == null)
            {
                MessageBox.Show("无法解析该图像！");
                return;
            }

            LuminanceSource source = new BitmapLuminanceSource(bmap);
            BinaryBitmap bitmap = new BinaryBitmap(new HybridBinarizer(source));
            Result result;
            try
            {
                result = new MultiFormatReader().decode(bitmap);
            }
            catch (ReaderException re)
            {
                MessageBox.Show("解码出现异常，可能不支持此编码！", "提示");
                //MessageBox.Show(re.ToString());
                return;
            }
            if (result == null)
            {
                DmtxImageDecoder decoder = new DmtxImageDecoder();
                var decodedData = decoder.DecodeImage(bmap, 1, new TimeSpan(0, 0, 3));
                if (decodedData.Count != 1)
                    throw new Exception("Encoding or decoding failed!");
            }
            else
            {

                Console.WriteLine("decode result => format: " + result.BarcodeFormat + "   value: " + result.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DmtxImageEncoder encoder = new DmtxImageEncoder();
            DmtxImageEncoderOptions options = new DmtxImageEncoderOptions();
            options.ModuleSize = 8;
            options.MarginSize = 4;
            options.BackColor = Color.White;
            options.ForeColor = Color.Black;
            Bitmap encodedBitmap = encoder.EncodeImage("1231234567890-lkjhgfds");
            pictureBox1.Image = encodedBitmap;
            return;
        }
    }
}
