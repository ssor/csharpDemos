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
using System.Drawing.Drawing2D;
using System.Drawing;

namespace NextUI.Border
{
    public class BorderShadow : Border
    {
        private Color _layer1Shadow = Color.FromArgb(255, 160, 160, 160);
        private Color _layer2Shadow = Color.FromArgb(255, 190, 190, 190);
        private Color _layer3Shadow = Color.FromArgb(255, 220, 220, 220);
        private Color _layer4Shadow = Color.FromArgb(255, 250, 250, 250);

        public override void DrawBorder(Graphics e, System.Drawing.Drawing2D.GraphicsPath path)
        {
            GraphicsState state =  e.Save();
            e.TranslateTransform(4, 4);
            e.FillPath(new SolidBrush(_layer1Shadow), path);
            e.TranslateTransform(-1, -1);
            e.FillPath(new SolidBrush(_layer2Shadow), path);
            e.TranslateTransform(-1, -1);
            e.FillPath(new SolidBrush(_layer3Shadow), path);
            e.TranslateTransform(-1, -1);
            e.FillPath(new SolidBrush(_layer1Shadow), path);
            e.Restore(state);
        }
        
    }
}
