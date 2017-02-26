using System;
using System.Windows.Forms;
using WindowRememberer.Properties;

namespace WindowRememberer
{
    class SystemTrayIcon : IDisposable
    {
        private readonly NotifyIcon notifyIcon;

        public SystemTrayIcon()
        {
            notifyIcon = new NotifyIcon();
        }

        public void Display()
        {
            // TODO: Mouse click event for opening the menu? (as in, in addition to right-clicking)

            notifyIcon.Icon = Resources.SystemTrayApp;
            notifyIcon.Text = "WindowRememberer"; // FIXME: Localizable string, if such a thing exists in WPF.
            notifyIcon.Visible = true;
            notifyIcon.ContextMenuStrip = MainMenuFactory.Create();
        }

        public void Dispose()
        {
            notifyIcon.Dispose();
        }
    }
}
