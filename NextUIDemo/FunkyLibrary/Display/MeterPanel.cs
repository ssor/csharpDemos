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
using System.Windows.Forms;
using NextUI.Common;
using NextUI.Collection;


namespace NextUI.Display
{
    public class MeterPanel
    {
        public enum Marking { NONE, LINE, CONT };
        private int _x, _y, _width, _height;
        private MeterLabelCollection _collection = null;
        private Control _parent;
        private float _interval;
        private Rectangle _clientRect ;
        private PointF _center;

        //to be configured
        private Color _outlineColor = Color.Red;
        //Angle measure from positive X
        private  float _startGapAngle = 35;
        private  float _sweepAngle = 30;
        private Marking _marking = Marking.CONT;
        private Font _font = new Font(FontFamily.GenericMonospace, 8);
        private bool _flowLeftToRight = true;
        private float _target;
        private float _curAngle = 0;
        private float _displayValue;
        private Timer _timer;
        private bool _upDirection = true;
        private Color _pointerColor = Color.Red;
        private Color _pointerHandleColor = Color.Black;
        private Color _fontColor = Color.Black;
        private bool _border = true;

        public bool InnerBoderRing
        {
            get { return _border; }
            set { _border = value; }
        }

        public Color FontColor
        {
            get { return _fontColor; }
            set { _fontColor = value; }
        }

        public Font DisplayFont
        {
            get { return _font; }
            set
            {
                if (_font != value)
                {
                    _font = value;
                }
            }
        }

        public Color PointerColor
        {
            set { _pointerColor = value; }
            get { return _pointerColor; }
        }

        public Color PointerHandleColor
        {
            set { _pointerHandleColor = value; }
            get { return _pointerHandleColor; }
        }

        public float DisplayValue
        {
            get { return _displayValue; }
            set           {
                if (_displayValue != value)
                {
                    if (_collection.Count > 0)
                    {
                        if (_flowLeftToRight)
                        {
                            if (value >= _collection[0].Value && value <= _collection[_collection.Count - 1].Value)
                            {
                                _displayValue = value;
                            }
                            else if (value < _collection[0].Value)
                            {
                                _displayValue = _collection[0].Value;
                            }
                            else
                            {
                                _displayValue = _collection[_collection.Count - 1].Value;
                            }
                        }
                        else
                        {
                            if (value <= _collection[0].Value && value >= _collection[_collection.Count - 1].Value)
                            {
                                _displayValue = value;
                            }
                            else if (value > _collection[0].Value)
                            {
                                _displayValue = _collection[0].Value;
                            }
                            else
                            {
                                _displayValue = _collection[_collection.Count - 1].Value;
                            }
                            
                        }
                        float _tempTarget = CalculateAngle(_displayValue);
                        if (_tempTarget >= _target)
                        {
                            _upDirection = true;
                        }
                        else
                        {
                            _upDirection = false;
                        }
                        _target = _tempTarget;
                        //Using another timer cause problem
                        //will remove timer in future
                        //now just set the value to work around

                        _curAngle = _target;
                        
                        _timer.Start();

                    }
                }
            }
        }

        public float StartAngle
        {
            get { return _startGapAngle; }
            set
            {
                if (_startGapAngle != value)
                {
                    if (_sweepAngle + _startGapAngle < 350f)
                    {
                        _startGapAngle = value;
                        UpdateInterval();
                    }
                }
            }
        }

        public float SweepAngle
        {
            get { return _sweepAngle; }
            set
            {
                if (_sweepAngle != value)
                {
                    if (_sweepAngle  + _startGapAngle < 350f)
                    {
                        _sweepAngle = value;
                        UpdateInterval();
                    }
                }
            }
        }


        public MeterLabelCollection Labels
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

        public MeterPanel( Control parent)
        {
            _parent = parent;
            InternalConstruct(0, 0, 30, 30);
        }

        public MeterPanel(Rectangle location, Control parent)
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
            _target = _startGapAngle + _sweepAngle;
            _curAngle = _target;
            if (_collection == null)
            {
                _collection = new MeterLabelCollection();
                _collection.Insert += new OnInsert(_collection_Insert);
                _collection.Delete += new OnRemove(_collection_Delete);
                _collection.Set += new OnSet(_collection_Set);
            }
            if (_timer == null)
            {
                _timer = new Timer();
                _timer.Interval = 1;
                _timer.Tick += new EventHandler(_timer_Tick);
            }
                

        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if (_upDirection)
            {
                if (_curAngle >= _target)
                {
                    _timer.Stop();
                }
                _curAngle++;
            }
            else
            {
                if (_curAngle <= _target)
                {
                    _timer.Stop();
                }
                _curAngle--;
            }
            //Console.WriteLine("curangle = " + _curAngle + "  target=" + _target);
            _parent.Invalidate();
        }

