using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Color_Test_WPF_App_NET_Framework
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class About : Window
    {

        /// <summary>
        /// Constructor for the about page
        /// </summary>
        public About()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Open the website in an external browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openWebsite(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://colororacle.org");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }


        /// <summary>
        /// Open an email client via a mailto address
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openEmail(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("mailto:colororacleproject@gmail.com");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Open the source code on Github
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openSource(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://github.com/colororacle");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
