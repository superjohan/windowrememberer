using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace WindowRememberer
{
    class WindowPropertyManager
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public int width
            {
                get
                {
                    return right - left;
                }
            }

            public int height
            {
                get
                {
                    return bottom - top;
                }
            }

            override public string ToString()
            {
                return "(x: " + left + ", y: " + top + ", width: " + width + ", height: " + height + ")";
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hWnd, ref Rect Rect);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int Width, int Height, bool Repaint);

        private readonly Dictionary<string, Rect> windowProperties = new Dictionary<string, Rect>();
        
        public void SaveWindowProperties(object sender, EventArgs e)
        {
            Process[] windowedProcesses = WindowedProcesses();

            foreach (Process process in windowedProcesses)
            {
                Rect rect = new Rect();
                if (GetWindowRect(process.MainWindowHandle, ref rect))
                {
                    windowProperties[process.ProcessName] = rect;
                }
            }

            // TODO: Save to disk.
        }

        public void RestoreWindowProperties(object sender, EventArgs e)
        {
            Process[] windowedProcesses = WindowedProcesses();

            foreach (Process process in windowedProcesses)
            {
                string processName = process.ProcessName;

                if (! windowProperties.ContainsKey(processName))
                {
                    continue;
                }

                Rect rect = windowProperties[processName];

                if (! MoveWindow(process.MainWindowHandle, rect.left, rect.top, rect.width, rect.height, true))
                {
                    Debug.WriteLine("Could not restore window for process: " + processName);
                }
            }
        }

        private static Process[] WindowedProcesses()
        {
            Process[] processlist = Process.GetProcesses();

            return processlist.Where(process => !string.IsNullOrEmpty(process.MainWindowTitle)).ToArray();
        }
    }
}
