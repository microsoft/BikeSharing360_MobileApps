namespace BikeSharing.Clients.Core.Models.Events
{
    public class Venue
    {
        public int Id { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public string Name { get; set; }

        public string ExternalId { get; set; }
    }
}