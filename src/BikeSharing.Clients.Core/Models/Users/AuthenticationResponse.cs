namespace BikeSharing.Clients.Core.Models.Users
{
    public class AuthenticationResponse
    {
        public int UserId { get; set; }

        public int ProfileId { get; set; }

        public string AccessToken { get; set; }
    }
}
