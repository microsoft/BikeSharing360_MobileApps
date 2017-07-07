namespace BikeSharing.Clients.Core.ViewModels.Base
{
    public class MessengerKeys
    {
        // Sign Up Keys
        public const string NextCard = "NextCard";
        public const string CloseCard = "CloseCard";
        public const string LastCard = "LastCard";
        public const string IsValid = "IsValid";

        // Booking keys
        public const string BookingRequested = "BookingRequested";
        public const string BookingFinished = "BookingFinished";
        public const string BookingReloadRequest = "BookingReloadRequest";

        // Report keys
        public const string ReportSent = "ReportSent";
        public const string GoBackFromReportRequest = "GoBackFromReportRequest";

        // Profile keys
        public const string ProfileUpdated = "ProfileUpdated";

        // iOS
        public const string iOSMainPageCurrentChanged = "iOSMainPageCurrentChanged";
    }
}
