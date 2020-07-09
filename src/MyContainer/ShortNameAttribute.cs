using System;
using System.Collections.Generic;
using System.Text;

namespace MyContainer
{
    public class ShortNameAttribute : Attribute
    {
        public string shortName;
        public ShortNameAttribute(string shortName)
        {
            this.shortName = shortName;
        }
    }
}