        void _collection_Set(object sender, int index)
        {
            if (index == 0)
            {
                if (_flowLeftToRight)
                {
                    if (_collection[index].Value >= _collection[index + 1].Value)
                    {
                        throw new Exception("Value of Meterlabel " + _collection[index].Desc + " at index "
                                       + index + " is bigger or equal than Next label");
                    }
                }
                else
                {
                    if (_collection[index].Value <= _collection[index + 1].Value)
                    {
                        throw new Exception("Value of Meterlabel " + _collection[index].Desc + " at index "
                                     + index + " is lesser or equal than Next label");
                    }

                }
            }
            else
            {
                if (_flowLeftToRight)
                {
                    if (_collection[index].Value <= _collection[index - 1].Value)
                    {
                        throw new Exception("Value of Meterlabel " + _collection[index].Desc + " at index "
                                       + index + " is lesser or equal than Next label");
                    }
                }
                else
                {
                    if (_collection[index].Value >= _collection[index - 1].Value)
                    {
                        throw new Exception("Value of Meterlabel " + _collection[index].Desc + " at index "
                                     + index + " is bigger or equal than Next label");
                    }

                }

            }

        }

        void _collection_Delete(object sender, int index)
        {
            UpdateInterval();
           
        }

        void _collection_Insert(object sender, int index)
        {
            if (index == 1)
            {
                if (_collection[index - 1].Value > _collection[index].Value)
                {
                    _flowLeftToRight = false;
                }
                else if (_collection[index - 1].Value < _collection[index].Value)
                {
                    _flowLeftToRight = true;
                }
                else
                {
                    throw new Exception("Value of Meterlabel " + _collection[index].Desc + " at index "
                                         + index + " is same as previous Meterlabel");
                }
            }
            if (index > 1)
            {
                if (_flowLeftToRight)
                {
                    if (_collection[index - 1].Value >= _collection[index].Value)
                    {
                        throw new Exception("Value of Meterlabel " + _collection[index].Desc + " at index "
                                        + index + " is not in ascending order");
                    }
                }
                else
                {
                    if (_collection[index - 1].Value <= _collection[index].Value)
                    {
                        throw new Exception("Value of Meterlabel " + _collection[index].Desc + " at index "
                                        + index + " is not in descending order");
                    }
                }
            }
      
            UpdateInterval();


        }

        private float CalculateAngle(float value)
        {
            //Check where this value is 
            int step = 0;
            foreach (MeterLabel ml in _collection)
            {
                if (_flowLeftToRight)
                {
                    if (value <= ml.Value)
                    {
                        break;
                    }

                }
                else
                {
                    if (value >= ml.Value)
                    {
                        break;
                    }
                }
                step++;
            }
            // now calculate how many angle need to go there
            float angle;
            if (step > 0)
            {
                float ratio = Math.Abs((value - _collection[step - 1].Value) / (_collection[step].Value - _collection[step - 1].Value));
                angle = _interval * ( step - 1)  + (_interval * ratio) + _sweepAngle + _startGapAngle;
            }
            else
            {
                angle = _sweepAngle + _startGapAngle;
            }
            return angle;

        }

        private void UpdateInterval()
        {
            if (_collection != null)
            {
                if (_collection.Count > 1)
                {
                    _interval = (360f - _sweepAngle) /( _collection.Count - 1);
                }
                else
                {
                    _interval = (360f - _sweepAngle);
                }
            }
            _target = _startGapAngle + _sweepAngle;
            _curAngle = _target;
            //Console.WriteLine("UpdateInterval =" + _collection.Count  + " : " + _interval);
        }

