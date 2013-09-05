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

namespace NextUI.Collection
{
    public delegate void OnInsert(object sender, int index);
    public delegate void OnSet(object sender, int index);
    public delegate void OnRemove(object sender, int index);
    public class BaseStack : CollectionBase
    {
        public event OnInsert Insert;
        public event OnRemove Delete;
        public event OnSet Set;


        protected override void OnInsertComplete(int index, object value)
        {
            if (Insert != null)
                Insert(value, index);
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            if (Delete != null)
                Delete(value, index);
        }

        protected override void OnSetComplete(int index, object oldValue, object newValue)
        {
            if (Set != null)
                Set(newValue, index);
        }

        protected override void OnValidate(Object value)
        {
        }

    }
}
