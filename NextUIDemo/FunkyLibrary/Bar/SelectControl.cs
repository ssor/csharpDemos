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
using System.Threading;

namespace NextUI.Bar
{
    /// <summary>
    /// delegate for Slide event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="val"> the value that will be generated 
    /// is depends on the property "value" of the particular MeterLabel"</param>
    public delegate void OnSlide(object sender,int val);
    /// <summary>
    /// A control similiar to standard slide control except with more 
    /// customize feature
    /// </summary>
    public partial class SelectControl : UserControl
    {
        public enum Marking { CONT, NONE, LINE, BOTH };
        public enum Flip { Up, Down };
        private Dragger _drag = null;
        private Bitmap _map = null;
        private MeterLabelCollection _collection = null;
        private float _interval = 0;
        private Marking _marking = Marking.LINE;
        private Flip _flip = Flip.Up;
        private int _barThinkness = 5;
        private Font _font = new Font(FontFamily.GenericMonospace, 8);
        private Image _backImage = null;
        private Color _backColor = Color.LightGray;
        private Color _pointerBodyColor = Color.Red;
        private Color _pointerColor = Color.Red;
        private Color _pointerHoverColor = Color.DarkRed;
        private Color _fontColor = Color.Black;
        /// <summary>
        /// Slide event will be generated whenever a slide is moved 
        /// </summary>
        public event OnSlide Slide;
        private Rectangle _bar;

        /// <summary>
        /// Control the direction of the label .
        /// UP will display the label on top
        /// DOWN will display the label on the bottom
        /// </summary>
        [
            Category("SelectControl"),
            Description("Diretion of Marking , either up or down")
        ]

