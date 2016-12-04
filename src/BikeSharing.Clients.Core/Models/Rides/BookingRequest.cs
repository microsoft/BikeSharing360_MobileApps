namespace BikeSharing.Clients.Core.Models.Rides
{
    public class BookingRequest
    {
        public int EndStationId { get; set; }

        public int UserId { get; set; }

        public BookingRequestEvent Event { get; set; }

        public class BookingRequestEvent
        {
            public int Id { get; set; }

            public string EventName { get; set; }

            public RideType EventType { get; set; }
        }
    }
}
