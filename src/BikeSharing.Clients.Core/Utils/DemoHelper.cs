using BikeSharing.Clients.Core.Models;
using Xamarin.Forms.Maps;

namespace BikeSharing.Clients.Core.Utils
{
    public static class DemoHelper
    {
        public static GeoLocation DefaultLocation = new GeoLocation
        {
            Latitude = 47.608013,
            Longitude = -122.335167
        };

        public static void CenterMapInDefaultLocation(Map map)
        {
            var initialPosition = new Position(
                DefaultLocation.Latitude,
                DefaultLocation.Longitude);

            var mapSpan = MapSpan.FromCenterAndRadius(
                initialPosition,
                Distance.FromMiles(1.0));

            map.MoveToRegion(mapSpan);
        }
    }
}
