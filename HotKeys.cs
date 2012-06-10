using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace H2Memory_class
{
    public class HotKeys
    {
        [DllImport("user32.dll")]
        private static extern short GetKeyState(Keys nVirtKey);
        public static bool Keystate(Keys key)
        {
            int keyState = GetKeyState(key);
            if ((keyState != -127) && (keyState != -128))
            {
                return false;
            }
            return true;
        }
        public static bool Keystate(Keys[] ky)
        {
            int i = 0;
            foreach (Keys Key in ky)
            {
                int keyState = GetKeyState(Key);
                if ((keyState != -127) && (keyState != -128))
                { }
                else i++;
            }
            if (i == ky.Length - 1) return true; else return false;
        }
    }
}
