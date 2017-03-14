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
					new Station { Id = 1, Name = "Children's Hospital / Sandpoint Way NE & 40th Ave NE", Latitude = 47.663509f, Longitude = -122.284119f },
					new Station { Id = 2, Name = "Burke-Gilman Trail / NE Blakeley St & 24th Ave NE", Latitude = 47.666145f, Longitude = -122.301491f },
					new Station { Id = 3, Name = "NE 47th St & 12th Ave NE", Latitude = 47.663143f, Longitude = -122.315086f }
				};
			}
		}
	}
}