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
using System.Collections;
using System.Drawing;

namespace NextUI.Collection
{
    /// <summary>
    /// The main class for a label 
    /// </summary>
    public class MeterLabel
    {
        private int _value = 0;
        private string _desc;
        private Image _image = null;
        private Color _mainColor = Color.Red;
        /// <summary>
        /// Value is  the actual numeric value of the label
        /// it is the actual value that will be raised when meter label
        /// is used for control like knob,
        /// It is also the actual value that is used to calculated the position
        /// of the pointer .
        /// </summary>
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// This property is used for the actual text display for a label
        /// you can set Desc to "Hallo", but Value to 10 ,
        /// So this label will be displayed as Hello but the numerical 10
        /// will be the actual representation of the label
        /// </summary>
        public string Desc
        {
            get { return _desc; }
            set { _desc = value; }
        }
        /// <summary>
        /// Used to show an Image for the label, 
        /// Note : Not all the control supported this
        /// </summary>
        public Image Image
        {
            get { return _image; }
            set { _image = value; }
        }

        /// <summary>
        /// This is used to determined the color of the shade between label
        /// </summary>
        public Color MainColor
        {
            get { return _mainColor; }
            set { _mainColor = value; }
        }


        public MeterLabel()
        {
        }

        public MeterLabel(int value, string desc)
        {
            _value = value;
            _desc = desc;
        }
    }

    public class MeterLabelCollection : BaseStack
    {
        public MeterLabel Add(MeterLabel value)
        {
            base.List.Add(value as object);
            return value;
        }

        public void AddRange(MeterLabel[] value)
        {
            foreach (MeterLabel Gbase in value)
            {
                base.List.Add(Gbase as object);
            }

        }

        public void Remove(MeterLabel value)
        {
            base.List.Remove(value as Object);

        }

        public MeterLabel this[int index]
        {
            get { return (MeterLabel)base.List[index]; }
            set { base.List[index] = value as object; }
        }
    }
}
