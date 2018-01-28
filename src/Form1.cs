using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyHackerNews
{
    public partial class Form1 : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;

        public Form1()
        {
            // Create a simple tray menu with only one item.
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Exit", OnExit);

            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            trayIcon = new NotifyIcon();
            trayIcon.Text = "MyTrayApp";
            trayIcon.Icon = new Icon(SystemIcons.Application, 40, 40);

            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;

            trayIcon.MouseClick += TrayIcon_MouseClick;

            InitializeComponent();
        }

        private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.TopMost = true;
            this.Focus();// BUG: This is only working for the very first time. After that not in focus
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.Hide();
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.

            PlaceLowerRight();
            base.OnLoad(e);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="isDisposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }

                // Release the icon resource.
                trayIcon.Dispose();
            }

            base.Dispose(isDisposing);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PlaceLowerRight()
        {
            //Determine "rightmost" screen
            //Screen rightmost = Screen.AllScreens[0];
            Screen rightmost = Screen.PrimaryScreen;
            //foreach (Screen screen in Screen.AllScreens)
            //{
            //    if (screen.WorkingArea.Right > rightmost.WorkingArea.Right)
            //        rightmost = screen;
            //}

            this.Left = rightmost.WorkingArea.Right - this.Width;
            this.Top = rightmost.WorkingArea.Bottom - this.Height;
        }
    }
}
