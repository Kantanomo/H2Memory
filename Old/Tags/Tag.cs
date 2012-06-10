using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H2Memory_class.Tags
{
    public class Tag
    {
        public TagType Type;
        public string Path;
        public uint Identifier;
        public int RawMetaOffset;
        public int RawMetaSize;
        public int MetaOffset;
        public H2Memory H2Memory;
        public Tag(long Type, uint Ident, int RawMetaOffset, int RawMetaSize, int MetaOffset, string path, ref H2Memory H2Memory)
        {
            this.Type = (TagType)Type;
            this.Identifier = Ident;
            this.RawMetaOffset = RawMetaOffset;
            this.RawMetaSize = RawMetaSize;
            this.MetaOffset = MetaOffset;
            this.Path = path;
            this.H2Memory = H2Memory;
        }
        public Tag(TagType Type, uint Ident, int RawMetaOffset, int RawMetaSize, int MetaOffset, string path, ref H2Memory H2Memory)
        {
            this.Type = Type;
            this.Identifier = Ident;
            this.RawMetaOffset = RawMetaOffset;
            this.RawMetaSize = RawMetaSize;
            this.MetaOffset = MetaOffset;
            this.Path = path;
            this.H2Memory = H2Memory;
        }
    }
    public class TagTypeConverter
    {
        public static TagType Convert(string Type)
        {
            string Builder = "";
            char[] ca = Type.ToCharArray();
            Array.Reverse(ca);
            foreach(char c in new string(ca))
                Builder += BitConverter.GetBytes(c)[0].ToString("X");
            return (TagType)long.Parse(Builder, System.Globalization.NumberStyles.AllowHexSpecifier);
        }
        public static string Revert(TagType Type)
        {
            char[] ca = Type.ToString().ToCharArray();
            Array.Reverse(ca);
            return new string(ca);
        }
    }
    public enum TagType : long
    {
        hlmt = 0x746D6C68,
        mode = 0x65646F6D,
        coll = 0x6C6C6F63,
        phmo = 0x6F6D6870,
        bitm = 0x6D746962,
        colo = 0x6F6C6F63,
        unic = 0x63696E75,
        unit = 0x74696E75,
        bipd = 0x64706962,
        vehi = 0x69686576,
        scen = 0x6E656373,
        bloc = 0x636F6C62,
        crea = 0x61657263,
        phys = 0x73796870,
        obje = 0x656A626F,
        cont = 0x746E6F63,
        weap = 0x70616577,
        ligh = 0x6867696C,
        effe = 0x65666665,
        prt3 = 0x33747270,
        PRTM = 0x4D545250,
        pmov = 0x766F6D70,
        matg = 0x6774616D,
        snd_ = 0x5F646E73,
        lsnd = 0x646E736C,
        item = 0x6D657469,
        eqip = 0x70697165,
        ant_ = 0x5F746E61,
        MGS2 = 0x3253474D,
        tdtl = 0x6C746474,
        devo = 0x6F766564,
        whip = 0x70696877,
        BooM = 0x4D6F6F42,
        trak = 0x6B617274,
        proj = 0x6A6F7270,
        devi = 0x69766564,
        mach = 0x6863616D,
        ctrl = 0x6C727463,
        lifi = 0x6966696C,
        pphy = 0x79687070,
        ltmp = 0x706D746C,
        sbsp = 0x70736273,
        scnr = 0x726E6373,
        shad = 0x64616873,
        stem = 0x6D657473,
        slit = 0x74696C73,
        spas = 0x73617073,
        vrtx = 0x78747276,
        pixl = 0x6C786970,
        DECR = 0x52434544,
        DECP = 0x50434544,
        sky_ = 0x5F796B73,
        wind = 0x646E6977,
        snde = 0x65646E73,
        lens = 0x736E656C,
        fog = 0x676F66,
        fpch = 0x68637066,
        metr = 0x7274656D,
        deca = 0x61636564,
        coln = 0x6E6C6F63,
        jpt_ = 0x5F74706A,
        udlg = 0x676C6475,
        itmc = 0x636D7469,
        vehc = 0x63686576,
        wphi = 0x69687077,
        grhi = 0x69687267,
        unhi = 0x69686E75,
        nhdt = 0x7464686E,
        hud_ = 0x5F647568,
        hudg = 0x67647568,
        mply = 0x796C706D,
        dobc = 0x63626F64,
        ssce = 0x65637373,
        hmt_ = 0x5F746D68,
        wgit = 0x74696777,
        skin = 0x6E696B73,
        wgtz = 0x7A746777,
        wigl = 0x6C676977,
        sily = 0x796C6973,
        goof = 0x666F6F67,
        foot = 0x746F6F66,
        garb = 0x62726167,
        styl = 0x6C797473,
        char_ = 0x5F72616863,
        adlg = 0x676C6461,
        mdlg = 0x676C646D,
        srscen = 0x6E6563737273,
        srbipd = 0x647069627273,
        srvehi = 0x696865767273,
        sreqip = 0x706971657273,
        srweap = 0x706165777273,
        srssce = 0x656373737273,
        srligh = 0x6867696C7273,
        srdgrp = 0x707267647273,
        srdeca = 0x616365647273,
        srcine = 0x656E69637273,
        srtrgr = 0x726772747273,
        srclut = 0x74756C637273,
        srcrea = 0x616572637273,
        srdcrs = 0x737263647273,
        srsslt = 0x746C73737273,
        srhscf = 0x666373687273,
        srai = 0x69617273,
        srcmmt = 0x746D6D637273,
        bsdt = 0x74647362,
        mpdt = 0x7464706D,
        sncl = 0x6C636E73,
        mulg = 0x676C756D,
        _fx_ = 0x5F78665F,
        sfx_ = 0x5F786673,
        gldf = 0x66646C67,
        jmad = 0x64616D6A,
        clwd = 0x64776C63,
        egor = 0x726F6765,
        weat = 0x74616577,
        snmx = 0x786D6E73,
        spk_ = 0x5F6B7073,
        ugh_ = 0x5F686775,
        shit = 0x74696873,
        mcsr = 0x7273636D
    }
}
