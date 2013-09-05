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
    public class RoundedTriangle : ShapePath
    {
        private int _x;
        private int _y;
        private int _width;
        private int _height;
        private Rectangle _clientRect;
        private GraphicsPath _graphicPath = null;

        public int Left
        {
            get { return _x; }
            set
            {
                _x = value;
                InternalContruct(_x, _y, _width, _height);
            }
        }

        public int Top
        {
            get { return _y; }
            set
            {
                _y = value;
                InternalContruct(_x, _y, _width, _height);
            }
        }

        public int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                InternalContruct(_x, _y, _width, _height);
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
                InternalContruct(_x, _y, _width, _height);
            }
        }


        public Rectangle ClientRect
        {
            get { return _clientRect; }
            set
            {
                Rectangle rect = value;
                InternalContruct(rect.X, rect.Y, rect.Width, rect.Height);
            }
        }

        public RoundedTriangle(int x, int y, int width, int height)
        {
            InternalContruct(x, y, width, height);
        }

        public RoundedTriangle(Rectangle rect)
        {
            InternalContruct(rect.X, rect.Y, rect.Width, rect.Height);
        }

        private void InternalContruct(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _clientRect = new Rectangle(_x, _y, _width, _height);
            _graphicPath = null;

        }

        public void Shrink(int pixel)
        {
            _x = _x + pixel;
            _y = _y + pixel;
            _width = _width - 2 * pixel;
            _height = _height - 2 * pixel;
            InternalContruct(_x, _y, _width, _height);
        }

        public void Expand(int pixel)
        {
            _x = _x - pixel;
            _y = _y - pixel;
            _width = _width + 2 * pixel;
            _height = _height + 2 * pixel;
            InternalContruct(_x, _y, _width, _height);
        }


        public override GraphicsPath GetGraphicsPath()
        {
            if (_graphicPath == null)
            {
                float up = this.ClientRect.Width / 20;
                float side = this.ClientRect.Height / 12;
                RectangleF topRect = new RectangleF((float)_x + 9*up,(float)_y + side, 2*up,2*side);
                RectangleF leftRect = new RectangleF((float)_x + up,(float)_y + 10* side,2*up,2*side);
                RectangleF rightRect = new RectangleF((float)_x + 17 *up , (float) _y + 10* side,2*up,2*side);
                PointF L1 = new PointF((float)_x + 9 * up - 0.1f *up, (float)_y + 2 * side  - 0.3f*side);
                PointF L2 = new PointF((float)_x + 2 * up - 0.3f * up, (float)_y + 10 * side);
                PointF B1 = new PointF((float)_x + 3 * up, (float)_y + 12 * side);
                PointF B2 = new PointF((float)_x + 17 * up, (float)_y + 12 * side);
                PointF R1 = new PointF((float)_x + 11 * up + 0.1f * up, (float)_y + 2 * side - 0.3f * side);
                PointF R2 = new PointF((float)_x + 18 * up + 0.3f * up, (float)_y + 10 * side);
                _graphicPath = new GraphicsPath();
                _graphicPath.AddArc(topRect, 180f, 180f);
                _graphicPath.AddLine(R1, R2);
                _graphicPath.AddArc(rightRect, 270f, 180f);
                _graphicPath.AddLine(B2, B1);
                _graphicPath.AddArc(leftRect, 90f, 180f);
                _graphicPath.AddLine(L2, L1);
                _graphicPath.CloseAllFigures();
             
            }
            return _graphicPath;
        }

        public override bool Contain(Point location)
        {
            return _graphicPath.IsVisible(location);
        }
    }
}
