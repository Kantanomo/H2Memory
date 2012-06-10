using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using System.Collections;
namespace H2Memory_class
{
    public class OpCode
    {
        private bool IsPatched;
        /// <summary>
        /// Constructor function
        /// </summary>
        /// <param name="H2">H2Memory Class</param>
        public OpCode(H2Memory H2)
        {
        }

        /// <summary>
        /// Patches xlive.dll integrity check
        /// </summary>
        /// <param name="H2">H2Memory Class</param>
        public void PatchChecks(H2Memory H2)
        {
            if (IsPatched)
            {
                Int32 xliveBase = H2.H2Mem.DLLBaseAddress("xlive.dll");
                //Thanks to Kiwi who made this code because i couldn't find mine!
                for (Int32 i = 0; i <= 0x351000; i += 1)
                {
                    byte[] Magic = H2.H2Mem.ReadMem(xliveBase + i, 16, false);
                    if (Magic[0] == 0x8b && Magic[1] == 0xec && Magic[2] == 0x83 && Magic[3] == 0xec && Magic[4] == 0x20 && Magic[5] == 0x53 && Magic[6] == 0x56 && Magic[7] == 0x57 && Magic[8] == 0x8d && Magic[9] == 0x45 && Magic[10] == 0xe0 && Magic[11] == 0x33 && Magic[12] == 0xf6 && Magic[13] == 0x50 && Magic[14] == 0xff && Magic[15] == 0x75)
                    {
                        byte[] mBytes = { 0xc2, 0xc, 0x0 };
                        H2.H2Mem.WriteMem((xliveBase + i) - 3, mBytes, false);
                        IsPatched = true;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// movss [edi+00000180],xmm0
        /// to
        /// nop nop nop nop nop nop nop nop nop
        /// </summary>
        /// <param name="H2">H2Memory Class</param>
        public void PatchSpeed(H2Memory H2)
        {
            if (IsPatched)
                if (H2.HType == H2Type.Halo2Vista)
                    H2.H2Mem.WriteMem(0x6AB7f, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 }, true);
                else if(H2.HType == H2Type.H2server)
                    H2.H2Mem.WriteMem(0x6A3BA, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 }, true);
        }
        /// <summary>
        /// mov byte ptr [eax],ff / mov byte ptr [eax-01],ff
        /// to
        /// nop nop nop nop nop nop nop
        /// </summary>
        /// <param name="H2">H2Memory Class</param>
        public void PatchRank(H2Memory H2)
        {
            if (IsPatched)
                if (H2.HType == H2Type.Halo2Vista)
                    H2.H2Mem.WriteMem(0x1B2C29, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 }, true);
        }
    }
}
