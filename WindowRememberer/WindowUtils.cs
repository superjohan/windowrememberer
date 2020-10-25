using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowRememberer
{
    class WindowUtils
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref Rect Rect);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int Width, int Height, bool Repaint);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        public static Process[] WindowedProcesses()
        {
            Process[] processlist = Process.GetProcesses();

            return processlist.Where(process => !string.IsNullOrEmpty(process.MainWindowTitle)).ToArray();
        }

        public static Screen CurrentWindowScreen()
        {
            var handle = GetForegroundWindow();

            return Screen.FromHandle(handle);
        }

        public static void SetCurrentWindowRect(Rect rect)
        {
            var handle = GetForegroundWindow();

            if (!MoveWindow(handle, rect.left, rect.top, rect.width, rect.height, true))
            {
                Debug.WriteLine("Could not set rect for window.");
            }
        }
    }
}
