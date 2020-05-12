using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Timers;
using Timer = System.Timers.Timer;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
using Button = System.Windows.Forms.Button;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using Icon = System.Drawing.Icon;
using Rectangle = System.Drawing.Rectangle;
using NHotkey.Wpf;
using System.Runtime.InteropServices;
using ContextMenu = System.Windows.Forms.ContextMenu;

namespace Color_Test_WPF_App_NET_Framework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    ///
    /// </summary>
    ///

    public partial class MainWindow : Window {

        //initialize the realtime filter and default to normal
        public static int color_filter_key = 0;
        Program1 mode = new Program1(color_filter_key);
        //if the default is normal, the filter is off for color blindness
        public Boolean mode_status = false;
        //a notifyIcon for WPF
        NotifyIcon nIcon = new NotifyIcon();
        //the shortcut menu for notifyIcon
        public ContextMenu contextMenu1 = new ContextMenu();

        public MainWindow(){
            InitializeComponent();

            //Keyboard Shortcuts (Main Window Shortcuts)
            RoutedCommand keyPro = new RoutedCommand();     keyPro.InputGestures.Add(new KeyGesture(Key.W, ModifierKeys.Control));      CommandBindings.Add(new CommandBinding(keyPro, setPro));
            RoutedCommand keyDu = new RoutedCommand();      keyDu.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));       CommandBindings.Add(new CommandBinding(keyDu, setDeu));
            RoutedCommand keyTri = new RoutedCommand();     keyTri.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));      CommandBindings.Add(new CommandBinding(keyTri, setTri));
            RoutedCommand keyGr = new RoutedCommand();      keyGr.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control));       CommandBindings.Add(new CommandBinding(keyGr, setGr));
            RoutedCommand keyAbout = new RoutedCommand();   keyAbout.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));    CommandBindings.Add(new CommandBinding(keyAbout, openAbout));
            RoutedCommand keyFeedback = new RoutedCommand();keyFeedback.InputGestures.Add(new KeyGesture(Key.G, ModifierKeys.Control)); CommandBindings.Add(new CommandBinding(keyFeedback, openSendFeedback));
            RoutedCommand keyHelp = new RoutedCommand();    keyHelp.InputGestures.Add(new KeyGesture(Key.H, ModifierKeys.Control));     CommandBindings.Add(new CommandBinding(keyHelp, openHelp));

            //Global Shortcuts
            HotkeyManager.Current.AddOrReplace("toggleRealTimeGS", Key.L, ModifierKeys.Control | ModifierKeys.Alt, toggleRealTimeGS);
            HotkeyManager.Current.AddOrReplace("screenshotGS", Key.M, ModifierKeys.Control | ModifierKeys.Alt, screenshotGS);
            HotkeyManager.Current.AddOrReplace("switchTypesGS", Key.N, ModifierKeys.Control | ModifierKeys.Alt, switchTypesGS);
        }


        private void toggleRealTimeGS(object sender, EventArgs t) {realTime();}
        private void screenshotGS(object sender, EventArgs t) { runScreenshot(); }
        private void switchTypesGS(object sender, EventArgs t) {
            switch (mode.color_filter_key){
                case 0:    deuSetter(); break;
                case 1:    proSetter(); break;
                case 2:    triSetter(); break;
                case 3:     grSetter(); break;
                case 4: normalSetter(); break;
            }
        }

        //Switches Real Time Filter Mode
        void changeMode(){if (mode_status){mode.live();}}
        private void setDeu(object sender, RoutedEventArgs e){ deuSetter(); }
        private void setPro(object sender, RoutedEventArgs e){ proSetter(); }
        private void setTri(object sender, RoutedEventArgs e){ triSetter(); }
        private void setGr(object sender, RoutedEventArgs e){ grSetter(); }


        private void normalSetter() {mode.color_filter_key = 0; changeMode(); chosenType.Content = "Chosen type: None";         }
        private void deuSetter()    {mode.color_filter_key = 1; changeMode(); chosenType.Content = "Chosen type: Deuteranopia"; }
        private void proSetter()    {mode.color_filter_key = 2; changeMode(); chosenType.Content = "Chosen type: Protanopia";   }
        private void triSetter()    {mode.color_filter_key = 3; changeMode(); chosenType.Content = "Chosen type: Tritanopia";   }
        private void grSetter()     {mode.color_filter_key = 4; changeMode(); chosenType.Content = "Chosen type: Grayscale";    }
        private void Screenshot(object sender, RoutedEventArgs e){runScreenshot();}

        private void runScreenshot(){
            SelectArea obj = new SelectArea();
            //if u wanna hide the main window, uncomment the bottom line
            //this.Visibility = Visibility.Hidden;
            //objScreenshot.Opacity = 0.9;
            //obj.Opacity = 0.99;
            obj.Show();
        }



        private void realTime(){
            if (!mode_status){ //Turn on if not already on
                mode_status = true;
                changeMode();
            }
            else{ //Reset the graph.
                mode.color_filter_key = 0;
                mode.live();
                mode_status = false;
                chosenType.Content = "Chosen type: None";
            }
        }

        public void clickWindow(object sender, RoutedEventArgs e){realTime();}

        private void openSendFeedback(object sender, RoutedEventArgs e) { feedbackOpener(); }
        private void openAbout(object sender, RoutedEventArgs e) { aboutOpener(); }
        private void openHelp(object sender, RoutedEventArgs e){ helpOpener(); }



        private void eventArgHelpOpen(object sender, EventArgs e) { helpOpener(); }
        private void eventArgAboutOpen(object sender, EventArgs e) { aboutOpener(); }
        private void eventArgFeedback(object sender, EventArgs e) { feedbackOpener(); }
        private void eventArgSetDeu(object sender, EventArgs e) { deuSetter(); }
        private void eventArgSetPro(object sender, EventArgs e) { proSetter(); }
        private void eventArgSetTri(object sender, EventArgs e) { triSetter(); }
        private void eventArgSetGr(object sender, EventArgs e) { grSetter(); }



        private void feedbackOpener() { System.Diagnostics.Process.Start("https://docs.google.com/forms/d/1wsFwdaKLq53ZWrAkwraCXpOVz41yqyYolzkifKklSkc/edit"); }

        private void helpOpener() { System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ"); }

        private void aboutOpener() { About aboutPage = new About(); aboutPage.Show(); }

        private void Button_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
            this.nIcon.Icon = new Icon("../../images/trayicon.ico");
            contextMenu1.MenuItems.Add("Help", (s, d) => this.eventArgHelpOpen(s, d));
            contextMenu1.MenuItems.Add("About", (s, d) => this.eventArgAboutOpen(s, d));
            contextMenu1.MenuItems.Add("Live Mode", (s, d) => this.toggleRealTimeGS(s, d));
            contextMenu1.MenuItems.Add("openSendFeedback", (s, d) => this.eventArgFeedback(s, d));
            contextMenu1.MenuItems.Add("Deuteranopia", (s, d) => this.eventArgSetDeu(s, d));
            contextMenu1.MenuItems.Add("Protanopia", (s, d) => this.eventArgSetPro(s, d));
            contextMenu1.MenuItems.Add("Tritanopia", (s, d) => this.eventArgSetTri(s, d));
            contextMenu1.MenuItems.Add("Grayscale", (s, d) => this.eventArgSetGr(s, d));
            contextMenu1.MenuItems.Add("ScreenMode", (s, d) => this.screenshotGS(s, d));


            nIcon.ContextMenu = contextMenu1;

            /*
            this.nIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.nIcon.ContextMenuStrip.Items.Add("Deuteranopia(Common)");
            this.nIcon.ContextMenuStrip.Items.Add("Protanopia(Rare)");
            this.nIcon.ContextMenuStrip.Items.Add("Tritanopia(Very Rare)");
            this.nIcon.ContextMenuStrip.Items.Add("Grayscale");
            this.nIcon.ContextMenuStrip.Items.Add("Screenshot Mode");
            this.nIcon.ContextMenuStrip.Items.Add("Live Mode");
            this.nIcon.ContextMenuStrip.Items.Add("About");
            this.nIcon.ContextMenu.MenuItems.AddRange("Help", null, (s, e) => this.openAbout(s, e));
            */
            this.nIcon.Visible = true;
            this.nIcon.ShowBalloonTip(5000, "Hi", "This is a BallonTip from Windows Notification", ToolTipIcon.Info);
            this.nIcon.Text = "Right Click";


        }


        void window_maximize() {
            this.WindowState = WindowState.Maximized;


        }
    }
}
