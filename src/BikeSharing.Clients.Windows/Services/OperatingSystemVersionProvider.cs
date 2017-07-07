using BikeSharing.Clients.Core;
using Windows.System.Profile;

namespace BikeSharing.Clients.Windows.Services
{
    public class OperatingSystemVersionProvider : IOperatingSystemVersionProvider
    {
        public string GetOperatingSystemVersionString()
        {
            string deviceFamilyVersion = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
            ulong version = ulong.Parse(deviceFamilyVersion);
            ulong major = (version & 0xFFFF000000000000L) >> 48;
            ulong minor = (version & 0x0000FFFF00000000L) >> 32;
            ulong build = (version & 0x00000000FFFF0000L) >> 16;
            ulong revision = (version & 0x000000000000FFFFL);
            var osVersion = $"{major}.{minor}.{build}.{revision}";

            return osVersion;
        }
    }
}
