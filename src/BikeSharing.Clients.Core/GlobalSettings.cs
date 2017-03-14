using System;

namespace BikeSharing.Clients.Core
{
    public static class GlobalSettings
    {
        public const string AuthenticationEndpoint = "http://YOUR_PROFILE_SERVICE.azurewebsites.net/";
        public const string EventsEndpoint = "http://YOUR_EVENTS_SERVICE.azurewebsites.net/";
        public const string IssuesEndpoint = "http://YOUR_ISSUES_SERVICE.azurewebsites.net/";
        public const string RidesEndpoint = "http://YOUR_RIDES_SERVICE.azurewebsites.net/";

        public const string OpenWeatherMapAPIKey = "YOUR_WEATHERMAP_API_KEY";

        public const string HockeyAppAPIKeyForAndroid = "YOUR_HOCKEY_APP_ID";
        public const string HockeyAppAPIKeyForiOS = "YOUR_HOCKEY_APP_ID";

        public const string SkypeBotAccount = "skype:YOUR_BOT_ID?chat";

        public const string BingMapsAPIKey = "YOUR_BINGMAPS_API_KEY";


        public static string City => "Redmond";

        public static int TenantId = 1;

        public static DateTime EventDate = new DateTime(2017, 03, 07);
        public static float EventLatitude = 47.673988f;
        public static float EventLongitude = -122.121513f;
    }
}
