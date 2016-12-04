using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Events;
using BikeSharing.Clients.Core.Models.Rides;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.DataServices.Interfaces
{
    public interface IRidesService
    {
        Task<IEnumerable<Suggestion>> GetSuggestions();

        Task<IEnumerable<Ride>> GetUserRides();

        Task<Station> GetNearestStationTo(GeoLocation location);

        Task<IEnumerable<Station>> GetNearestStations();

        Task<Station> GetInfoForNearestStation();

        Task<Station> GetInfoForNearestStationTo(GeoLocation toGeoLocation);

        Task<Station> GetStation(int stationId);

        Task<Booking> RequestBikeBooking(Station fromStation, Station toStation, Event @event);

        Task<Booking> RequestBikeBooking(Station fromStation, Station toStation);

        Task<IEnumerable<Station>> GetNearestStationsTo(GeoLocation location);

        Task<Booking> GetBooking(int bookingId);

        void RemoveCurrentBooking();
    }
}
