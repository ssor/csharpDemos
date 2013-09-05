using System;
using System.Collections.Generic;
using System.Text;

namespace jsonTest
{
    public class jsonClass
    {
        public string name = string.Empty;
        public string age = string.Empty;
        public jsonClass()
        {

        }
        public jsonClass(string _name, string _age)
        {
            this.name = _name;
            this.age = _age;
        }
    }
}
