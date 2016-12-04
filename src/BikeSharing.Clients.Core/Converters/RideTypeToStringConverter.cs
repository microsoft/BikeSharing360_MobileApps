using BikeSharing.Clients.Core.Models.Rides;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Converters
{
    public class RideTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RideType? type = value as RideType?;

            if (!type.HasValue)
            {
                return null;
            }

            switch (type.Value)
            {
                case RideType.Event:
                    return "Event";
                case RideType.Suggestion:
                    return "Suggested";
                case RideType.Custom:
                    return "Custom ride";
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
