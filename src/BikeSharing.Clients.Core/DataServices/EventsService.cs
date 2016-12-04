using BikeSharing.Clients.Core.DataServices.Base;
using BikeSharing.Clients.Core.DataServices.Interfaces;
using BikeSharing.Clients.Core.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.DataServices
{
    public class EventsService : IEventsService
    {
        private readonly IRequestProvider _requestProvider;

        public EventsService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.EventsEndpoint);
            builder.Path = "api/Events";

            string uri = builder.ToString();

            IEnumerable<Event> events = await _requestProvider.GetAsync<IEnumerable<Event>>(uri);

            return events;
        }

        public async Task<Event> GetEventById(int eventId)
        {
            var allEvents = await GetEvents();

            return allEvents.FirstOrDefault(e => e.Id == eventId);
        }
    }
}
