// *****************************************************************************
// 
//  (c) NextWave Software  2007
//  All rights reserved. The software and associated documentation 
//  supplied hereunder are the proprietary information of NextWave Software 
//	Limited, Kuala Lumpur , Malaysia and are supplied subject to 
//	licence terms.
// 
//  Version 0.9 	www.nextwavesoft.com
// *****************************************************************************
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;



namespace NextUI.Helper
{
    public class TextEffectHelper
    {
        public static void HaloEffect(Font f, Brush b, Graphics e, Point p, string text)
        {
            Size z = TextRenderer.MeasureText(text, f);
            Bitmap map = new Bitmap(z.Width, z.Height);
            Graphics g = Graphics.FromImage(map);
            GraphicsPath pth = new GraphicsPath();
            pth.AddString(text, f.FontFamily,(int)f.Style, f.Size,new Point(0,0), StringFormat.GenericDefault);
        //    Matrix mx = new Matrix(1.0f / 5, 0, 0, 1.0f / 5, -(1.0f / 5), -(1.0f / 5));
            g.SmoothingMode = SmoothingMode.AntiAlias;
          //  g.Transform = mx;
            Pen p1 = new Pen(Color.FromArgb(200,Color.White), 3);
            g.DrawPath(p1, pth);
            g.FillPath(Brushes.Black, pth);
            g.Dispose();
            e.SmoothingMode = SmoothingMode.AntiAlias;
            e.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.DrawImage(map, new Rectangle(p.X,p.Y,z.Width,z.Height), 0, 0, map.Width, map.Height, GraphicsUnit.Pixel);

        }

        public static void ShadowEffect(Font f, Brush b, Graphics e, Point p)
        {

        }

        public static void EmbossEffect(Font f, Brush b, Graphics e, PointF p, string text)
        {

            e.DrawString(text, f, Brushes.White, p.X, p.Y, StringFormat.GenericTypographic);
            e.DrawString(text, f, Brushes.DarkGray, p.X + 4, p.Y +4, StringFormat.GenericTypographic);
            e.DrawString(text, f, b, p.X + 2, p.Y + 2, StringFormat.GenericTypographic);

        }
    }
}
