using System;
using System.Windows;
using System.Windows.Input;

namespace Color_Test_WPF_App_NET_Framework
{
    /// <summary>
    /// The logic for the MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// Initialise the filter to none (0)
        /// </summary>
        public static int color_filter_key = 0;

        /// <summary>
        /// Create an instance of the Program Class that runs the real time filter
        /// </summary>
        public Program1 mode = new Program1(color_filter_key);


        //Keyboard Shortcuts
        int currentKeyboardShortcuts = 0;
        RoutedCommand keyPro = new RoutedCommand();
        RoutedCommand keyDu = new RoutedCommand();
        RoutedCommand keyTri = new RoutedCommand();
        RoutedCommand keyGr = new RoutedCommand();
        RoutedCommand keyAbout = new RoutedCommand();
        RoutedCommand keyFeedback = new RoutedCommand();
        RoutedCommand keyHelp = new RoutedCommand();

        KeyGesture gestDu = new KeyGesture(Key.Q, ModifierKeys.Control);
        KeyGesture gestPro = new KeyGesture(Key.W, ModifierKeys.Control);
        KeyGesture gestTri = new KeyGesture(Key.E, ModifierKeys.Control);
        KeyGesture gestGr = new KeyGesture(Key.R, ModifierKeys.Control);
        KeyGesture gestAbout = new KeyGesture(Key.A, ModifierKeys.Control);
        KeyGesture gestFeed = new KeyGesture(Key.G, ModifierKeys.Control);
        KeyGesture gestHelp = new KeyGesture(Key.H, ModifierKeys.Control);

        KeyGesture altGestDu = new KeyGesture(Key.V, ModifierKeys.Control);
        KeyGesture altGestPro = new KeyGesture(Key.B, ModifierKeys.Control);
        KeyGesture altGestTri = new KeyGesture(Key.N, ModifierKeys.Control);
        KeyGesture altGestGr = new KeyGesture(Key.M, ModifierKeys.Control);
        KeyGesture altGestAbout = new KeyGesture(Key.I, ModifierKeys.Control);
        KeyGesture altGestFeed = new KeyGesture(Key.O, ModifierKeys.Control);
        KeyGesture altGestHelp = new KeyGesture(Key.P, ModifierKeys.Control);

        /// <summary>
        /// Initialise the window
        /// In addition, setup the local keyboard shortcuts via input gestures and command bindings
        /// </summary>
        public MainWindow() {
            InitializeComponent();

            keyDu.InputGestures.Add(gestDu);
            keyPro.InputGestures.Add(gestPro);
            keyTri.InputGestures.Add(gestTri);
            keyGr.InputGestures.Add(gestGr);
            keyAbout.InputGestures.Add(gestAbout);
            keyFeedback.InputGestures.Add(gestFeed);
            keyHelp.InputGestures.Add(gestHelp);

            //Initialise Keyboard Shortcuts (Local Main Window Shortcuts)
            CommandBindings.Add(new CommandBinding(keyDu, setDeu));
            CommandBindings.Add(new CommandBinding(keyPro, setPro));
            CommandBindings.Add(new CommandBinding(keyTri, setTri));
            CommandBindings.Add(new CommandBinding(keyGr, setGr));
            CommandBindings.Add(new CommandBinding(keyAbout, openAbout));
            CommandBindings.Add(new CommandBinding(keyFeedback, openSendFeedback));
            CommandBindings.Add(new CommandBinding(keyHelp, openHelp));


        }

        private void setAltKeyboardShortcuts(){
            //Remove old ones
            keyDu.InputGestures.Remove(gestDu);
            keyPro.InputGestures.Remove(gestPro);
            keyTri.InputGestures.Remove(gestTri);
            keyGr.InputGestures.Remove(gestGr);
            keyAbout.InputGestures.Remove(gestAbout);
            keyFeedback.InputGestures.Remove(gestFeed);
            keyHelp.InputGestures.Remove(gestHelp);

            //Setup alternate keyboard shortcuts (customisation for user)

            keyDu.InputGestures.Add(altGestDu);
            keyPro.InputGestures.Add(altGestPro);
            keyTri.InputGestures.Add(altGestTri);
            keyGr.InputGestures.Add(altGestGr);
            keyAbout.InputGestures.Add(altGestAbout);
            keyFeedback.InputGestures.Add(altGestFeed);
            keyHelp.InputGestures.Add(altGestHelp);

            settingsDeu.InputGestureText = "Ctrl+V";
            settingsPro.InputGestureText = "Ctrl+B";
            settingsTri.InputGestureText = "Ctrl+N";
            settingsGr.InputGestureText = "Ctrl+M";
            settingsAbout.InputGestureText = "Ctrl+I";
            settingsFeed.InputGestureText = "Ctrl+O";
            settingsHelp.InputGestureText = "Ctrl+P";
        }

