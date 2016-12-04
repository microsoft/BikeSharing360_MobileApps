using System;

namespace BikeSharing.Clients.Core.Models
{
    public class CreditCardInformation
    {
        public string CardNumber { get; set; }

        public string ExpirationMonth { get; set; }

        public string ExpirationYear { get; set; }
    }
}
