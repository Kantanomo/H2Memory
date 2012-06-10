using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H2Memory_class.Tags
{
    public class Ident
    {
        public string Name;
        public TagType Type;
        public FieldValue Value;
        public int Offset;
        private H2Memory H2Memory;
        public Ident(TagType Type, int Value,int Offset,string Name, ref H2Memory H2Memory)
        {
            this.Value = new FieldValue(typeof(int), Value);
            this.Name = Name;
            this.H2Memory = H2Memory;
            this.Offset = Offset;
        }
        public Ident(int Offset, string Name, ref H2Memory H2Memory)
        {
            this.Type = TagTypeConverter.Convert(H2Memory.H2Mem.ReadStringAscii(false, Offset, 4));
            Value = new FieldValue(typeof(int), H2Memory.H2Mem.ReadInt(false, Offset + 4));
            this.H2Memory = H2Memory;
            this.Offset = Offset;
            this.Name = Name;
        }
        public Ident(int Offset, string Name, ref H2Memory H2Memory, ReflexLayout Parent)
        {
            this.H2Memory = H2Memory;
            this.Offset = Offset;
            this.Name = Name;
        }
        public void Write(TagType Type, FieldValue Value)
        {
            if (Value.Type == typeof(int))
            {
                H2Memory.H2Mem.WriteStringAscii(false, Offset, TagTypeConverter.Revert(Type));
                H2Memory.H2Mem.WriteInt(false, Offset + 4, ((int)Value.Value));
            }
            else
                throw new Exception("Ident Values must be a 32 bit aligned integer");
        }
        public void Read()
        {
            this.Type = TagTypeConverter.Convert(H2Memory.H2Mem.ReadStringAscii(false, Offset, 4));
            this.Value = new FieldValue(typeof(int), H2Memory.H2Mem.ReadInt(false, Offset + 4));
        }
    }
}
