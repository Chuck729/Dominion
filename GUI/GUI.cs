using System;
using System.Windows.Forms;

namespace GUI
{
    internal static class Gui
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(new [] { "bob", "larry" }, new Random().Next()));
        }
    }
}
