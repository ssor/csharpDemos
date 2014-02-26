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
using System.Web;
using System.IO;
using System.Drawing.Imaging;

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

            //string value = "郭辉翰";
            //string value = "english";
            string value = "english郭辉翰中";
            MultiFormatWriter mutiWriter = new MultiFormatWriter();
            //BarcodeFormat format = BarcodeFormat.AZTEC;
            //BarcodeFormat format = BarcodeFormat.PDF_417;
            //BarcodeFormat format = BarcodeFormat.DATA_MATRIX;
            BarcodeFormat format = BarcodeFormat.QR_CODE;
            string encodedString = value;
            if (format == BarcodeFormat.PDF_417 || format == BarcodeFormat.AZTEC)
            {
                encodedString = HttpUtility.UrlEncode(value, Encoding.UTF8);
            }
            BitMatrix bm = mutiWriter.encode(encodedString, format, 150, 150);
            //BitMatrix bm = mutiWriter.encode("郭辉翰中", format, 150, 150);
            BarcodeWriter bw = new BarcodeWriter { Format = format };
            Bitmap img = bw.Write(bm);
            int borderWidth = 2;
            Color borderColor = Color.White;
            int width = img.Width + 2 * borderWidth;
            int height = img.Height + 2 * borderWidth;
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(img, borderWidth, borderWidth);
            g.DrawRectangle(new Pen(borderColor, borderWidth), 0, 0, width, height);

            g.Dispose();
            pictureBox1.Image = bmp;

            //pictureBox1.Image = img;
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
                //DmtxImageDecoder decoder = new DmtxImageDecoder();
                //var decodedData = decoder.DecodeImage(bmap, 1, new TimeSpan(0, 0, 3));
                //if (decodedData.Count != 1)
                //    throw new Exception("Encoding or decoding failed!");
                Console.WriteLine("解码失败");
            }
            else
            {
                string value = result.Text;
                if (result.BarcodeFormat == BarcodeFormat.PDF_417 || result.BarcodeFormat == BarcodeFormat.AZTEC)
                {
                    value = HttpUtility.UrlDecode(result.Text);
                }
                Console.WriteLine("decode result => format: " + result.BarcodeFormat + "   value: " + value);
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

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog odf = new OpenFileDialog();
            if (odf.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Image img = Image.FromFile(odf.FileName);
            this.pictureBox1.Image = img;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = @"PNG (*.png)|*.png|Bitmap (*.bmp)|*.bmp";
            saveFileDialog.FileName = Path.GetFileName("temp");
            saveFileDialog.DefaultExt = "png";

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Bitmap bitmap = (Bitmap)this.pictureBox1.Image;
            try
            {
                bitmap.Save(
                    saveFileDialog.FileName,
                    saveFileDialog.FileName.EndsWith("png")
                        ? ImageFormat.Png
                        : ImageFormat.Bmp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