        public void Draw(Graphics e)
        {
            e.SmoothingMode = SmoothingMode.HighQuality;
            Rectangle ori = new Rectangle(_clientRect.X, _clientRect.Y, _clientRect.Width, _clientRect.Width);
            _center = new PointF(ori.X + ori.Width / 2, ori.Top + ori.Height / 2);
            Rectangle r = Helper.RectangleHelper.Shrink(ori,1);
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(r);
            if (_border)
            {
                Border.Border border = new Border.Inner3D();
                border.DrawBorder(e, gp);
            }
            Rectangle r2 = Helper.RectangleHelper.Shrink(r, 15);
            Pen p = new Pen(_outlineColor);
            GraphicsPath gp2 = new GraphicsPath();
            gp2.AddArc(r2, _startGapAngle + _sweepAngle, 360 -  _sweepAngle);
            gp2.Reverse();
            Rectangle r3 = Helper.RectangleHelper.Shrink(r2, 5);
            PointF out1 = Helper.AlgorithmHelper.LocationToCenter(_center, r2.Width / 2, _startGapAngle);
            PointF in1 = Helper.AlgorithmHelper.LocationToCenter(_center, r3.Width / 2, _startGapAngle);
            PointF out2 = Helper.AlgorithmHelper.LocationToCenter(_center, r2.Width / 2, _startGapAngle + _sweepAngle);
            PointF in2 = Helper.AlgorithmHelper.LocationToCenter(_center, r3.Width / 2, _startGapAngle + _sweepAngle);
            gp2.AddLine(out2, in2);
            gp2.AddArc( r3, _startGapAngle + _sweepAngle, 360 - _sweepAngle);
            gp2.AddLine(in1, out1);
            gp2.CloseAllFigures();
           
            DrawMarking(e, r3, r2);
            PointF point = Helper.AlgorithmHelper.LocationToCenter(_center, 50, _curAngle);
            e.DrawLine(new Pen(_pointerColor, 2), _center, point);
            Rectangle r4 = Helper.RectangleHelper.Shrink(r3, 30);
            LinearGradientBrush lb = new LinearGradientBrush(r4, _pointerHandleColor, Color.LightGray, 45f);
            e.FillEllipse(lb, r4);
            e.DrawPath(p, gp2);
            //Draw the pointer
            //Rectangle ptr = Helper.RectangleHelper.Shrink(r4, 20);
            //e.DrawEllipse(new Pen(Color.Black), ptr);
           


          

        }

        private void DrawMarking(Graphics e, Rectangle inBound , Rectangle outBound)
        {
            GraphicsState state = e.Save();
            e.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            float startAngle = _sweepAngle + _startGapAngle;
            startAngle = Helper.AlgorithmHelper.NormalizeDegree(270f, startAngle);
            Matrix m = new Matrix();
            //Console.WriteLine("Interval =" + _interval);

            int i = _collection.Count - 1;
            foreach (MeterLabel ml in _collection)
            {
             //   Console.WriteLine(ml.Desc + " : " + startAngle);
                Size z = TextRenderer.MeasureText(ml.Desc, _font);
                m.RotateAt(startAngle, _center);
                e.Transform = m;
                e.DrawString(ml.Desc, _font, new SolidBrush(_fontColor),
                    new PointF(outBound.Left + outBound.Width / 2 - z.Width / 2,
                               outBound.Top - z.Height));
                if (_marking == Marking.LINE)
                {
                    PointF p1 = new PointF(_center.X, outBound.Top);
                    PointF p2 = new PointF(_center.X, inBound.Top);
                    Pen p = new Pen(ml.MainColor, 2);
                    e.DrawLine(p, p1, p2);
                    p.Dispose();
                }
                else if (_marking == Marking.CONT)
                {
                    if (i > 0)
                    {
                        GraphicsPath gp = new GraphicsPath();
                        PointF p1 = new PointF(_center.X, outBound.Top);
                        PointF p2 = new PointF(_center.X, inBound.Top);
                        gp.AddArc(outBound, 270, _interval);
                        gp.Reverse();
                        gp.AddLine(p1, p2);
                        gp.AddArc(inBound, 270, _interval);
                        float Angle = Helper.AlgorithmHelper.NormalizeDegreeFromX(270 + _interval);
                        PointF p3 = Helper.AlgorithmHelper.LocationToCenter(_center, outBound.Width / 2, Angle);
                        PointF p4 = Helper.AlgorithmHelper.LocationToCenter(_center, inBound.Width / 2, Angle);
                        gp.AddLine(p4, p3);
                        gp.CloseFigure();
                        e.FillPath(new SolidBrush(ml.MainColor), gp);
                    }

                }
              
                startAngle = _interval;
                i--;

            }
            e.Restore(state);
        }
    }
    
}
