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

namespace NextUI.Bar
{
    public class SquareLight : SignalLight
    {
        public override void Draw(Graphics e)
        {
            if (HaloEffect)
            {
                if (Lit)
                {
                    Bitmap map = new Bitmap(ClientRect.Width / 2, ClientRect.Height / 2);
                    Graphics g = Graphics.FromImage(map);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.FillRectangle(new SolidBrush(Color.FromArgb(150,MainColor)), new Rectangle(0,0,map.Width,map.Height));
                    g.Dispose();
                    GraphicsState state = e.Save();
                    e.SmoothingMode = SmoothingMode.AntiAlias;
                    e.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Rectangle exprect = Helper.RectangleHelper.Expand(ClientRect, 10);
                    e.DrawImage(map, exprect, new Rectangle(0, 0, map.Width, map.Height), GraphicsUnit.Pixel);
                    e.FillRectangle(new SolidBrush(MainColor), ClientRect);
                    e.Restore(state);
                    e.DrawRectangle(new Pen(Color.DarkGreen), ClientRect);
                  //  e.FillRectangle(new SolidBrush(NonlitColor), ClientRect);
                }
                else
                {
                    e.FillRectangle(new SolidBrush(NonlitColor), ClientRect);
                    e.DrawRectangle(new Pen(Color.DarkGreen), ClientRect);
                }
            }
            else
            {
                if (Lit)
                {
                    e.FillRectangle(new SolidBrush(MainColor), ClientRect);
                    e.DrawRectangle(new Pen(Color.DarkGreen), ClientRect);
                }
                else
                {
                    e.FillRectangle(new SolidBrush(NonlitColor), ClientRect);
                    e.DrawRectangle(new Pen(Color.DarkGreen), ClientRect);
                }

            }
        } 

    }
}
