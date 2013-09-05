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
    /// Provide a speedometer like control
    /// </summary>
    public partial class PointerMeter : UserControl
    {
        private Bitmap _map = null;
        private MeterPanel _panel = null;
        private int _displayValue = 0;
        private Image _backImage = null;
        private Color _backColor = Color.LightGray;
        private Font _font =  new Font(FontFamily.GenericMonospace, 8);
        private float _startGapAngle =  30f;
        private float _sweepAngle = 30f;
        private Color _pointerColor = Color.Red;
        private Color _pointerHandleColor = Color.Black;
        private Color _fontColor = Color.Black;
        private bool _border = true;

        /// <summary>
        /// Set to true will show a inner circle border , by default it is true
        /// </summary>
        [
           Category("PointerMeter"),
           Description("Enable the inner Boder")
        ]
        public bool InnerBorderRing
        {
            set
            {
                if (_border != value)
                {
                    _border = value;
                    this.Invalidate();
                }

            }
            get { return _border; }
        }

        /// <summary>
        /// Use this to set the color of the font ,
        /// the font is used to display the label
        /// </summary>

        [
            Category("PointerMeter"),
            Description("The Color of the Font")
        ]
        public Color LabelFontColor
        {
            set
            {
                if (_fontColor != value)
                {
                    _fontColor = value;
                    this.Invalidate();
                }

            }
            get { return _fontColor; }
        }

        /// <summary>
        /// The color of the pointer , by default it is red
        /// </summary>
        [
            Category("PointerMeter"),
            Description("The Color of the pointer")
        ]
        public Color PointerColor
        {
            set
            {
                if (_pointerColor != value)
                {
                    _pointerColor = value;
                    this.Invalidate();
                }

            }
            get { return _pointerColor; }
        }

        /// <summary>
        /// the color of the pointer base.
        /// </summary>
        [
            Category("PointerMeter"),
            Description("The Color of the pointer handle")
        ]
        public Color PointerHandleColor
        {
            set
            {
                if (_pointerHandleColor != value)
                {
                    _pointerHandleColor = value;
                    this.Invalidate();
                }
            }
            get { return _pointerHandleColor; }
        }
        
        /// <summary>
        /// Provides the starting angle of gap , measure from the positive x axis.
        /// </summary>
        [
             Category("PointerMeter"),
             Description("The Starting angle of the Gap")
        ]
        public float StartGapAngle
        {
            get { return _startGapAngle; }
            set
            {
                if (_startGapAngle != value)
                {
                    _startGapAngle = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// determined how wide the gap is in angle.
        /// </summary>
        [
            Category("PointerMeter"),
            Description("The Sweep Angle of the Gap, it determines how large the gap is")
        ]
        public float GapWidth
        {
            get { return _sweepAngle; }
            set
            {
                if (_sweepAngle != value)
                {
                    _sweepAngle = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// The font that is used to display the label
        /// </summary>
        [
            Category("PointerMeter"),
            Description("The Font of Label")
        ]
        public Font DisplayFont
        {
            get { return _font; }
            set
            {
                if (_font != value)
                {
                    _font = value;
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
           Category("PointerMeter"),
           Description("The back ground Color")
        ]
        public Color BackGrdColor
        {
            get { return _backColor; }
            set
            {
                if (_backColor != value)
                {
                    _backColor = value;
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
           Category("PointerMeter"),
           Description("The back ground Image")
        ]
        public Image BackGroundImage
        {
            get { return _backImage; }
            set
            {
                if (_backImage != value)
                {
                    _backImage = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// The Value to be shown in the control , the minimum and maximum depends on the 
        /// "Value" property of Meterlabel. 
        /// </summary>
        [
           Category("PointerMeter"),
           Description("The value  to be shown")
        ]
        public int Number
        {
            get { return _displayValue;}
            set { 
               if ( _displayValue != value)
               {
                   _displayValue = value;
                    this.Invalidate();
               }
            }
        }
        /// <summary>
        /// It returns a collection that allow user to add a meterlabel.
        /// "Image" property for this control is currently not supported .
        /// 
        /// </summary>
        [
           Category("PointerMeter"),
           Description("Labels to be added to the control")
        ]

        public MeterLabelCollection Labels
        {
            get
            {
                if (_panel != null)
                {
                    return _panel.Labels;
                }
                return null;
            }
        }

        public PointerMeter()
        {
            InitializeComponent();
            _panel = new MeterPanel(this);
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
            Pen p = new Pen(Color.WhiteSmoke);
            LinearGradientBrush b = new LinearGradientBrush(rect.ClientRect, Color.WhiteSmoke, _backColor, 90f);
            g.FillPath(b, rect.GetGraphicsPath());
            g.DrawPath(p, rect.GetGraphicsPath());
            g.Clip = new Region(rect.GetGraphicsPath());
            if (_backImage != null)
            {
                g.DrawImage(_backImage, rect.ClientRect);
            }
            g.ResetClip();
            rect.Shrink(10);
            if (_panel != null)
            {
                _panel.Left = rect.ClientRect.Left;
                _panel.Top = rect.ClientRect.Top;
                _panel.Width = rect.ClientRect.Width;
                _panel.Height = rect.ClientRect.Height;
                _panel.DisplayValue = _displayValue;
                _panel.DisplayFont = _font;
                _panel.SweepAngle = _sweepAngle;
                _panel.StartAngle = _startGapAngle;
                _panel.PointerColor = _pointerColor;
                _panel.PointerHandleColor = _pointerHandleColor;
                _panel.FontColor = _fontColor;
                _panel.InnerBoderRing = _border;
            }
            _panel.Draw(g);
            e.Graphics.DrawImage(_map, new Point(0, 0));
                

        }
    }
}
