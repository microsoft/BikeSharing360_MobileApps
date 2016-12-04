using System;

namespace BikeSharing.Clients.Core.DataServices
{
    public class NoAvailableBikesException : Exception
    {
        public NoAvailableBikesException()
        {
        }

        public NoAvailableBikesException(string message) : base(message)
        {
        }
    }
}
