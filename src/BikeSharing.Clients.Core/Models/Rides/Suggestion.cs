namespace BikeSharing.Clients.Core.Models.Rides
{
    public class Suggestion
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public int Distance { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}
