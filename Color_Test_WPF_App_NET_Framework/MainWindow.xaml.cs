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
using System.Runtime.InteropServices;
using ContextMenu = System.Windows.Forms.ContextMenu;

namespace Color_Test_WPF_App_NET_Framework
{
    ///

    public partial class MainWindow : Window
    {

        //initialize the realtime filter and default to normal
        public static int color_filter_key = 0;
        public Program1 mode = new Program1(color_filter_key);
        //if the default is normal, the filter is off for color blindness
        public Boolean mode_status = false;



        public MainWindow()
        {
            InitializeComponent();

            //Keyboard Shortcuts (Main Window Shortcuts)
            RoutedCommand keyPro = new RoutedCommand(); keyPro.InputGestures.Add(new KeyGesture(Key.W, ModifierKeys.Control)); CommandBindings.Add(new CommandBinding(keyPro, setPro));
            RoutedCommand keyDu = new RoutedCommand(); keyDu.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control)); CommandBindings.Add(new CommandBinding(keyDu, setDeu));
            RoutedCommand keyTri = new RoutedCommand(); keyTri.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control)); CommandBindings.Add(new CommandBinding(keyTri, setTri));
            RoutedCommand keyGr = new RoutedCommand(); keyGr.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control)); CommandBindings.Add(new CommandBinding(keyGr, setGr));
            RoutedCommand keyAbout = new RoutedCommand(); keyAbout.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control)); CommandBindings.Add(new CommandBinding(keyAbout, openAbout));
            RoutedCommand keyFeedback = new RoutedCommand(); keyFeedback.InputGestures.Add(new KeyGesture(Key.G, ModifierKeys.Control)); CommandBindings.Add(new CommandBinding(keyFeedback, openSendFeedback));
            RoutedCommand keyHelp = new RoutedCommand(); keyHelp.InputGestures.Add(new KeyGesture(Key.H, ModifierKeys.Control)); CommandBindings.Add(new CommandBinding(keyHelp, openHelp));

        }

        private void dontClose(object sender, System.ComponentModel.CancelEventArgs e) { e.Cancel = true; Hide(); } //Minimise Instead

        //Public, so that the trayicon can also get these methods.
        public void toggleRealTimeGS(object sender, EventArgs t) { realTime(); }
        public void screenshotGS(object sender, EventArgs t) { runScreenshot(); }
        public void switchTypesGS(object sender, EventArgs t){
            switch (mode.color_filter_key){
                case 0: deuSetter();   break;
                case 1: proSetter();   break;
                case 2: triSetter();   break;
                case 3: grSetter();    break;
                case 4: normalSetter();break;
            }
        }

        //Switches Real Time Filter Mode
        void changeMode() { if (mode_status) { mode.live(); } }
        private void setDeu(object sender, RoutedEventArgs e) { deuSetter(); }
        private void setPro(object sender, RoutedEventArgs e) { proSetter(); }
        private void setTri(object sender, RoutedEventArgs e) { triSetter(); }
        private void setGr(object sender, RoutedEventArgs e) { grSetter(); }


        private void normalSetter() { mode.color_filter_key = 0; changeMode(); chosenType.Content = "Chosen type: None"; }
        private void deuSetter() { mode.color_filter_key = 1; changeMode(); chosenType.Content = "Chosen type: Deuteranopia"; }
        private void proSetter() { mode.color_filter_key = 2; changeMode(); chosenType.Content = "Chosen type: Protanopia"; }
        private void triSetter() { mode.color_filter_key = 3; changeMode(); chosenType.Content = "Chosen type: Tritanopia"; }
        private void grSetter() { mode.color_filter_key = 4; changeMode(); chosenType.Content = "Chosen type: Grayscale"; }
        private void Screenshot(object sender, RoutedEventArgs e) { runScreenshot(); }

        private void runScreenshot()
        {
            SelectArea obj = new SelectArea();
            //if u wanna hide the main window, uncomment the bottom line
            //this.Visibility = Visibility.Hidden;
            //objScreenshot.Opacity = 0.9;
            //obj.Opacity = 0.99;
            obj.Show();
        }



        private void realTime()
        {
            if (!mode_status)
            { //Turn on if not already on
                mode_status = true;
                changeMode();
            }
            else
            { //Reset the graph.
                mode.color_filter_key = 0;
                mode.live();
                mode_status = false;
                chosenType.Content = "Chosen type: None";
            }
        }

        public void clickWindow(object sender, RoutedEventArgs e) { realTime(); }

        private void openSendFeedback(object sender, RoutedEventArgs e) { feedbackOpener(); }
        private void openAbout(object sender, RoutedEventArgs e) { aboutOpener(); }
        private void openHelp(object sender, RoutedEventArgs e) { helpOpener(); }



        private void eventArgHelpOpen(object sender, EventArgs e) { helpOpener(); }
        private void eventArgAboutOpen(object sender, EventArgs e) { aboutOpener(); }
        private void eventArgFeedback(object sender, EventArgs e) { feedbackOpener(); }
        private void eventArgSetDeu(object sender, EventArgs e) { deuSetter(); }
        private void eventArgSetPro(object sender, EventArgs e) { proSetter(); }
        private void eventArgSetTri(object sender, EventArgs e) { triSetter(); }
        private void eventArgSetGr(object sender, EventArgs e) { grSetter(); }

        private void eventArgExit(object sender, EventArgs e) { System.Windows.Application.Current.Shutdown(); }


        private void feedbackOpener() { System.Diagnostics.Process.Start("https://docs.google.com/forms/d/1wsFwdaKLq53ZWrAkwraCXpOVz41yqyYolzkifKklSkc/edit"); }

        private void helpOpener() { System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ"); }

        private void aboutOpener() { About aboutPage = new About(); aboutPage.Show(); }

        }
    }
}
