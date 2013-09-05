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
using System.Drawing;

namespace NextUI.Common
{
    public abstract class ShapePath
    {
        public abstract System.Drawing.Drawing2D.GraphicsPath GetGraphicsPath();
        public abstract bool Contain(Point location);

    }
}
