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
    public class AlgorithmHelper
    {
        public static float NormalizeDegreeFromX(float degree)
        {
            float temp = degree;
            while (temp < 0 || temp >= 360)
            {
                if (temp >= 360)
                {
                    temp -= 360;
                }
                else if (temp <= 0)
                {
                    temp += 360;
                }
            }
            return temp;
        }

        //Normalze degree to reference

        public static float NormalizeDegree(float reference , float degree)
        {
            float temp = degree - reference;
            if (temp < 0)
            {
                temp = degree + ( 360 - reference);
            }
            while (temp < 0 || temp >= 360)
            {
                if (temp >= 360)
                {
                    temp -= 360;
                }
                else if (temp <= 0)
                {
                    temp += 360;
                }
            }
            //Console.WriteLine("Normalize to " + reference + ",converted=" + degree + " to " + temp);
            return temp;
        }

        public static float AngleDifferentClockWise(float startAngle, float endAngle)
        {
            //Lets normalize the angle;
            float nStartAngle = NormalizeDegreeFromX(startAngle);
            float nEndAngle = NormalizeDegreeFromX(endAngle);
            //Calculate the Diff, where the 0 is always on the positive x axis. 
            float result = 360f;
            if (nStartAngle > nEndAngle)
            {
                result = 360 - (nStartAngle - nEndAngle);
            }
            if (nStartAngle < nEndAngle)
            {
                result = nEndAngle - nStartAngle;
            }
            return result;

        }
        
        public static PointF LocationToCenter(PointF center, float radius, float angle)
        {
            //angle is clockwise from x axis .
            //translate the angle relative to x axis .
            PointF location = new PointF(0,0);
            if (angle <= 90)
            {
                location.X = radius * (float)Math.Cos(angle * (Math.PI / 180f)) + center.X;
                location.Y = radius * (float)Math.Sin(angle * (Math.PI / 180f)) + center.Y;
            }
            else if (angle <= 180)
            {
                location.X = center.X - radius * (float)Math.Cos((180 - angle) * (Math.PI / 180f));
                location.Y = radius * (float)Math.Sin((180 - angle) * (Math.PI / 180f)) + center.Y;
            }
            else if (angle <= 270)
            {
                location.X = center.X - radius * (float)Math.Cos((angle - 180) * (Math.PI / 180f));
                location.Y = center.Y - radius * (float)Math.Sin((angle - 180) * (Math.PI / 180f));
            }
            else
            {
                location.X = radius * (float)Math.Cos((360 - angle) * (Math.PI / 180f)) + center.X;
                location.Y = center.Y - radius * (float)Math.Sin((360 - angle) * (Math.PI / 180f));
            }
            return location;
        }

        public static float PointToCenterAngle(PointF center, Point location)
        {
            float x = (float)location.X - center.X;
            float y = (float)location.Y - center.Y;
          //  Console.WriteLine("x=" + x + ":y=" + y);
            float angle = 0;
            if (x > 0 && y >= 0)
            {
                //we are at the 1 st rn
                angle = (float)(Math.Atan(Math.Abs(y / x)) * (180 / Math.PI));
            }
            else if (x <= 0 && y > 0)
            {
                //at 2nd Qua
                angle = (float)(180 - Math.Atan(Math.Abs(y / x)) * (180 / Math.PI));
            }
            else if (x < 0 && y <= 0)
            {
                angle = (float)(Math.Atan(Math.Abs(y / x)) * (180 / Math.PI) + 180);
            }
            else
            {
                angle = (float)(360 - Math.Atan(Math.Abs(y / x)) * (180 / Math.PI));
            }
            return angle;
        }

        public static bool IsPointInCircle(PointF center, float radius, Point location, float offset)
        {
            //  ( x - a )^2 + ( y - b )^2= r2. 
            // Console.WriteLine(rsqt);
            float rsqt = (float)(Math.Pow((float)(location.X) - center.X, 2) + Math.Pow((float)(location.Y) - center.Y, 2));
            //  Console.WriteLine(rsqt + " square=" + Math.Pow(radius,2) + " : " + Math.Pow(radius + (float)offset,2));
            if (rsqt >= Math.Pow(radius, 2) && rsqt <= Math.Pow(radius + (float)offset, 2))
            {
                return true;
            }
            return false;

        }


        public static PointF CalXYCircle(PointF center, float radius, float angle)
        {
            float x = center.X + (radius * (float)(Math.Cos((angle / 360) * 2 * Math.PI)));
            float y = center.Y + (radius * (float)(Math.Sin((angle / 360) * 2 * Math.PI)));
            return new PointF(x, y);
        }

        public static bool IsPointInRectF(RectangleF x1, PointF n)
        {
            if (n.X > x1.Left && n.X < x1.Left + x1.Width
                && n.Y > x1.Top && n.Y < x1.Top + x1.Height)
            {
                return true;
            }
            return false;
        }


        public static bool IsPointInRect(Rectangle x1, Point n)
        {
            if (n.X > x1.Left && n.X < x1.Left + x1.Width
                && n.Y > x1.Top && n.Y < x1.Top + x1.Height)
            {
                return true;
            }
            return false;
        }

        public static bool IsPointInLine(Point x1, Point x2, Point n)
        {
            //Lets calculate the gradient x1 and x2
            bool inLine = false;
            if ((n.X >= x1.X && n.X <= x2.X && n.Y >= x1.Y && n.Y <= x2.Y) ||
                 (n.X <= x1.X && n.X >= x2.X && n.Y >= x1.Y && n.Y <= x2.Y) ||
                 (n.X <= x1.X && n.X >= x2.X && n.Y <= x1.Y && n.Y >= x2.Y) ||
                (n.X >= x1.X && n.X <= x2.X && n.Y <= x1.Y && n.Y >= x2.Y))
            {
                //this is a horizontal line, cannot use
                //gradient since it will be infinity.
                if (x1.X == x2.X)
                {
                    if ((n.X == x1.X && n.Y > x1.Y && n.Y < x2.Y) ||
                        (n.X == x1.X && n.Y < x1.Y && n.Y > x2.Y))
                    {
                        inLine = true;
                    }
                }
                else if (x1.X != n.X)
                {
                    Double m = Math.Round((float)(x1.Y - x2.Y) / (float)(x1.X - x2.X), 1);
                    Double z = Math.Round((float)(x1.Y - n.Y) / (float)(x1.X - n.X), 1);
                    if (m == z)
                    {
                        inLine = true;
                    }
                }
            }
            return inLine;
        }


    }
}
