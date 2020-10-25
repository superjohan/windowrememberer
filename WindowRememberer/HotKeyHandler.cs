using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowRememberer
{
    class HotKeyHandler
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        enum EventId
        {
            Left = 1,
            Right = 2,
            Up = 3,
            Down = 4
        }

        public HotKeyHandler(IntPtr handle)
        {
            int baseModifiers = 1 | 4 | 8; // alt - shift - win

            // TODO: maybe these need to be unregistered on quit?
            RegisterHotKey(handle, (int)EventId.Left, baseModifiers, (int)Keys.Left);
            RegisterHotKey(handle, (int)EventId.Right, baseModifiers, (int)Keys.Right);
            RegisterHotKey(handle, (int)EventId.Up, baseModifiers, (int)Keys.Up);
            RegisterHotKey(handle, (int)EventId.Down, baseModifiers, (int)Keys.Down);
        }

        public void OnHotKeyPressed(IntPtr param)
        {            
            uint p = (uint)param.ToInt64();
            Keys key = (Keys)((p & 0xffff0000) >> 16);
            Debug.WriteLine("key: " + key);
        }
    }
}
