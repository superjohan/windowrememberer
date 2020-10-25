using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace WindowRememberer
{
    class WindowPropertyManager
    {
        private readonly Dictionary<string, Rect> windowProperties = new Dictionary<string, Rect>();
        
        public void SaveWindowProperties(object sender, EventArgs e)
        {
            Process[] windowedProcesses = WindowUtils.WindowedProcesses();

            foreach (Process process in windowedProcesses)
            {
                Rect rect = new Rect();
                if (WindowUtils.GetWindowRect(process.MainWindowHandle, ref rect))
                {
                    windowProperties[process.ProcessName] = rect;
                }
            }

            // TODO: Save to disk.
        }

        public void RestoreWindowProperties(object sender, EventArgs e)
        {
            Process[] windowedProcesses = WindowUtils.WindowedProcesses();

            foreach (Process process in windowedProcesses)
            {
                string processName = process.ProcessName;

                if (! windowProperties.ContainsKey(processName))
                {
                    continue;
                }

                Rect rect = windowProperties[processName];

                if (! WindowUtils.MoveWindow(process.MainWindowHandle, rect.left, rect.top, rect.width, rect.height, true))
                {
                    Debug.WriteLine("Could not restore window for process: " + processName);
                }
            }
        }
    }
}
