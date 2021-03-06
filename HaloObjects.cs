﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        /// Gets or Sets the Team of the player
        /// </summary>
        public Team PlayerTeam
        {
            get
            {return (Team)Mem.ReadByte(false, ((H2.HType == H2Type.Halo2Vista) ? 0x30002BD8 : 0x30002784) + index);}
            set
            { Mem.WriteByte(false, ((H2.HType == H2Type.Halo2Vista) ? 0x30002BD8 : 0x30002784) + index, (byte)value); }
        }
        /// <summary>
        /// Gets or Sets the Handicap level of the player
        /// </summary>
        public Handicap PlayerHandicap
        {
            get
            { return (Handicap)Mem.ReadByte(false, ((H2.HType == H2Type.Halo2Vista) ? 0x30002BD9 : 0x30002785) + index); }
            set
            { Mem.WriteByte(false, ((H2.HType == H2Type.Halo2Vista) ? 0x30002BD9 : 0x30002785) + index, (byte)value); }
        }
        /// <summary>
        /// Gets or Sets the Player's Speed"Can only be used when OpCode.PatchSpeed(H2Memory H2) is applied to the game"
        /// </summary>
        public float Speed
        {//0x30002BD9 Whats this comment for?
            get
            { return Mem.ReadFloat(false, ((H2.HType == H2Type.Halo2Vista) ? 0x30002C9C : 0x30002848) + index); }
            set
            { Mem.WriteFloat(false, ((H2.HType == H2Type.Halo2Vista) ? 0x30002C9C : 0x30002848) + index, value); }
        }
        /// <summary>
        /// Get
        /// </summary>
        public float RespawnTimer
        {
            get
            { return Mem.ReadFloat(false, ((H2.HType == H2Type.Halo2Vista) ? 0x30002C64 : 0x30002810) + index); }
            set
            { Mem.WriteFloat(false, ((H2.HType == H2Type.Halo2Vista) ? 0x30002C64 : 0x30002810) + index, value); }
        }
        /// <summary>
        /// Gets or Sets the players X-Axis Position
        /// </summary>
        public float XPos
        {
            get
            { return Mem.ReadFloat(false, DynamicObjTable.GetPlayerDynamic(H2, index / 0x204) + 0x64); }
            set
            { Mem.WriteFloat(false, DynamicObjTable.GetPlayerDynamic(H2, index / 0x204) + 0x64, value); }
        }
        /// <summary>
        /// Gets or Sets the players Y-Axis Position
        /// </summary>
        public float YPos
        {
            get
            {return Mem.ReadFloat(false, DynamicObjTable.GetPlayerDynamic(H2,index / 0x204) + 0x68); }
            set
            { Mem.WriteFloat(false, DynamicObjTable.GetPlayerDynamic(H2,index / 0x204) + 0x68, value);}
        }
        /// <summary>
        /// Gets or Sets the players Z-Axis Postion
        /// </summary>
        public float ZPos
        {
            get
            {return Mem.ReadFloat(false, DynamicObjTable.GetPlayerDynamic(H2,index / 0x204) + 0x6C);}
            set
            {  Mem.WriteFloat(false, DynamicObjTable.GetPlayerDynamic(H2,index / 0x204) + 0x6C, value); }
        }
        /// <summary>
        /// Returns a int that has somthing to do with the plane or surface you are standing on.
        /// </summary>
        public int CurrentPlane
        {
            get
            { return Mem.ReadInt(false, DynamicObjTable.GetPlayerDynamic(H2, index / 0x204) + 0x468); }
        }
        /// <summary>
        /// Returns a int that has somthing to do with the plane or surface you are standing on.
        /// </summary>
        public int CurrentPlane2
        {
            get
            { return Mem.ReadInt(false, DynamicObjTable.GetPlayerDynamic(H2, index / 0x204) + 0x46C); }
        }
        /// <summary>
        /// Returns the angle of standing based on 180º:1f ratio (See Player Data Notes for more details)
        /// </summary>
        public float CurrentPlaneAngle
        {
            get
            { return Mem.ReadFloat(false, DynamicObjTable.GetPlayerDynamic(H2,index / 0x204) + 0x394);}
            set
            { Mem.WriteFloat(false, DynamicObjTable.GetPlayerDynamic(H2,index / 0x204) + 0x394, value);}
        }
        public BipedState BipedState
        {
            get
            { return (BipedState)Mem.ReadByte(false, DynamicObjTable.GetPlayerDynamic(H2, index / 0x204) + 0x3F4);}
            set
            { Mem.WriteByte(false, DynamicObjTable.GetPlayerDynamic(H2, index / 0x204) + 0x3F4, (byte)value); }
        }
        /// <summary>
        /// Returns the players static gamertag, unlike the Dynamic gamertag this can also be the players Profile name.
        /// </summary>
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
        /// <summary>
        /// Returns the players IPAdress in IPv4 format Ex. 127.0.0.1
        /// </summary>
        public string IPAdress
        {
            get
            {
                if (HType == H2Type.Halo2Vista)
                {
                    byte[] tmp =  H2.H2Mem.ReadMem(Offset + (0x10c * (index / 0x204)),4,true);
                    return tmp[0].ToString() + "." + tmp[1].ToString() + "." + tmp[2].ToString() + "." + tmp[3].ToString();
                }
                if (HType == H2Type.H2server)
                    return "";// H2.H2Mem.ReadInt(true, 0x5321E0 + (0x10c * (index / 0x204)));
                return "";
            }
        }
        /// <summary>
        /// Returns the the players "Received" PCID (Look at Player Data Notes for Explenation on "Received PCID Vs. Accutal PCID")
        /// </summary>
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
        /// <summary>
        /// Returns the players "Recived" PCAccount (Look at the Player Data Notes for explenation on "Recived PCA Vs. Acctual PCA")
        /// </summary>
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
            Mem.WriteMem(DynamicObjTable.GetPlayerDynamic(H2,this.index / 0x204) + 24, new byte[] { 0xff, 0xff, 0xff, 0xff }, false);
        }
        /// <summary>
        /// Kills the player without remorse (Troll)
        /// </summary>
        public void KillPlayer()
        {
            Mem.WriteMem(DynamicObjTable.GetPlayerDynamic(H2,this.index / 0x204) + 0x208, new byte[] { 0x01, 0xFE, 0xFE, 0xFF }, false);
        }
        public WeaponBase WeaponOut()
        {
            return DynamicObjTable.GetPlayerWeaponOut(H2, BitConverter.ToInt32(CameraID,0));
        }
    }

    public static class Map
    {
        /// <summary>
        /// Checks if the current map is the mainmenu
        /// </summary>
        /// <returns>Returns true if the map is mainmenu</returns>
        public static bool MainMenuCheck(H2Memory H2)
        {
            #region Halo2Vista
            if (H2.HType == H2Type.Halo2Vista)
            {
                if (H2.H2Mem.ReadStringUnicode(true, 0x47cf0c, 32) != "mainmenu") return true;
                else return false;
            }
            #endregion
            #region H2Server
            if (H2.HType == H2Type.H2server)
            {
                if (H2.H2Mem.ReadInt(true, 0x3C40AC) != 3) return true;
                else return false;
            }
            #endregion
            return false;
        }
        /// <summary>
        /// Resets the current map to default
        /// </summary>
        public static void ResetMap(H2Memory H2)
        {
            H2.H2Mem.WriteInt(false, ((H2.HType == H2Type.Halo2Vista) ? 0x300056C4 : 0x30005270), 0);
        }
        /// <summary>
        /// Gets the current map
        /// </summary>
        /// <returns>name of the map</returns>
        public static string CurrentMap(H2Memory H2)
        {
            #region Halo2Vista
            if (H2.HType == H2Type.Halo2Vista) return H2.H2Mem.ReadStringAscii(true, 0x47cf0c, 32);
            #endregion
            #region H2Server
            if (H2.HType == H2Type.H2server)
                if (MainMenuCheck(H2)) return "mainmenu";
                else return H2.H2Mem.ReadStringAscii(true, 0x4A2B74, 32);
            #endregion //NEEDS WORK
            return string.Empty;
        }
    }
    public class Camera
    {
        private H2Memory H2;
        public Camera(H2Memory H2)
        {
            this.H2 = H2;
        }
        public byte GetCameraModifier()
        {
            if (H2.H2Mem.ReadByte(true, 0x4a84b0) != 0xE1 && H2.H2Mem.ReadByte(true, 0x4a84b0) != 0x44)
                return (byte)(H2.H2Mem.ReadByte(true, 0x4a84b2) + 3);
            else return (byte)(H2.H2Mem.ReadByte(true, 0x4a84b2));
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
    public static class DynamicObjTable
    {
        /// <summary>
        /// Gets the dynamic player address by index
        /// </summary>
        public static int GetPlayerDynamic(H2Memory H2, int index)
        {

            int TempSight = H2.H2Mem.ReadInt(false, ((H2.HType == H2Type.Halo2Vista) ? 0x30002B44 : 0x300026F0) + (index * 0x204));
            if (TempSight != -1 && TempSight != 0)
                for (int j = 0; j < 2048; j++)
                {
                    int DynamicBase = H2.H2Mem.ReadInt(false, ((H2.HType == H2Type.Halo2Vista) ? 0x3003CF3C : 0x3003CAE8) + (j * 12) + 8);
                    int DynamicS = H2.H2Mem.ReadInt(false, DynamicBase + 0x3F8);
                    if (DynamicS == TempSight)
                        return DynamicBase;
                }
            return -1;
        }
        /// <summary>
        /// Gets a List of Offsets for every type of the given weaponclass
        /// </summary>
        public static int[] GetWeaponSet(H2Memory H2, Weapon WeaponClass)
        {
            List<int> Storage = new List<int>();
                for (int j = 0; j < 2048; j++)
                {
                    int DynamicBase = H2.H2Mem.ReadInt(false, ((H2.HType == H2Type.Halo2Vista) ? 0x3003CF3C : 0x3003CAE8) + (j * 12) + 8);
                    int DynamicC = H2.H2Mem.ReadInt(false, DynamicBase);
                    if (DynamicC == (int)WeaponClass)
                        Storage.Add(DynamicBase);
                }
            return Storage.ToArray();
        }
        public static int[] GetAllWeapons(H2Memory H2)
        {
            List<int> Storage = new List<int>();
            for (int i = 0; i < 2048; i++)
            {
                int DynamicBase = H2.H2Mem.ReadInt(false, ((H2.HType == H2Type.Halo2Vista) ? 0x3003CF3C : 0x3003CAE8) + (i * 12) + 8);
                int DynamicC = H2.H2Mem.ReadInt(false, DynamicBase);
                if (Regex.IsMatch(((Weapon)DynamicC).ToString(), "[a-zA-Z]"))
                    Storage.Add(DynamicBase);
            }
            return Storage.ToArray();
        }
        /// <summary>
        /// Gets the WeaponBase that matches the player camera
        /// </summary>
        public static WeaponBase GetPlayerWeaponOut(H2Memory H2, int PlayerCamera)
        {
            for (int j = 0; j < 2048; j++)
            {
                int DynamicBase = H2.H2Mem.ReadInt(false, ((H2.HType == H2Type.Halo2Vista) ? 0x3003CF3C : 0x3003CAE8) + (j * 12) + 8);
                int DynamicS = H2.H2Mem.ReadInt(false, DynamicBase +0x14);
                if (DynamicS == PlayerCamera)
                    return new WeaponBase(H2, DynamicBase);
            }
            return new WeaponBase();
        }
        public static int[] GetAllBases(H2Memory H2)
        {
            int[] Storage = new int[2048];
            for (int i = 0; i < 2048; i++)
                Storage[i] = H2.H2Mem.ReadInt(false, ((H2.HType == H2Type.Halo2Vista) ? 0x3003CF3C : 0x3003CAE8) + (i * 12) + 8);
            return Storage;
        }
    }
    public class WeaponSet : System.Collections.CollectionBase, System.Collections.IEnumerable
    {
        public WeaponSet(H2Memory H2, Weapon WeaponClass)
        {
            foreach (int i in DynamicObjTable.GetWeaponSet(H2, WeaponClass))
                List.Add(new WeaponBase(H2,i));
        }
    }
    public class WeaponBase
    {
        public Weapon WeaponClass = Weapon.None;
        public int Offset;
        private H2Memory H2;
        public WeaponBase()
        {
        }
        public WeaponBase(H2Memory H2, int Offset)
        {
            this.Offset = Offset;
            this.H2 = H2;
            try {WeaponClass = (Weapon)H2.H2Mem.ReadInt(false, Offset);} catch(Exception) {WeaponClass = Weapon.Unknown;};
        }
        public short AmmoInClip
        {
            get
            { return H2.H2Mem.ReadShort(false, Offset + 0x22A); }
            set
            { H2.H2Mem.WriteShort(false, Offset + 0x22A, value); }
        }
        public short AmmoLeft
        {
            get
            { return H2.H2Mem.ReadShort(false, Offset + 0x22C); }
            set
            { H2.H2Mem.WriteShort(false, Offset + 0x22C, value); }
        }
        public float BatteryPower
        {
            get
            { return H2.H2Mem.ReadFloat(false, Offset + 0x184); }
            set
            { H2.H2Mem.WriteFloat(false, Offset + 0x184, value); }
        }
        public float WeaponHeat
        {
            get
            { return H2.H2Mem.ReadFloat(false, Offset + 0x180); }
            set
            { H2.H2Mem.WriteFloat(false, Offset + 0x180, value); }
        }
        public byte[] ControllingCamera
        {
            get
            { return H2.H2Mem.ReadMem(Offset + 0x14, 4, false); }
        }
    }
}
