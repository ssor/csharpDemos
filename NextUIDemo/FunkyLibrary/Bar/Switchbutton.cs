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
    /// Delegate that handle event when the button is switch on
    /// </summary>
    /// <param name="sender"></param>
    public delegate void OnSwitchOn(object sender);
    /// <summary>
    /// Delegate that handle event when the button is switch off
    /// </summary>
    /// <param name="sender"></param>
    public delegate void OnSwitchOff(object sender);
    /// <summary>
    /// A toggle button that provide a blinking light
    /// </summary>
    public partial class Switchbutton : UserControl
    {
        /// <summary>
        /// Event that raised when button is toggled on 
        /// </summary>
        public event OnSwitchOn SwitchOn;
        /// <summary>
        /// Event that raised when button is toggled off
        /// </summary>
        public event OnSwitchOff SwitchOff;
        /// <summary>
        /// if set to true , when the button is toggled , the led will
        /// start blinking
        /// </summary>
        [
           Category("Switchbutton"),
           Description("Blink the light")
        ]
        public bool Blink
        {
            get { return _blink; }
            set
            {
                _blink = value;
            }
        }
        /// <summary>
        /// set the rate of blinking if Blink is set to true
        /// </summary>
        [
        Category("Switchbutton"),
        Description("Rate of blinking in ms")
        ]
        public int BlinkRate
        {
            get { return _blinkRate; }
            set
            {
                if (_blinkRate != value)
                {
                    _blinkRate = value;
                    if (_timer.Enabled)
                    {
                        _timer.Stop();
                        _timer.Interval = _blinkRate;
                        _timer.Start();
                    }
                }
            }
        }

        /// <summary>
        /// Set the background color of the control, the color is used to 
        /// paint the back ground as a path gradient brush
        /// to fill a solid color , consider using a image
        /// </summary>
        [
         Category("Switchbutton"),
         Description("The BackGround Color of the control")
        ]
        public Color MainColor
        {
            get { return _backcolor; }
            set
            {
                if (_backcolor != value)
                {
                    _backcolor = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// The color of the led when button is not toggled
        /// </summary>

        [
         Category("Switchbutton"),
         Description("The LED Color when it is not press")
        ]
        public Color NonLitColor
        {
            get { return _nonLitColor; }
            set
            {
                if (_nonLitColor != value)
                {
                    _nonLitColor = value;
                    _light.NonlitColor = _nonLitColor;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// The color of the leb when button is toggled
        /// </summary>
        [
         Category("Switchbutton"),
         Description("The LED Color when it is press ")
        ]
        public Color LitColor
        {
            get { return _litColor; }
            set
            {
                if (_litColor != value)
                {
                    _litColor = value;
                    _light.MainColor = _litColor;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// Enable a grow effect of the led when button is toggled
        /// </summary>
        [
            Category("Switchbutton"),
            Description("Enable Growing effect when the button is lit")
        ]
        public Boolean HaloEffect
        {
            get { return _haloEffect; }
            set
            {
                _haloEffect = value;
                _light.HaloEffect = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// use this to set the Image of the button when button is toggled
        /// </summary>
        [
           Category("Switchbutton"),
           Description("Image when the button is switch on ")
        ]
        public Image OnImage
        {
            get { return _onImage; }
            set
            {
                if (_onImage != value)
                {
                    _onImage = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// use this to set the image of the button when button is toggled off
        /// </summary>
        [
           Category("Switchbutton"),
           Description("Image when the button is switch off ")
        ]
        public Image OffImage
        {
            get { return _offImage; }
            set
            {
                if (_offImage != value)
                {
                    _offImage = value;
                    this.Invalidate();
                }
            }
        }

        private Bitmap _map = null;
        private bool _mouseDown = false;
        private Color _backcolor = Color.DarkGray;
        private bool _haloEffect = false;
        private bool _lit = false;
        private Timer _timer = new Timer();
        private bool _blink = false;
        private Color _nonLitColor = Color.Black;
        private Color _litColor = Color.Blue;
        private int _blinkRate = 500;
        private Image _onImage = null;
        private Image _offImage = null;

        private SignalLight _light = new SquareLight();
        public Switchbutton()
        {
            InitializeComponent();
            _light.MainColor = _litColor;
            _light.NonlitColor = _nonLitColor;
            _light.HaloEffect = _haloEffect;
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Interval = _blinkRate;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if (_lit)
            {
                _lit = false;
            }
            else
            {
                _lit = true;
            }
            _light.Lit = _lit;
            this.Invalidate();
          
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
            if (e.Button == MouseButtons.Left)
            {
                if (_mouseDown == false)
                {
                    _mouseDown = true;
                    _light.Lit = true;
                    if (_blink)
                    {
                        _timer.Start();
                    }
                    if (SwitchOn != null)
                    SwitchOn(this);

                }
                else
                {
                    _mouseDown = false;
                    _light.Lit = false;
                    if (_timer.Enabled)
                    {
                        _timer.Stop();
                    }
                    if ( SwitchOff != null)
                    SwitchOff(this);
                }
            }
            this.Invalidate();
            base.OnMouseDown(e);
  
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
            PathGradientBrush pb = new PathGradientBrush(rect.GetGraphicsPath());
            if (_mouseDown == false)
            {
                pb.CenterColor = Color.White;
                pb.SurroundColors = new Color[] { _backcolor };
                pb.CenterPoint = new PointF(-2f, -2f);
            }
            else
            {
                pb.CenterColor = Lighten(_backcolor);
                pb.SurroundColors = new Color[] { _backcolor };
         
            }
            g.FillPath(pb, rect.GetGraphicsPath());
            if (_offImage != null && _mouseDown == false)
            {
                g.DrawImage(_offImage, Helper.RectangleHelper.Shrink(rect.ClientRect, 5));
            }
            if (_onImage != null && _mouseDown == true)
            {
                g.DrawImage(_onImage, Helper.RectangleHelper.Shrink(rect.ClientRect, 5));
            }
            _light.ClientRect = new Rectangle(rect.Left + 5 , 5, rect.Width - 10 , 5);
            _light.Draw(g);
            e.Graphics.DrawImage(_map, 0, 0);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        private Color Lighten(Color c)
        {
            int R = c.R;
            int G = c.G;
            int B = c.B;
            R = R + 100;
            if (R > 255)
                R = 255;
            B = B + 100;
            if (B > 255)
                B = 255;
            G = G + 100;
            if (G > 255)
                G = 255;
            return Color.FromArgb(R, G, B);
        }
    }
}
