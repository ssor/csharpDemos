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
using NextUI.Common;
using NextUI.Collection;
using System.Windows.Forms;

namespace NextUI.Display
{
    /// <summary>
    /// A class that simulate a digital panel.
    /// You can use this class to descrement or increment.
    /// </summary>
    public partial class DigitalDisplay : UserControl
    {
        public enum location { LEFT, RIGHT, CENTER, FILL };
        private Bitmap _map = null;
        private DigitalPanelCollection _collection = new DigitalPanelCollection();
        private int _numberOfPanel = 0;
        private float _valueDisplay = 0;
        private int _panelLength;
        private int _panelHeight;
        private location _location = location.CENTER;
        private Image _backgrdImage = null;
        private Color _fontColor = Color.Red;
        private Color _panelColor = Color.Black;
        private Color _backgrdColor = Color.LightGray;

   
        private bool _enableGlare = false;

        /// <summary>
        /// Enable a kind of glowing effect for the digital display
        /// </summary>
        [
           Category("DigitalDisplay"),
           Description("Enable Glare effect")
        ]
        public bool EnableGlare
        {
            get { return _enableGlare; }
            set
            {
                if (_enableGlare != value)
                {
                    _enableGlare = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// Set the color of the display
        /// </summary>
        [
            Category("DigitalDisplay"),
            Description("The Font Color")
        ]
        public Color FontColor
        {
            get { return _fontColor; }
            set
            {
                if (_fontColor != value)
                {
                    _fontColor = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// The color of each digital panel, where each panel hold 1 numerical 
        /// value 
        /// </summary>
        [
           Category("DigitalDisplay"),
           Description("The BackPanel Color")
        ]
        public Color PanelColor
        {
            get { return _panelColor; }
            set
            {
                if (_panelColor != value)
                {
                    _panelColor = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// Set the background color of the control, the color is used to 
        /// paint the back ground as a linear gradient brush
        /// to fill a solid color , consider using a image
        /// </summary>
        [
           Category("DigitalDisplay"),
           Description("The BackGround Color")
        ]
        public Color BackGrdColor
        {
            get { return _backgrdColor; }
            set
            {
                if (_backgrdColor != value)
                {
                    _backgrdColor = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// Set the alignment , it can be left , right or center
        /// </summary>
        [
           Category("DigitalDisplay"),
           Description("The Alignment of the number ")
        ]
        public location Alignment
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// Use this to set the background , for example , you can set 
        /// the background to  solid color by setting this image to 
        /// solod color image
        /// </summary>
        [
           Category("DigitalDisplay"),
           Description("The Background Image of the Control ")
        ]

        public Image BackGroundImage
        {
            get { return _backgrdImage; }
            set
            {
                _backgrdImage = value;
                this.Invalidate();
            }
        }
  
        /// <summary>
        /// The Value to be displayed . It can be floating point or negative number
        /// as well . if the number exceeded the value that can be supported by 
        /// the control, a ------ will be displayed
        /// </summary>

        [
          Category("DigitalDisplay"),
          Description("The value to be displayed")
        ]
        public float Number
        {
            get { Console.WriteLine("get = " + _valueDisplay); return _valueDisplay; }
            set
            {
                if (_valueDisplay != value)
                {
                    //    float shown = NormalizeCount(value);
                    string num = Convert.ToString(value);
              //      Console.WriteLine(num.Length + " " + value);
                    if (num.Length <= _numberOfPanel)
                    {
                        int index = num.IndexOf(".");
                        DigitalPanel panel;
                        string cnum = "";
                        if (index >= 0)
                        {
                            //the index is always calculate from the end.
                            index = num.Length - index - 2;
                            for (int i = 0; i < num.Length; i++)
                            {
                                if (num[i] != '.')
                                    cnum += num[i];

                            }
                        }
                        else
                            cnum = num;


                        for (int i = 0; i < cnum.Length; i++)
                        {
                            panel = (DigitalPanel)_collection[_collection.Count + i - cnum.Length];
                            panel.DisplayNumber = Translate(cnum[i]);
                            panel.EnableDot = false;
                        }
                        for (int i = cnum.Length; i < _collection.Count; i++)
                        {
                            panel = (DigitalPanel)_collection[i - cnum.Length];
                            panel.DisplayNumber = DigitalPanel.Number.NONE;
                        }
                        if (index >= 0)
                            ((DigitalPanel)_collection[_numberOfPanel - 1 - index]).EnableDot = true;

                        _valueDisplay = value;
                        this.Invalidate();
                    }
                    else
                    {
                        //Display "----" to show value out of range
                        DigitalPanel panel;
                        for (int i = 0; i < _collection.Count; i++)
                        {
                            panel = (DigitalPanel)_collection[i];
                            panel.EnableDot = false;
                            panel.DisplayNumber = DigitalPanel.Number.NEGATIVE;
                        }
                        this.Invalidate();
                    }
                }
                
            }
        
        }
        /// <summary>
        /// The number of panel that can be displayed . the more the panel ,the larger
        /// value will be supported 
        /// </summary>
        [
          Category("DigitalDisplay"),
          Description("The number of  digital panel")
        ]
        public int PanelNumber
        {
            get { return _numberOfPanel; }
            set
            {
                if (value >= 0)
                {
                    if (_numberOfPanel > value)
                    {
                        int diff = _numberOfPanel - value;
                        for (int i = diff; i > 0; i--)
                        {
                            _collection.Remove();
                        }
                    }
                    else if (_numberOfPanel < value)
                    {
                        int diff = value - _numberOfPanel;
                        for (int i = 0; i < diff; i++)
                        {
                            _collection.Add(new DigitalPanel(this));
                        }
                    }
                    else
                    {
                        return;
                    }
                    _numberOfPanel = value;
                    this.Invalidate();

                }
            }
        }

        public DigitalDisplay()
        {
            InitializeComponent();
            this.PanelNumber = 6;

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
            Graphics g = Graphics.FromImage(_map);
            rect.Shrink(5);
            Border.Border border = new Border.Border3D();
            border.DrawBorder(g, rect.GetGraphicsPath());
            Pen p = new Pen(Color.DarkGray);
            LinearGradientBrush b = new LinearGradientBrush(rect.ClientRect, Color.WhiteSmoke, _backgrdColor, 90f);
            g.FillPath(b, rect.GetGraphicsPath());
            g.DrawPath(p, rect.GetGraphicsPath());
            g.Clip = new Region(rect.GetGraphicsPath());
            if (_backgrdImage != null)
            {
                g.DrawImage(_backgrdImage, rect.ClientRect);
            }
            rect.Shrink(5);
            g.Clip = new Region(rect.GetGraphicsPath());
            int left = 0;
            _panelLength = rect.Width /( _numberOfPanel + 2);
            _panelHeight = rect.Height;
            if (_location == location.FILL)
            {
                _panelLength = rect.Width / (_numberOfPanel ) - 1;
                int totalLength = (_numberOfPanel * _panelLength) / 2;
                if (rect.Width / 2 > totalLength)
                {
                    left = rect.Left + (rect.Width / 2 - totalLength);
                }
            }
            else if (_location == location.LEFT)
            {
                left = rect.Left + 3;
            }
            else if (_location == location.CENTER)
            {
                int totalLength = (_numberOfPanel * _panelLength) / 2;
                if (rect.Width / 2 > totalLength)
                {
                    left = rect.Left + (rect.Width / 2 - totalLength);
                }

            }
            else
            {
                left = rect.Left + rect.Width - (_numberOfPanel * _panelLength);
            }
            int i = 0;
            
            foreach (DigitalPanel dp in _collection)
            {
                dp.Left = left + i * (_panelLength) ;
                dp.Top = rect.Top;
                dp.Width = _panelLength - 1;
                dp.Height = _panelHeight;
                dp.MainColor = _fontColor;
                dp.BackColor = _panelColor;
                dp.EnableGlare = _enableGlare;
                dp.Draw(g);

                i++;
                
            }
            e.Graphics.DrawImage(_map, new Point(0, 0));

        }

        private float NormalizeCount(float number)
        {
            Console.WriteLine(number);   
            double max = Math.Pow(10, _numberOfPanel);
            double result = number;
            if (number < 0)
            {
                result = max - 1;
            }
            while (result >= max)
            {
                result -= max;
            }
            return (float)result;
        }

        private DigitalPanel.Number Translate(char i)
        {
            switch (i)
            {
                case '1':
                    return DigitalPanel.Number.ONE;
                case '2':
                    return DigitalPanel.Number.TWO;
                case '3':
                    return DigitalPanel.Number.THREE;
                case '4':
                    return DigitalPanel.Number.FOUR;
                case '5':
                    return DigitalPanel.Number.FIVE;
                case '6':
                    return DigitalPanel.Number.SIX;
                case '7':
                    return DigitalPanel.Number.SEVEN;
                case '8':
                    return DigitalPanel.Number.EIGHT;
                case '9':
                    return DigitalPanel.Number.NINE;
                case '-':
                    return DigitalPanel.Number.NEGATIVE;
                default:
                    return DigitalPanel.Number.ZERO;
            }
        }
    }
    
}
