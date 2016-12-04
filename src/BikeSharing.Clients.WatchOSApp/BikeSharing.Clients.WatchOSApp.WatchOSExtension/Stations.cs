using System.Collections.Generic;

namespace BikeSharing.Clients.WatchOSApp.WatchOSExtension
{
	public static class Stations
	{
		public static List<Station> All
		{
			get
			{
				return new List<Station>
				{
					new Station { Id = 1, Name = "Central Park West & W 68 St", Latitude = 40.7734066f, Longitude = -73.97782542f },
					new Station { Id = 2, Name = "E 85 St & York Ave", Latitude = 40.77536905f, Longitude = -73.94803392f },
					new Station { Id = 3, Name = "1 Ave & E 62 St", Latitude = 40.7612274f, Longitude = -73.96094022f }
				};
			}
		}
	}
}