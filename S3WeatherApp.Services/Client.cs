using S3WeatherApp.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace S3WeatherApp.Services
{
    public static class Client
    {
        private const string apiKey = "be3af3fd5849838c549f1d557af0e382";
        private const string formatMode = "xml";
        private const string serverUrl = "api.openweathermap.org/data/2.5/weather";

        public static Task<WeatherData> GetCurrentTemperature(string city)
        {
            return Task<WeatherData>.Factory.StartNew(() =>
            {
                string apiOutput = "";
                UriBuilder uriBuilder = new UriBuilder(serverUrl);
                uriBuilder.Query = "" +
                "q=" + city + ",dk&" +
                "units=metric&" +
                "mode=" + formatMode + "&" +
                "appid=" + apiKey;
                WebRequest request = WebRequest.Create(uriBuilder.Uri);
                request.Timeout = 5000;
                try
                {
                    WebResponse response = request.GetResponse();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        apiOutput = reader.ReadToEnd();
                    }
                }
                catch (WebException ex) when (ex.Status == WebExceptionStatus.Timeout)
                {
                    throw new TimeoutException("Request took to long");
                }
                catch (WebException ex)
                {
                    throw new HttpListenerException(ex.HResult, "Request failed");
                }
                XmlDocument xml = new XmlDocument();
                try
                {
                    xml.LoadXml(apiOutput);
                }
                catch
                {
                    throw new FormatException("API returned data in invalid format.");
                }
                XmlNode temperatureNode = xml.SelectSingleNode("current").SelectSingleNode("temperature");
                string temperature = temperatureNode.Attributes.GetNamedItem("value").Value;
                return new WeatherData(city, temperature);
            });
        }
    }
}
