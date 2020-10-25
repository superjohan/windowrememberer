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

            HandleEvent(key);
        }

        private void HandleEvent(Keys key)
        {
            var screen = WindowUtils.CurrentWindowScreen();
            var newPosition = new Rect();

            switch (key)
            {
                case Keys.Left:
                    newPosition.left = 0;
                    newPosition.top = 0;
                    newPosition.width = screen.WorkingArea.Width / 2;
                    newPosition.height = screen.WorkingArea.Height;
                    WindowUtils.SetCurrentWindowRect(newPosition);
                    break;
                case Keys.Up:
                    newPosition.left = 0;
                    newPosition.top = 0;
                    newPosition.width = screen.WorkingArea.Width;
                    newPosition.height = screen.WorkingArea.Height / 2;
                    WindowUtils.SetCurrentWindowRect(newPosition);
                    break;
                case Keys.Right:
                    newPosition.left = screen.WorkingArea.Width / 2;
                    newPosition.top = 0;
                    newPosition.width = screen.WorkingArea.Width / 2;
                    newPosition.height = screen.WorkingArea.Height;
                    WindowUtils.SetCurrentWindowRect(newPosition);
                    break;
                case Keys.Down:
                    newPosition.left = 0;
                    newPosition.top = screen.WorkingArea.Height / 2;
                    newPosition.width = screen.WorkingArea.Width;
                    newPosition.height = screen.WorkingArea.Height / 2;
                    WindowUtils.SetCurrentWindowRect(newPosition);
                    break;
                default:
                    Debug.WriteLine("Unexpected key: " + key);
                    break;
            }
        }
    }
}
