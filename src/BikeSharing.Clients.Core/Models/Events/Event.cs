using System;

namespace BikeSharing.Clients.Core.Models.Events
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public DateTime StartTime { get; set; }

        public string ExternalId { get; set; }

        public Venue Venue { get; set; }
    }
}
