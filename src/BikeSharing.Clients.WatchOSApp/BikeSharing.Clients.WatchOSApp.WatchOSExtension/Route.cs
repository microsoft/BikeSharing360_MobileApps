namespace BikeSharing.Clients.WatchOSApp.WatchOSExtension
{
    public class Station
    {
        public string Name { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }

    public class Route
    {
        public Station From { get; set; }
        public Station To { get; set; }
    }
}