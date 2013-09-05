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
    public class GRectangleF : ShapePath
    {
        private PointF _tleft, _tright, _bleft, _bright;
        private float _width , _height;
        private GraphicsPath _graphicPath = null;

        public float Left
        {
            get { return _tleft.X; }
        }

        public float Top
        {
            get { return _tleft.Y; }
        }

        public float Width 
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

        public float Height 
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

        public PointF Location
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

        public PointF TLeft
        {
            get { return _tleft; }

        }

        public PointF TRight
        {
            get { return _tright; }

        }

        public PointF BRight
        {
            get { return _bright; }
        }

        public PointF BLeft
        {
            get { return _bleft; }

        }

      
        public GRectangleF(float x, float y, float width, float height)
        {
            InternalContruct(x, y, width, height);
        }

        public GRectangleF(RectangleF rect)
        {
            InternalContruct(rect.X, rect.Y, rect.Width, rect.Height);
        }

        private void InternalContruct(float x, float y, float width, float height)
        {
            _width = width;
            _height = height;
            _tleft = new PointF(x, y);
            _tright = new PointF(x + width, y);
            _bleft = new PointF(x, y + height);
            _bright = new PointF(x + width, y + height);
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
