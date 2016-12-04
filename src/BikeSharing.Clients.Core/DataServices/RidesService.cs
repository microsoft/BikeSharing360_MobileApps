using BikeSharing.Clients.Core.DataServices.Base;
using BikeSharing.Clients.Core.DataServices.Interfaces;
using BikeSharing.Clients.Core.Helpers;
using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Events;
using BikeSharing.Clients.Core.Models.Rides;
using BikeSharing.Clients.Core.Services;
using BikeSharing.Clients.Core.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.DataServices
{
    public class RidesService : IRidesService
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILocationProvider _locationProvider;

        private static List<Suggestion> suggestions = StaticData.GetSuggestions();

        public RidesService(IRequestProvider requestProvider, IAuthenticationService authenticationService, ILocationProvider locationProvider)
        {
            _requestProvider = requestProvider;
            _authenticationService = authenticationService;
            _locationProvider = locationProvider;
        }

        public async Task<Booking> RequestBikeBooking(Station fromStation, Station toStation, Event @event)
        {
            CheckBikesAvailability(fromStation);

            var userId = _authenticationService.GetCurrentUserId();
            var fromStationId = fromStation.Id;

            UriBuilder builder = new UriBuilder(GlobalSettings.RidesEndpoint);
            builder.Path = $"api/stations/{fromStationId}/checkout";

            string uri = builder.ToString();

            BookingRequest request = new BookingRequest
            {
                EndStationId = toStation.Id,
                UserId = userId,
                Event = new BookingRequest.BookingRequestEvent
                {
                    Id = @event.Id,
                    EventName = @event.Name,
                    EventType = RideType.Event
                }
            };

            int bookingId = await _requestProvider.PutAsync<BookingRequest, int>(uri, request);
            Booking booking = await GetBooking(bookingId);

            Settings.CurrentBookingId = booking.Id;

            return booking;
        }

        public async Task<Booking> RequestBikeBooking(Station fromStation, Station toStation)
        {
            CheckBikesAvailability(fromStation);

            var userId = _authenticationService.GetCurrentUserId();
            var fromStationId = fromStation.Id;

            UriBuilder builder = new UriBuilder(GlobalSettings.RidesEndpoint);
            builder.Path = $"api/stations/{fromStationId}/checkout";

            string uri = builder.ToString();

            BookingRequest request = new BookingRequest
            {
                EndStationId = toStation.Id,
                UserId = userId
            };

            int bookingId = await _requestProvider.PutAsync<BookingRequest, int>(uri, request);
            Booking booking = await GetBooking(bookingId);

            Settings.CurrentBookingId = booking.Id;

            return booking;
        }

        public async Task<Booking> GetBooking(int bookingId)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.RidesEndpoint);
            builder.Path = $"api/bookings/{bookingId}";

            string uri = builder.ToString();

            Booking booking = await _requestProvider.GetAsync<Booking>(uri);

            return booking;
        }

        public async Task<Station> GetNearestStationTo(GeoLocation location)
        {
            IEnumerable<Station> stations = await GetNearestStationsTo(location);

            return stations.FirstOrDefault();
        }

        public async Task<Station> GetInfoForNearestStationTo(GeoLocation toGeoLocation)
        {
            Station station = await GetNearestStationTo(toGeoLocation);
            station = await GetStation(station.Id);

            return station;
        }

        public async Task<Station> GetInfoForNearestStation()
        {
            IEnumerable<Station> nearest = await GetNearestStations();
            Station station = nearest.FirstOrDefault();

            if (station != null)
            {
                station = await GetStation(station.Id);
            }

            return station;
        }

        public Task<Station> GetStation(int stationId)
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.RidesEndpoint);
            builder.Path = $"api/stations/{stationId}";

            string uri = builder.ToString();

            return _requestProvider.GetAsync<Station>(uri);
        }

        public async Task<IEnumerable<Suggestion>> GetSuggestions()
        {
            await Task.Delay(200);

            return suggestions;
        }

        public async Task<IEnumerable<Ride>> GetUserRides()
        {
            var userId = _authenticationService.GetCurrentUserId();

            UriBuilder builder = new UriBuilder(GlobalSettings.RidesEndpoint);
            builder.Path = $"api/rides/user/{userId}";

            string uri = builder.ToString();

            IEnumerable<Ride> userRides = await _requestProvider.GetAsync<IEnumerable<Ride>>(uri);

            return userRides;
        }

        public void RemoveCurrentBooking()
        {
            Settings.RemoveCurrentBookingId();
        }

        public async Task<IEnumerable<Station>> GetNearestStations()
        {
            ILocationResponse location = await _locationProvider.GetPositionAsync();

            return location is GeoLocation
                ? await GetNearestStationsTo(location as GeoLocation)
                : Enumerable.Empty<Station>();
        }

        public async Task<IEnumerable<Station>> GetNearestStationsTo(GeoLocation location)
        {
            int maxStations = Device.Idiom == TargetIdiom.Desktop ? 40 : 30;

            var userId = _authenticationService.GetCurrentUserId();

            UriBuilder builder = new UriBuilder(GlobalSettings.RidesEndpoint);
            builder.Path = $"/api/stations/nearto?latitude={location.Latitude.ToString(CultureInfo.InvariantCulture)}&longitude={location.Longitude.ToString(CultureInfo.InvariantCulture)}&count={maxStations}";

            string uri = builder.ToString();

            IEnumerable<Station> stations = await _requestProvider.GetAsync<IEnumerable<Station>>(uri);

            var all = stations.Select(s => s.Id.ToString()).Aggregate((s1, s2) => $"{s1}, {s2}");

            return stations;
        }

        private void CheckBikesAvailability(Station fromStation)
        {
            if (fromStation?.Occupied <= 0)
            {
                throw new NoAvailableBikesException($"Station {fromStation?.Id} has not any available bike");
            }
        }
    }
}
