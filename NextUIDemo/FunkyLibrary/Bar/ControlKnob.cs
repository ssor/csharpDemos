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
using NextUI.Common;
using System.Windows.Forms;

namespace NextUI.Bar
{
    /// <summary>
    /// Rotate will be raised whenever a mouse is rotating the knob
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="value"> the value of the knob that will be generated 
    /// is depends on the property "value" of the particular MeterLabel"</param>
    public delegate void OnRotate(object sender, int value);
    /// <summary>
    /// 
    /// This Control provides a rotating knob 
    /// </summary>
    public partial class ControlKnob : UserControl
    {
        private Bitmap _map = null;
        private KnobPanel _panel;
        private Image _knobHandleImage = null;
        private Image _backImage = null;
        private KnobPanel.Marking _markingType = KnobPanel.Marking.BOTH;
        private KnobPanel.MarkingImage _markingImage = KnobPanel.MarkingImage.IMAGE;
        private Color _fontColor = Color.Black;
        private Font _font = new Font(FontFamily.GenericMonospace, 8);
        private Color _backColor = Color.LightGray;

        /// <summary>
        /// To receive rotate event whenever a mouse is use to move the knob
        /// </summary>
        public event OnRotate Rotate;

        /// <summary>
        /// Set the background color of the control, the color is used to 
        /// paint the back ground as a linear gradient brush
        /// to fill a solid color , consider using a image
        /// </summary>
        [
           Category("ControlKnob"),
           Description("The back ground color")
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
        /// The font that is used to display the label
        /// </summary>

        [
            Category("ControlKnob"),
            Description("The Font to be displayed ")
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
        /// Use this to set the color of the font ,
        /// the font is used to display the label
        /// </summary>
        [
           Category("ControlKnob"),
           Description("Color of the Font ")
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
        /// The type of label to display , if Image , the control
        /// will use the "Image" property of the MeterLabel class to display
        /// the label, if set to Font , the control will used the "Desc" property
        /// to display the label, if both, font and Image will be displayed
        /// </summary>
        [
           Category("ControlKnob"),
           Description("The Type of Marking Display , Image , Font or Both ")
        ]
        public KnobPanel.MarkingImage MarkingImageType
        {
            get { return _markingImage; }
            set
            {
                if (_markingImage != value)
                {
                    _markingImage = value;
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
          Category("ControlKnob"),
          Description("The Type of Marking")
        ]
        public KnobPanel.Marking Marking
        {
            get { return _markingType; }
            set
            {
                if (_markingType != value)
                {
                    _markingType = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// To set the image of the center handle
        /// </summary>
        [
         Category("ControlKnob"),
         Description("The Image to be displayed on the knob handle")
        ]

        public Image KnobHandleImage
        {
            get { return _knobHandleImage; }
            set
            {
                _knobHandleImage = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Use this to set the background , for example , you can set 
        /// the background to  solid color by setting this image to 
        /// solod color image
        /// </summary>
        [
           Category("ControlKnob"),
           Description("The Image to be displayed on the back")
        ]

        public Image BackImage
        {
            get { return _backImage; }
            set
            {
                _backImage = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// It returns a collection that allow user to add a meterlabel.
        /// 
        /// </summary>
        [
         Category("ControlKnob"),
         Description("Provide a collection to add a list of Label")
        ]
        public Collection.MeterLabelCollection Labels
        {
            get { return _panel.Collection; }
        }

        public ControlKnob()
        {
            InitializeComponent();
            _panel = new KnobPanel(this);
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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (_panel != null)
                _panel.MouseDown(e);
            base.OnMouseDown(e);
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_panel != null)
                _panel.MouseUp(e);
            base.OnMouseUp(e);
            this.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_panel != null)
                _panel.MouseMove(e);
            if (_panel.IsMouseDown)
            {
                if (Rotate != null)
                {
                    Rotate(this, _panel.PointerValue);
                }
            }
            base.OnMouseMove(e);
            this.Invalidate();
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
            //Draw the back image
            Pen p = new Pen(Color.DarkGray);
            LinearGradientBrush b = new LinearGradientBrush(rect.ClientRect, Color.WhiteSmoke, _backColor, 90f);
            g.FillPath(b, rect.GetGraphicsPath());
            g.DrawPath(p, rect.GetGraphicsPath());
            if (_backImage != null)
            {
                g.SetClip(rect.GetGraphicsPath());
                g.DrawImage(_backImage, rect.ClientRect);
                g.ResetClip();
            }
            _panel.Left = rect.ClientRect.Left;
            _panel.Top = rect.ClientRect.Top;
            _panel.Width = rect.ClientRect.Width;
            _panel.Height = rect.ClientRect.Height;
            _panel.KnobHandleimage = _knobHandleImage;
            _panel.MarkingType = _markingType;
            _panel.MarkingImageType = _markingImage;
            _panel.FontColor = _fontColor;
            _panel.MainFont = _font;
            _panel.Draw(g);
            e.Graphics.DrawImage(_map, new Point(0, 0));

        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            
        }
    }
}
