using System;
using Android.OS;
using BikeSharing.Clients.Core;

namespace BikeSharing.Clients.Droid
{
	public class OperatingSystemVersionProvider : IOperatingSystemVersionProvider
	{
		public string GetOperatingSystemVersionString()
		{
			return Build.VERSION.Release;
		}
	}
}