        private void setOriginalKeyboardShortcuts(){

            keyDu.InputGestures.Remove(altGestDu);
            keyPro.InputGestures.Remove(altGestPro);
            keyTri.InputGestures.Remove(altGestTri);
            keyGr.InputGestures.Remove(altGestGr);
            keyAbout.InputGestures.Remove(altGestAbout);
            keyFeedback.InputGestures.Remove(altGestFeed);
            keyHelp.InputGestures.Remove(altGestHelp);

            //Setup alternate keyboard shortcuts (customisation for user)

            keyDu.InputGestures.Add(gestDu);
            keyPro.InputGestures.Add(gestPro);
            keyTri.InputGestures.Add(gestTri);
            keyGr.InputGestures.Add(gestGr);
            keyAbout.InputGestures.Add(gestAbout);
            keyFeedback.InputGestures.Add(gestFeed);
            keyHelp.InputGestures.Add(gestHelp);

            settingsDeu.InputGestureText = "Ctrl+Q";
            settingsPro.InputGestureText = "Ctrl+W";
            settingsTri.InputGestureText = "Ctrl+E";
            settingsGr.InputGestureText = "Ctrl+R";
            settingsAbout.InputGestureText = "Ctrl+A";
            settingsFeed.InputGestureText = "Ctrl+G";
            settingsHelp.InputGestureText = "Ctrl+H";




        }

        private void changeShortcuts(object sender, RoutedEventArgs e){
            if (currentKeyboardShortcuts == 0) { setAltKeyboardShortcuts(); currentKeyboardShortcuts = 1; AltKeyShortBut.IsChecked = true; }
            else { setOriginalKeyboardShortcuts(); currentKeyboardShortcuts = 0; AltKeyShortBut.IsChecked = false; }



        }


        /// <summary>
        /// Instead of closing the application via the close window, we instead want to keep the trayicon running
        /// Therefore, we hide this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dontClose(object sender, System.ComponentModel.CancelEventArgs e) { e.Cancel = true; Hide(); } //Minimise Instead


        /// <summary>
        /// A public getter for the real time filter, so that the trayicon can call it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="t"></param>
        public void toggleRealTimeGS(object sender, EventArgs t) { realTime(); }

        /// <summary>
        /// A public getter for the screenshot method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="t"></param>
        public void screenshotGS(object sender, EventArgs t) { runScreenshot(); }

        /// <summary>
        /// A public method to change the filter type, by calling the +1th % 5 filter type via a switch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="t"></param>
        public void switchTypesGS(object sender, EventArgs t){
            switch (color_filter_key){
                case 0: deuSetter();   break;
                case 1: proSetter();   break;
                case 2: triSetter();   break;
                case 3: grSetter();    break;
                case 4: deuSetter();   break;
            }
        }


        /// <summary>
        /// Sets the color filter to deuteranopia, and if real time is called, run it
        /// </summary>
        private void deuSetter()
        {
            color_filter_key = 1;
            chosenType.Content = "Chosen type: Deuteranopia";
            if (mode.status) mode.live();
        }

        /// <summary>
        /// Sets the color filter to protanopia, and if real time is called, run it
        /// </summary>
        private void proSetter()
        {
            color_filter_key = 2;
            chosenType.Content = "Chosen type: Protanopia";
            if (mode.status) mode.live();
        }

        /// <summary>
        /// Sets the color filter to tritanopia, and if real time is called, run it
        /// </summary>
        private void triSetter() 
        {
            color_filter_key = 3;
            chosenType.Content = "Chosen type: Tritanopia";
            if (mode.status) mode.live();
        }

        /// <summary>
        /// Sets the color filter to grayscale, and if real time is called, run it
        /// </summary>
        private void grSetter()
        {
            color_filter_key = 4;
            chosenType.Content = "Chosen type: Grayscale";
            if (mode.status) mode.live();
        }


