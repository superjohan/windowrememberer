using System.Diagnostics;
using System.Windows.Forms;

namespace WindowRememberer
{
    class MainMenuFactory
    {
        public static ContextMenuStrip Create()
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

            // TODO: Localizable strings for all UI strings.
            // TODO: Separate class for save/restore handling.

            ToolStripMenuItem saveItem = new ToolStripMenuItem();
            saveItem.Text = "Save window sizes and positions";
            saveItem.Click += SaveItem_Click;
            contextMenuStrip.Items.Add(saveItem);

            ToolStripMenuItem restoreItem = new ToolStripMenuItem();
            restoreItem.Text = "Restore window sizes and positions";
            restoreItem.Click += RestoreItem_Click;
            contextMenuStrip.Items.Add(restoreItem);

            ToolStripSeparator separator = new ToolStripSeparator();
            contextMenuStrip.Items.Add(separator);

            ToolStripMenuItem quitItem = new ToolStripMenuItem();
            quitItem.Text = "Quit";
            quitItem.Click += QuitItem_Click;
            contextMenuStrip.Items.Add(quitItem);

            return contextMenuStrip;
        }

        private static void SaveItem_Click(object sender, System.EventArgs e)
        {
            Debug.WriteLine("TODO: Save");
        }

        private static void RestoreItem_Click(object sender, System.EventArgs e)
        {
            Debug.WriteLine("TODO: Restore");
        }

        private static void QuitItem_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }
    }
}
