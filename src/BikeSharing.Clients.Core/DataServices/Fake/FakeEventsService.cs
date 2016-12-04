using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BikeSharing.Clients.Core.DataServices.Interfaces;
using BikeSharing.Clients.Core.Models.Events;
using System.Linq;

namespace BikeSharing.Clients.Core.DataServices.Fake
{
    public class FakeEventsService : IEventsService
    {
        private static List<Event> events = new List<Event>
        {
            new Event
            {
                Name = "NBA Match",
                Venue = new Venue
                {
                    Name = "New York Knicks vs. Brooklyn Nets"
                },
                StartTime = DateTime.Now,
                ImagePath = "https://connect16test.blob.core.windows.net/imgs/i_nba-match.png"
            },
            new Event
            {
                Name = "Music Ride",
                Venue = new Venue
                {
                    Name = "Green day"
                },
                StartTime = DateTime.Now,
                ImagePath = "https://connect16test.blob.core.windows.net/imgs/i_music-ride.png"
            }
        };

        public Task<Event> GetEventById(int eventId)
        {
            var data = events[0];

            return Task.FromResult(data);
        }

        public Task<IEnumerable<Event>> GetEvents()
        {
            var data = Enumerable.Range(0, 4).Select((index) => events[index % events.Count]);

            return Task.FromResult(data);
        }
    }
}
