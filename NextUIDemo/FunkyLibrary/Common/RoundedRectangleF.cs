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
    public class RoundedRectangleF : ShapePath
    {
        private float _x;
        private float _y;
        private float _width;
        private float _height;
        private float _redius;
        private RectangleF _clientRect;
        private RectangleF _innerRect;
        private GraphicsPath _graphicPath = null;

        public float Left
        {
            get { return _x; }
            set
            {
                _x = value;
                InternalContruct(_x, _y, _width, _height, _redius);
            }
        }

        public float Top
        {
            get { return _y; }
            set
            {
                _y = value;
                InternalContruct(_x, _y, _width, _height, _redius);
            }
        }

        public float Width
        {
            get { return _width; }
            set
            {
                _width = value;
                InternalContruct(_x, _y, _width, _height, _redius);
            }
        }

        public PointF Location
        {
            get { return new PointF(_x, _y); }
        }

        public float Height
        {
            get { return _height; }
            set
            {
                _height = value;
                InternalContruct(_x, _y, _width, _height, _redius);
            }
        }

        public RectangleF InnerRect
        {
            get { return _innerRect; }
        }

        public RectangleF ClientRect
        {
            get { return _clientRect; }
            set
            {
                RectangleF rect = value;
                InternalContruct(rect.X, rect.Y, rect.Width, rect.Height, _redius);
            }
        }

        public RoundedRectangleF(float x, float y, float width, float height, float radius)
        {
            InternalContruct(x, y, width, height, radius);
        }

        public RoundedRectangleF(RectangleF rect, float radius)
        {
            InternalContruct(rect.X, rect.Y, rect.Width, rect.Height, radius);
        }

        private void InternalContruct(float x, float y, float width, float height, float radius)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _clientRect = new RectangleF(_x, _y, _width, _height);
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
                SizeF cornerSize = new SizeF(_redius, _redius);
                float xr = (_width + _x - cornerSize.Width);
                float yr = (_height + _y - cornerSize.Height);
                float xts = (_x + cornerSize.Width);
                float xte = (xts + (_width - (2 * cornerSize.Width)));
                float yls = (_y + cornerSize.Height);
                float yle = (yls + (_height - (2 * cornerSize.Height)));
                //Lets create the 4 corner
                RectangleF tl = new RectangleF(_x, _y, cornerSize.Width, cornerSize.Height);
                RectangleF tr = new RectangleF(xr, _y, cornerSize.Width, cornerSize.Height);
                RectangleF bl = new RectangleF(_x, yr, cornerSize.Width, cornerSize.Height);
                RectangleF br = new RectangleF(xr, yr, cornerSize.Width, cornerSize.Height);
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
                _innerRect = new RectangleF(tl.Left + tl.Width, tl.Top + tl.Height,
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
