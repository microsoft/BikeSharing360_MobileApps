using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Converters
{
    public class BirthDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime))
            {
                throw new InvalidOperationException("The target must be a DateTime");
            }

            var date = (DateTime)value;

            var result = date.ToString("dd MMMM yyyy");
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
