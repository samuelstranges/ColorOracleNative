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


        /// <summary>
        /// Initialise the window
        /// In addition, setup the local keyboard shortcuts via input gestures and command bindings
        /// </summary>
        public MainWindow() {
            InitializeComponent();

            //Keyboard Shortcuts (Local Main Window Shortcuts)
            RoutedCommand keyPro = new RoutedCommand(); keyPro.InputGestures.Add(new KeyGesture(Key.W, ModifierKeys.Control)); CommandBindings.Add(new CommandBinding(keyPro, setPro));
            RoutedCommand keyDu = new RoutedCommand(); keyDu.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control)); CommandBindings.Add(new CommandBinding(keyDu, setDeu));
            RoutedCommand keyTri = new RoutedCommand(); keyTri.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control)); CommandBindings.Add(new CommandBinding(keyTri, setTri));
            RoutedCommand keyGr = new RoutedCommand(); keyGr.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control)); CommandBindings.Add(new CommandBinding(keyGr, setGr));
            RoutedCommand keyAbout = new RoutedCommand(); keyAbout.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control)); CommandBindings.Add(new CommandBinding(keyAbout, openAbout));
            RoutedCommand keyFeedback = new RoutedCommand(); keyFeedback.InputGestures.Add(new KeyGesture(Key.G, ModifierKeys.Control)); CommandBindings.Add(new CommandBinding(keyFeedback, openSendFeedback));
            RoutedCommand keyHelp = new RoutedCommand(); keyHelp.InputGestures.Add(new KeyGesture(Key.H, ModifierKeys.Control)); CommandBindings.Add(new CommandBinding(keyHelp, openHelp));

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
                case 4: normalSetter();break;
            }
        }



        /// <summary>
        /// Sets the color filter to normal, and if real time is called, run it
        /// </summary>
        private void normalSetter() {
            if (mode.status) realTime();
            color_filter_key = 0;
            chosenType.Content = "Chosen type: None";
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

            if (!mode.status)
            {
                if (color_filter_key == 0) selectFirstMsgBox();
                else
                {
                    color_filter_key = 0;
                    //mode.color_filter_key = color_filter_key;
                    chosenType.Content = "Chosen type: None";
                }
              
            }
        }


        /// <summary>
        /// Prompt the user to select a blindness type via a message box
        /// </summary>
        private void selectFirstMsgBox(){System.Windows.Forms.MessageBox.Show("Please select a blindness type before proceeding.");}

        /// <summary>
        /// Open the feedback page via an external web browser
        /// </summary>
        private void feedbackOpener() { System.Diagnostics.Process.Start("https://docs.google.com/forms/d/1wsFwdaKLq53ZWrAkwraCXpOVz41yqyYolzkifKklSkc/edit"); }


        /// <summary>
        /// Load a youtube video in an external browser about Color Oracle (currently a rickroll placeholder)
        /// </summary>
        private void helpOpener() { System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ"); }

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
