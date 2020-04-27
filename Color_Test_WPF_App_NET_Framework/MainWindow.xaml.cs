using Color_Test_WPF_App_NET_Framework.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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
using System.Windows.Forms;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using Rectangle = System.Drawing.Rectangle;

using System;
using System.Runtime.InteropServices;


namespace Color_Test_WPF_App_NET_Framework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// </summary>
    /// 
   
    public partial class MainWindow : Window
    {
        //initialize the realtime filter and default to normal 
        public static int color_filter_key = 0;
        Program1 mode = new Program1(color_filter_key);
        //if the default is normal, the filter is off for color blindness 
        public Boolean mode_status = false;
        public MainWindow()
        {
            InitializeComponent();
            
        }
        
        void changeMode() {
            //function to switch the filter mode in real-time filter
            if (mode_status)
            {
                mode.live();
                //Console.WriteLine("change");
            }
        }
        private void setDu(object sender, RoutedEventArgs e)
        {
            mode.color_filter_key = 1;
            changeMode();
        }
        
        private void setPro(object sender, RoutedEventArgs e)
        {
            mode.color_filter_key = 2;
            changeMode();
        }
        private void setTri(object sender, RoutedEventArgs e)
        {
            mode.color_filter_key = 3;
            changeMode();
        }
        private void setGr(object sender, RoutedEventArgs e)
        {
            mode.color_filter_key = 4;
            changeMode();
            
        }

        private void Screenshot(object sender, RoutedEventArgs e)
        {
            SelectArea obj = new SelectArea();
            //if u wanna hide the main window, uncomment the bottom line
            //this.Visibility = Visibility.Hidden;
            //objScreenshot.Opacity = 0.9;
            //obj.Opacity = 0.99;
            obj.Show();


        }

        public void clickWindow(object sender, RoutedEventArgs e)
        {
            //If mode off, set status to true and trun on the mode.
            if (!mode_status)
            {
                mode_status = true;
                changeMode();
                
            }
            //else reset the graph.
            else {

                mode.color_filter_key = 0;
                mode.live();
                mode_status = false;
            }
            
        }

        private void openSendFeedback(object sender, RoutedEventArgs e)
        {
            //Go to feedback page
            FeedbackPage win1 = new FeedbackPage();
            win1.Show();
        }

        private void openAbout(object sender, RoutedEventArgs e)
        {
            //Open About Page
            About aboutPage = new About();
            aboutPage.Show();
        }

        private void openHelp(object sender, RoutedEventArgs e)
        {
            Help helpPage = new Help();
            helpPage.Show();
        }

    }
}
   
