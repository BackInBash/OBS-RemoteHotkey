using OBS_RemoteHotkey.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OBS_RemoteHotkey
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
            OBS obs = new OBS();
            Application.Run(new TrayApplicationContext());
        }
    }

    public class TrayApplicationContext : ApplicationContext
    {
        private NotifyIcon trayIcon;

        public TrayApplicationContext()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = Icon.FromHandle(Resources.OBS.GetHicon()),
                ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("Options", ShowOptions),
                    new MenuItem("Exit", Exit)
                }),
                Visible = true,
                Text = "OBS Remote Hotkey"
            };
        }

        void ShowOptions(object sender, EventArgs e)
        {
            Options o = new Options();
            o.Show();
        }

        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
