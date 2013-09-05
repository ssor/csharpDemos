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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using NextUI.Common;
using NextUI.Collection;

namespace NextUI.Display
{
    /// <summary>
    /// A control that simulate an analog counter
    /// the analog counter can count forward and backward
    /// </summary>
    public partial class AnalogCounter : UserControl
    {
        public enum Alignment { Left, Right, Center };

        /// <summary>
        /// Set this to true will provides a scrolling effect when counter
        /// change from 1 number to another. Not recommended if the counter 
        /// has rapid changing value
        /// Set this to false will provides a faster performance
        /// </summary>
        [
           Category("AnalogCounter"),
           Description("Enable / Disable ScrollingEffect")
        ]

        public bool ScrollEffect
        {
            get { return _scrollEffect; }
            set
            {
                if (_scrollEffect != value)
                {
                    _scrollEffect = value;
                }
            }
        }
        /// <summary>
        /// This will determined the number of panel that will be displayed
        /// If set to 3 , there will be 3 panel, which mean the counter
        /// will support 0 to 999 , anyrhing beyong 999 will be set back to 0.
        /// </summary>
        [
           Category("AnalogCounter"),
           Description("The number of Panel to be shown")
        ]
        public int CounterNumber
        {
            get { return _numCounter; }
            set
            {
                int val = value;
                if (_numCounter > val)
                {
                    for (int i = 0; i < _numCounter - val; i++)
                    {
                        _collection.Remove();
                    }
                }
                else if (_numCounter < val)
                {
                    for (int i = 0; i < val - _numCounter; i++)
                    {
                        RollingCounter c = new RollingCounter(this);
                        _collection.Add(c);
                    }
                }
                _numCounter = val;
            }
        }
        /// <summary>
        /// This determined the value that will be displayed
        /// </summary>

