using System.Diagnostics;
using System.Runtime.InteropServices;
using System;
using System.Windows.Forms;
using System.Text;
using System.Linq;
using System.Threading;
namespace H2Memory_class
{
    public class ProcessMemory
    {
        #region Fields
        protected int[] BaseAddress;
        protected Process[] BaseProcesses;
        protected ProcessModule[] BaseModule;
        public string[] BaseServerNames { get; set; }
        #region Constants
        private const uint PAGE_EXECUTE = 16;
        private const uint PAGE_EXECUTE_READ = 32;
        private const uint PAGE_EXECUTE_READWRITE = 64;
        private const uint PAGE_EXECUTE_WRITECOPY = 128;
        private const uint PAGE_GUARD = 256;
        private const uint PAGE_NOACCESS = 1;
        private const uint PAGE_NOCACHE = 512;
        private const uint PAGE_READONLY = 2;
        private const uint PAGE_READWRITE = 4;
        private const uint PAGE_WRITECOPY = 8;
        private const uint PROCESS_ALL_ACCESS = 2035711;
        #endregion
        protected int[] BaseHandle;
        public IntPtr[] BaseMainHandle;
        public int BaseIndex;
        protected string BaseProcessName;
        public int Proccount { get; set; }
        protected H2Type HType;
        #endregion
        #region Imports
        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(int hObject);
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern int FindWindowByCaption(int ZeroOnly, string lpWindowName);
        [DllImport("kernel32.dll")]
        public static extern int OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesRead);
        [DllImport("kernel32.dll")]
        public static extern bool VirtualProtectEx(int hProcess, int lpAddress, int dwSize, uint flNewProtect, out uint lpflOldProtect);
        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesWritten);
        #endregion
        /// <summary>
        /// Creates a New instance of Processmemory with the Designated H2Type
        /// </summary>
        /// <param name="Type">Designated H2Type</param>
        public ProcessMemory(H2Type Type)
        {
            if (Type == H2Type.H2server) BaseProcessName = "H2server";
            if (Type == H2Type.Halo2Vista) BaseProcessName = "Halo2";
            this.HType = Type;
        }
        #region Methods
        /// <summary>
        /// Skips to the next process in the list of process's
        /// </summary>
        public void NextProc()
        {
            if (BaseIndex + 1 < Proccount) BaseIndex += 1;
            else BaseIndex = 0;
        }
        /// <summary>
        /// skips to the specified index
        /// </summary>
        /// <param name="index">Index to skip to</param>
        public void SkipToProc(int index)
        {
            if (index < Proccount - 1)
                BaseIndex = index;
        }
        /// <summary>
        /// Skips to the specified server
        /// </summary>
        /// <param name="sName">Name of server to skip to</param>
        public void SkipToProc(string sName)
        {
            if (BaseServerNames.Contains(sName) && HType == H2Type.H2server) BaseIndex = BaseServerNames.ToList().IndexOf(sName);
            else throw new Exception();
        }
        /// <summary>
        /// refreshes the process list
        /// </summary>
        public void Refresh()
        {
            if (this.BaseProcessName != "")
            {
                this.BaseProcesses = Process.GetProcessesByName(this.BaseProcessName);
                this.BaseHandle = new int[BaseProcesses.Length];
                this.BaseModule = new ProcessModule[BaseProcesses.Length];
                this.BaseAddress = new int[BaseProcesses.Length];
                this.BaseServerNames = new string[BaseProcesses.Length];
                this.BaseMainHandle = new IntPtr[BaseProcesses.Length];
                this.Proccount = BaseProcesses.Length;
                for (int i = 0; i < BaseProcesses.Length; i++)
                {
                    this.BaseHandle[i] = OpenProcess(PROCESS_ALL_ACCESS, false, this.BaseProcesses[i].Id);
                    this.BaseMainHandle[i] = BaseProcesses[i].MainWindowHandle;
                    BaseIndex = i;
                    if (this.HType == H2Type.H2server)
                        this.BaseServerNames[i] = this.ReadStringUnicode(true, 0x52040a, 32);
                }
                BaseIndex = 0;
            }
        }
        /// <summary>
        /// Checks if the Process is runnig
        /// </summary>
        /// <returns>if the process is running</returns>
        public bool CheckProcess()
        {
            return (Process.GetProcessesByName(this.BaseProcessName).Length > 0);
        }
        /// <summary>
        /// Starts The process memory hook
        /// </summary>
        public void StartProcess()
        {
            if (this.BaseProcessName != "")
            {
                this.BaseProcesses = Process.GetProcessesByName(this.BaseProcessName);
                this.BaseHandle = new int[BaseProcesses.Length];
                this.BaseModule = new ProcessModule[BaseProcesses.Length];
                this.BaseAddress = new int[BaseProcesses.Length];
                this.BaseServerNames = new string[BaseProcesses.Length];
                this.BaseMainHandle = new IntPtr[BaseProcesses.Length];
                this.Proccount = BaseProcesses.Length;
                for (int i = 0; i < BaseProcesses.Length; i++)
                {
                    this.BaseHandle[i] = OpenProcess(PROCESS_ALL_ACCESS, false, this.BaseProcesses[i].Id);
                    this.BaseMainHandle[i] = BaseProcesses[i].MainWindowHandle;
                    BaseIndex = i;
                    if (this.HType == H2Type.H2server)
                        this.BaseServerNames[i] = this.ReadStringUnicode(true, 0x52040a, 32);
                }
                BaseIndex = 0;
            }
        }
        /// <summary>
        /// Trims the given string
        /// </summary>
        /// <param name="mystring">String to trim</param>
        /// <returns>Trims a string</returns>
        public string TrimString(string mystring)
        {
            char[] chArray = mystring.ToCharArray();
            string str = "";
            for (int i = 0; i < mystring.Length; i++)
            {
                if ((chArray[i] == ' ') && (chArray[i + 1] == ' ')) return str;
                if (chArray[i] == '\0') return str;
                str = str + chArray[i].ToString();
            }
            return mystring.TrimEnd(new char[] { '0' });
        }
        /// <summary>
        /// Gets the Base adress of a specified DLL
        /// </summary>
        /// <param name="dllname">DLL name to locate</param>
        /// <returns>Address to dll base</returns>
        public int DLLBaseAddress(string dllname)
        {
            ProcessModuleCollection modules = this.BaseProcesses[BaseIndex].Modules;
            foreach (ProcessModule procmodule in modules)
                if (dllname == procmodule.ModuleName)
                    return (int)procmodule.BaseAddress;
            return -1;
        }
        /// <summary>
        /// Gets the Image Address of a process
        /// </summary>
        /// <returns>Address to base</returns>
        public int ImageAddress()
        {
            this.BaseAddress[BaseIndex] = 0;
            this.BaseModule[BaseIndex] = this.BaseProcesses[BaseIndex].MainModule;
            this.BaseAddress[BaseIndex] = (int)this.BaseModule[BaseIndex].BaseAddress;
            return this.BaseAddress[BaseIndex];
        }
        /// <summary>
        /// Gets The Image Address of a Process with the given offset
        /// </summary>
        /// <param name="pOffset">Offset</param>
        /// <returns>Address to offset</returns>
        public int ImageAddress(int pOffset)
        {
            this.BaseAddress[BaseIndex] = 0;
            this.BaseModule[BaseIndex] = this.BaseProcesses[BaseIndex].MainModule;
            this.BaseAddress[BaseIndex] = (int)this.BaseModule[BaseIndex].BaseAddress;
            return (this.BaseAddress[BaseIndex] + pOffset);
        }
        #endregion
        #region Read Memory
        /// <summary>
        /// Reads Raw Memory from a process in bytes
        /// </summary>
        /// <param name="pOffset">Postion offset to read</param>
        /// <param name="pSize">Amount of Bytes to read</param>
        /// <param name="AddToImageAddress">if add to image address or not</param>
        /// <returns>Byte Array</returns>
        public byte[] ReadMem(int pOffset, int pSize, bool AddToImageAddress)
        {
            byte[] buffer = new byte[pSize];
            if (AddToImageAddress) ReadProcessMemory(this.BaseHandle[BaseIndex], this.ImageAddress(pOffset), buffer, pSize, 0);
            else ReadProcessMemory(this.BaseHandle[BaseIndex], pOffset, buffer, pSize, 0);
            return buffer;
        }
        /// <summary>
        /// Reads a Single Byte from the memory
        /// </summary>
        /// <param name="AddToImageAddress">If add to image address or not</param>
        /// <param name="pOffset">Postion offset to read</param>
        /// <returns>Byte</returns>
        public byte ReadByte(bool AddToImageAddress, int pOffset)
        {
            byte[] buffer = new byte[1];
            if (AddToImageAddress) ReadProcessMemory(this.BaseHandle[BaseIndex], ImageAddress(pOffset), buffer, 1, 0);
            else ReadProcessMemory(this.BaseHandle[BaseIndex], pOffset, buffer, 1, 0);
            return buffer[0];
        }
        /// <summary>
        /// Reads a Single Byte from the memory
        /// </summary>
        /// <param name="AddToImageAddress">If add to image address or not</param>
        /// <param name="pOffset">Postion offset to read</param>
        /// <returns>Bool</returns>
        public bool ReadBool(bool Addtoimageadress, int pOffset)
        {
            byte tmp = this.ReadByte(Addtoimageadress, pOffset);
            if (tmp == 0)
                return false;
            if (tmp <= 1)
                return true;
            return false;
        }
        /// <summary>
        /// Reads a byte from a Module
        /// </summary>
        /// <param name="Module">Module Name</param>
        /// <param name="pOffset">Position Offset</param>
        /// <returns>Byte</returns>
        public byte ReadByte(string Module, int pOffset)
        {
            byte[] buffer = new byte[1];
            ReadProcessMemory(this.BaseHandle[BaseIndex], this.DLLBaseAddress(Module) + pOffset, buffer, 1, 0);
            return buffer[0];
        }
        /// <summary>
        /// Reads a Float from the memory
        /// </summary>
        /// <param name="AddToImageAddress">To add to Image adress</param>
        /// <param name="pOffset">Position offset</param>
        /// <returns>Float</returns>
        public float ReadFloat(bool AddToImageAddress, int pOffset)
        {
            return BitConverter.ToSingle(this.ReadMem(pOffset, 4, AddToImageAddress), 0);
        }
        /// <summary>
        /// Reads a Float from the memory in a specific module
        /// </summary>
        /// <param name="Module">module name</param>
        /// <param name="pOffset">position offset</param>
        /// <returns>Float</returns>
        public float ReadFloat(string Module, int pOffset)
        {
            return BitConverter.ToSingle(this.ReadMem(this.DLLBaseAddress(Module) + pOffset, 4, false), 0);
        }
        /// <summary>
        /// Reads a Double from the memory
        /// </summary>
        /// <param name="AddToImageAddress">To add to Image adress</param>
        /// <param name="pOffset">Position offset</param>
        /// <returns>Double</returns>
        public double ReadDouble(bool AddToImageAddress, int pOffset)
        {
            return BitConverter.ToDouble(this.ReadMem(pOffset, 8, AddToImageAddress), 0);
        }
        /// <summary>
        /// Reads a Double from the memory in a specific module
        /// </summary>
        /// <param name="Module">module name</param>
        /// <param name="pOffset">position offset</param>
        /// <returns>Double</returns>
        public double ReadDouble(string Module, int pOffset)
        {
            return BitConverter.ToDouble(this.ReadMem(this.DLLBaseAddress(Module) + pOffset, 4, false), 0);
        }
        /// <summary>
        /// Reads a 4 Byte integer from the memory
        /// </summary>
        /// <param name="AddToImageAddress">Add to image Adress</param>
        /// <param name="pOffset">Postion offset</param>
        /// <returns></returns>
        public int ReadInt(bool AddToImageAddress, int pOffset)
        {
            return BitConverter.ToInt32(this.ReadMem(pOffset, 4, AddToImageAddress), 0);
        }
        /// <summary>
        /// Reads a 4 Byte integer from the memory at the specific module
        /// </summary>
        /// <param name="Module">Module Name</param>
        /// <param name="pOffset">Position offset</param>
        /// <returns>Int</returns>
        public int ReadInt(string Module, int pOffset)
        {
            return BitConverter.ToInt32(this.ReadMem(this.DLLBaseAddress(Module) + pOffset, 4, false), 0);
        }
        /// <summary>
        /// Reads a 2 Byte Short from the memory
        /// </summary>
        /// <param name="AddToImageAddress">To add to image adress or not</param>
        /// <param name="pOffset">Position offset</param>
        /// <returns>Short</returns>
        public short ReadShort(bool AddToImageAddress, int pOffset)
        {
            return BitConverter.ToInt16(this.ReadMem(pOffset, 2, AddToImageAddress), 0);
        }
        /// <summary>
        /// Reads a 2 Byte Short from the memory at the specific module
        /// </summary>
        /// <param name="Module">Module Name</param>
        /// <param name="pOffset">Position offset</param>
        /// <returns></returns>
        public short ReadShort(string Module, int pOffset)
        {
            return BitConverter.ToInt16(this.ReadMem(this.DLLBaseAddress(Module) + pOffset, 2, false), 0);
        }
        /// <summary>
        /// Reads a ASCII String from the memory
        /// </summary>
        /// <param name="AddToImageAddress">To add to image adress or not</param>
        /// <param name="pOffset">Position offset</param>
        /// <returns>String</returns>
        public string ReadStringAscii(bool AddToImageAddress, int pOffset, int pSize)
        {
            return this.TrimString(Encoding.ASCII.GetString(this.ReadMem(pOffset, pSize, AddToImageAddress)));
        }
        /// <summary>
        /// Reads a ASCII from the memory at the specific module
        /// </summary>
        /// <param name="Module">Module Name</param>
        /// <param name="pOffset">Position offset</param>
        /// <returns>String</returns>
        public string ReadStringAscii(string Module, int pOffset, int pSize)
        {
            return this.TrimString(Encoding.ASCII.GetString(this.ReadMem(this.DLLBaseAddress(Module) + pOffset, pSize, false)));
        }
        /// <summary>
        /// Reads a Unicode String from the memory
        /// </summary>
        /// <param name="AddToImageAddress">To add to image adress or not</param>
        /// <param name="pOffset">Position offset</param>
        /// <returns>String</returns>
        public string ReadStringUnicode(bool AddToImageAddress, int pOffset, int pSize)
        {
            return this.TrimString(Encoding.Unicode.GetString(this.ReadMem(pOffset, pSize, AddToImageAddress)));
        }
        /// <summary>
        /// Reads a Unicode from the memory at the specific module
        /// </summary>
        /// <param name="Module">Module Name</param>
        /// <param name="pOffset">Position offset</param>
        /// <returns>String</returns>
        public string ReadStringUnicode(string Module, int pOffset, int pSize)
        {
            return this.TrimString(Encoding.Unicode.GetString(this.ReadMem(this.DLLBaseAddress(Module) + pOffset, pSize, false)));
        }
        #endregion
        #region write memory
        public void WriteMem(int pOffset, byte[] pBytes, bool AddToImageAddress)
        {
            if (AddToImageAddress)
                WriteProcessMemory(this.BaseHandle[BaseIndex], this.ImageAddress(pOffset), pBytes, pBytes.Length, 0);
            else
                WriteProcessMemory(this.BaseHandle[BaseIndex], pOffset, pBytes, pBytes.Length, 0);
        }
        public void writebool( bool addtoimageadress, int pOffset, bool pBytes)
        {
            if (pBytes == false)
                this.WriteByte(addtoimageadress, pOffset, 0);
            if (pBytes == true)
                this.WriteByte(addtoimageadress, pOffset, 1);
        }
        public void WriteShort(bool addtoimageadress, int pOffset, short pBytes)
        {
            this.WriteMem(pOffset, BitConverter.GetBytes(pBytes), addtoimageadress);
        }
        public void WriteShort(string Module, int pOffset, short pBytes)
        {
            this.WriteMem(this.DLLBaseAddress(Module) + pOffset, BitConverter.GetBytes(pBytes), false);
        }
        public void WriteStringAscii(bool AddToImageAddress, int pOffset, string pBytes)
        {
            this.WriteMem(pOffset, Encoding.ASCII.GetBytes(pBytes + "\0"), AddToImageAddress);
        }
        public void WriteStringAscii(string Module, int pOffset, string pBytes)
        {
            this.WriteMem(this.DLLBaseAddress(Module) + pOffset, Encoding.ASCII.GetBytes(pBytes + "\0"), false);
        }
        public void WriteStringUnicode(bool AddToImageAddress, int pOffset, string pBytes)
        {
            this.WriteMem(pOffset, Encoding.Unicode.GetBytes(pBytes + "\0"), AddToImageAddress);
        }
        public void WriteStringUnicode(string Module, int pOffset, string pBytes)
        {
            this.WriteMem(this.DLLBaseAddress(Module) + pOffset, Encoding.Unicode.GetBytes(pBytes + "\0"), false);
        }
        public void WriteInt(bool AddToImageAddress, int pOffset, int pBytes)
        {
            this.WriteMem(pOffset, BitConverter.GetBytes(pBytes), AddToImageAddress);
        }
        public void WriteInt(string Module, int pOffset, int pBytes)
        {
            this.WriteMem(this.DLLBaseAddress(Module) + pOffset, BitConverter.GetBytes(pBytes), false);
        }
        public void WriteFloat(bool AddToImageAddress, int pOffset, float pBytes)
        {
            this.WriteMem(pOffset, BitConverter.GetBytes(pBytes), AddToImageAddress);
        }
        public void WriteFloat(string Module, int pOffset, float pBytes)
        {
            this.WriteMem(this.DLLBaseAddress(Module) + pOffset, BitConverter.GetBytes(pBytes), false);
        }
        public void WriteDouble(bool AddToImageAddress, int pOffset, double pBytes)
        {
            this.WriteMem(pOffset, BitConverter.GetBytes(pBytes), AddToImageAddress);
        }
        public void WriteDouble(string Module, int pOffset, double pBytes)
        {
            this.WriteMem(this.DLLBaseAddress(Module) + pOffset, BitConverter.GetBytes(pBytes), false);
        }
        public void WriteByte(bool AddToImageAddress, int pOffset, byte pBytes)
        {
            this.WriteMem(pOffset, new byte[]{pBytes}, AddToImageAddress);
        }
        public void WriteByte(string Module, int pOffset, byte pBytes)
        {
            this.WriteMem(this.DLLBaseAddress(Module) + pOffset, BitConverter.GetBytes((byte)pBytes), false);
        }
        #endregion
        #region Pointers
        public int MapBasePointer(int pOff)
        {
            #region Halo2Vista
            if (HType == H2Type.Halo2Vista) return (this.ReadInt(true, 0x479E70) + pOff);
            #endregion
            #region H2Server
            if (HType == H2Type.H2server) return (this.ReadInt(true, 0x4A642C) + pOff);
            #endregion
            return -1;
        }
        public int Pointer(bool AddToImageAddress, int pOffset)
        {
            return this.ReadInt(AddToImageAddress, pOffset);
        }
        public int Pointer(string Module, int pOffset)
        {
            return this.ReadInt(false, this.DLLBaseAddress(Module) + pOffset);
        }
        public int Pointer(bool AddToImageAddress, int pOffset, int pOffset2)
        {
            return (this.ReadInt(AddToImageAddress, pOffset) + pOffset2);
        }
        public int Pointer(string Module, int pOffset, int pOffset2)
        {
            return (this.ReadInt(false, this.DLLBaseAddress(Module) + pOffset) + pOffset2);
        }
        public int Pointer(bool AddToImageAddress, int pOffset, int pOffset2, int pOffset3)
        {
            return (this.ReadInt(false, this.ReadInt(AddToImageAddress, pOffset) + pOffset2) + pOffset3);
        }
        public int Pointer(string Module, int pOffset, int pOffset2, int pOffset3)
        {
            return (this.ReadInt(false, this.ReadInt(false, this.DLLBaseAddress(Module) + pOffset) + pOffset2) + pOffset3);
        }
        public int Pointer(bool AddToImageAddress, int pOffset, int pOffset2, int pOffset3, int pOffset4)
        {
            return (this.ReadInt(false, this.ReadInt(false, this.ReadInt(AddToImageAddress, pOffset) + pOffset2) + pOffset3) + pOffset4);
        }
        public int Pointer(string Module, int pOffset, int pOffset2, int pOffset3, int pOffset4)
        {
            return (this.ReadInt(false, this.ReadInt(false, this.ReadInt(false, this.DLLBaseAddress(Module) + pOffset) + pOffset2) + pOffset3) + pOffset4);
        }
        #endregion
    }
}