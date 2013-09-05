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
using NextUI.Common;
using NextUI.Helper;
using NextUI.Collection;
using System.Windows.Forms;

namespace NextUI.Bar
{
    public class KnobPanel
    {
        public enum Marking { NONE, LINE, CONT, BOTH };
        public enum MarkingImage { FONT, IMAGE, NONE };
        private int _x, _y, _width, _height;
        private Control _parent;
        private Rectangle _clientRect ;
        private bool _mouseDown = false;
        private float _currentAngle = 360f;
        private PointF _center = new PointF(0, 0);
        private float _distance = 360f;
        private Marking _markingType = Marking.CONT;
        private MarkingImage _markingImage = MarkingImage.IMAGE;
        private Font _f = new Font(FontFamily.GenericMonospace, 8);
        private bool _step = true;
        private Collection.MeterLabelCollection _collection = null;
        private Image _knobHandleImage = null;
        private Color _fontColor = Color.Black;
        private int _value = -1;
        public Color FontColor
        {
            get { return _fontColor; }
            set { _fontColor = value; }
        }

        public int PointerValue
        {
            get { return _value; }
        }

        public bool IsMouseDown
        {
            get { return _mouseDown; }
        }

        public KnobPanel.MarkingImage MarkingImageType
        {
            get { return _markingImage; }
            set
            {
                if (_markingImage != value)
                {
                    _markingImage = value;
                }
            }
        }

        public KnobPanel.Marking MarkingType
        {
            get { return _markingType; }
            set
            {
                if (_markingType != value)
                {
                    _markingType = value;
                }
            }
        }

        public Image KnobHandleimage
        {
            get { return _knobHandleImage; }
            set { _knobHandleImage = value; }
        }

        public Font MainFont
        {
            get { return _f; }
            set { _f = value; }
        }

        public bool Step
        {
            get { return _step; }
            set { _step = value; }
        }

        public MeterLabelCollection Collection
        {
            get { return _collection; }
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

        public KnobPanel(Control parent)
        {
            _parent = parent;
            InternalConstruct(0, 0, 20, 20);
        }

        public KnobPanel(Rectangle location, Control parent)
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
            _clientRect = new Rectangle(_x,_y,_width,_height);
            if (_collection == null)
            {
                _collection = new MeterLabelCollection();
                _collection.Delete += new OnRemove(_collection_Delete);
                _collection.Insert += new OnInsert(_collection_Insert);
                _collection.Set += new OnSet(_collection_Set);
            }
        }

        void _collection_Set(object sender, int index)
        {
        }

        void _collection_Insert(object sender, int index)
        {
            _distance = 360f / _collection.Count;
            if (_step)
            {
                NearestAngle();
            }

           
        }

        void _collection_Delete(object sender, int index)
        {
            if (_step)
            {
                if (_collection.Count > 0)
                {
                    _distance = 360f / _collection.Count;
                    NearestAngle();
                }
                else
                {
                    _distance = 360f;
                    _currentAngle = _distance;
                }
            }
            
        }

