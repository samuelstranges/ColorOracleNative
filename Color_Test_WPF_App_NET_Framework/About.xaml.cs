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
        public About()
        {
            InitializeComponent();
        }

        private void openWebsite(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://colororacle.org");
        }

        private void openEmail(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:colororacle@gmail.com");
        }

        private void openSource(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://github.com/colororacle");
        }
    }
}
