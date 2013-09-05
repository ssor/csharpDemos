using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.google.zxing;
using com.google.zxing.common;

namespace zxingDemo
{
    public partial class frm2DBarcodeDecode : Form
    {
        public frm2DBarcodeDecode()
        {
            InitializeComponent();
            this.lblFormat.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog odf = new OpenFileDialog();
            if (odf.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Image img = Image.FromFile(odf.FileName);
            this.pictureBox1.Image = img;

        }

        private void button3_Click(object sender, EventArgs e)
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
            LuminanceSource source = new RGBLuminanceSource(bmap, bmap.Width, bmap.Height);
            com.google.zxing.BinaryBitmap bitmap = new com.google.zxing.BinaryBitmap(new HybridBinarizer(source));
            Result result;
            try
            {
                result = new MultiFormatReader().decode(bitmap);
            }
            catch (ReaderException re)
            {
                MessageBox.Show(re.ToString());
                return;
            }
            this.textBox1.Text = result.Text;
            if (result.BarcodeFormat == BarcodeFormat.DATAMATRIX)
            {
                this.lblFormat.Text = "Datamatrix";
            }
            else if (result.BarcodeFormat == BarcodeFormat.PDF417)
            {
                this.lblFormat.Text = "PDF417";
            }
            else if (result.BarcodeFormat == BarcodeFormat.QR_CODE)
            {
                this.lblFormat.Text = "QR Code";
            }
            else
            {

                this.lblFormat.Text = "未知格式";
            }

        }
    }
}
