using System.Diagnostics;
using System.Windows.Forms;

namespace WindowRememberer
{
    class MainMenuFactory
    {
        public static ContextMenuStrip Create(WindowPropertyManager windowPropertyManager)
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

            // TODO: Localizable strings for all UI strings.

            ToolStripMenuItem saveItem = new ToolStripMenuItem();
            saveItem.Text = "Save window sizes and positions";
            saveItem.Click += windowPropertyManager.SaveWindowProperties;
            contextMenuStrip.Items.Add(saveItem);

            ToolStripMenuItem restoreItem = new ToolStripMenuItem();
            restoreItem.Text = "Restore window sizes and positions";
            restoreItem.Click += windowPropertyManager.RestoreWindowProperties;
            contextMenuStrip.Items.Add(restoreItem);

            ToolStripSeparator separator = new ToolStripSeparator();
            contextMenuStrip.Items.Add(separator);

            ToolStripMenuItem quitItem = new ToolStripMenuItem();
            quitItem.Text = "Quit";
            quitItem.Click += QuitItem_Click;
            contextMenuStrip.Items.Add(quitItem);

            return contextMenuStrip;
        }

        private static void QuitItem_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }
    }
}
