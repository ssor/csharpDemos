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
using NextUI.Display;

namespace NextUI.Collection
{
    public class DigitalPanelCollection : BaseStack
    {
        public DigitalPanel Add(DigitalPanel value)
        {
            base.List.Add(value as object);
            return value;
        }

        public void AddRange(DigitalPanel[] value)
        {
            foreach (DigitalPanel Gbase in value)
            {
                base.List.Add(Gbase as object);
            }

        }

        public void Remove(DigitalPanel value)
        {
            base.List.Remove(value as Object);

        }

        public void Remove()
        {
            if (base.List.Count > 0)
            {
                base.List.RemoveAt(base.List.Count - 1);
            }

        }

        public DigitalPanel this[int index]
        {
            get { return (DigitalPanel)base.List[index]; }
            set { base.List[index] = value as object; }
        }
    }
}
