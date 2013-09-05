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
using System.Collections.Generic;
using NextUI.Display;


namespace NextUI.Collection
{
    public class CounterCollection : BaseStack
    {
        public RollingCounter Add(RollingCounter value)
        {
            base.List.Add(value as object);
            return value;
        }

        public void AddRange(RollingCounter[] value)
        {
            foreach (RollingCounter Gbase in value)
            {
                base.List.Add(Gbase as object);
            }

        }

        public void Remove(RollingCounter value)
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

        public RollingCounter this[int index]
        {
            get { return (RollingCounter)base.List[index]; }
            set { base.List[index] = value as object; }
        }
    }
}
