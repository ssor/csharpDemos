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

namespace NextUI.Bar
{
    public abstract class SignalLight
    {
        private Rectangle _size;
        private Color _color = Color.YellowGreen;
        private Color _nonLitColor = Color.Black;
        private bool _lit = false;
        private bool _haloEffect = false;

        public Color MainColor
        {
            get { return _color; }
            set { _color = value; }
        }

        public Color NonlitColor
        {
            get { return _nonLitColor; }
            set { _nonLitColor = value; }
        }

        public bool Lit
        {
            get { return _lit; }
            set { _lit = value; }
        }


        public bool HaloEffect
        {
            get { return _haloEffect; }
            set { _haloEffect = value; }
        }



        public Rectangle ClientRect
        {
            get{ return _size;}
            set { _size = value;}
        }

        public SignalLight()
        {
            _size = new Rectangle(0,0,10,10);
        }

        public abstract void Draw(Graphics e);
    }
}
