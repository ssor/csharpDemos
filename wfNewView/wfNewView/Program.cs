using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace wfNewView
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Application.Run(new Form2());
        }
        public static int baseWidth = 80;
        public static int baseHeight = 20;

        public static int realWidth = 0;
        public static int realHeight = 0;


        public static int getRealHeight(int height)
        {
            return (height * realHeight / baseHeight);
        }
        public static int getRealWidth(int width)
        {
            return (width * realWidth / baseWidth);
        }
        public static void setScreenPara(int width, int heigth)
        {
            realHeight = heigth;
            realWidth = width;
        }
        public static Point getRealPoint(Point p)
        {
            Point _p = new Point(p.X * realWidth / baseWidth, p.Y * realHeight / baseHeight);
            return _p;
        }
        public static Size getRealSize(Size s)
        {
            Size _s = new Size(s.Width * realWidth / baseWidth, s.Height * realHeight / baseHeight);
            return _s;
        }
    }
}
