using BikeSharing.Clients.Core.Controls;
using System;
using System.Globalization;
using Xamarin.Forms;
using static BikeSharing.Clients.Core.Controls.CustomPin;

namespace BikeSharing.Clients.Core.Converters
{
    class StationTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var customPin = value as CustomPin;

            if(customPin != null)
            {
                return customPin.Type;
            }

            return AnnotationType.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
