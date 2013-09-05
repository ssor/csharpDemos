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
using System.Drawing.Drawing2D;
using System.Drawing;
using NextUI.Common;
using System.Windows.Forms;

namespace NextUI.Display
{
    public class DigitalPanel
    {
        public enum Number { ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, ZERO , NONE, NEGATIVE};
        private int _x, _y, _width, _height;
        private Control _parent;
        private Rectangle _clientRect;
        private int _length = 6;
        private int _fheight = 6;
        private int _thickness = 3;
        private Number _number = Number.NONE;
        private Color _fontColor = Color.Red;
        private float _gap = 1;
        private Color _backColor = Color.Black;
        private bool _enableDot = false;
        private bool _enableGlare = false;

        public bool EnableGlare
        {
            get { return _enableGlare; }
            set { _enableGlare = value; }
        }

        public bool EnableDot
        {
            get { return _enableDot; }
            set { _enableDot = value; }
        }

        public Color BackColor
        {
            get { return _backColor; }
            set { _backColor = value; }
        }
        

        public int FontSize
        {
            get { return _length; }
            set { _length = value; }
        }

        public int FontThickness
        {
            get { return _thickness; }
            set { _thickness = value; }
        }

        public float FontGapSpace
        {
            get { return _gap; }
            set { _gap = value; }
        }

        public Number DisplayNumber
        {
            get { return _number; }
            set { _number = value; }
        }

        public Color MainColor
        {
            get { return _fontColor; }
            set { _fontColor = value; }
        }

        public int Left
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

        public int Top
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

        public int Width
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

        public int Height
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

        public DigitalPanel(Control parent)
        {
            _parent = parent;
            InternalConstruct(0, 0, 20, 20);
        }

        public DigitalPanel(Rectangle location, Control parent)
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
            _clientRect = new Rectangle(_x, _y, _width, _height);
            _length = (_width / 10)*4 ;
            _fheight = _height / 3;
        }


