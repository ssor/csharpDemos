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
using System.Drawing.Drawing2D;

namespace NextUI.Border
{
    public class Inner3D : Border
    {
        private static Color _outerDark = Color.Silver;
        private static Color _innerDark = Color.FromArgb(30, 30, 30);
        private static Color _innerLight = Color.White;
        private static Color _outerLight = Color.White;

        public override void DrawBorder(Graphics e, GraphicsPath path)
        {
            //create a light side first with pixel depth 1 on the north and west of a rectangle.
            
            GraphicsPath p = new GraphicsPath();
            p.AddPath(path, false);
            p.Widen(new Pen(Brushes.Black, 6));
            LinearGradientBrush brush = new LinearGradientBrush(p.GetBounds(), _outerLight, _outerDark, 45f);
            e.FillPath(brush, p);
            GraphicsPath p1 = new GraphicsPath();
            p1.AddPath(path,false);
            p1.Widen(new Pen(Brushes.Black, 3));
            LinearGradientBrush brush2 = new LinearGradientBrush(p.GetBounds(), _innerDark, _innerLight, 45f);
            e.FillPath(brush2, p1);
            e.FillPath(Brushes.Transparent, path);
            p1.Dispose();
            p.Dispose();
          

        }
    }
}