        [
           Category("AnalogCounter"),
           Description("The value to be displayed"),
           DefaultValue(0)
        ]
        public Double Number
        {
            get { return _number; }
            set
            {
                if (_number != value)
                {
                    bool forward = true;
                    Double max = Math.Pow(10, _numCounter) - 1;
                    if (_number > value)
                    {
                        forward = false;
                    }
                    Double shown = NormalizeCount(value);
                    string cnum = Convert.ToString(shown);
                    RollingCounter counter;
                    for (int i = 0; i < cnum.Length; i++)
                    {
                        counter = (RollingCounter)_collection[_collection.Count + i - cnum.Length];
                        counter.Forward = forward;
                        counter.Number = Translate(cnum[i]);
                    }
                    //set the remaining count = 0 
                    for (int i = cnum.Length  ; i < _collection.Count ; i++)
                    {
                        counter = (RollingCounter)_collection[i - cnum.Length];
                        counter.ScrollEffect = _scrollEffect;
                        counter.Number = RollingCounter.Counter.Zero;
                    }
                    _number = shown;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// The font to be displayed on each sub panel .
        /// the size of the font will be ignored and is calculated based on 
        /// the size of the sub panel
        /// </summary>

        [
           Category("AnalogCounter"),
           Description("The Font to be displayed")
        ]
        public Font DisplayFont
        {
            get { return _font; }
            set
            {
                _font = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Return tha sub panel object , you can use this to set up the color
        /// </summary>

        [
           Category("AnalogCounter"),
           Description("Return the panel of counter")
        ]
        public CounterCollection Panels
        {
            get { return _collection; }
        }

        /// <summary>
        /// Set the background color of the control, the color is used to 
        /// paint the back ground as a linear gradient brush
        /// to fill a solid color , consider using a image
        /// </summary>
        [
           Category("AnalogCounter"),
           Description("The BackGround Color of the control")
        ]
        ///The color of the shading 
        public Color MainColor
        {
            get { return _backcolor; }
            set { 
                _backcolor = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Determined the location of the counter
        /// you can set this to left , center or right
        /// </summary>
        [
            Category("AnalogCounter"),
            Description("The Alignment of the counter")
        ]
        public Alignment alignment
        {
            get { return _alignment; }
            set
            {
                if (_alignment != value)
                {
                    _alignment = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// This is used to set up a analog counter .
        /// </summary>
        [
           Category("AnalogCounter"),
           Description("BackGroundImage")
        ]
        
        public Image FrontImage
        {
            get { return _backimage; }
            set {
                _backimage = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// this determined the edge from the side , 
        /// </summary>
        [
            Category("AnalogCounter"),
            Description("Minimum distance from horizontal edge, Will not exceed half the width of control")
        ]

        public int MinumumDistanceFromH
        {
            get { return _edgeOffsetFromX; }
            set
            {
                if (_edgeOffsetFromX != value)
                {
                    if (2 * value < Width / 2 || value >= 7 )
                    {
                        _edgeOffsetFromX = value;
                        this.Invalidate();
                    }
                }
            }
        }


        private Color _color = Color.Black;
        private Color _counterColor = Color.Khaki;
        private bool _scrollEffect = false;
        private Alignment _alignment = Alignment.Right;
        private Image _backimage = null;
        private Color _backcolor = Color.LightGray;
        private Bitmap _map = null;
        private Font _font = new Font(FontFamily.GenericMonospace, 5);
        private int _numCounter = 0;
        private Double _number = 0;
        private int _minimumWidth = 10;
        private int _edgeOffsetFromX = 20;// the minumum distance from X;
        private int _edgeOffsetFromY = 10;//the minimum distance from Y;
        private CounterCollection _collection = new CounterCollection();
        public AnalogCounter()
        {
            InitializeComponent();
            this.CounterNumber = 6;
          
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020;
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
       
        }

        protected override void OnPaint(PaintEventArgs e)
        {
           
            RoundedRectangle rect = new RoundedRectangle(0, 0,
                                                         this.Width, this.Height, 10);
            if (_map == null)
            {
                _map = new Bitmap(this.Width, this.Height);
            }
            if ( _font == null)
            {
                _font = new Font(FontFamily.GenericMonospace,5);
            }
            Graphics g = Graphics.FromImage(_map);
            rect.Shrink(5);
            Border.Border border = new Border.Border3D();
            border.DrawBorder(g, rect.GetGraphicsPath());
            Pen p = new Pen(Color.DarkGray);
            LinearGradientBrush b = new LinearGradientBrush(rect.ClientRect, Color.WhiteSmoke, _backcolor, 90f);
            g.FillPath(b, rect.GetGraphicsPath());
            g.DrawPath(p, rect.GetGraphicsPath());
            GraphicsState state = g.Save();
            g.SetClip(rect.GetGraphicsPath());
            //Fill in backImgae
            if (_backimage != null)
            {
                g.DrawImage(_backimage, rect.ClientRect,
                            new Rectangle(0, 0, _backimage.Width, _backimage.Height), GraphicsUnit.Pixel);
            }
            g.Restore(state);
       
            int i = 0;
            int width = CalculateWidth();
            foreach ( RollingCounter dispanel  in _collection )
            {
                if (_alignment == Alignment.Center)
                {
                    dispanel.Left = i * width + _edgeOffsetFromX;
                }
                else if (_alignment == Alignment.Left)
                {
                    dispanel.Left = i * width + 7;
                }
                else
                {
                    dispanel.Left = i * width + 2 * _edgeOffsetFromX - 7;
                }
                dispanel.Top = _edgeOffsetFromY;
                dispanel.Width = width;
                dispanel.Height = this.Height - 2 * _edgeOffsetFromY;
                
                dispanel.Font = _font;
                /*dispanel.FontColor = _color;
                dispanel.MainColor = _counterColor;*/
                dispanel.ScrollEffect = _scrollEffect;
                dispanel.Draw(g);
                i++;

            }
            g.Dispose();
            e.Graphics.DrawImage(_map, new Point(0, 0));

        }

        private int CalculateWidth()
        {
            if ((this.Width - 2 * _edgeOffsetFromX) / _numCounter < _minimumWidth)
            {
                return _minimumWidth;
            }
            else
            {
                return (this.Width - 2 * _edgeOffsetFromX) / _numCounter;
            }
        }

        private Double NormalizeCount(Double number)
        {
            //current maximum support number is 10*(numbercounter) - 1;
            Double max = Math.Pow(10, _numCounter);
           // Console.WriteLine("Max = " + max);
            Double result  = number;
            if (number < 0)
            {
                //if number < 0 , set it to maximum
                result = max - 1;
            }
            while (result >= max)
            {
                result -= max;
            }
            return result;
        }

        private RollingCounter.Counter Translate(char i)
        {
            switch (i)
            {
                case '1':
                    return RollingCounter.Counter.One;
                case '2':
                    return RollingCounter.Counter.Two;
                case '3':
                    return RollingCounter.Counter.Three;
                case '4':
                    return RollingCounter.Counter.Four;
                case '5':
                    return RollingCounter.Counter.Five;
                case '6':
                    return RollingCounter.Counter.Six;
                case '7':
                    return RollingCounter.Counter.Seven;
                case '8':
                    return RollingCounter.Counter.Eight;
                case '9':
                    return RollingCounter.Counter.Nine;
                default:
                    return RollingCounter.Counter.Zero;
            }
        }

 
    }
}
