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

namespace NextUI.Border
{
    public abstract class Border
    {
        public abstract void DrawBorder(Graphics e, System.Drawing.Drawing2D.GraphicsPath path);
        
    }
}
