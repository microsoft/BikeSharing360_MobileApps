using System;
using BikeSharing.Clients.Core;
using UIKit;

namespace BikeSharing.Clients.iOS
{
	public class OperatingSystemVersionProvider : IOperatingSystemVersionProvider
	{
		public string GetOperatingSystemVersionString()
		{
			return UIDevice.CurrentDevice.SystemVersion;
		}
	}
}
