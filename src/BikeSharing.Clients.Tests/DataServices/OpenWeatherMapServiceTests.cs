using BikeSharing.Clients.Core.DataServices;
using BikeSharing.Clients.Core.DataServices.Base;
using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Services;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Tests
{
    [TestFixture]
    public class OpenWeatherMapServiceTests
    {
        [Test]
        public async Task VerifyServiceWorks()
        {
            var locationProvider = new Mock<ILocationProvider>();

            locationProvider.Setup(m => m.GetPositionAsync())
            .ReturnsAsync(new GeoLocation
            {
                Latitude = 44.55d,
                Longitude= 23,
            });

            var weatherService = new OpenWeatherMapService(locationProvider.Object, new RequestProvider());

            for (var i = 0; i < 15; i++)
            {
                var weather = await weatherService.GetWeatherInfoAsync();

                Assert.IsInstanceOf(typeof(WeatherInfo), weather);
            }
        }
    }
}