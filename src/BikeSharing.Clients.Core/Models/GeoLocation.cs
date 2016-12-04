namespace BikeSharing.Clients.Core.Models
{
    public class GeoLocation : ILocationResponse
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}