        public  Flip Flipsize
        {
            get { return _flip; }
            set
            {
                if (_flip != value)
                {
                    _flip = value;
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
            Category("SelectControl"),
            Description("Type of Marking")
        ]

        public Marking MarkingType
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
        /// The font that is used to display the label
        /// </summary>
        [
            Category("SelectControl"),
            Description("The Display Font")
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
            Category("SelectControl"),
            Description("The Color of the Font")
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
        /// the color of the pointer body whenever a mouse over
        /// </summary>
        [
            Category("SelectControl"),
            Description("The Color of the Pointer Body when mouse over")
        ]

        public Color PointerHoverColor
        {
            get { return _pointerHoverColor; }
            set
            {
                if (_pointerHoverColor != value)
                {
                    _pointerHoverColor = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// The color of the pointer body
        /// </summary>
        [
            Category("SelectControl"),
            Description("The Color of the Pointer Body ")
        ]

        public Color PointerBodyColor
        {
            get { return _pointerBodyColor; }
            set
            {
                if (_pointerBodyColor != value)
                {
                    _pointerBodyColor = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// get or set the color of the pointer
        /// </summary>
        [
            Category("SelectControl"),
            Description("The Color of the Pointer ")
        ]

        public Color PointerColor
        {
            get { return _pointerColor; }
            set
            {
                if (_pointerColor != value)
                {
                    _pointerColor = value;
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
            Category("SelectControl"),
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
        /// Use this to set the background , for example , you can set 
        /// the background to  solid color by setting this image to 
        /// solod color image
        /// </summary>
        [
            Category("SelectControl"),
            Description("The back ground Image")
        ]

        public Image BackImage
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
        ///  It returns a collection that allow user to add a meterlabel.
        /// </summary>

        public MeterLabelCollection Labels
        {
            get { return _collection; }
        }

        public SelectControl()
        {
            InitializeComponent();
            _collection = new MeterLabelCollection();
            _collection.Set += new OnSet(_collection_Set);
            _collection.Insert += new OnInsert(_collection_Insert);
        }

        void _collection_Insert(object sender, int index)
        {
            for (int i = 0; i < _collection.Count; i++)
            {
                if (i != index && (_collection[i].Value == _collection[index].Value))
                {
                    throw new Exception("Index at " + index + " is same as  Value at index " + i);
                }

            }
        }

        void _collection_Set(object sender, int index)
        {
            for (int i = 0; i < _collection.Count; i++)
            {
                if (i != index && (_collection[i].Value == _collection[index].Value))
                {
                    throw new Exception("Index at " + index + " is same as  Value at index " + i);
                }

            }

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
                if (_drag != null)
                    _drag.MouseDown(e.Location);
                this.Invalidate(true);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_drag != null)
            {
                _drag.MouseMove(e.Location);
                if (_drag.IsMouseDown)
                {
                    if (_collection.Count > 1)
                    {
                        bool hit = false;
                        int val = PositionToValue((int)_collection[0].Value, (int)_collection[_collection.Count - 1].Value, (int)_drag.PointerValue,ref hit);
                        if (hit)
                        {
                            Thread.Sleep(200);
                        }
                 //       Console.WriteLine("---- VAL = " + val + " X=" + (int)_drag.PointerValue);
                        if (Slide != null)
                            Slide(this, val);
                    }
                }

                this.Invalidate(true);
            }
            
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_drag != null)
                _drag.MouseUp(e.Location);
            this.Invalidate(true);
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
            if (_backImage != null)
            {
                g.DrawImage(_backImage, rect.ClientRect);
            }
            _bar = new Rectangle(rect.Left + 10, rect.Top + rect.Height / 2 - _barThinkness / 2, rect.Width - 20, _barThinkness);
     
            RoundedRectangle r2 = new RoundedRectangle(_bar, 5);
            if (_collection.Count > 2)
            {
                float temp = r2.Width / (_collection.Count - 1);
                if (temp >= 1)
                {
                    _interval = temp;
                }
            }
            else
            {
                _interval = r2.Width;
            }

            b.LinearColors = new Color[] { Color.Gray, Color.DarkGray };
            g.FillPath(b, r2.GetGraphicsPath());
            DrawMarking(g, _bar);
            if (_drag == null)
            {
                _drag = new Dragger(r2.ClientRect, Dragger.DragPosition.Horizontal);
            }
            if (_drag != null)
            {
                _drag.NormalColor = _pointerBodyColor;
                _drag.SelectedColor = _pointerHoverColor;
                _drag.PointerColor = _pointerColor;
            }
            _drag.draw(g);
            e.Graphics.DrawImage(_map, 0, 0);
        }

        public void DrawMarking(Graphics e, Rectangle bound)
        {
            int i = 0;
            PointF p1;
            PointF p2;
            RectangleF cRect;
            GraphicsState state = e.Save();
            e.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            foreach (MeterLabel ml in _collection)
            {
                Size s = TextRenderer.MeasureText(ml.Desc,_font);
                if (_flip == Flip.Up)
                {
                    p1 = new PointF(bound.Left + i * (_interval), bound.Top);
                    p2 = new PointF(bound.Left + i * (_interval), bound.Top - 5);
                    cRect = new RectangleF(bound.Left + i * (_interval), bound.Top - 4, _interval, 4);
                    e.DrawString(ml.Desc, _font, new SolidBrush(_fontColor), new PointF(bound.Left - s.Width / 2 + i * (_interval), bound.Top - 5 - s.Height));
                }
                else
                {
                    p1 = new PointF(bound.Left + i * (_interval), bound.Top + bound.Height);
                    p2 = new PointF(bound.Left + i * (_interval), bound.Top + bound.Height + 5 );
                    cRect = new RectangleF(bound.Left + i * (_interval), bound.Top + bound.Height, _interval, 4);
                    e.DrawString(ml.Desc, _font, new SolidBrush(_fontColor), new PointF(bound.Left - s.Width / 2 + i * (_interval), bound.Top + bound.Height + 5));
                }

                if (_marking == Marking.BOTH || _marking == Marking.CONT)
                {
                    //we would not draw the last one 
                    if (i != _collection.Count - 1)
                    {
                        e.FillRectangle(new SolidBrush(ml.MainColor), cRect);
                    }
                    
                }
                if (_marking == Marking.LINE || _marking == Marking.BOTH)
                {
                    e.DrawLine(new Pen(_fontColor), p1, p2);
                }
                i++;

            }
        }

        private int PositionToValue(int start, int end , int position , ref bool hit)
        {
           float pvalue = 0;
            if (_collection.Count > 1)
            {
                int normalizepos = position - _bar.Left ;
              //  Console.WriteLine("interval=" + _interval + " end=" + end + " Normalized = " + normalizepos + " position=" + position);
                if (normalizepos >= 0 && normalizepos <= _interval * (_collection.Count - 1))
                {
                    float remainder = normalizepos % _interval;
                    int divisor = (int)( normalizepos / _interval);
                    if (remainder == 0)
                    {
                        hit = true;
                    }
                    else
                    {
                        hit = false;
                    }

                   //Console.WriteLine("interval=" + _interval + " end=" + end + " Normalized = " + normalizepos + " divisor=" + divisor + " remainder=" + remainder + " position=" + position);

                    if (divisor <= _collection.Count - 2)
                    {
                        if (_collection[divisor].Value > _collection[divisor + 1].Value)
                        {
                            pvalue = _collection[divisor].Value - _collection[divisor + 1].Value;
                            pvalue = pvalue * remainder / _interval;
                            pvalue = _collection[divisor].Value - pvalue;
                        }
                        else
                        {
                            pvalue = _collection[divisor + 1].Value - _collection[divisor].Value;
                            pvalue = pvalue * remainder / _interval;
                            pvalue = _collection[divisor ].Value + pvalue;
                        }
                    }
                    else
                    {
                        pvalue = _collection[_collection.Count - 1].Value;
                    }
                }
                else if (normalizepos < 0)
                {
                    pvalue = _collection[0].Value;
                }
                else
                {
                   pvalue =  _collection[_collection.Count - 1].Value;
                }
            }
            else 
            {
                pvalue = _collection[0].Value;
            }
            return (int)pvalue;

        }


        protected override void OnPaintBackground(PaintEventArgs e)
        {
           
        }

        protected override void OnResize(EventArgs e)
        {
            this.Invalidate();
           // base.OnResize(e);
        }




    }
}
