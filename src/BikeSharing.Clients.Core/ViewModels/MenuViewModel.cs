using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Enums;
using BikeSharing.Clients.Core.Utils;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BikeSharing.Clients.Core.DataServices;
using System;
using System.Net.Http;
using System.Diagnostics;
using System.Net;
using Xamarin.Forms;
using MenuItem = BikeSharing.Clients.Core.Models.MenuItem;
using BikeSharing.Clients.Core.Models.Rides;
using BikeSharing.Clients.Core.ViewModels.Base;
using System.Linq;
using BikeSharing.Clients.Core.Helpers;
using BikeSharing.Clients.Core.Models.Users;

namespace BikeSharing.Clients.Core.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly IProfileService _profileService;
        private readonly IAuthenticationService _authenticationService;

        public ICommand ItemSelectedCommand => new Command<MenuItem>(OnSelectItem);

        public ICommand LogoutCommand => new Command(OnLogout);

        ObservableCollection<MenuItem> menuItems = new ObservableCollection<MenuItem>();

        public ObservableCollection<MenuItem> MenuItems
        {
            get
            {
                return menuItems;
            }
            set
            {
                menuItems = value;
                RaisePropertyChanged(() => MenuItems);
            }
        }

        UserProfile profile;

        public UserProfile Profile
        {
            get
            {
                return profile;
            }

            set
            {
                profile = value;
                RaisePropertyChanged(() => Profile);
                RaisePropertyChanged(() => ProfileFullName);
            }
        }

        public string ProfileFullName
        {
            get
            {
                if (Profile == null)
                {
                    return "-";
                }
                else
                {
                    return Profile.FirstName + " " + Profile.LastName;
                }
            }
        }

        public MenuViewModel(IProfileService profileService, IAuthenticationService authenticationService)
        {
            _profileService = profileService;
            _authenticationService = authenticationService;

            InitMenuItems();
            MessagingCenter.Subscribe<Booking>(this, MessengerKeys.BookingRequested, OnBookingRequested);
            MessagingCenter.Subscribe<Booking>(this, MessengerKeys.BookingFinished, OnBookingFinished);
            MessagingCenter.Subscribe<UserProfile>(this, MessengerKeys.ProfileUpdated, OnProfileUpdated);
        }

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                Profile = await _profileService.GetCurrentProfileAsync();
            }
            catch (Exception ex) when (ex is WebException || ex is HttpRequestException)
            {
                await DialogService.ShowAlertAsync("Communication error", "Error", "Ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching profile with exception: {ex}");
            }
        }

        private void InitMenuItems()
        {
            if(Device.OS == TargetPlatform.Windows)
            {
                MenuItems.Add(new MenuItem
                {
                    Title = "Home",
                    MenuItemType = MenuItemType.Home,
                    ViewModelType = typeof(MainViewModel),
                    IsEnabled = true
                });

                MenuItems.Add(new MenuItem
                {
                    Title = "New Ride",
                    MenuItemType = MenuItemType.NewRide,
                    ViewModelType = typeof(CustomRideViewModel),
                    IsEnabled = true
                });
            }

            if(Device.OS == TargetPlatform.Android)
            {
                MenuItems.Add(new MenuItem
                {
                    Title = "New Ride",
                    MenuItemType = MenuItemType.NewRide,
                    ViewModelType = typeof(CustomRideViewModel),
                    IsEnabled = true
                });
            }

            MenuItems.Add(new MenuItem
            {
                Title = "My Rides",
                MenuItemType = MenuItemType.MyRides,
                ViewModelType = Device.Idiom == TargetIdiom.Desktop ? typeof(UwpMyRidesViewModel) : typeof(MyRidesViewModel),
                IsEnabled = true
            });

            MenuItems.Add(new MenuItem
            {
                Title = "Upcoming ride",
                MenuItemType = MenuItemType.UpcomingRide,
                ViewModelType = typeof(BookingViewModel),
                IsEnabled = Settings.CurrentBookingId != 0
            });

            MenuItems.Add(new MenuItem
            {
                Title = "Report",
                MenuItemType = MenuItemType.ReportIncident,
                ViewModelType = typeof(ReportIncidentViewModel),
                IsEnabled = Settings.CurrentBookingId != 0
            });

            MenuItems.Add(new MenuItem
            {
                Title = "Profile",
                MenuItemType = MenuItemType.Profile,
                ViewModelType = typeof(ProfileViewModel),
                IsEnabled = true
            });
        }

        private async void OnSelectItem(MenuItem item)
        {
            if (item.IsEnabled)
            {
                object parameter = null;

                if (item.MenuItemType == MenuItemType.UpcomingRide)
                {
                    parameter = new BookingViewModel.BookingViewModelNavigationParameter
                    {
                        ShowThanks = false,
                        BookingId = Settings.CurrentBookingId
                    };
                }

                await NavigationService.NavigateToAsync(item.ViewModelType, parameter);
            }
        }

        private async void OnLogout()
        {
            await _authenticationService.LogoutAsync();
            await NavigationService.NavigateToAsync<LoginViewModel>();
        }

        private void OnBookingRequested(Booking booking)
        {
            SetMenuItemStatus(MenuItemType.UpcomingRide, true);
            SetMenuItemStatus(MenuItemType.ReportIncident, true);
        }

        private void OnBookingFinished(Booking booking)
        {
            SetMenuItemStatus(MenuItemType.UpcomingRide, false);
        }

        private void SetMenuItemStatus(MenuItemType type, bool enabled)
        {
            MenuItem rideItem = MenuItems.FirstOrDefault(m => m.MenuItemType == type);

            if (rideItem != null)
            {
                rideItem.IsEnabled = enabled;
            }
        }

        private async void OnProfileUpdated(UserProfile profile)
        {
            Profile = null;

            if (Device.OS == TargetPlatform.Windows)
            {
                await Task.Delay(2000); // Give UWP enough time (for Photo reload)
            }

            Profile = profile;
        }
    }
}