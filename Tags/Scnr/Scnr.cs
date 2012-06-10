using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H2Memory_class.Tags
{
    public class ScenarioTag
    {
        public Tag Tag;
        /// <summary>
        /// Initializes a new instance of the scenario class for the given tag.
        /// </summary>
        /// <param name="Tag">The tag the the new instance will be based on.</param>
        public ScenarioTag(Tag Tag)
        {
            this.Tag = Tag;
        }
        public Ident UnusedBSP()
        {
            return new Ident(Tag.MetaOffset,"Unused BSP", ref Tag.H2Memory);
        }
        public Reflexive Sky_Palette()
        {
            ReflexLayout Layout = new ReflexLayout();
            Layout.Add(typeof(Ident), new Ident(0,"Sky", ref Tag.H2Memory, Layout));
            return new Reflexive(Tag.MetaOffset + 8, 8,"Sky Palette", ref Tag.H2Memory, Layout);
        }
    }
}
