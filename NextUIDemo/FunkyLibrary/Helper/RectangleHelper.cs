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

namespace NextUI.Helper
{
    public class RectangleHelper
    {
        public static Rectangle Shrink(Rectangle ori, int size)
        {
            Rectangle end = ori;
            if (end.Width >= 2 * size && end.Height >= 2 * size)
            {
                ori.Location = new Point(ori.Left + size, ori.Top + size);
                ori.Size = new Size(end.Width - 2 * size, end.Height - 2 * size);
            }
            return ori;
        }

        public static Rectangle Expand(Rectangle ori, int size)
        {
            Rectangle end = ori;
            ori.Location = new Point(ori.Left - size, ori.Top - size);
            ori.Size = new Size(end.Width + 2 * size, end.Height + 2 * size);

            return ori;
        }
        
                


    }
}
