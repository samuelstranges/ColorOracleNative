using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Color_Test_WPF_App_NET_Framework
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// This is the root object of the project that is created and run.
    /// </summary>
    public partial class App : Application{

        /// <summary>
        /// Begin the application by creating an instance of the trayicon, which is the anchor of the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Startup(object sender, StartupEventArgs e){
            TheTrayIcon theIcon = new TheTrayIcon();
        }
    }
}
