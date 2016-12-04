using System;
using System.Globalization;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Converters
{
    public class MetersToMilesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int))
            {
                throw new InvalidOperationException("The target must be a int");
            }

            int meters = (int)value;

            return (meters / 1609.344);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
