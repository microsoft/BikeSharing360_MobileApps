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
                    Name = "Central Park",
                    Distance = 1900,
                    ImagePath = Device.OS == TargetPlatform.Windows
                        ? @"Assets\suggestion_central_park.png"
                        : "suggestion_central_park",
                    Latitude = 40.7828687f,
                    Longitude = -73.9675438f
                },
                new Suggestion
                {
                    Name = "Flushing Meadows Corona Park",
                    Distance = 2200,
                    ImagePath = Device.OS == TargetPlatform.Windows
                        ? @"Assets\suggestion_corona_park.png"
                        : "suggestion_corona_park",
                    Latitude = 40.7397176f,
                    Longitude = -73.8429737f
                },
                new Suggestion
                {
                    Name = "Liberty State Park",
                    Distance = 3500,
                    ImagePath = Device.OS == TargetPlatform.Windows
                        ? @"Assets\suggestion_liberty_state_park.png"
                        : "suggestion_liberty_state_park",
                    Latitude = 40.703336f,
                    Longitude = -73.8429737f
                }
            };

            if (Device.OS == TargetPlatform.Windows && Device.Idiom == TargetIdiom.Desktop)
            {
                var desktopOnly = new List<Suggestion>()
                {
                    new Suggestion
                    {
                        Name = "Bronx River",
                        Distance = 3200,
                        ImagePath = @"Assets\suggestion_bronx_river.png",
                        Latitude = 40.7397176f,
                        Longitude = -73.8429737f
                    },
                    new Suggestion
                    {
                        Name = "Coney Island",
                        Distance = 6300,
                        ImagePath = @"Assets\suggestion_coney_island.png",
                        Latitude = 40.577318f,
                        Longitude = -73.9894199f
                    },
                    new Suggestion
                    {
                        Name = "Hudson River",
                        Distance = 8740,
                        ImagePath = @"Assets\suggestion_hudson_river.png",
                        Latitude = 40.762337f,
                        Longitude = -74.0186763f
                    },
                    new Suggestion
                    {
                        Name = "Rockways",
                        Distance = 5182,
                        ImagePath = @"Assets\suggestion_rockways.png",
                        Latitude = 40.5786896f,
                        Longitude = -73.874552f
                    },
                };

                suggestions.AddRange(desktopOnly);
            }

            return suggestions;
        }
    }
}
