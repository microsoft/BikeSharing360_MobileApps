using BikeSharing.Clients.Core.DataServices.Base;
using BikeSharing.Clients.Core.DataServices.Interfaces;
using BikeSharing.Clients.Core.Helpers;
using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Events;
using BikeSharing.Clients.Core.Models.Rides;
using BikeSharing.Clients.Core.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.DataServices
{
    public class RidesService : IRidesService
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IAuthenticationService _authenticationService;

        private static List<Suggestion> suggestions = StaticData.GetSuggestions();

        private static int StationsCounter = 0;

        private static List<Station> stations = new List<Station>
        {
            new Station
            {
                Name = "Alki Beach Park I",
                Slots = 22,
                Occupied = 4,
                Latitude = 47.5790791f,
                Longitude = -122.4136163f
            },
            new Station
            {
                Name = "Alki Beach Park II",
                Slots = 12,
                Occupied = 7,
                Latitude = 47.5743905f,
                Longitude = -122.4023376f
            },
            new Station
            {
                Name = "Alki Point Lighthouse",
                Slots = 15,
                Occupied = 5,
                Latitude = 47.5766275f,
                Longitude = -122.4217906f
            }
        };

        private static List<Ride> rides = new List<Ride>
        {
            new Ride
            {
                EventId = 1,
                RideType = RideType.Event,
                Name = "Ride Cultural",
                Start = DateTime.Now.AddDays(-7),
                Stop = DateTime.Now.AddDays(-7),
                Duration = 3600,
                Distance = 19,
                From = stations[0].Name,
                FromStation = stations[0],
                To = stations[2].Name,
                ToStation = stations[2]
            },
            new Ride
            {
                RideType = RideType.Custom,
                Start = DateTime.Now.AddDays(-14),
                Stop = DateTime.Now.AddDays(-14),
                Duration = 2500,
                Distance = 8900,
                From = stations[1].Name,
                FromStation = stations[1],
                To = stations[0].Name,
                ToStation = stations[0]
            },
            new Ride
            {
                RideType = RideType.Suggestion,
                Start = DateTime.Now.AddDays(-14),
                Stop = DateTime.Now.AddDays(-14),
                Duration = 1800,
                Distance = 10100,
                From = stations[2].Name,
                FromStation = stations[2],
                To = stations[1].Name,
                ToStation = stations[1]
            }
        };

        public RidesService(IRequestProvider requestProvider, IAuthenticationService authenticationService)
        {
            _requestProvider = requestProvider;
            _authenticationService = authenticationService;
        }

        public Task<Booking> RequestBikeBooking(Station station, Event @event)
        {
            return BikeBooking(station, RideType.Event, @event.Id);
        }

        public Task<Booking> RequestBikeBooking(Station station, Suggestion suggestion)
        {
            return BikeBooking(station, RideType.Suggestion, suggestion.Id);
        }

        public Task<Booking> RequestBikeBooking(Station fromStation, Station toStation)
        {
            return BikeBooking(fromStation, RideType.Custom, 0);
        }

        private async Task<Booking> BikeBooking(Station station, RideType type, int id)
        {
            await Task.Delay(500);

            var booking = new Booking
            {
                Id = 222,
                FromStation = station,
                ToStation = stations[0],
                RegistrationDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddMinutes(3),
                EventId = id,
                BikeId = 2332,
                RideType = type
            };

            Settings.CurrentBookingId = booking.Id;

            return booking;
        }

        public async Task<Booking> GetBooking(int bookingId)
        {
            await Task.Delay(500);

            var booking = new Booking
            {
                Id = bookingId,
                FromStation = stations[0],
                ToStation = stations[1],
                RegistrationDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddMinutes(3),
                EventId = 1,
                BikeId = 2332,
                RideType = RideType.Event
            };

            return booking;
        }

        public Task<Station> GetNearestStationTo(GeoLocation location)
        {
            var station = stations[StationsCounter++ % stations.Count()];

            return Task.FromResult(station);
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

            IEnumerable<Ride> rides = await _requestProvider.GetAsync<IEnumerable<Ride>>(uri);

            return rides;
        }

        public void RemoveCurrentBooking()
        {
            Settings.RemoveCurrentBookingId();
        }

        public async Task<IEnumerable<Station>> GetNearestStations()
        {
            await Task.Delay(200);

            return stations;
        }

        public async Task<IEnumerable<Station>> GetNearestStationsTo(GeoLocation location)
        {
            try
            {
                const int count = 10;
                var userId = _authenticationService.GetCurrentUserId();

                UriBuilder builder = new UriBuilder(GlobalSettings.RidesEndpoint);
                builder.Path = $"/api/stations/nearto?latitude={location.Latitude.ToString(CultureInfo.InvariantCulture)}&longitude={location.Longitude.ToString(CultureInfo.InvariantCulture)}&count={count}";

                string uri = builder.ToString();

                IEnumerable<Station> stations = await _requestProvider.GetAsync<IEnumerable<Station>>(uri);

                return stations;
            }
            catch
            {
                await Task.Delay(200);

                return stations;
            }
        }

        public async Task<Station> GetInfoForNearestStation()
        {
            await Task.Delay(500);

            return stations.FirstOrDefault();
        }

        public async Task<Station> GetInfoForNearestStationTo(GeoLocation toGeoLocation)
        {
            await Task.Delay(500);

            return stations.FirstOrDefault();
        }

        public async Task<Station> GetStation(int stationId)
        {
            await Task.Delay(500);

            return stations.FirstOrDefault();
        }

        public Task<Booking> RequestBikeBooking(Station fromStation, Station toStation, Event @event)
        {
            return BikeBooking(fromStation, RideType.Event, @event.Id);
        }
    }
}
