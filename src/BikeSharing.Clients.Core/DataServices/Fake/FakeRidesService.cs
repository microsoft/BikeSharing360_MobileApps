using BikeSharing.Clients.Core.DataServices.Interfaces;
using BikeSharing.Clients.Core.Helpers;
using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Events;
using BikeSharing.Clients.Core.Models.Rides;
using BikeSharing.Clients.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.DataServices.Fake
{
    public class FakeRidesService : IRidesService
    {
        private static List<Suggestion> suggestions = StaticData.GetSuggestions();

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

        private static int StationsCounter = 0;
        private static List<Station> stations = new List<Station>
        {
            new Station
            {
                Id = 1,
                Name = "Alki Beach Park I",
                Slots = 22,
                Occupied = 4,
                Latitude = 47.5790791f,
                Longitude = -122.4136163f
            },
            new Station
            {
                Id = 2,
                Name = "Alki Beach Park II",
                Slots = 12,
                Occupied = 7,
                Latitude = 47.5743905f,
                Longitude = -122.4023376f
            },
            new Station
            {
                Id = 3,
                Name = "Alki Point Lighthouse",
                Slots = 5,
                Occupied = 15,
                Latitude = 47.5766275f,
                Longitude = -122.4217906f
            }
        };

        public async Task<IEnumerable<Suggestion>> GetSuggestions()
        {
            await Task.Delay(200);

            return suggestions;
        }

        public async Task<IEnumerable<Ride>> GetUserRides()
        {
            await Task.Delay(200);

            return rides;
        }

        public Task<Station> GetNearestStationTo(GeoLocation location)
        {
            var station = stations[StationsCounter++ % stations.Count()];

            return Task.FromResult(station);
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

        public Task<Booking> RequestBikeBooking(Station fromStation, Station toStation, Event @event)
        {
            return BikeBooking(fromStation, RideType.Event, @event.Id);
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
            await Task.Delay(200);

            return stations;
        }

        public Task<Station> GetInfoForNearestStationTo(GeoLocation toGeoLocation)
        {
            return GetNearestStationTo(toGeoLocation);
        }

        public async Task<Station> GetInfoForNearestStation()
        {
            await Task.Delay(200);
            return stations[0];
        }

        public Task<Station> GetStation(int stationId)
        {
            var st = stations.FirstOrDefault(s => s.Id == stationId) ?? stations[0];

            return Task.FromResult(st);
        }
    }
}