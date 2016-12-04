namespace BikeSharing.Clients.Core.Models.Rides
{
    public class Station
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public int Slots { get; set; }

        public int Occupied { get; set; }

        public int EmptyDocks => Slots - Occupied;
    }
}