        public void Draw(Graphics e)
        {
         //   LinearGradientBrush b = new LinearGradientBrush(this._clientRect, Color.WhiteSmoke, _backColor, 0f);
            GraphicsState state = e.Save();
            e.Clip = new Region(this._clientRect);
            e.FillRectangle(new SolidBrush(_backColor), this._clientRect);
            Pen p = new Pen(Color.FromArgb(35,_fontColor), _thickness);
            p.EndCap = LineCap.Triangle;
            p.StartCap = LineCap.Triangle;
           // GraphicsPath gp = new GraphicsPath();
            //gp.
            if (_enableDot)
            {
                e.FillRectangle(new SolidBrush(_fontColor), new Rectangle(_x, _y + 2 * _thickness + 2 * _fheight, _thickness, _thickness));
            }
            else
            {
                e.FillRectangle(new SolidBrush(Color.FromArgb(35, _fontColor)), new Rectangle(_x, _y + 2 * _thickness + 2 * _fheight, _thickness, _thickness));
            }
            e.TranslateTransform(9, 0);
            e.DrawLine(p, new PointF(_x, _y), new PointF(_x + _length, _y));
            e.DrawLine(p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness / 2 + _gap), new PointF(_x - _thickness / 2 - _gap, _y + _fheight + _thickness / 2 + _gap));
            e.DrawLine(p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness / 2 + _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + _fheight + _thickness / 2 + _gap));
            e.DrawLine(p, new PointF(_x, _y + _fheight + _thickness + _gap), new PointF(_x + _length, _y + _fheight + _thickness + _gap));
            e.DrawLine(p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness + _fheight + _thickness / 2 + 2* _gap), new PointF(_x - _thickness / 2 - _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 *_gap));
            e.DrawLine(p, new PointF(_x + _length + _thickness / 2 +  _gap, _y + _thickness + _fheight + _thickness / 2 + 2*_gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2*_gap));
            e.DrawLine(p, new PointF(_x, _y + 2 * _thickness + 2 * _fheight  + 2* _gap), new PointF(_x + _length, _y + 2 * _thickness + 2 * _fheight + 2* _gap ));
            DrawNumber(_number, e);
            p.Dispose();
            e.Restore(state);
         //   b.Dispose();


        }

        private void DrawNumber(Number num, Graphics e)
        {
            Pen p = new Pen(_fontColor, _thickness);
            p.EndCap = LineCap.Triangle;
            p.StartCap = LineCap.Triangle;
            switch (num)
            {
                case Number.ONE:
                    e.DrawLine(p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness / 2 + _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + _fheight + _thickness / 2 + _gap));
                    e.DrawLine(p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                    if (_enableGlare)
                    {
                        DrawGlare(e,p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness / 2 + _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + _fheight + _thickness / 2 + _gap));
                        DrawGlare(e,p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                    }
                    break;
                case Number.TWO:
                    e.DrawLine(p, new PointF(_x, _y), new PointF(_x + _length, _y));
                    e.DrawLine(p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness / 2 + _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + _fheight + _thickness / 2 + _gap));
                    e.DrawLine(p, new PointF(_x, _y + _fheight + _thickness + _gap), new PointF(_x + _length, _y + _fheight + _thickness + _gap));
                    e.DrawLine(p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x - _thickness / 2 - _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                    e.DrawLine(p, new PointF(_x, _y + 2 * _thickness + 2 * _fheight + 2 * _gap), new PointF(_x + _length, _y + 2 * _thickness + 2 * _fheight + 2 * _gap));

                    if (_enableGlare)
                    {
                        DrawGlare(e,p, new PointF(_x, _y), new PointF(_x + _length, _y));
                        DrawGlare(e,p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness / 2 + _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + _fheight + _thickness / 2 + _gap));
                        DrawGlare(e,p, new PointF(_x, _y + _fheight + _thickness + _gap), new PointF(_x + _length, _y + _fheight + _thickness + _gap));
                        DrawGlare(e,p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x - _thickness / 2 - _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                        DrawGlare(e,p, new PointF(_x, _y + 2 * _thickness + 2 * _fheight + 2 * _gap), new PointF(_x + _length, _y + 2 * _thickness + 2 * _fheight + 2 * _gap));
                    }
                    break;
                case Number.THREE:
                    e.DrawLine(p, new PointF(_x, _y), new PointF(_x + _length, _y));
                    e.DrawLine(p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness / 2 + _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + _fheight + _thickness / 2 + _gap));
                    e.DrawLine(p, new PointF(_x, _y + _fheight + _thickness + _gap), new PointF(_x + _length, _y + _fheight + _thickness + _gap));
                    e.DrawLine(p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                    e.DrawLine(p, new PointF(_x, _y + 2 * _thickness + 2 * _fheight + 2 * _gap), new PointF(_x + _length, _y + 2 * _thickness + 2 * _fheight + 2 * _gap));
                    if (_enableGlare)
                    {
                        DrawGlare(e,p, new PointF(_x, _y), new PointF(_x + _length, _y));
                        DrawGlare(e,p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness / 2 + _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + _fheight + _thickness / 2 + _gap));
                        DrawGlare(e,p, new PointF(_x, _y + _fheight + _thickness + _gap), new PointF(_x + _length, _y + _fheight + _thickness + _gap));
                        DrawGlare(e,p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                        DrawGlare(e,p, new PointF(_x, _y + 2 * _thickness + 2 * _fheight + 2 * _gap), new PointF(_x + _length, _y + 2 * _thickness + 2 * _fheight + 2 * _gap));
                    }
                    break;
                case Number.FOUR:
                    
                    e.DrawLine(p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness / 2 + _gap), new PointF(_x - _thickness / 2 - _gap, _y + _fheight + _thickness / 2 + _gap));
                    e.DrawLine(p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness / 2 + _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + _fheight + _thickness / 2 + _gap));
                    e.DrawLine(p, new PointF(_x, _y + _fheight + _thickness + _gap), new PointF(_x + _length, _y + _fheight + _thickness + _gap));
                    e.DrawLine(p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                    if (_enableGlare)
                    {
                        DrawGlare(e,p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness / 2 + _gap), new PointF(_x - _thickness / 2 - _gap, _y + _fheight + _thickness / 2 + _gap));
                        DrawGlare(e,p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness / 2 + _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + _fheight + _thickness / 2 + _gap));
                        DrawGlare(e,p, new PointF(_x, _y + _fheight + _thickness + _gap), new PointF(_x + _length, _y + _fheight + _thickness + _gap));
                        DrawGlare(e,p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                    }
                    break;
                case Number.FIVE:
                    e.DrawLine(p, new PointF(_x, _y), new PointF(_x + _length, _y));
                    e.DrawLine(p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness / 2 + _gap), new PointF(_x - _thickness / 2 - _gap, _y + _fheight + _thickness / 2 + _gap));
                    e.DrawLine(p, new PointF(_x, _y + _fheight + _thickness + _gap), new PointF(_x + _length, _y + _fheight + _thickness + _gap));
                    e.DrawLine(p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                    e.DrawLine(p, new PointF(_x, _y + 2 * _thickness + 2 * _fheight + 2 * _gap), new PointF(_x + _length, _y + 2 * _thickness + 2 * _fheight + 2 * _gap));
                    if (_enableGlare)
                    {
                        DrawGlare(e, p, new PointF(_x, _y), new PointF(_x + _length, _y));
                        DrawGlare(e, p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness / 2 + _gap), new PointF(_x - _thickness / 2 - _gap, _y + _fheight + _thickness / 2 + _gap));
                        DrawGlare(e, p, new PointF(_x, _y + _fheight + _thickness + _gap), new PointF(_x + _length, _y + _fheight + _thickness + _gap));
                        DrawGlare(e, p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                        DrawGlare(e, p, new PointF(_x, _y + 2 * _thickness + 2 * _fheight + 2 * _gap), new PointF(_x + _length, _y + 2 * _thickness + 2 * _fheight + 2 * _gap));
                    }
                    break;
                case Number.SIX:
                    e.DrawLine(p, new PointF(_x, _y), new PointF(_x + _length, _y));
                    e.DrawLine(p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness / 2 + _gap), new PointF(_x - _thickness / 2 - _gap, _y + _fheight + _thickness / 2 + _gap));
                    e.DrawLine(p, new PointF(_x, _y + _fheight + _thickness + _gap), new PointF(_x + _length, _y + _fheight + _thickness + _gap));
                    e.DrawLine(p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x - _thickness / 2 - _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                    e.DrawLine(p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                    e.DrawLine(p, new PointF(_x, _y + 2 * _thickness + 2 * _fheight + 2 * _gap), new PointF(_x + _length, _y + 2 * _thickness + 2 * _fheight + 2 * _gap));
                    if (_enableGlare)
                    {
                        DrawGlare(e,p, new PointF(_x, _y), new PointF(_x + _length, _y));
                        DrawGlare(e,p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness / 2 + _gap), new PointF(_x - _thickness / 2 - _gap, _y + _fheight + _thickness / 2 + _gap));
                        DrawGlare(e,p, new PointF(_x, _y + _fheight + _thickness + _gap), new PointF(_x + _length, _y + _fheight + _thickness + _gap));
                        DrawGlare(e,p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x - _thickness / 2 - _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                        DrawGlare(e,p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                        DrawGlare(e,p, new PointF(_x, _y + 2 * _thickness + 2 * _fheight + 2 * _gap), new PointF(_x + _length, _y + 2 * _thickness + 2 * _fheight + 2 * _gap));
                    }
                    break;
                case Number.SEVEN:
                    e.DrawLine(p, new PointF(_x, _y), new PointF(_x + _length, _y));
                    e.DrawLine(p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness / 2 + _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + _fheight + _thickness / 2 + _gap));
                    e.DrawLine(p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                    if (_enableGlare)
                    {
                        DrawGlare(e,p, new PointF(_x, _y), new PointF(_x + _length, _y));
                        DrawGlare(e,p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness / 2 + _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + _fheight + _thickness / 2 + _gap));
                        DrawGlare(e,p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                    }
                    break;
                case Number.EIGHT:
                    e.DrawLine(p, new PointF(_x, _y), new PointF(_x + _length, _y));
                    e.DrawLine(p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness / 2 + _gap), new PointF(_x - _thickness / 2 - _gap, _y + _fheight + _thickness / 2 + _gap));
                    e.DrawLine(p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness / 2 + _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + _fheight + _thickness / 2 + _gap));
                    e.DrawLine(p, new PointF(_x, _y + _fheight + _thickness + _gap), new PointF(_x + _length, _y + _fheight + _thickness + _gap));
                    e.DrawLine(p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x - _thickness / 2 - _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                    e.DrawLine(p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                    e.DrawLine(p, new PointF(_x, _y + 2 * _thickness + 2 * _fheight + 2 * _gap), new PointF(_x + _length, _y + 2 * _thickness + 2 * _fheight + 2 * _gap));
                    if (_enableGlare)
                    {
                        DrawGlare(e,p, new PointF(_x, _y), new PointF(_x + _length, _y));
                        DrawGlare(e,p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness / 2 + _gap), new PointF(_x - _thickness / 2 - _gap, _y + _fheight + _thickness / 2 + _gap));
                        DrawGlare(e,p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness / 2 + _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + _fheight + _thickness / 2 + _gap));
                        DrawGlare(e,p, new PointF(_x, _y + _fheight + _thickness + _gap), new PointF(_x + _length, _y + _fheight + _thickness + _gap));
                        DrawGlare(e,p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x - _thickness / 2 - _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                        DrawGlare(e,p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                        DrawGlare(e,p, new PointF(_x, _y + 2 * _thickness + 2 * _fheight + 2 * _gap), new PointF(_x + _length, _y + 2 * _thickness + 2 * _fheight + 2 * _gap));
                    }
                    break;
                case Number.NINE:
                    e.DrawLine(p, new PointF(_x, _y), new PointF(_x + _length, _y));
                    e.DrawLine(p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness / 2 + _gap), new PointF(_x - _thickness / 2 - _gap, _y + _fheight + _thickness / 2 + _gap));
                    e.DrawLine(p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness / 2 + _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + _fheight + _thickness / 2 + _gap));
                    e.DrawLine(p, new PointF(_x, _y + _fheight + _thickness + _gap), new PointF(_x + _length, _y + _fheight + _thickness + _gap));
                    e.DrawLine(p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                    e.DrawLine(p, new PointF(_x, _y + 2 * _thickness + 2 * _fheight + 2 * _gap), new PointF(_x + _length, _y + 2 * _thickness + 2 * _fheight + 2 * _gap));
                    if (_enableGlare)
                    {
                        DrawGlare(e,p, new PointF(_x, _y), new PointF(_x + _length, _y));
                        DrawGlare(e,p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness / 2 + _gap), new PointF(_x - _thickness / 2 - _gap, _y + _fheight + _thickness / 2 + _gap));
                        DrawGlare(e,p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness / 2 + _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + _fheight + _thickness / 2 + _gap));
                        DrawGlare(e,p, new PointF(_x, _y + _fheight + _thickness + _gap), new PointF(_x + _length, _y + _fheight + _thickness + _gap));
                        DrawGlare(e,p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                        DrawGlare(e,p, new PointF(_x, _y + 2 * _thickness + 2 * _fheight + 2 * _gap), new PointF(_x + _length, _y + 2 * _thickness + 2 * _fheight + 2 * _gap));
                    }
                    break;
                case Number.ZERO:
                    e.DrawLine(p, new PointF(_x, _y), new PointF(_x + _length, _y));
                    e.DrawLine(p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness / 2 + _gap), new PointF(_x - _thickness / 2 - _gap, _y + _fheight + _thickness / 2 + _gap));
                    e.DrawLine(p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness / 2 + _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + _fheight + _thickness / 2 + _gap));
                    e.DrawLine(p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x - _thickness / 2 - _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                    e.DrawLine(p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                    e.DrawLine(p, new PointF(_x, _y + 2 * _thickness + 2 * _fheight + 2 * _gap), new PointF(_x + _length, _y + 2 * _thickness + 2 * _fheight + 2 * _gap));
                    if (_enableGlare)
                    {
                        DrawGlare(e,p, new PointF(_x, _y), new PointF(_x + _length, _y));
                        DrawGlare(e,p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness / 2 + _gap), new PointF(_x - _thickness / 2 - _gap, _y + _fheight + _thickness / 2 + _gap));
                        DrawGlare(e,p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness / 2 + _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + _fheight + _thickness / 2 + _gap));
                        DrawGlare(e,p, new PointF(_x - _thickness / 2 - _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x - _thickness / 2 - _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                        DrawGlare(e,p, new PointF(_x + _length + _thickness / 2 + _gap, _y + _thickness + _fheight + _thickness / 2 + 2 * _gap), new PointF(_x + _length + _thickness / 2 + _gap, _y + 2 * _fheight + _thickness + _thickness / 2 + 2 * _gap));
                        DrawGlare(e,p, new PointF(_x, _y + 2 * _thickness + 2 * _fheight + 2 * _gap), new PointF(_x + _length, _y + 2 * _thickness + 2 * _fheight + 2 * _gap));
                    }
                    break;
                case Number.NONE:
                    break;
                case Number.NEGATIVE:
                    e.DrawLine(p, new PointF(_x, _y + _fheight + _thickness + _gap), new PointF(_x + _length, _y + _fheight + _thickness + _gap));
                    if (_enableGlare)
                    {
                        DrawGlare(e,p, new PointF(_x, _y + _fheight + _thickness + _gap), new PointF(_x + _length, _y + _fheight + _thickness + _gap));
                    }
                    break;

            }
            p.Dispose();
        }

        private void DrawGlare(Graphics e, Pen p, PointF p1, PointF p2)
        {
            Bitmap map;
            RectangleF exprect;
            float size = 3;
            if (p1.X < p2.X)
            {
                map = new Bitmap((int)(p2.X - p1.X), (int)p.Width);
                exprect = new RectangleF(p1.X - size, p1.Y - size, map.Width + 2 * size, map.Height + 2 * size);
            }
            else if (p1.X > p2.X)
            {
                map = new Bitmap((int)(p1.X - p2.X), (int)p.Width);
                exprect = new RectangleF(p2.X - size, p2.Y - size, map.Width + 2 * size, map.Height + 2 * size);
            }
            else if (p1.Y < p2.Y)
            {
                map = new Bitmap((int)p.Width, (int)(p2.Y - p1.Y));
                exprect = new RectangleF(p2.X - size, p1.Y - size, map.Width + 2 * size, map.Height + 2 * size);
            }
            else
            {
                map = new Bitmap((int)p.Width, (int)(p1.Y - p2.Y));
                exprect = new RectangleF(p2.X - size, p2.Y - size, map.Width + 2 * size, map.Height + 2 * size);
            }
            Graphics g = Graphics.FromImage(map);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillRectangle(new SolidBrush(Color.FromArgb(150,p.Color)), new RectangleF(0, 0, map.Width, map.Height));
            g.Dispose();
            GraphicsState state = e.Save();
            e.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.DrawImage(map, exprect, new Rectangle(0, 0, map.Width, map.Height), GraphicsUnit.Pixel);
            e.Restore(state);
        }
    }
    
}
