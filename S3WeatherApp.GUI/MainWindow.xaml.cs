using S3WeatherApp.Entities;
using S3WeatherApp.GUI.UserControls;
using S3WeatherApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace S3WeatherApp.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            searcBar.Content = new SearchCity(this);
        }

        public void getTemperature(string city)
        {
            Task<WeatherData> task;
            try
            {
                task = Client.GetCurrentTemperature(city);
                task.Wait();
                WeatherData weatherData = task.Result;
                temperatureTextBlock.Text = weatherData.Temperature + "°C";
                if (temperatureTextBlock.FontSize == 20)
                    temperatureTextBlock.FontSize = 150;
            }
            catch (AggregateException ex) when(ex.InnerException is TimeoutException)
            {
                if (temperatureTextBlock.FontSize == 150)
                    temperatureTextBlock.FontSize = 20;
                temperatureTextBlock.Text = "Timeout. Svar ikke opnået på 5 sekunder.";
                return;
            }
            catch (AggregateException ex) when(ex.InnerException is HttpListenerException)
            {
                if (temperatureTextBlock.FontSize == 150)
                    temperatureTextBlock.FontSize = 20;
                temperatureTextBlock.Text = $"Kunne ikke finde byen \"{city}\". Prøv at sikre dig, at du ikke har skrevet noget forkert.";
                return;
            }
        }
    }
}
