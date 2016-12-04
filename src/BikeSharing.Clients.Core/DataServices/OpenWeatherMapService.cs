using BikeSharing.Clients.Core.DataServices.Base;
using BikeSharing.Clients.Core.DataServices.Interfaces;
using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Services;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.DataServices
{
    public class OpenWeatherMapService : IWeatherService
    {
        private const int OkResponseCode = 200;
        private readonly string OpenWeatherMapEndpoint = "http://api.openweathermap.org/";

        private readonly ILocationProvider _locationProvider;
        private readonly IRequestProvider _requestProvider;

        public OpenWeatherMapService(ILocationProvider locationProvider, IRequestProvider requestProvider)
        {
            this._locationProvider = locationProvider;
            this._requestProvider = requestProvider;
        }

        public async Task<IWeatherResponse> GetWeatherInfoAsync()
        {
            var location = await this._locationProvider.GetPositionAsync();
            if (location is GeoLocation)
            {
                var geolocation = location as GeoLocation;
                var latitude = geolocation.Latitude.ToString("0.0000", CultureInfo.InvariantCulture);
                var longitude = geolocation.Longitude.ToString("0.0000", CultureInfo.InvariantCulture);

                var builder = new UriBuilder(OpenWeatherMapEndpoint);
                builder.Path = $"data/2.5/weather";
                builder.Query = $"lat={latitude}&lon={longitude}&units=imperial&appid={GlobalSettings.OpenWeatherMapAPIKey}";
                var uri = builder.ToString();

                var response = await _requestProvider.GetAsync<OpenWeatherMapResponse>(uri);
                if (response?.cod == OkResponseCode)
                {
                    var weatherInfo = new WeatherInfo
                    {
                        LocationName = response.name,
                        Temp = response.main.temp,
                        TempUnit = TempUnit.Fahrenheit
                    };

                    return weatherInfo;
                }

                Debug.WriteLine("OpenWeatherMap API answered with: " + ((response != null) ? $"Error code = {response.cod}." : "Invalid response."));
            }

            // Default data for demo
            return new WeatherInfo
            {
                LocationName = GlobalSettings.City,
                Temp = 56,
                TempUnit = TempUnit.Fahrenheit
            };
        }
    }
}