        /// <summary>
        /// Attempt to run the screenshot method (or prompt the user choose one)
        /// Do this via creating and running a SelectArea object
        /// </summary>
        private void runScreenshot()
        {
            if (color_filter_key > 0 && color_filter_key < 5)
            {
                SelectArea obj = new SelectArea();
                //if u wanna hide the main window, uncomment the bottom line
                //this.Visibility = Visibility.Hidden;
                //objScreenshot.Opacity = 0.9;
                //obj.Opacity = 0.99;
                obj.Show();
            }
            else
            {
                selectFirstMsgBox();
            }
        }



        /// <summary>
        /// Attempt to run the real time filter by calling the Program1() class object
        /// If the user has not selected a blindness type, prompt to do so
        /// </summary>
        private void realTime()
        {
            if (color_filter_key > 0 && color_filter_key < 5)
            {
                mode.status = !mode.status;
                mode.live();
            }

            if (!mode.status && color_filter_key == 0) selectFirstMsgBox();
        }


        /// <summary>
        /// Prompt the user to select a blindness type via a message box
        /// </summary>
        private void selectFirstMsgBox(){System.Windows.Forms.MessageBox.Show("Please select a blindness type before proceeding.");}

        /// <summary>
        /// Open the feedback page via an external web browser
        /// </summary>
        private void feedbackOpener() { System.Diagnostics.Process.Start("https://docs.google.com/forms/d/e/1FAIpQLSc4TDuoVZX2SN-VAkzextT5rSZFY_5ZM2-4B_Z6BxHVwHsozQ/viewform"); }


        /// <summary>
        /// Load a youtube video in an external browser about Color Oracle (currently a rickroll placeholder)
        /// </summary>
        private void helpOpener() { System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=f5dhUYKFQic"); }

        /// <summary>
        /// Open the about page by creating a new page object
        /// </summary>
        private void aboutOpener() { About aboutPage = new About(); aboutPage.Show(); }


        //The following functions (until the end of the class) are simple getters for events occuring on the main window, using different arguments for the different inputs they are called with



        /// <summary>
        /// Call the deusetter for inputs using routedeventargs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setDeu(object sender, RoutedEventArgs e) { deuSetter(); }

        /// <summary>
        /// Call the prosetter for inputs using routedeventargs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setPro(object sender, RoutedEventArgs e) { proSetter(); }

        /// <summary>
        /// Call the trisetter for inputs using routedeventargs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setTri(object sender, RoutedEventArgs e) { triSetter(); }

        /// <summary>
        /// Call the grsetter for inputs using routedeventargs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setGr(object sender, RoutedEventArgs e) { grSetter(); }

        /// <summary>
        /// Call the realTime function for inputs using routedeventargs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void clickWindow(object sender, RoutedEventArgs e) { realTime(); }

        /// <summary>
        /// Call the feedbackOpener for inputs using routedeventargs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openSendFeedback(object sender, RoutedEventArgs e) { feedbackOpener(); }

        /// <summary>
        /// Call the aboutOpener for inputs using routedeventargs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openAbout(object sender, RoutedEventArgs e) { aboutOpener(); }

        /// <summary>
        /// Call the helpOpener for inputs using routedeventargs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openHelp(object sender, RoutedEventArgs e) { helpOpener(); }

        /// <summary>
        /// Call the runScreenshot for inputs using routedeventargs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Screenshot(object sender, RoutedEventArgs e) { runScreenshot(); }



        /// <summary>
        /// Call the helpOpener for inputs using EventArgs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventArgHelpOpen(object sender, EventArgs e) { helpOpener(); }

        /// <summary>
        /// Call the aboutOpener for inputs using EventArgs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventArgAboutOpen(object sender, EventArgs e) { aboutOpener(); }

        /// <summary>
        /// Call the feedbackOpener for inputs using EventArgs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventArgFeedback(object sender, EventArgs e) { feedbackOpener(); }

        /// <summary>
        /// Call the deuSetter for inputs using EventArgs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventArgSetDeu(object sender, EventArgs e) { deuSetter(); }

        /// <summary>
        /// Call the proSetter for inputs using EventArgs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventArgSetPro(object sender, EventArgs e) { proSetter(); }

        /// <summary>
        /// Call the triSetter for inputs using EventArgs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventArgSetTri(object sender, EventArgs e) { triSetter(); }

        /// <summary>
        /// Call the grSetter for inputs using EventArgs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventArgSetGr(object sender, EventArgs e) { grSetter(); }

        /// <summary>
        /// Shotdown the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventArgExit(object sender, EventArgs e) { System.Windows.Application.Current.Shutdown(); }
    }
}
