using System;

namespace BikeSharing.Clients.Core.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string CreditCard { get; set; }
        public string CreditCardType { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
