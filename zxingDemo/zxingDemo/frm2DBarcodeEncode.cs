using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.google.zxing.common;
using com.google.zxing;
using System.Drawing.Imaging;
using System.IO;

namespace zxingDemo
{
    public partial class frm2DBarcodeEncode : Form
    {
        public frm2DBarcodeEncode()
        {
            InitializeComponent();

            this.comboBox1.Items.Add("QR Code");
            this.comboBox1.Items.Add("PDF417");
            this.comboBox1.Items.Add("Datamatrix");
            this.comboBox1.SelectedIndex = 0;


        }


        public static void writeToFile(ByteMatrix matrix, System.Drawing.Imaging.ImageFormat format, string file)
        {
            Bitmap bmap = toBitmap(matrix);
            bmap.Save(file, format);
        }

        public static Bitmap toBitmap(ByteMatrix matrix)
        {
            int width = matrix.Width;
            int height = matrix.Height;
            Bitmap bmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bmap.SetPixel(x, y, matrix.get_Renamed(x, y) != -1 ? ColorTranslator.FromHtml("0xFF000000") : ColorTranslator.FromHtml("0xFFFFFFFF"));
                }
            }
            return bmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string content = textBox1.Text;
            if (content == null || content.Length <= 0)
            {
                return;
            }
            int heigth = (int)this.numericHeigth.Value;
            int width = (int)this.numericWidth.Value;
            BarcodeFormat format = BarcodeFormat.QR_CODE;
            switch (this.comboBox1.SelectedIndex)
            {
                case 1:
                    format = BarcodeFormat.PDF417;
                    break;
                case 2:
                    format = BarcodeFormat.DATAMATRIX;
                    break;
            }
            ByteMatrix byteMatrix = new MultiFormatWriter().encode(content, format, width, heigth);
            Bitmap bitmap = toBitmap(byteMatrix);
            pictureBox1.Image = bitmap;
            //writeToFile(byteMatrix, System.Drawing.Imaging.ImageFormat.Png, sFD.FileName);

            //SaveFileDialog sFD = new SaveFileDialog();
            //sFD.DefaultExt = "*.png|*.png";
            //sFD.AddExtension = true;
            //try
            //{
            //    if (sFD.ShowDialog() == DialogResult.OK)
            //    {



            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);

            //}




        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = @"PNG (*.png)|*.png|Bitmap (*.bmp)|*.bmp";
            saveFileDialog.FileName = Path.GetFileName(GetFileNameProposal());
            saveFileDialog.DefaultExt = "png";

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

                Bitmap bitmap = (Bitmap)this.pictureBox1.Image;
                bitmap.Save(
                    saveFileDialog.FileName,
                    saveFileDialog.FileName.EndsWith("png")
                        ? ImageFormat.Png
                        : ImageFormat.Bmp);
        }
        private string GetFileNameProposal()
        {
            return textBox1.Text.Length > 10 ? textBox1.Text.Substring(0, 10) : textBox1.Text;
        }



    }
}
