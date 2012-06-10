﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace H2Memory_class
{
    public class Player
    {
        private ProcessMemory Mem;
        private H2Memory H2;
        private H2Type HType;
        private int index;
        private int Offset;
        #region Player Fields
        /// <summary>
        /// Gets or Sets the CameraID of the player instance
        /// </summary>
        public byte[] CameraID
        {
            get
            {
                if(H2.HType == H2Type.Halo2Vista)
                    return Mem.ReadMem(0x30002B44 + index, 4, false);
                if (H2.HType == H2Type.H2server)
                    return Mem.ReadMem(0x300026F0 + index, 4, false);
                return BitConverter.GetBytes('0');
            }
            set
            {
                if (H2.HType == H2Type.Halo2Vista)
                    Mem.WriteMem(0x30002B44 + index, (byte[])value, false);
                if (H2.HType == H2Type.H2server)
                    Mem.WriteMem(0x300026F0 + index, (byte[])value, false);
            }
        }
        /// <summary>
        /// Gets or Sets the LiveID of the player instance
        /// </summary>
        public byte[] LiveID
        {
            get
            {
                if (H2.HType == H2Type.Halo2Vista)
                    return Mem.ReadMem(0x30002B20 + index, 4, false);
                if (H2.HType == H2Type.H2server)
                    return Mem.ReadMem(0x300026CC + index, 4, false);
                return BitConverter.GetBytes('0');
            }
            set
            {
                if (H2.HType == H2Type.Halo2Vista)
                    Mem.WriteMem(0x30002B20 + index, (byte[])value, false);
                if (H2.HType == H2Type.H2server)
                    Mem.WriteMem(0x300026CC + index, (byte[])value, false);
            }
        }
        /// <summary>
        /// Gets or Sets the GamerTag of the player instance
        /// </summary>
        public string GamerTag
        {
            get
            {
                if (H2.HType == H2Type.Halo2Vista)
                    return Mem.ReadStringUnicode(false, 0x30002B5C + index, 32);
                if (H2.HType == H2Type.H2server)
                    return Mem.ReadStringUnicode(false, 0x30002708 + index, 32);
                return string.Empty;
            }
            set
            {
                if (H2.HType == H2Type.Halo2Vista)
                    Mem.WriteStringUnicode(false, 0x30002B5C + index, (string)value);
                if (H2.HType == H2Type.H2server)
                    Mem.WriteStringUnicode(false, 0x30002708 + index, (string)value);
            }
        }
        /// <summary>
        /// Gets or Sets the Primary Armor Color of the player instance
        /// </summary>
        public Color PrimaryPlayer
        {
            get
            {
                if (H2.HType == H2Type.Halo2Vista)
                    return (Color)Mem.ReadByte(false, 0x30002B9C + index);
                if (H2.HType == H2Type.H2server)
                    return (Color)Mem.ReadByte(false, 0x30002748 + index);
                return Color.White;
            }
            set
            {
                if (H2.HType == H2Type.Halo2Vista)
                    Mem.WriteByte(false, 0x30002B9C + index, (byte)value);
                if(H2.HType == H2Type.H2server)
                    Mem.WriteByte(false, 0x30002748 + index, (byte)value);
            }
        }
        /// <summary>
        /// Gets or Sets the Secondary Armor Color of the player instance
        /// </summary>
        public Color SecondaryPlayer
        {
            get
            {
                if (H2.HType == H2Type.Halo2Vista)
                    return (Color)Mem.ReadByte(false, 0x30002B9D + index);
                if (H2.HType == H2Type.H2server)
                    return (Color)Mem.ReadByte(false, 0x30002749 + index);
                return Color.White;
            }
            set
            {
                if (H2.HType == H2Type.Halo2Vista)
                    Mem.WriteByte(false, 0x30002B9D + index, (byte)value);
                if (H2.HType == H2Type.H2server)
                    Mem.WriteByte(false, 0x30002749 + index, (byte)value);
            }
        }
        /// <summary>
        /// Gets or Sets the Primary Emblem Color of the player instance
        /// </summary>
        public Color PrimaryEmblem
        {
            get
            {
                if (H2.HType == H2Type.Halo2Vista)
                    return (Color)Mem.ReadByte(false, 0x30002B9E + index);
                if (H2.HType == H2Type.H2server)
                    return (Color)Mem.ReadByte(false, 0x3000274A + index);
                return Color.White;
            }
            set
            {
                if (H2.HType == H2Type.Halo2Vista)
                    Mem.WriteByte(false, 0x30002B9E + index, (byte)value);
                if (H2.HType == H2Type.H2server)
                    Mem.WriteByte(false, 0x3000274A + index, (byte)value);
            }
        }
        /// <summary>
        /// Gets or Sets the Secondary Emblem Color of the player instance
        /// </summary>
        public Color SecondaryEmblem
        {
            get
            {
                if (H2.HType == H2Type.Halo2Vista)
                    return (Color)Mem.ReadByte(false, 0x30002B9F + index);
                if (H2.HType == H2Type.H2server)
                    return (Color)Mem.ReadByte(false, 0x3000274B + index);
                return Color.White;
            }
            set
            {
                if (H2.HType == H2Type.Halo2Vista)
                    Mem.WriteByte(false, 0x30002B9F + index, (byte)value);
                if (H2.HType == H2Type.H2server)
                    Mem.WriteByte(false, 0x3000274B + index, (byte)value);
            }
        }
        /// <summary>
        /// Gets or Sets the Emblem Foreground Image of the player instance
        /// </summary>
        public Foreground EmblemForeground
        {
            get
            {
                if (H2.HType == H2Type.Halo2Vista)
                    return (Foreground)Mem.ReadByte(false, 0x30002BA1 + index);
                if (H2.HType == H2Type.H2server)
                    return (Foreground)Mem.ReadByte(false, 0x3000274D + index);
                return Foreground.Marathon;
            }
            set
            {
                if (H2.HType == H2Type.Halo2Vista)
                    Mem.WriteByte(false, 0x30002BA1 + index, (byte)value);
                if (H2.HType == H2Type.H2server)
                    Mem.WriteByte(false, 0x3000274D + index, (byte)value);
            }
        }
        /// <summary>
        /// Gets or Sets the Emblem Backround Image of the player instance
        /// </summary>
        public Background EmblemBackground
        {
            get
            {
                if (H2.HType == H2Type.Halo2Vista)
                    return (Background)Mem.ReadByte(false, 0x30002BA2 + index);
                if (H2.HType == H2Type.H2server)
                    return (Background)Mem.ReadByte(false, 0x3000274E + index);
                return Background.Solid;
            }
            set
            {
                if (H2.HType == H2Type.Halo2Vista)
                    Mem.WriteByte(false, 0x30002BA2 + index, (byte)value);
                if (H2.HType == H2Type.H2server)
                    Mem.WriteByte(false, 0x3000274E + index, (byte)value);
            }
        }
        /// <summary>
        /// Gets or Sets the Emblem X toggle setting of the player instance
        /// </summary>
        public bool EmblemToggle
        {
            get
            {
                if (H2.HType == H2Type.Halo2Vista)
                    return Mem.ReadBool(false, 0x30002BA3 + index);
                if (H2.HType == H2Type.H2server)
                    return Mem.ReadBool(false, 0x3000274F + index);
                return false;
            }
            set
            {
                if (H2.HType == H2Type.Halo2Vista)
                    Mem.writebool(false, 0x30002BA3 + index, value);
                if (H2.HType == H2Type.H2server)
                    Mem.writebool(false, 0x3000274F + index, value);
            }
        }
        /// <summary>
        /// Gets or Sets the Biped of the player instance
        /// </summary>
        public Biped PlayerModel
        {
            get
            {
                if (H2.HType == H2Type.Halo2Vista)
                    return (Biped)Mem.ReadByte(false, 0x30002BA0 + index);
                if (H2.HType == H2Type.H2server)
                    return (Biped)Mem.ReadByte(false, 0x3000274C + index);
                return Biped.Spartan;
            }
            set
            {
                if (H2.HType == H2Type.Halo2Vista)
                    Mem.WriteByte(false, 0x30002BA0 + index, (byte)value);
                if (H2.HType == H2Type.H2server)
                    Mem.WriteByte(false, 0x3000274C + index, (byte)value);
            }
        }
        /// <summary>
        /// Gets or Sets the Team of the player instance
        /// </summary>
        public Team PlayerTeam
        {
            get
            {
                if (H2.HType == H2Type.Halo2Vista)
                    return (Team)Mem.ReadByte(false, 0x30002BD8 + index);
                if (H2.HType == H2Type.H2server)
                    return (Team)Mem.ReadByte(false, 0x30002784 + index);
                return Team.Observer;
            }
            set
            {
                if (H2.HType == H2Type.Halo2Vista)
                    Mem.WriteByte(false, 0x30002BD8 + index, (byte)value);
                if (H2.HType == H2Type.H2server)
                    Mem.WriteByte(false, 0x30002784 + index, (byte)value);
            }
        }
        /// <summary>
        /// Gets or Sets the Handicap level of the player instance
        /// </summary>
        public Handicap PlayerHandicap
        {
            get
            {
                if (H2.HType == H2Type.Halo2Vista)
                    return (Handicap)Mem.ReadByte(false, 0x30002BD9 + index);
                if (H2.HType == H2Type.H2server)
                    return (Handicap)Mem.ReadByte(false, 0x30002785 + index);
                return Handicap.None;
            }
            set
            {
                if (H2.HType == H2Type.Halo2Vista)
                    Mem.WriteByte(false, 0x30002BD9 + index, (byte)value);
                if (H2.HType == H2Type.H2server)
                    Mem.WriteByte(false, 0x30002785 + index, (byte)value);
            }   
        }
        /// <summary>
        /// Gets or Sets the Primary ammo count of the player instance
        /// </summary>
        public int PrimaryAmmo
        {
            get
            {
                return Mem.ReadInt(false, H2.GetPlayerDynamic(index / 0x204) + 6364);
            }
            set
            {
                Mem.WriteInt(false, H2.GetPlayerDynamic(index / 0x204) + 6364, value);
                Mem.WriteInt(false, H2.GetPlayerDynamic(index / 0x204) + 8906, value);
            }
        }
        /// <summary>
        /// Gets or Sets the Player Speed of the player instance "Can only be used when OpCode.PatchSpeed() is applied to the game"
        /// </summary>
        public float Speed
        {
            get
            {//0x30002BD9
                if (H2.HType == H2Type.Halo2Vista)
                    return Mem.ReadFloat(false, 0x30002C9C + index);
                if (H2.HType == H2Type.H2server)
                    return Mem.ReadFloat(false, 0x30002848 + index);
                return -1f;
            }
            set
            {
                if (H2.HType == H2Type.Halo2Vista)
                    Mem.WriteFloat(false, 0x30002C9C + index, value);
                if (H2.HType == H2Type.H2server)
                    Mem.WriteFloat(false, 0x30002848 + index, value);
            }
        }
        public float RespawnTimer
        {
            get
            {
                if (H2.HType == H2Type.Halo2Vista)
                    return Mem.ReadFloat(false, 0x30002C64 + index);
                if (H2.HType == H2Type.H2server)
                    return Mem.ReadFloat(false, 0x30002810 + index);
                return -1;
            }
            set
            {
                if (H2.HType == H2Type.Halo2Vista)
                    Mem.WriteFloat(false, 0x30002C64 + index, value);
                if (H2.HType == H2Type.H2server)
                    Mem.WriteFloat(false, 0x30002810 + index, value);
            }
        }
        public float XPos
        {
            get
            {
                return Mem.ReadFloat(false, H2.GetPlayerDynamic(index / 0x204) + 0x64);
            }
            set
            {
                Mem.WriteFloat(false, H2.GetPlayerDynamic(index / 0x204) + 0x264, value);
            }
        }
        public float YPos
        {
            get
            {
                return Mem.ReadFloat(false, H2.GetPlayerDynamic(index / 0x204) + 0x68);
            }
            set
            {
                Mem.WriteFloat(false, H2.GetPlayerDynamic(index / 0x204) + 0x268, value);
            }
        }
        public float ZPos
        {
            get
            {
                return Mem.ReadFloat(false, H2.GetPlayerDynamic(index / 0x204) + 0x6C);
            }
            set
            {
                Mem.WriteFloat(false, H2.GetPlayerDynamic(index / 0x204) + 0x26C, value);
            }
        }
        public int CurrentPlane
        {
            get
            {
                return Mem.ReadInt(false, H2.GetPlayerDynamic(index / 0x204) + 0x468);
            }
        }
        public int CurrentPlane2
        {
            get
            {
                return Mem.ReadInt(false, H2.GetPlayerDynamic(index / 0x204) + 0x46C);
            }
        }
        public float CurrentPlaneAngle
        {
            get
            {
                return Mem.ReadFloat(false, H2.GetPlayerDynamic(index / 0x204) + 0x394);
            }
            set
            {
                Mem.WriteFloat(false, H2.GetPlayerDynamic(index / 0x204) + 0x394, value);
            }
        }
        public string SGamerTag
        {
            get
            {
                if (HType == H2Type.Halo2Vista)
                    return H2.H2Mem.ReadStringUnicode(true, Offset + 36 + (0x10c * (index / 0x204)), 36);
                if (HType == H2Type.H2server)
                    return H2.H2Mem.ReadStringUnicode(true, 0x532204 + (0x10c * (index / 0x204)), 36);
                return "";
            }
        }
        public string IPAdress
        {
            get
            {
                if (HType == H2Type.Halo2Vista)
                {
                    byte[] tmp =  H2.H2Mem.ReadMem(Offset + (0x10c * (index / 0x204)),4,true);
                    return BitConverter.ToInt32(tmp, 0).ToString("X");
                    //return tmp[0].ToString() + "." + tmp[1].ToString() + "." + tmp[2].ToString() + "." + tmp[3].ToString();
                }
                if (HType == H2Type.H2server)
                    return "";// H2.H2Mem.ReadInt(true, 0x5321E0 + (0x10c * (index / 0x204)));
                return "";
            }
        }
        public int PCIdentifier
        {
            get
            {
                if (HType == H2Type.Halo2Vista)
                    return BitConverter.ToInt32(H2.H2Mem.ReadMem(Offset + 8 + (0x10c * (index / 0x204)), 4, true), 0);
                if (HType == H2Type.H2server)
                    return BitConverter.ToInt32(H2.H2Mem.ReadMem(0x5321E8 + (0x10c * (index / 0x204)), 4, true), 0);
                return 0;
            }
        }
        public int PCAccount
        {
            get
            {
                if (HType == H2Type.Halo2Vista)
                    return BitConverter.ToInt32(H2.H2Mem.ReadMem(Offset + 20 + (0x10c * (index / 0x204)), 4, true), 0);
                if (HType == H2Type.H2server)
                    return BitConverter.ToInt32(H2.H2Mem.ReadMem(0x5321F4 + (0x10c * (index / 0x204)), 4, true), 0);
                return 0;
            }
        }
        #endregion
        /// <summary>
        /// Overloaded constructor
        /// </summary>
        public Player(H2Memory  H2, int index)
        {
            this.Mem = H2.H2Mem;
            this.H2 = H2;
            this.HType = H2.HType;
            this.index = index * 0x204;
            if (HType == H2Type.Halo2Vista)
            {
                if ((Mem.ReadInt(true, 0x5057AC)).ToString() == "0") Offset = 0x50D334;
                else Offset = 0x5057AC;
            }
        }
        /// <summary>
        /// Default constructor
        /// </summary>
        public Player() { }
        /// <summary>
        /// Forces the player to drop there weapons infinately.
        /// </summary>
        public void TakeWeapons()
        {
            Mem.WriteMem(H2.GetPlayerDynamic(this.index / 0x204) + 24, new byte[] { 0xff, 0xff, 0xff, 0xff }, false);
        }
        /// <summary>
        /// Kills the player without remorse (Troll)
        /// </summary>
        public void KillPlayer()
        {
            Mem.WriteMem(H2.GetPlayerDynamic(this.index / 0x204) + 0x208, new byte[] { 0x01, 0xFE, 0xFE, 0xFF }, false);
        }
    }

    public class Map
    {
        private H2Memory H2;

        public Map(H2Memory H2)
        {
            this.H2 = H2;
        }

        /// <summary>
        /// Resets the current map to default
        /// </summary>
        public void ResetMap()
        {
            if (H2.HType == H2Type.Halo2Vista)
                H2.H2Mem.WriteInt(false, 0x300056C4, 0);
            if (H2.HType == H2Type.H2server) // check if correct
                H2.H2Mem.WriteInt(false, 0x30005270, 0);
        }
        /// <summary>
        /// Gets the current map
        /// </summary>
        /// <returns>name of the map</returns>
        public string CurrentMap()
        {
            #region Halo2Vista
            if (H2.HType == H2Type.Halo2Vista)
                return H2.H2Mem.ReadStringUnicode(true, 0x47cf0c, 32);
            #endregion
            #region H2Server
            if (H2.HType == H2Type.H2server)
                if (H2.MainMenuCheck())
                    return "mainmenu";
                else
                    return H2.H2Mem.ReadStringAscii(true, 0x4A2B74, 32);
            #endregion //NEEDS WORK
            return string.Empty;
        }
    }
    #region Camera Stuff
    public class Camera
    {
        private H2Memory H2;

        public void Map(H2Memory H2)
        {
            this.H2 = H2;
        }

        public byte GetCameraModifier()
        {
            if (H2.H2Mem.ReadByte(true, 0x4a84b0) != 0xE1 && H2.H2Mem.ReadByte(true, 0x4a84b0) != 0x44)
                return (byte)(H2.H2Mem.ReadByte(true, 0x4a84b2) + 3);
            else
                return (byte)(H2.H2Mem.ReadByte(true, 0x4a84b2));
        }
        public void ThirdPersonCamMode()
        {
            if (GetCameraModifier() != ((byte)0xFF))
            {
                H2.H2Mem.WriteMem(0x4a84b0, new byte[3] { 0x44, 0xD1, GetCameraModifier() }, true); //set camera state.
                H2.H2Mem.WriteByte(true, 0x4a84ae, 1);
                H2.H2Mem.WriteFloat(true, 0x4A84CC, 1);
            }
        }
        public void NormalCamMode()
        {
            if (GetCameraModifier() != ((byte)0xFF))
            {
                H2.H2Mem.WriteMem(0x4a84b0, new byte[3] { 0xE1, 0xD7, GetCameraModifier() }, true); //set camera state.
                H2.H2Mem.WriteByte(true, 0x4a84ae, 0);
            }
        }
        public void StillCamMode()
        {
            if (GetCameraModifier() != ((byte)0xFF))
            {
                H2.H2Mem.WriteMem(0x4a84b0, new byte[3] { 0x39, 0x80, ((byte)(GetCameraModifier() - 3)) }, true); //set camera state.
                H2.H2Mem.WriteByte(true, 0x4a84ae, 1);
            }
        }
    }
    #endregion
}
