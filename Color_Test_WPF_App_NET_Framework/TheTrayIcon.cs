using System;
using NHotkey.Wpf;
using System.Windows.Input;
using System.Windows.Forms;
using Icon = System.Drawing.Icon;
using ContextMenu = System.Windows.Forms.ContextMenu;

namespace Color_Test_WPF_App_NET_Framework
{
    /// <summary>
    /// Logic for the trayicon
    /// </summary>
    class TheTrayIcon{

        /// <summary>
        /// The Trayicon contains a single instance of a window class, which has public methods that interact with the trayicon.
        /// Therefore, the trayicon holds the window, rather than the window holding the trayicon
        /// The purpose of this is that you are able to close the main window, while still being able to interact with the application.
        /// </summary>
        private MainWindow theWindow = new MainWindow();

        /// <summary>
        /// The constructor for the trayicon
        /// Here, we set up the mainwindow, the trayicon and functions, and global keyboard shortcuts
        /// </summary>
        public TheTrayIcon() {

            theWindow.Show();

            NotifyIcon nIcon = new NotifyIcon();
            //the shortcut menu for notifyIcon
            ContextMenu contextMenu1 = new ContextMenu();

            //Global Shortcuts
            HotkeyManager.Current.AddOrReplace("toggleRealTimeGS", Key.L, ModifierKeys.Control | ModifierKeys.Shift, theWindow.toggleRealTimeGS);
            HotkeyManager.Current.AddOrReplace("screenshotGS", Key.M, ModifierKeys.Control | ModifierKeys.Shift, theWindow.screenshotGS);
            HotkeyManager.Current.AddOrReplace("switchTypesGS", Key.N, ModifierKeys.Control | ModifierKeys.Shift, theWindow.switchTypesGS);


            //Most of these functions use the MainWindow's public functions
            MenuItem mainWindow = new MenuItem("Open Color Oracle Window", (s, d) => openMainWindow(s, d));
            MenuItem toggleRealTime = new MenuItem("Live Mode", (s, d) => theWindow.toggleRealTimeGS(s, d)); toggleRealTime.Shortcut = Shortcut.CtrlShiftL;
            MenuItem screenshot = new MenuItem("Screenshot", (s, d) => theWindow.screenshotGS(s, d));        screenshot.Shortcut = Shortcut.CtrlShiftM;
            MenuItem toggleMethod = new MenuItem("Cycle Blindness Type", (s, d) => theWindow.switchTypesGS(s, d));  toggleMethod.Shortcut = Shortcut.CtrlShiftN;
            MenuItem exit = new MenuItem("Exit Color Oracle", (s, d) => this.close(s, d));

            contextMenu1.MenuItems.Add(mainWindow);
            contextMenu1.MenuItems.Add(exit);
            contextMenu1.MenuItems.Add(toggleRealTime);
            contextMenu1.MenuItems.Add(screenshot);
            contextMenu1.MenuItems.Add(toggleMethod);
            


            nIcon.ContextMenu = contextMenu1;
            nIcon.Icon = Properties.Resources.menuIcon;
            nIcon.Visible = true;
            nIcon.Text = "Color Oracle";
        }


        /// <summary>
        /// Close the trayicon (by shutting down the application)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close(object sender, EventArgs e){
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        /// As the MainWindow can hide itself via the close button, we allow the trayicon to show the main window again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openMainWindow(object sender, EventArgs e) { this.theWindow.Show(); }

    }
}
