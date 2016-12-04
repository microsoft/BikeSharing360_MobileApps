using BikeSharing.Clients.Core.Models.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.DataServices.Interfaces
{
    public interface IEventsService
    {
        Task<IEnumerable<Event>> GetEvents();

        Task<Event> GetEventById(int eventId);
    }
}
