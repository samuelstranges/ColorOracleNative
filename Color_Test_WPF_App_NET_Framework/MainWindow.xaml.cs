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
        private void setDeu(object sender, RoutedEventArgs e) { deuSetter(); }
        private void setPro(object sender, RoutedEventArgs e) { proSetter(); }
        private void setTri(object sender, RoutedEventArgs e) { triSetter(); }
        private void setGr(object sender, RoutedEventArgs e) { grSetter(); }

        private void refreshLive(int key)
        {
            mode.color_filter_key = key;
            mode.live();
        }

        private void normalSetter() 
        {
            if (mode.status) realTime();
            color_filter_key = 0;
            chosenType.Content = "Chosen type: None";
        }
        private void deuSetter()
        {
            color_filter_key = 1;
            chosenType.Content = "Chosen type: Deuteranopia";
            if (mode.status) refreshLive(1);
        }
        private void proSetter()
        {
            color_filter_key = 2;
            chosenType.Content = "Chosen type: Protanopia";
            if (mode.status) refreshLive(2);      
        }
        private void triSetter() 
        {
            color_filter_key = 3;
            chosenType.Content = "Chosen type: Tritanopia";
            if (mode.status) refreshLive(3);           
        }
        private void grSetter()
        {
            color_filter_key = 4;
            chosenType.Content = "Chosen type: Grayscale";
            if (mode.status) refreshLive(4);          
        }
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
            if (color_filter_key > 0 && color_filter_key < 5)
            {
                mode.color_filter_key = color_filter_key;
                mode.status = !mode.status;
                mode.live();
            }

            if (!mode.status)
            {
                color_filter_key = 0;
                mode.color_filter_key = color_filter_key;
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
