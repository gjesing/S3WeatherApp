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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace S3WeatherApp.GUI.UserControls
{
    /// <summary>
    /// Interaction logic for SearchCity.xaml
    /// </summary>
    public partial class SearchCity : UserControl
    {
        MainWindow mainWindow;
        public SearchCity(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(searchBar.Text))
                mainWindow.getTemperature(searchBar.Text);
        }
    }
}
