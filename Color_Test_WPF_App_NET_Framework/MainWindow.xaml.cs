using System;
using System.Windows;
using System.Windows.Input;
using NHotkey.Wpf;


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

        private void openSendFeedback(object sender, RoutedEventArgs e){System.Diagnostics.Process.Start("https://docs.google.com/forms/d/1wsFwdaKLq53ZWrAkwraCXpOVz41yqyYolzkifKklSkc/edit");}

        private void openAbout(object sender, RoutedEventArgs e){About aboutPage = new About(); aboutPage.Show();}
        private void openHelp(object sender, RoutedEventArgs e){System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ");}
    }
}
