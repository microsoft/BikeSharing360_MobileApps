namespace BikeSharing.Clients.Core.Models.Users
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LastLogin { get; set; }
        public UserProfile Profile { get; set; }
        public System.Collections.Generic.List<Subscription> Subscriptions { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}