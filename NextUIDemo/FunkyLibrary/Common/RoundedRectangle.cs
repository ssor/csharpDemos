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

namespace NextUI.Common
{
    public class RoundedRectangle : ShapePath
    {
        private int _x;
        private int _y;
        private int _width;
        private int _height;
        private int _redius;
        private Rectangle _clientRect;
        private Rectangle _innerRect;
        private GraphicsPath _graphicPath = null;

        public int Left
        {
            get { return _x; }
            set
            {
                _x = value;
                InternalContruct(_x, _y, _width, _height, _redius);
            }
        }

        public int Top
        {
            get { return _y; }
            set
            {
                _y = value;
                InternalContruct(_x, _y, _width, _height, _redius);
            }
        }

        public int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                InternalContruct(_x, _y, _width, _height, _redius);
            }
        }

        public Point Location
        {
            get { return new Point(_x, _y); }
        }

        public int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                InternalContruct(_x, _y, _width, _height, _redius);
            }
        }

        public Rectangle InnerRect
        {
            get { return _innerRect; }
        }

        public Rectangle ClientRect
        {
            get { return _clientRect; }
            set
            {
                Rectangle rect = value;
                InternalContruct(rect.X, rect.Y, rect.Width, rect.Height, _redius);
            }
        }

        public RoundedRectangle(int x, int y, int width, int height, int radius)
        {
            InternalContruct(x, y, width, height, radius);
        }

        public RoundedRectangle(Rectangle rect, int radius)
        {
            InternalContruct(rect.X, rect.Y, rect.Width, rect.Height, radius);
        }

        private void InternalContruct(int x, int y, int width, int height, int radius)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _clientRect = new Rectangle(_x, _y, _width, _height);
            _redius = radius;
            _graphicPath = null;

        }

        public void Shrink(int pixel)
        {
            _x = _x + pixel;
            _y = _y + pixel;
            _width = _width - 2 * pixel;
            _height = _height - 2 * pixel;
            InternalContruct(_x, _y, _width, _height, _redius);
        }

        public void Expand(int pixel)
        {
            _x = _x - pixel;
            _y = _y - pixel;
            _width = _width + 2 * pixel;
            _height = _height + 2 * pixel;
            InternalContruct(_x, _y, _width, _height, _redius);
        }


        public override GraphicsPath GetGraphicsPath()
        {
            if (_graphicPath == null)
            {
                _graphicPath = new GraphicsPath();
                Size cornerSize = new Size(_redius, _redius);
                int xr = (_width + _x - cornerSize.Width);
                int yr = (_height + _y - cornerSize.Height);
                int xts = (_x + cornerSize.Width);
                int xte = (xts + (_width - (2 * cornerSize.Width)));
                int yls = (_y + cornerSize.Height);
                int yle = (yls + (_height - (2 * cornerSize.Height)));
                //Lets create the 4 corner
                Rectangle tl = new Rectangle(_x, _y, cornerSize.Width, cornerSize.Height);
                Rectangle tr = new Rectangle(xr, _y, cornerSize.Width, cornerSize.Height);
                Rectangle bl = new Rectangle(_x, yr, cornerSize.Width, cornerSize.Height);
                Rectangle br = new Rectangle(xr, yr, cornerSize.Width, cornerSize.Height);
                //Now we create the Graphic Parh
                _graphicPath.AddArc(tl, 180f, 90f);
                _graphicPath.AddLine(xts, _y, xte, _y);
                _graphicPath.AddArc(tr, 270f, 90f);
                _graphicPath.AddLine(_x + _width, yls, _x + _width, yle);
                _graphicPath.AddArc(br, 0f, 90f);
                _graphicPath.AddLine(xte, _y + _height, xts, _y + _height);
                _graphicPath.AddArc(bl, 90f, 90f);
                _graphicPath.AddLine(_x, yle, _x, yls);
                _graphicPath.CloseAllFigures();
                _innerRect = new Rectangle(tl.Left + tl.Width, tl.Top + tl.Height,
                                           tr.Left - (tl.Left + tl.Width),
                                           bl.Top - (tl.Top + tl.Height));
            }
            return _graphicPath;
        }

        public override bool Contain(Point location)
        {
            return _graphicPath.IsVisible(location);
        }
    }
}
