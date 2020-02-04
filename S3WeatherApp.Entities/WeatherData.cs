using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3WeatherApp.Entities
{
    public struct WeatherData
    {
        private readonly string city;
        private readonly string temperature;

        public WeatherData(string city, string temperature)
        {
            this.city = city;
            this.temperature = temperature;
        }

        public string City { get { return city; } }
        public string Temperature { get { return temperature; } }
    }
}
