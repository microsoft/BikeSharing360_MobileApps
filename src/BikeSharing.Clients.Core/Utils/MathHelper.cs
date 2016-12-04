namespace BikeSharing.Clients.Core.Utils
{
    public static class MathHelper
    {
        public static double ReMap(double oldValue, double oldMin, double oldMax, double newMin, double newMax)
        {
            return (((oldValue - oldMin) / (oldMax - oldMin)) * (newMax - newMin)) + newMin;
        }
    }
}
