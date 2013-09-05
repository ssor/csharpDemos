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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using NextUI.Common;
using NextUI.Collection;

namespace NextUI.Display
{
    /// <summary>
    /// a control that simulate a thermometer
    /// </summary>
    /// 
    public partial class ThermoDisplay : UserControl
    {

        private ThermoPanel _panel = null;
        private Bitmap _map = null;
        private Image _backgrdImage = null;
        private Font _font = new Font(FontFamily.GenericMonospace, 7);
        private Color _indicatorColor= Color.DarkBlue;
      //  private int _meterWidth = 6;
        private ThermoPanel.Flip _flipMarking = ThermoPanel.Flip.right;
        private ThermoPanel.Alignment _alignment = ThermoPanel.Alignment.left;
        private ThermoPanel.Marking _marking = ThermoPanel.Marking.BOTH;
        private Color _backColor = Color.LightGray;
        private int _displayValue = 0;
        private Color _labelFontColor = Color.Black;

        /// <summary>
        /// The color of the Font label
        /// </summary>
        [
           Category("ThermoDisplay"),
           Description("The Color of Font ") 
        ]
        public Color LabelFontColor
        {
            get { return _labelFontColor; }
            set
            {
                if (_labelFontColor != value)
                {
                    _labelFontColor = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// The Value to be shown in the control , the minimum and maximum depends on the 
        /// "Value" property of Meterlabel. 
        /// </summary>
        [
          Category("ThermoDisplay"),
          Description("The Value to be display ")
        ]
        public int Number
        {
            set
            {
                if (_displayValue != value)
                {
                    _displayValue = value;
                    this.Invalidate();
                }
            }
            get { return _displayValue; }
        }
        /// <summary>
        /// Use this to set the background , for example , you can set 
        /// the background to  solid color by setting this image to 
        /// solod color image
        /// </summary>
        [
           Category("ThermoDisplay"),
           Description("The BackColor of the Control ")
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
        /// The direction of the label , it can be left, which means the label
        /// will display on the left hand side , or right ,
        /// which mean label will be displayed on the right hand side.
        /// </summary>
        [
            Category("ThermoDisplay")
        ]
        public ThermoPanel.Flip Flip
        {
            get { return _flipMarking; }
            set
            {
                if (_flipMarking != value)
                {
                    _flipMarking = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// This property is used to set the marking.
        /// Line will display a line as a marking using the color of the font
        /// Cont will provide a color shade between 2 marking using the 
        /// "color" property of MeterLabel
        /// Both will display Line as well as the shade
        /// None will not display anything
        /// </summary>
        [
          Category("ThermoDisplay")
        ]
        public ThermoPanel.Marking Marking
        {
            get { return _marking; }
            set
            {
                if (_marking != value)
                {
                    _marking = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// Set the alignment , it can be left ,right or center
        /// </summary>
        [
          Category("ThermoDisplay"),
          Description("The Label Collection ")
        ]
        public ThermoPanel.Alignment Alignment
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
        /// The font that will be used to display the label
        /// </summary>
        [
            Category("ThermoDisplay"),
            Description("The Font of the Label ")
        ]
        public Font LabelFont
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
        /// The color of the indicator
        /// </summary>
        [
           Category("ThermoDisplay"),
           Description("The Color of the indicator ")
        ]
        public Color IndicatorColor
        {
            get { return _indicatorColor ; }
            set
            {
                if (_indicatorColor != value)
                {
                    _indicatorColor = value;
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
          Category("ThermoDisplay"),
          Description("The Background Image ")
        ]

        public Image BackGroundImage
        {
            get { return _backgrdImage; }
            set
            {
                if (_backgrdImage != value)
                {
                    _backgrdImage = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// return the collection of MeterLabel
        /// </summary>
        [
           Category("ThermoDisplay"),
           Description("The Label Collection ")
        ]
        public MeterLabelCollection Label
        {
            get
            {
                return _panel.Label;
                
            }
        }

        public ThermoDisplay()
        {
            InitializeComponent();
            _panel = new ThermoPanel(this);
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
            g.Clip = new Region(rect.GetGraphicsPath());
            Pen p = new Pen(Color.DarkGray);
            LinearGradientBrush b = new LinearGradientBrush(rect.ClientRect, Color.WhiteSmoke, _backColor, 90f);
            g.FillPath(b, rect.GetGraphicsPath());
            g.DrawPath(p, rect.GetGraphicsPath());
            if (_backgrdImage != null)
            {
                g.DrawImage(_backgrdImage, rect.ClientRect);
            }
            rect.Shrink(10);
            _panel.Left = rect.ClientRect.Left;
            _panel.Top = rect.ClientRect.Top;
            _panel.Width = rect.ClientRect.Width;
            _panel.Height = rect.ClientRect.Height;
            _panel.Align = _alignment;
            _panel.FlipMarking = _flipMarking;
            _panel.MarkingStyle = _marking;
            _panel.DisplayValue = _displayValue;
            _panel.IndicatorColor = _indicatorColor;
            _panel.LabelFont = _font;
            _panel.LabelFontColor = _labelFontColor;

            _panel.Draw(g);
            e.Graphics.DrawImage(_map, new Point(0, 0));
                
        }
    }
}
