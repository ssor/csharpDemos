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
    public class ThermoPanel
    {
        public enum Marking { CONT, NONE, LINE, BOTH };
        public enum Alignment { left, right, center };
        public enum Flip { Left, right };
        private int _x, _y, _width, _height;
        private Control _parent;
        private Rectangle _clientRect ;
        private Timer _timer = null;
        private MeterLabelCollection _collection = null;
        private int _interval = 1;
        private int _range = 0;
        private RoundedRectangle rect;
        private Color _indicator = Color.DarkBlue;
        //this keep the amount target to reach
        private float _target = 0;


        //configurable
        private Font _font = new Font(FontFamily.GenericMonospace, 7);
        private int _meterWidth = 6;
        private Flip _flipMarking = Flip.right;
        private Alignment _alignment = Alignment.left;
        private Marking _marking = Marking.BOTH;
        private int _displayValue;
        private bool _topDown = false;
        private float yIndex = 0;
        private bool _firstrun = true;
        private bool _upDirection = true;
        private Color _labelFontColor = Color.Black;

        public Color LabelFontColor
        {
            get { return _labelFontColor; }
            set
            {
                if (_labelFontColor != value)
                {
                    _labelFontColor = value;
                }
            }
        }


        public Font LabelFont
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

        public Color IndicatorColor
        {
            get { return _indicator; }
            set
            {
                if (_indicator != value)
                {
                    _indicator = value;
                }
            }
        }
        
        public Flip FlipMarking
        {
            get { return _flipMarking; }
            set { _flipMarking = value; }
        }

        public Alignment Align
        {
            get { return _alignment; }
            set { _alignment = value; }
        }

        public Marking MarkingStyle
        {
            get { return _marking; }
            set { _marking = value; }
        }

        public int DisplayValue
        {
            get { return _displayValue; }
            set
            {
                //to set the display value upon first start
                if (_firstrun && _collection.Count > 1)
                {
                    _displayValue = (int)_collection[0].Value;
                    _firstrun = false;
                }
                if (_displayValue != value)
                {
                    if (_collection.Count > 0)
                    {
                        MeterLabel m1 = _collection[0];
                        MeterLabel m2 = _collection[_collection.Count - 1];
                        if (_topDown)
                        {
                            if (value >= m1.Value && value <= m2.Value)
                            {
                                _displayValue = value;
                            }
                            else if (value < m1.Value)
                            {
                                _displayValue = (int)m1.Value;
                            }
                            else if (value > m2.Value)
                            {
                                _displayValue = (int)m2.Value;
                            }
                        }
                        else
                        {
                            if (value <= m1.Value && value >= m2.Value)
                            {
                                _displayValue = value;
                            }
                            else if (value > m1.Value)
                            {
                                _displayValue = (int)m1.Value;
                            }
                            else if (value < m2.Value)
                            {
                                _displayValue = (int)m2.Value;
                            }
                        }
                        int step = calculateStepping(_displayValue);
                        float curTarget;
                        if (step > 0)
                        {
                            float range = rect.Height / _collection.Count;
                            float ratio = Math.Abs((float)(_displayValue - _collection[step - 1].Value) / (float)(_collection[step].Value - _collection[step - 1].Value));
                            curTarget = range * ratio + step * range;
                        }
                        else
                        {
                            curTarget = 0;
                        }
                        if (_target < curTarget)
                        {
                            _upDirection = true;
                        }
                        else
                        {
                            _upDirection = false;
                        }
                        _target = curTarget;
                        //TODO remove the timer
                        yIndex = _target;
                        _timer.Start();

                    }
                }
            }
        }
        

        public MeterLabelCollection Label
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

        public ThermoPanel(Control parent)
        {
            _parent = parent;
            InternalConstruct(0,0,10,10);
        }

        public ThermoPanel(Rectangle location, Control parent)
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
            int ox = _clientRect.X + _clientRect.Width / 2 - _meterWidth / 2;
            int oy = _clientRect.Y + 4;
            Rectangle oRect = new Rectangle(ox, oy, _meterWidth, _clientRect.Height - 8);
            rect = new RoundedRectangle(oRect, 4);
            
            if (_collection == null)
            {
                _collection = new MeterLabelCollection();
                _collection.Delete += new OnRemove(_collection_Delete);
                _collection.Insert += new OnInsert(_collection_Insert);
                _collection.Set += new OnSet(_collection_Set);
            }
            if (_timer == null)
            {
                _timer = new Timer();
                _timer.Interval = _interval;
                _timer.Tick += new EventHandler(_timer_Tick);
            }

        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if ( _upDirection )
            {
                if (yIndex >= _target)
                {
                    _timer.Stop();
                }
                else
                {
                    yIndex += 1;
                }
            }
            else
            {
                if (yIndex <= _target)
                {
                    _timer.Stop();
                }
                else
                {
                    yIndex -= 1;
                }
            }
           //     Console.WriteLine(" target = " + _target + " yIndex = " + yIndex);
                _parent.Invalidate();
           
        }

        void _collection_Set(object sender, int index)
        {
            if (index == 0)
            {
                if (_topDown)
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
                if (_topDown)
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

        void _collection_Insert(object sender, int index)
        {
            if ( index == 1 )
            {
                if (_collection[index - 1].Value > _collection[index].Value)
                {
                    _topDown = false;
                }
                else if (_collection[index - 1].Value < _collection[index].Value)
                {
                    _topDown = true;
                }
                else
                {
                    throw new Exception("Value of Meterlabel " + _collection[index].Desc + " at index "
                                         + index + " is same as previous Meterlabel");
                }
            }
            if (index > 1)
            {
                if (_topDown)
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
            if (_collection.Count >= 1)
            {
                _range = (int)Math.Abs(_collection[0].Value - _collection[index].Value);
            }
            //Console.WriteLine("Range = " + _range);
           
        }

        void _collection_Delete(object sender, int index)
        {
            
        }

        int calculateStepping(int val)
        {
            int stepNumber = 0;
            if (_topDown)
            {

                foreach (MeterLabel m in _collection)
                {
                    if (val <= m.Value)
                    {
                        break;
                    }
                    stepNumber++;
                }
            }
            else
            {
                foreach (MeterLabel m in _collection)
                {
                    if (val >= m.Value)
                    {
                        break;
                    }
                    stepNumber++;
                }

            }
            return stepNumber;

        }


        public void Draw(Graphics e)
        {
            int diff = _clientRect.X + _clientRect.Width / 2 - _meterWidth / 2  -_clientRect.X;
            if (_collection.Count > 1)
            {
                int temp = rect.Height / _collection.Count;
                if (temp >= 1)
                {
                    _interval = temp;
                }
            }
            else
            {
                _interval = rect.Height;
            }
            GraphicsState state = e.Save();
            if (_alignment == Alignment.right)
            {
                e.TranslateTransform(diff, 0);
            }
            else if (_alignment == Alignment.left)
            {
                e.TranslateTransform(-diff, 0);
            }

            e.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            int i = 1;
            RectangleF cRect;
            RectangleF pictRect;
            PointF p1, p2;
            foreach (MeterLabel ml in _collection)
            {
                Size s = TextRenderer.MeasureText(ml.Desc,_font);
                if (_flipMarking == Flip.Left)
                {
                     p1 = new PointF(rect.Left , rect.Top + rect.Height - (i * _interval));
                     p2 = new PointF(rect.Left - 5, rect.Top +  rect.Height - (i * _interval));
                     cRect = new RectangleF(rect.Left - 5, rect.Top + rect.Height - (i * _interval), 5, _interval);
                     pictRect = new RectangleF(rect.Left - s.Width - 17, rect.Top + rect.Height - (i * _interval) - 6, 12, 12);
                     e.DrawString(ml.Desc, _font, new SolidBrush(LabelFontColor), rect.Left - s.Width - 5, rect.Top + rect.Height - (i * _interval) - s.Height / 2);
            
                }
                else
                {
                    p1 = new PointF(rect.Left + rect.Width, rect.Top +  rect.Height - (i * _interval));
                    p2 = new PointF(rect.Left + rect.Width + 5, rect.Top +  rect.Height - (i * _interval));
                    cRect = new RectangleF(rect.Left + rect.Width, rect.Top + rect.Height - (i * _interval), 5, _interval);
                    pictRect = new RectangleF(rect.Left + rect.Width + s.Width , rect.Top + rect.Height - (i * _interval) - 6, 12, 12);
                    e.DrawString(ml.Desc, _font, new SolidBrush(LabelFontColor), rect.Left + rect.Width + 5, rect.Top + rect.Height - (i * _interval) - s.Height / 2);

                }
                if (ml.Image != null)
                {
                    e.DrawImage(ml.Image, pictRect, new RectangleF(0, 0, ml.Image.Width, ml.Image.Height), GraphicsUnit.Pixel);
                }
                if (_marking == Marking.BOTH || _marking == Marking.CONT)
                {
                    e.FillRectangle(new SolidBrush(Color.FromArgb(200,ml.MainColor)), cRect);
                   
                }
                if (_marking == Marking.BOTH || _marking == Marking.LINE)
                {
                    e.DrawLine(new Pen(_labelFontColor), p1, p2);
                }
                i++;
            }
            Pen p = new Pen(Color.DarkGray,2);
            e.FillPath(Brushes.White, rect.GetGraphicsPath());
            e.FillRectangle(new SolidBrush(_indicator), new RectangleF((float)rect.Left,
                            (float)(rect.Top + rect.Height - yIndex),
                            (float)rect.Width, yIndex));
            e.DrawPath(p, rect.GetGraphicsPath());
            e.Restore(state);
          
      
        }
    }
}
