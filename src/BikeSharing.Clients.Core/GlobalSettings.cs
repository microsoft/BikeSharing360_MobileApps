using System;

namespace BikeSharing.Clients.Core
{
    public static class GlobalSettings
    {
        public const string AuthenticationEndpoint = "http://bikesharingservicesprofiles.azurewebsites.net/";
        public const string EventsEndpoint = "http://bikesharingservicesevents20170320105247.azurewebsites.net/";
        public const string IssuesEndpoint = "http://bikesharingservicesfeedback.azurewebsites.net/";
        public const string RidesEndpoint = "http://bikesharingrides.azurewebsites.net/";

        public const string OpenWeatherMapAPIKey = "YOUR_WEATHERMAP_API_KEY";

        public const string HockeyAppAPIKeyForAndroid = "81c09d5d-781b-4007-809b-383d09946185";
        public const string HockeyAppAPIKeyForiOS = "81c09d5d-781b-4007-809b-383d09946185";

        public const string SkypeBotAccount = "skype:YOUR_BOT_ID?chat";

        public const string BingMapsAPIKey = "YOUR_BINGMAPS_API_KEY";


        public static string City => "Redmond";

        public static int TenantId = 1;

        public static DateTime EventDate = new DateTime(2017, 03, 07);
        public static float EventLatitude = 47.673988f;
        public static float EventLongitude = -122.121513f;
    }
}
