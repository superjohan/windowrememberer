using System;
using System.Windows.Forms;

namespace WindowRememberer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (SystemTrayIcon pi = new SystemTrayIcon())
            {
                pi.Display();

                Application.Run();
            }
        }
    }
}
