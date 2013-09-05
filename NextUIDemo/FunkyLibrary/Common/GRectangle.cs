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
    public class GRectangle : ShapePath
    {
        private Point _tleft, _tright, _bleft, _bright;
        private int _width , _height;
        private GraphicsPath _graphicPath = null;

        public int Left
        {
            get { return _tleft.X; }
        }

        public int Top
        {
            get { return _tleft.Y; }
        }

        public int Width 
        {
            get { return _width;}
            set {
                if ( _width != value )
                {
                    _width = value;
                    InternalContruct(_tleft.X,_tleft.Y,_width,_height);
                }
            }
        }

        public int Height 
        {
            get { return _height;}
            set {
                if ( _height != value )
                {
                    _height = value;
                    InternalContruct(_tleft.X,_tleft.Y,_width,_height);
                }
            }
        }

        public Point Location
        {
            get { return _tleft; }
            set
            {
                if (_tleft != value)
                {
                    _tleft = value;
                    InternalContruct(_tleft.X, _tleft.Y, _width, _height);
                }
            }
        }

        public Point TLeft
        {
            get { return _tleft; }

        }

        public Point TRight
        {
            get { return _tright; }

        }

        public Point BRight
        {
            get { return _bright; }
        }

        public Point BLeft
        {
            get { return _bleft; }

        }

      
        public GRectangle(int x, int y, int width, int height)
        {
            InternalContruct(x, y, width, height);
        }

        public GRectangle(Rectangle rect)
        {
            InternalContruct(rect.X, rect.Y, rect.Width, rect.Height);
        }

        private void InternalContruct(int x, int y, int width, int height)
        {
            _width = width;
            _height = height;
            _tleft = new Point(x, y);
            _tright = new Point(x + width, y);
            _bleft = new Point(x, y + height);
            _bright = new Point(x + width, y + height);
            _graphicPath = null;

        }

        public void Shrink(int pixel)
        {
            _tleft.X += pixel ;
            _tleft.Y += pixel;
            _tright.X -= pixel;
            _tright.Y += pixel;
            _bleft.X += pixel;
            _bleft.Y -= pixel;
            _bright.X -= pixel;
            _bright.Y -= pixel;
 
            _graphicPath = null;
        }

        public void Expand(int pixel)
        {
            _tleft.X -= pixel;
            _tleft.Y -= pixel;
            _tright.X += pixel;
            _tright.Y -= pixel;
            _bleft.X -= pixel;
            _bleft.Y += pixel;
            _bright.X += pixel;
            _bright.Y += pixel;
            _graphicPath = null;
        }


        public override  GraphicsPath GetGraphicsPath()
        {
            if (_graphicPath == null)
            {
                _graphicPath = new GraphicsPath();
                _graphicPath.AddLine(_tleft, _tright);
                _graphicPath.AddLine(_tright, _bright);
                _graphicPath.AddLine(_bright, _bleft);
                _graphicPath.AddLine(_bleft, _tleft);
                _graphicPath.CloseAllFigures();

            }
            return _graphicPath;
        }

        public void Transform(Matrix matrix)
        {
            _graphicPath.Transform(matrix);
            
        }

        public override bool Contain(Point location)
        {
            if (_graphicPath != null)
            {
                return _graphicPath.IsVisible(location);
            }
            return false;
        }
    }
}
