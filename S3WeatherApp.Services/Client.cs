using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3WeatherApp.Services
{
    public class Client
    {
        string apiKey;
        string formatMode;
        string serverUrl;

        Task<WeatherData> GetCurrentTemperature(string city)
        {
            throw new NotImplementedException();
        }
    }
}
