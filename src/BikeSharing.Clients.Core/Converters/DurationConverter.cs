using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Enums;
using BikeSharing.Clients.Core.Utils;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Converters
{
    public class DurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int))
            {
                throw new InvalidOperationException("The target must be a int (time in seconds)");
            }

            var totalSeconds = (int)value;
            var timeSpan = TimeSpan.FromSeconds(totalSeconds);
            var result = timeSpan.Humanize(precision:2);
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
