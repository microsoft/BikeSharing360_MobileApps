using BikeSharing.Clients.Core.Models.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Converters
{
    public class MenuItemTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var menuItemType = (MenuItemType)value;

            var platform = Device.OS == TargetPlatform.Windows;

            switch(menuItemType)
            {
                case MenuItemType.Profile:
                    return platform ? "Assets/uwp_ic_user.png" : "menu_ic_user.png";
                case MenuItemType.MyRides:
                    return platform ? "Assets/uwp_ic_my_rides.png" : "menu_ic_bike.png";
                case MenuItemType.UpcomingRide:
                    return platform ? "Assets/uwp_ic_upcoming_ride.png" : "menu_ic_current_book.png";
                case MenuItemType.ReportIncident:
                    return platform ? "Assets/uwp_ic_report.png" : "menu_ic_report_incident.png";
                case MenuItemType.NewRide:
                    return platform ? "Assets/uwp_ic_new_ride.png" : "menu_ic_new_ride.png";
                case MenuItemType.Home:
                    return platform ? "Assets/uwp_ic_home.png" : string.Empty;
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
