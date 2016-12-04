using BikeSharing.Clients.Core.Controls;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Converters
{
    public class MapColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
         var pin = value as CustomPin;

            if(pin != null)
            {
                if (pin.Type == CustomPin.AnnotationType.From)
                    return Color.FromHex("3062F5");

                if (pin.Type == CustomPin.AnnotationType.To)
                    return Color.FromHex("FF5E4C");
            }

            return Color.Blue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
