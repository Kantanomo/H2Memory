using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H2Memory_class.Tags
{
    public class FieldValue
    {
        public Type Type;
        public object Value;
        public FieldValue(Type Type, object Value)
        {
            this.Type = Type;
            this.Value = Value;
        }
    }
}
