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
using ContextMenu = System.Windows.Forms.ContextMenu;

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
        //a notifyIcon for WPF
        NotifyIcon nIcon = new NotifyIcon();
        //the shortcut menu for notifyIcon
        public ContextMenu contextMenu1 = new ContextMenu();
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
        private void setDu(object sender, EventArgs e)
        {
            mode.color_filter_key = 1;
            changeMode();
        }
        
        private void setPro(object sender, EventArgs e)
        {
            mode.color_filter_key = 2;
            changeMode();
        }
        private void setTri(object sender, EventArgs e)
        {
            mode.color_filter_key = 3;
            changeMode();
        }
        private void setGr(object sender, EventArgs e)
        {
            mode.color_filter_key = 4;
            changeMode();
            
        }


        private void Screenshot(object sender, EventArgs e)
        {
            SelectArea obj = new SelectArea();
            //if u wanna hide the main window, uncomment the bottom line
            //this.Visibility = Visibility.Hidden;
            //objScreenshot.Opacity = 0.9;
            //obj.Opacity = 0.99;
            obj.Show();


        }

        public void clickWindow(object sender, EventArgs e)
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
     


        private void openSendFeedback(object sender, EventArgs e)
        {
            //Go to feedback page
            FeedbackPage win1 = new FeedbackPage();
            win1.Show();
        }

        private void openAbout(object sender,EventArgs e)
        {
           // throw new NotImplementedException();
            //Open About Page
            About aboutPage = new About();
            aboutPage.Show();
        }

        private void openHelp(object sender, EventArgs e)
        {
            Help helpPage = new Help();
            helpPage.Show();
        }
     
        private void Button_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
            this.nIcon.Icon = new Icon("../../images/trayicon.ico");
            contextMenu1.MenuItems.Add("Help", (s, d) => this.openHelp(s, d));
            contextMenu1.MenuItems.Add("About", (s, d) => this.openAbout(s, d));
            contextMenu1.MenuItems.Add("Live Mode", (s, d) => this.clickWindow(s, d));
            contextMenu1.MenuItems.Add("openSendFeedback", (s, d) => this.openSendFeedback(s, d));
            contextMenu1.MenuItems.Add("Deuteranopia", (s, d) => this.setDu(s, d));
            contextMenu1.MenuItems.Add("Protanopia", (s, d) => this.setPro(s, d));
            contextMenu1.MenuItems.Add("Tritanopia", (s, d) => this.setTri(s, d));
            contextMenu1.MenuItems.Add("Grayscale", (s, d) => this.setGr(s, d));
            contextMenu1.MenuItems.Add("ScreenMode", (s, d) => this.Screenshot(s, d));



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
   
