using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H2Memory_class.Tags
{
    public class Reflexive
    {
        public int ReflexCount;
        public string Name;
        public int ReflexOffset;
        public int ReflexChunkSize;
        public int Offset;
        public ReflexLayout ReflexLayout;
        public H2Memory H2Memory;
        public List<ReflexChunk> Chunks = new List<ReflexChunk>();
        public Reflexive(int Offset,int ReflexChunkSize,string Name, ref H2Memory H2Memory, ReflexLayout ReflexLayout)
        {
            this.ReflexCount = H2Memory.H2Mem.ReadInt(false, Offset);
            this.ReflexOffset = H2Memory.H2Mem.ReadInt(false, Offset + 4) - H2Memory.SecondaryMagic();
            this.ReflexChunkSize = ReflexChunkSize;
            this.H2Memory = H2Memory;
            this.Name = Name;
            this.Offset = Offset;
            this.ReflexLayout = ReflexLayout;
            for (int i = 0; i < this.ReflexCount; i++)
                Chunks.Add(new ReflexChunk(ReflexOffset + (i * ReflexChunkSize),ref ReflexLayout));
        }
    }
    public class ReflexChunk
    {
        public int Offset;
        public ReflexLayout ReflexLayout;
        public ReflexChunk(int Offset, ref ReflexLayout ReflexLayout)
        {
            this.Offset = Offset;
            this.ReflexLayout = ReflexLayout;
            foreach (ReflexLayoutItem I in ReflexLayout.Items)
            {
                if (I.Type == typeof(Ident))
                {
                    ((Ident)I.Item).Offset += Offset;
                    ((Ident)I.Item).Read();
                }
                if (I.Type == typeof(Reflexive))
                    ((Reflexive)I.Item).Offset += Offset;
            }
        }
    }
    public class ReflexLayout
    {
        public List<ReflexLayoutItem> Items = new List<ReflexLayoutItem>();
        public ReflexLayout()
        {

        }
        public ReflexLayout(List<ReflexLayoutItem> Items)
        {
            this.Items = Items;
        }
        public void Add(Type Type, object Item)
        {
            Items.Add(new ReflexLayoutItem(Type, Item));
        }
    }
    public class ReflexLayoutItem
    {
        public Type Type;
        public object Item;
        public ReflexLayoutItem(Type Type, object Item)
        {
            this.Type = Type;
            this.Item = Item;
        }
    }
}
