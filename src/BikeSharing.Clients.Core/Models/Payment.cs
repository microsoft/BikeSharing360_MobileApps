using System;

namespace BikeSharing.Clients.Core.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string CreditCard { get; set; }
        public int CreditCardType { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
