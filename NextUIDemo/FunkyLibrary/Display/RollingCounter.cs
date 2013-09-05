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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using NextUI.Common;
using NextUI.Helper;

namespace NextUI.Display
{
    public class RollingCounter
    {
        public enum Counter {Zero = 0, One, Two, Three, Four, Five, Six, Seven, Eight, Nine };
        public enum FillType{Gradient = 0 , Solid};
        private int _x, _y, _width, _height;
        private Bitmap _map = null;
        private Control _parent;
        private Counter _count = Counter.Zero;
        private Timer _timer = null;
        private int _timing = 5;
        private int _scrollY = 0;
        private int _scrollOffset = 1;
        private Font _font = new Font(FontFamily.GenericMonospace, 5);
        private Color _fontColor = Color.Black;
        private bool _forwardScroll = true;
        private bool _scrollEffect = true;
        private Color _mainColor = Color.Khaki;
        private FillType _filltype = FillType.Gradient;

        public FillType Type
        {
            get { return _filltype; }
            set
            {
                if (_filltype != value)
                {
                    _filltype = value;
                    FillScroll(_width, _height);
                }
            }
        }

        public Color MainColor
        {
            get { return _mainColor; }
            set
            {
                if (_mainColor != value)
                {
                    _mainColor = value;
                    FillScroll(_width, _height);
                }
            }
        }
        

        internal bool ScrollEffect
        {
            get { return _scrollEffect; }
            set
            {
                if (_scrollEffect != value)
                {
                    _scrollEffect = value; 
                }
            }
        }

        internal bool Forward
        {
            get { return _forwardScroll; }
            set
            {
                if (_forwardScroll != value)
                {
                    _forwardScroll = value;
                }
            }
        }


        internal Font Font
        {
            get { return _font; }
            set
            {
                if (_font != value)
                {
                    _font = value;
                    FillScroll(_width, _height);
                }
            }
        }

        public Color FontColor
        {
            get { return _fontColor; }
            set
            {
                if (_fontColor != value)
                {
                    _fontColor = value;
                    FillScroll(_width, _height);
                }
            }
        }

        internal Counter Number
        {
            get { return _count; }
            set
            {
                if (_count != value)
                {
                    _count = value;
                    if (_scrollEffect)
                    {

                        if (_timer != null)
                            _timer.Start();
                    }
                }
            }
        }
        

       
        internal int Left
        {
            get { return _x; }
            set
            {
                if (_x != value)
                {
                    _x = value;
                    InternalConstruct(_x, _y, _width, _height);
                }
            }
        }

        internal int Top
        {
            get { return _y; }
            set
            {
                if (_y != value)
                {
                    _y = value;
                    InternalConstruct(_x, _y, _width, _height);
                }
            }
        }

        internal int Width
        {
            get { return _width; }
            set
            {
                if (_width != value)
                {
                    _width = value;
                    InternalConstruct(_x, _y, _width, _height);
                }
            }
        }

        internal int Height
        {
            get { return _height; }
            set
            {
                if (_height != value)
                {
                    _height = value;
                    InternalConstruct(_x, _y, _width, _height);
                }
            }
        }


        internal RollingCounter(Control parent)
        {
            _parent = parent;
            InternalConstruct(0,0,10,10);
        }
        
        internal RollingCounter(Rectangle location,Control parent)
        {
            _parent = parent;
            InternalConstruct(location.X, location.Y, location.Width, location.Height);
        }

        private void InternalConstruct(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            FillScroll(width, height);
            if (_timer == null)
            {
                _timer = new Timer();
                _timer.Interval = _timing;
                _timer.Tick += new EventHandler(_timer_Tick);
             //  _timer.Start();
            }

        }

        private void FillScroll(int width , int height)
        {
            bool enable = false;
            Brush brush;
            if (_timer != null)
            {
                if (_timer.Enabled)
                {
                    enable = true;
                    _timer.Stop();
                }
            }
            _map = new Bitmap(width, (height) * 10);
            Graphics g = Graphics.FromImage(_map);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            if (_filltype == FillType.Gradient)
            {
                brush = new LinearGradientBrush(new Rectangle(0, 0, width, height), Color.White, _mainColor, 0f);
            }
            else
            {
                brush = new SolidBrush(_mainColor);
            }
            g.FillRectangle(brush, new Rectangle(0, 0, width, 10 * height));
            Font f = new Font(_font.Name, height / 2,_font.Style);
            Brush b = new SolidBrush(_fontColor);
            String text ;
            Size s;
            for (int i = (int)Counter.Zero; i <= (int)Counter.Nine; i++)
            {
                text = Convert.ToString(i);
                s = TextRenderer.MeasureText(text, f);
               
                //width is the width of panel - 6 , where 6 includes border
                g.DrawString(text, f, b, (width + 6f )/ 2 - s.Width / 2, (float)(i * height) +((height )/ 2 - s.Height / 2) , StringFormat.GenericTypographic);
            }
            g.Dispose();
            b.Dispose();
            f.Dispose();
            if (enable)
            {
                _timer.Start();
            }

        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if ( _scrollY == (int)_count *Height)
            {
                _timer.Stop();
            }
            else
            {
                ScrollY(_scrollOffset);
            }
            if (_parent != null)
                _parent.Invalidate(new Rectangle(_x, _y, _width, _height)); 
        }

        internal void Draw(Graphics e)
        {
            if (_scrollEffect == false)
            {
                _scrollY = (int)_count * Height;
            }
            int diff1, diff2;
            diff1 = 10 * Height - _scrollY;
            diff2 = Height - diff1;
            if (diff1 <= Height && diff1 >= 0)
            {
            //    Console.WriteLine(" XXscrollY = " + _scrollY + " diff1=" + diff1 + " diff2=" + diff2 + " Height=" + Height);

                e.DrawImage(_map, new Rectangle(_x, _y, _width, diff1), new Rectangle(0, _scrollY, _width, diff1), GraphicsUnit.Pixel);
                e.DrawImage(_map, new Rectangle(_x, diff1 + _y, _width, diff2), new Rectangle(0, 0, _width, diff2), GraphicsUnit.Pixel);
            }
            else
            {
            //    Console.WriteLine("YYscrollY = " + _scrollY + " diff1=" + diff1 + " diff2=" + diff2 + " Height=" + Height);

                e.DrawImage(_map, new Rectangle(_x, _y, _width, _height), new Rectangle(0, _scrollY, _width, _height), GraphicsUnit.Pixel);
            }

            Border.Border b = new Border.Inner3D();
            Rectangle drawRect = RectangleHelper.Shrink(new Rectangle(_x, _y, _width, _height), 3);
            b.DrawBorder(e, new GRectangle(drawRect).GetGraphicsPath());
        }
        

        private void ScrollY(int offset)
        {
            if (_forwardScroll)
            {
                _scrollY = _scrollY + offset;
                if (_scrollY > 10 * Height)
                {
                    _scrollY = 0;
                }
            }
            else
            {
                _scrollY = _scrollY - offset;
                if (_scrollY < 0 )
                {
                    _scrollY =  10 * Height ;
                }
            }
        }
    }
}
