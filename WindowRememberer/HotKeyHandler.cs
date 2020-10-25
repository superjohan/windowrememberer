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
            Left = Keys.Left,
            Right = Keys.Right,
            Up = Keys.Up,
            Down = Keys.Down,
            TopLeft = Keys.U,
            TopRight = Keys.I,
            BottomLeft = Keys.J,
            BottomRight = Keys.K,
            LeftThird = Keys.D,
            LeftTwoThirds = Keys.E,
            CenterThird = Keys.F,
            RightTwoThirds = Keys.T,
            RightThird = Keys.G,
            Maximize = Keys.Return,
            Center = Keys.C,
        }

        public HotKeyHandler(IntPtr handle)
        {
            int baseModifiers = 1 | 4 | 8; // alt - shift - win

            // TODO: maybe these need to be unregistered on quit?
            RegisterHotKey(handle, (int)EventId.Left, baseModifiers, (int)EventId.Left);
            RegisterHotKey(handle, (int)EventId.Right, baseModifiers, (int)EventId.Right);
            RegisterHotKey(handle, (int)EventId.Up, baseModifiers, (int)EventId.Up);
            RegisterHotKey(handle, (int)EventId.Down, baseModifiers, (int)EventId.Down);
            RegisterHotKey(handle, (int)EventId.TopLeft, baseModifiers, (int)EventId.TopLeft);
            RegisterHotKey(handle, (int)EventId.TopRight, baseModifiers, (int)EventId.TopRight);
            RegisterHotKey(handle, (int)EventId.BottomLeft, baseModifiers, (int)EventId.BottomLeft);
            RegisterHotKey(handle, (int)EventId.BottomRight, baseModifiers, (int)EventId.BottomRight);
            RegisterHotKey(handle, (int)EventId.LeftThird, baseModifiers, (int)EventId.LeftThird);
            RegisterHotKey(handle, (int)EventId.LeftTwoThirds, baseModifiers, (int)EventId.LeftTwoThirds);
            RegisterHotKey(handle, (int)EventId.CenterThird, baseModifiers, (int)EventId.CenterThird);
            RegisterHotKey(handle, (int)EventId.RightTwoThirds, baseModifiers, (int)EventId.RightTwoThirds);
            RegisterHotKey(handle, (int)EventId.RightThird, baseModifiers, (int)EventId.RightThird);
            RegisterHotKey(handle, (int)EventId.Maximize, baseModifiers, (int)EventId.Maximize);
            RegisterHotKey(handle, (int)EventId.Center, baseModifiers, (int)EventId.Center);
        }

        public void OnHotKeyPressed(IntPtr param)
        {            
            uint p = (uint)param.ToInt64();
            Keys key = (Keys)((p & 0xffff0000) >> 16);

            HandleEvent(key);
        }

        private void HandleEvent(Keys key)
        {
            var workingArea = WindowUtils.CurrentWindowScreen().WorkingArea;
            var newPosition = new Rect();
            EventId eventId = (EventId)key;

            switch (eventId)
            {
                case EventId.Left:
                    newPosition.width = workingArea.Width / 2;
                    newPosition.height = workingArea.Height;
                    WindowUtils.SetCurrentWindowRect(newPosition);
                    break;
                case EventId.Up:
                    newPosition.width = workingArea.Width;
                    newPosition.height = workingArea.Height / 2;
                    WindowUtils.SetCurrentWindowRect(newPosition);
                    break;
                case EventId.Right:
                    newPosition.left = workingArea.Width / 2;
                    newPosition.width = workingArea.Width / 2;
                    newPosition.height = workingArea.Height;
                    WindowUtils.SetCurrentWindowRect(newPosition);
                    break;
                case EventId.Down:
                    newPosition.top = workingArea.Height / 2;
                    newPosition.width = workingArea.Width;
                    newPosition.height = workingArea.Height / 2;
                    WindowUtils.SetCurrentWindowRect(newPosition);
                    break;
                case EventId.TopLeft:
                    newPosition.width = workingArea.Width / 2;
                    newPosition.height = workingArea.Height / 2;
                    WindowUtils.SetCurrentWindowRect(newPosition);
                    break;
                case EventId.TopRight:
                    newPosition.left = workingArea.Width / 2;
                    newPosition.width = workingArea.Width / 2;
                    newPosition.height = workingArea.Height / 2;
                    WindowUtils.SetCurrentWindowRect(newPosition);
                    break;
                case EventId.BottomLeft:
                    newPosition.top = workingArea.Height / 2;
                    newPosition.width = workingArea.Width / 2;
                    newPosition.height = workingArea.Height / 2;
                    WindowUtils.SetCurrentWindowRect(newPosition);
                    break;
                case EventId.BottomRight:
                    newPosition.left = workingArea.Width / 2;
                    newPosition.top = workingArea.Height / 2;
                    newPosition.width = workingArea.Width / 2;
                    newPosition.height = workingArea.Height / 2;
                    WindowUtils.SetCurrentWindowRect(newPosition);
                    break;
                case EventId.LeftThird:
                    newPosition.width = workingArea.Width / 3;
                    newPosition.height = workingArea.Height;
                    WindowUtils.SetCurrentWindowRect(newPosition);
                    break;
                case EventId.LeftTwoThirds:
                    newPosition.width = (workingArea.Width / 3) * 2;
                    newPosition.height = workingArea.Height;
                    WindowUtils.SetCurrentWindowRect(newPosition);
                    break;
                case EventId.CenterThird:
                    newPosition.left = workingArea.Width / 3;
                    newPosition.width = workingArea.Width / 3;
                    newPosition.height = workingArea.Height;
                    WindowUtils.SetCurrentWindowRect(newPosition);
                    break;
                case EventId.RightTwoThirds:
                    newPosition.left = workingArea.Width / 3;
                    newPosition.width = (workingArea.Width / 3) * 2;
                    newPosition.height = workingArea.Height;
                    WindowUtils.SetCurrentWindowRect(newPosition);
                    break;
                case EventId.RightThird:
                    newPosition.left = (workingArea.Width / 3) * 2;
                    newPosition.width = workingArea.Width / 3;
                    newPosition.height = workingArea.Height;
                    WindowUtils.SetCurrentWindowRect(newPosition);
                    break;
                case EventId.Maximize:
                    // FIXME: a real windows ass maximize could be better
                    newPosition.width = workingArea.Width;
                    newPosition.height = workingArea.Height;
                    WindowUtils.SetCurrentWindowRect(newPosition);
                    break;
                case EventId.Center:
                    var currentWindowRect = WindowUtils.GetCurrentWindowRect();

                    if (currentWindowRect.isValid)
                    {
                        newPosition.left = (workingArea.Width / 2) - (currentWindowRect.width / 2);
                        newPosition.top = (workingArea.Height / 2) - (currentWindowRect.height / 2);
                        newPosition.width = currentWindowRect.width;
                        newPosition.height = currentWindowRect.height;
                        WindowUtils.SetCurrentWindowRect(newPosition);
                    }

                    break;
                default:
                    Debug.WriteLine("Unexpected key: " + key);
                    break;
            }
        }
    }
}
