using System.Diagnostics;
using System.Runtime.InteropServices;
using System;
using System.Windows.Forms;
using System.Text;
using System.Linq;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
namespace H2Memory_class
{
    public class H2Memory
    {
        public ProcessMemory H2Mem;
        public H2Type HType;
        public OpCode OpCodeChanges;
        public bool H2Found = true;

        /// <summary>
        /// Constructor
        /// </summary>
        public H2Memory(H2Type Type, bool Message)
        {
            H2Mem = new ProcessMemory(Type);
            if (H2Mem.CheckProcess())
                H2Mem.StartProcess();
            else
            {
                H2Found = false;
                if (Message)
                {
                    if (MessageBox.Show("H2V and or H2S have not been found.", "", MessageBoxButtons.OK) == DialogResult.OK)
                        Process.GetCurrentProcess().Kill();
                }
            }
            HType = Type;
            this.OpCodeChanges = new OpCode(this);
        }

        /// <summary>
        /// Resets the current round
        /// </summary>
        public void ResetRound()
        {
            if (HType == H2Type.Halo2Vista)
                H2Mem.WriteInt(false, 0x300056C4, 2);
            if (HType == H2Type.H2server) // check if correct
                H2Mem.WriteInt(false, 0x30005270, 2);
        }
        public int GetPlayerCount()
        {
            #region Halo2Vista
            if (HType == H2Type.Halo2Vista) return H2Mem.ReadInt(false, 0x30004B60);
            #endregion
            #region H2Server
            if (HType == H2Type.H2server) return H2Mem.ReadInt(true, 0x53329C);
            #endregion
            return -1;
        }
        public string CurrentVarient()
        {
            #region Halo2Vista
            #endregion
            #region H2SErver
            if (HType == H2Type.H2server)
                return H2Mem.ReadStringUnicode(true, 0x991490, 32);
            #endregion
            return "";
        }
        public string NextVarient()
        {
            #region Halo2Vista
            #endregion
            #region H2SErver
            if (HType == H2Type.H2server)
                return H2Mem.ReadStringUnicode(true, 0x991624, 32);
            #endregion
            return "";
        }
        public EngineType EngineCheck()
        {
            #region Halo2Vista
            if (HType == H2Type.Halo2Vista)
                return (EngineType)(H2Mem.ReadByte(false, 0x30001758));
            #endregion
            #region H2Server
            if (HType == H2Type.H2server)
                return (EngineType)(H2Mem.ReadInt(false, 0x30001304));
            #endregion
            return EngineType.unknown;
        }
        public GameState GameStateCheck()
        {
            #region Halo2Vista
            if (HType == H2Type.Halo2Vista)
                return (GameState)(H2Mem.ReadByte(true, 0x420FC4));
            #endregion
            #region H2Server
            if (HType == H2Type.H2server)
                return (GameState)(H2Mem.ReadByte(true, 0x3C40AC));
            #endregion
            return GameState.unknwon;
        }
        public Player GetPlayer(int index)
        {
            if (HType == H2Type.Halo2Vista)
                return new Player(this, index);
            if (HType == H2Type.H2server)
                return new Player(this, index);
            return new Player();
        }
        public int SecondaryMagic()
        {
            if (HType == H2Type.Halo2Vista)
                return H2Mem.ReadInt(false, H2Mem.ReadInt(false, H2Mem.Pointer(true, 0x479e70, 0x13c4008)) + 8) - (H2Mem.Pointer(true, 0x479e70, 0x13c4000) + H2Mem.ReadInt(true, 0x47cd7c));
            if (HType == H2Type.H2server)
                return H2Mem.ReadInt(false, H2Mem.ReadInt(false, H2Mem.Pointer(true, 0x4A642C, 0x13c4008)) + 8) - (H2Mem.Pointer(true, 0x4A642C, 0x13c4000) + H2Mem.ReadInt(true, 0x4A29E4));
            return -1;
        }
        public int TagOffset(int index)
        {
            if (HType == H2Type.Halo2Vista)
                return H2Mem.ReadInt(false, H2Mem.ReadInt(false, H2Mem.Pointer(true, 0x479E70, 0x13C4008)) + 8 + (index * 0x16)) - SecondaryMagic();
            if (HType == H2Type.H2server)
                return H2Mem.ReadInt(false, H2Mem.ReadInt(false, H2Mem.Pointer(true, 0x4A642C, 0x13C4008)) + 8 + (index * 0x16)) - SecondaryMagic();
            return -1;
        }
        public string LocalGamertag()
        {
            if (HType == H2Type.H2server)
                return H2Mem.ReadStringUnicode(false, 0x52040A, 32);
            return "";
        }
    }
}
