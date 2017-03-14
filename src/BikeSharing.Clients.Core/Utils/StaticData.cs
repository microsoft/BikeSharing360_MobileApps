using BikeSharing.Clients.Core.Models.Rides;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Utils
{
    public class StaticData
    {
        public static List<Suggestion> GetSuggestions()
        {
            var suggestions = new List<Suggestion>()
            {
                new Suggestion
                {
                    Name = "Beacon Hill",
                    Distance = 1900,
                    ImagePath = Device.OS == TargetPlatform.Windows
                        ? @"Assets\suggestion_beacon_hill.png"
                        : "suggestion_beacon_hill",
                    Latitude = 47.608013f,
                    Longitude = -122.9675438f
                },
                new Suggestion
                {
                    Name = "Golden Gardens",
                    Distance = 2200,
                    ImagePath = Device.OS == TargetPlatform.Windows
                        ? @"Assets\suggestion_golden_gardens.png"
                        : "suggestion_golden_gardens",
                    Latitude = 47.7397176f,
                    Longitude = -122.8429737f
                },
                new Suggestion
                {
                    Name = "Lake Union Loop",
                    Distance = 3500,
                    ImagePath = Device.OS == TargetPlatform.Windows
                        ? @"Assets\suggestion_lake_union_loop.png"
                        : "suggestion_lake_union_loop",
                    Latitude = 47.703336f,
                    Longitude = -122.8429737f
                }
            };

            if (Device.OS == TargetPlatform.Windows && Device.Idiom == TargetIdiom.Desktop)
            {
                var desktopOnly = new List<Suggestion>()
                {                
                    new Suggestion
                    {
                        Name = "Alki Beack",
                        Distance = 3500,
                        ImagePath = @"Assets\suggestion_ride_in_alki_beach.png",
                        Latitude = 47.4786896f,
                        Longitude = -122.883552f
                    },
                    new Suggestion
                    {
                        Name = "Lake Sammamish",
                        Distance = 5182,
                        ImagePath = @"Assets\suggestion_ride_in_lake_sammamish.png",
                        Latitude = 47.5786896f,
                        Longitude = -122.754553f
                    },
                    new Suggestion
                    {
                        Name = "Seattle Watefront",
                        Distance = 2173,
                        ImagePath = @"Assets\suggestion_seattle_waterfront.png",
                        Latitude = 47.6986896f,
                        Longitude = -122.695552f
                    },
                    new Suggestion
                    {
                        Name = "West Seattle Alki",
                        Distance = 5182,
                        ImagePath = @"Assets\suggestion_west_seattle_alki.png",
                        Latitude = 40.6995683f,
                        Longitude = -122.784552f
                    }
                };

                suggestions.AddRange(desktopOnly);
            }

            return suggestions;
        }
    }
}