        public void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Helper.AlgorithmHelper.IsPointInRect(_clientRect, e.Location))
                {
                    _mouseDown = true;
                    if (_collection.Count > 1)
                    {
                        if (_step)
                        {
                            NearestAngle();
                        }
                        else
                        {
                            _currentAngle = AlgorithmHelper.PointToCenterAngle(_center, e.Location);
                            _currentAngle = AlgorithmHelper.NormalizeDegree(270, _currentAngle);
                        }
                    }
                }                       
            }
        }

        public void MouseUp(MouseEventArgs e)
        {
            _mouseDown = false;
            if (_step)
            {
                // we pick the closest target.
                NearestAngle();
           
            }
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (_mouseDown)
            {
                if (_collection.Count > 1)
                {
                        _currentAngle = Helper.AlgorithmHelper.PointToCenterAngle(_center, e.Location);
                        _currentAngle = AlgorithmHelper.NormalizeDegree(270, _currentAngle);
                       // Console.WriteLine("VAL ===== " + PointerToValue(_currentAngle));
                        _value = PointerToValue(_currentAngle);
                }
            }

        }

        public void Draw(Graphics e)
        {
            Rectangle r1 = RectangleHelper.Shrink(_clientRect,10);
            r1.Height = r1.Width;
            _center = new PointF((float)(r1.Left + r1.Width / 2),(float)( r1.Top + r1.Height / 2));
            GraphicsPath s1 = new GraphicsPath();
            s1.AddEllipse(r1);
          /*  PathGradientBrush pb = new PathGradientBrush(s1);
            pb.CenterColor = Color.White;
            pb.SurroundColors = new Color[] { Color.Gray };
            pb.CenterPoint = new PointF(-2f, -2f);
            e.FillEllipse(pb,r1);*/
            e.DrawEllipse(new Pen(Color.Black), r1);
            Rectangle r2 = RectangleHelper.Shrink(r1, 10);
            //the knob handle
            LinearGradientBrush b = new LinearGradientBrush(r2, Color.Silver, Color.White, 180f);
            e.FillEllipse(b, r2);
            e.DrawEllipse(new Pen(Color.DarkSlateBlue), r2);
            GraphicsPath p = new GraphicsPath();
            p.AddEllipse(r2);
            
            //The pointer

            GraphicsPath ptr = new GraphicsPath();
            ptr.AddEllipse(new Rectangle(r2.Left + r2.Width / 2 - 5, r2.Top , 10, 10));
            PathGradientBrush pbptr = new PathGradientBrush(ptr);
            pbptr.CenterColor = Color.LightGray;
         //   pbptr.CenterPoint = new PointF(0, -0.5f);
            pbptr.SurroundColors = new Color[] { Color.Black };

            DrawMarking(e, r1, r2);

            GraphicsState state = e.Save();
            Matrix m = new Matrix();
            m.RotateAt(_currentAngle, _center);
            e.Transform = m;
            e.FillPath(pbptr, ptr);
            e.SetClip(p);
            if (_knobHandleImage != null)
            {
                e.DrawImage(_knobHandleImage, r2);
            }
            e.ResetClip();
            p.Dispose();
            e.Restore(state);
            ptr.Dispose();
            pbptr.Dispose();
            //end
                
        }


        private void DrawMarking(Graphics e, Rectangle outBound , Rectangle inBound)
        {
           // e.SmoothingMode = SmoothingMode.HighQuality;
            GraphicsState state = e.Save();
            e.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.SmoothingMode = SmoothingMode.HighQuality;
            Matrix mat = new Matrix();
            Point sp = new Point(inBound.Left + inBound.Width / 2, outBound.Top );
            Point ep = new Point(inBound.Left + inBound.Width / 2, inBound.Top);
            if (_collection.Count > 0)
            {
                float angle = 0;
                for (int i = 0; i < _collection.Count; i++)
                {
                    mat.RotateAt(_distance, _center);
                 //   Console.WriteLine("rotate " + i + " : " + _distance + " collection value=" + _collection[i].Value);
                    angle += _distance;
                    e.Transform = mat;
                    if  ( _markingType == Marking.CONT || _markingType== Marking.BOTH)
                    {
                        GraphicsPath p = new GraphicsPath();
                        p.AddLine(sp, ep);
                        p.AddArc(inBound, 270, _distance);
                        p.Reverse();
                        GraphicsPath p2 = new GraphicsPath();
                        p2.AddArc(outBound, 270, _distance);
                        p2.AddLine(
                            AlgorithmHelper.LocationToCenter(_center, outBound.Width / 2, AlgorithmHelper.NormalizeDegreeFromX(270 + _distance)),
                            AlgorithmHelper.LocationToCenter(_center, inBound.Width / 2, AlgorithmHelper.NormalizeDegreeFromX(270 + _distance)));
                        p2.AddPath(p, true);
                        e.FillPath(new SolidBrush(_collection[i].MainColor), p2);

                    }
                    if  (_markingType == Marking.LINE || _markingType == Marking.BOTH)
                    {
                        e.DrawLine(new Pen(_fontColor, 2), sp, ep);
                    }
                     Size z = new Size(0,0);
                    if ( _markingImage == MarkingImage.FONT)
                    {
                        String text = _collection[i].Desc;
                        z = TextRenderer.MeasureText(text, _f);
                        Point fp = new Point(inBound.Left + inBound.Width / 2 - z.Width / 2, outBound.Top - z.Height);
                        e.DrawString(text, _f, new SolidBrush(_fontColor), fp);
                        //TextEffectHelper.HaloEffect(_f, new SolidBrush(_fontColor), e, fp, text);

                    }
                    if ( _markingImage == MarkingImage.IMAGE)
                    {
                        if (_collection[i].Image != null)
                        {
                            e.DrawImage(_collection[i].Image, new Rectangle(inBound.Left + inBound.Width / 2 - 6, outBound.Top - z.Height - 14, 12, 12),
                                        new Rectangle(0, 0, _collection[i].Image.Width, _collection[i].Image.Height), GraphicsUnit.Pixel);

                        }
                    }

                }
            }
            e.Restore(state);
        }

        private void NearestAngle()
        {
            //we use division to calculate the nearest angle
            //Console.Write("Nearest angle = " + _currentAngle);
            _currentAngle = ((int)(_currentAngle / _distance)) * _distance;
           // Console.WriteLine(" to = " + _currentAngle);

        }

        private int PointerToValue(float angle)
        {
            /*
             * rotate 0 : 120 collection value=0
             * rotate 1 : 120 collection value=1
             * rotate 2 : 120 collection value=2
             */
            int index = (int)( angle / _distance) - 1;
            if (index < 0)
            {
                index = _collection.Count - 1;
            }
            return (int)(_collection[index].Value);
        }



    }
}
