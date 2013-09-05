using System;
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

using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using NextUI.Common;

namespace NextUI.Bar
{
    public class Dragger
    {
        public enum DragPosition { Horizontal, Vertical };
        private float _parentX = 0 , _parentY = 0 , _parentWidth = 0, _parentHeight = 0;
        private PointF _location;
        private PointF _defaultLocation;
        private RectangleF _clientRec;
        private float _height = 30;
        private float _width = 30;
        private Color _selectColor = Color.Black; 
        private Color _normalColor = Color.White;
        private Color _pointerColor = Color.Red;
        //code currently is for horizontal only//
        private DragPosition _position = DragPosition.Horizontal;

        protected bool _mouseDown = false;
        protected bool _mouseHover = false;

        public float PointerValue
        {
            get { return _location.X + _width / 2; }
        }

        public bool IsMouseDown
        {
            get { return _mouseDown; }
        }

        public DragPosition Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Color SelectedColor
        {
            get { return _selectColor; }
            set
            {
                if (_selectColor != value)
                {
                    _selectColor = value;
                }
            }
        }

        public Color NormalColor
        {
            get { return _normalColor; }
            set
            {
                if (_normalColor != value)
                {
                    _normalColor = value;
                }
            }
        }

        public Color PointerColor
        {
            get { return _pointerColor; }
            set
            {
                if (_pointerColor != value)
                {
                    _pointerColor = value;
                }
            }
        }

        public RectangleF ClientRect
        {
            get { return _clientRec; }
        }

        public void MouseDown(Point location)
        {
            if (Helper.AlgorithmHelper.IsPointInRectF(this.ClientRect, new PointF(location.X, location.Y)))
            {
                _mouseDown = true;
            }
        }

        public void MouseUp(Point location)
        {
            _mouseDown = false;
        }

        public void MouseMove(Point location)
        {
            if (_mouseDown)
            {
                    this.Location = new PointF(location.X, location.Y);
            }
            if (Helper.AlgorithmHelper.IsPointInRectF(this.ClientRect, new PointF(location.X, location.Y)))
            {
                _mouseHover = true;
            }
            else
            {
                _mouseHover = false;
            }
        }

        public void ResetPosition()
        {
            _location = _defaultLocation;
            _clientRec = new RectangleF(_location, new SizeF(_width, _height));
        }

        public PointF Location
        {
            get { return _location; }
            set
            {
                PointF p = value;
           //     Console.WriteLine("point = " + p + "_parentX= " + _parentX + "  _parentWidth=" + _parentWidth);
                if (_position == DragPosition.Vertical)
                {
                    /* Not used at this moment */
                    if ( p.X >= this.ClientRect.Left
                         && p.X <= this.ClientRect.Left + this.ClientRect.Width
                         && p.Y >= _parentY - _height / 2
                         && p.Y <= _parentY + _parentHeight + _height/ 2)
                    {
                        //if within parent dimension
                        _location.Y = p.Y;
                        _clientRec = new RectangleF(_location, new SizeF(_width, _height));
                    }
                    
                }
                else
                {
                    if (p.X <= _parentX + _parentWidth 
                        && p.X >= _parentX )
                      /*  && p.Y >= ClientRect.Top
                        && p.Y <= ClientRect.Top + ClientRect.Height)*/
                    {
                        _location.X = p.X - _width /2;
                        _clientRec = new RectangleF(_location, new SizeF(_width, _height));
                    }
                }

            }

        }
        public Dragger(Rectangle rect, DragPosition position)
        {
            _position = position;
            InternalContruct((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
        }



        public Dragger(float x, float y, float width, float height,DragPosition position)
        {
            _position = position;
            InternalContruct(x, y, width, height);
        }

        private void InternalContruct(float x, float y, float width, float height)
        {
            _parentX = x;
            _parentY = y;
            _parentWidth = width;
            _parentHeight = height;
            if (_position == DragPosition.Vertical)
            {
                _width = _parentWidth + 7;
                _height = 20;
                _location = new PointF(_parentX + _parentWidth / 2 - _width / 2, _parentY + _parentHeight / 2f);
            }
            else
            {
                _width = 20;
                _height = _parentHeight + 7;
                _location = new PointF(_parentX - (_width / 2), _parentY + _parentHeight / 2 - _height / 2);
            }
            _defaultLocation = _location;
            _clientRec = new RectangleF(_location, new SizeF(_width, _height));

        }

        public virtual void draw(Graphics g)
        {

            Pen p = new Pen(Color.White);
            RoundedRectangleF outLayer = new RoundedRectangleF(_location.X, _location.Y, _width, _height, 1);
            GraphicsPath gp = new GraphicsPath();
            gp.AddPath(outLayer.GetGraphicsPath(), false);
            outLayer.Shrink(3);
            gp.AddPath(outLayer.GetGraphicsPath(), false);
           // Border.Border border = new Border.BorderShadow();
          //  border.DrawBorder(g, gp);
            GraphicsState state = g.Save();
            g.TranslateTransform(5, 1);
            g.FillPath(new SolidBrush(Color.FromArgb(150, Color.Black)), gp);
            g.Restore(state);
            PathGradientBrush pgb = new PathGradientBrush(gp);
            if (_mouseHover || _mouseDown)
            {
                pgb.CenterColor = _normalColor;
                pgb.SurroundColors = new Color[] { _selectColor };
            }
            else
            {
                pgb.CenterColor = _selectColor;
                pgb.SurroundColors = new Color[] { _normalColor };
            }
            Pen pointer = new Pen(_pointerColor, 4);
            pointer.StartCap = LineCap.DiamondAnchor;
            pointer.EndCap = LineCap.DiamondAnchor;
            g.DrawLine(new Pen(Color.FromArgb(150, Color.Black), 3), new PointF(_location.X + _width / 2 + 2, _parentY - 7), new PointF(_location.X + _width / 2 + 2, _parentY + _height - 4));
            g.DrawLine(pointer, new PointF(_location.X + _width / 2, _parentY - 5), new PointF(_location.X + _width / 2, _parentY + _height - 2));
            g.FillPath(pgb, gp);
            g.DrawPath(p, gp);
            pgb.Dispose();
            p.Dispose();
        }

    }
}
