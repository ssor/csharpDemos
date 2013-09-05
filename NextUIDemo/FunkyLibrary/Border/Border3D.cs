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
    public class Border3D : Border
    {
        private static Color _outerDark = Color.FromArgb(88, 88, 88);
        private static Color _innerDark = Color.FromArgb(129, 129, 129);
        private static Color _innerLight = Color.FromArgb(250, 250, 250);
        private static Color _outerLight = Color.FromArgb(200, 200, 200);

        public override void DrawBorder(Graphics e, GraphicsPath path)
        {
            //create a light side first with pixel depth 1 on the north and west of a rectangle.
            GraphicsState state = e.Save();
            e.TranslateTransform(0, -2);
            e.FillPath(new SolidBrush(_outerLight), path);
            e.TranslateTransform(3, 0);
            e.FillPath(new SolidBrush(_outerLight), path);
            e.TranslateTransform(0, 5);
            e.FillPath(new SolidBrush(_outerLight), path);
            e.TranslateTransform(-5, 0);
            e.FillPath(new SolidBrush(_outerLight), path);
            e.TranslateTransform(0, -5);
            e.FillPath(new SolidBrush(_outerLight), path);
            e.TranslateTransform(1, 1);
            e.FillPath(new SolidBrush(_innerLight), path);
            e.TranslateTransform(1, 0);
            e.FillPath(new SolidBrush(_innerLight), path);
            e.TranslateTransform(0, 3);
            e.FillPath(new SolidBrush(_innerLight), path);
            e.TranslateTransform(2, 0);
            e.FillPath(new SolidBrush(_innerLight), path);
            e.TranslateTransform(-3, -3);
            e.FillPath(new SolidBrush(_innerLight), path);
            e.TranslateTransform(4, 4);
            e.FillPath(new SolidBrush(_outerDark), path);
            e.TranslateTransform(-1, -1);
            e.FillPath(new SolidBrush(_innerDark), path);
            e.Restore(state);
            
        }
    }
}
