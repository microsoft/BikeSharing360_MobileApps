using BikeSharing.Clients.Core.Models;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms.Maps;

namespace BikeSharing.Clients.Core.Utils
{
    public static class DemoHelper
    {
        public static GeoLocation DefaultLocation = new GeoLocation
        {
            Latitude = 40.7211514,
            Longitude = -74.0057011
        };

        public static void CenterMapInDefaultLocation(Map map)
        {
            var initialPosition = new Xamarin.Forms.Maps.Position(
                DefaultLocation.Latitude,
                DefaultLocation.Longitude);

            var mapSpan = MapSpan.FromCenterAndRadius(
                initialPosition,
                Distance.FromMiles(1.0));

            map.MoveToRegion(mapSpan);
        }
    }
}
