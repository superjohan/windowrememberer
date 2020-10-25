using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace WindowRememberer
{
    class WindowUtils
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref Rect Rect);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int Width, int Height, bool Repaint);

        public static Process[] WindowedProcesses()
        {
            Process[] processlist = Process.GetProcesses();

            return processlist.Where(process => !string.IsNullOrEmpty(process.MainWindowTitle)).ToArray();
        }
    }
}
