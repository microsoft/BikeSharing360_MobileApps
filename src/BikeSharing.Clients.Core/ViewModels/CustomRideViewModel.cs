using BikeSharing.Clients.Core.Controls;
using BikeSharing.Clients.Core.DataServices;
using BikeSharing.Clients.Core.DataServices.Interfaces;
using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Rides;
using BikeSharing.Clients.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.ViewModels
{
    public class CustomRideViewModel : ViewModelBase
    {
        private IEnumerable<Station> _stations;
        private ObservableCollection<CustomPin> _customPins;
        private CustomPin _selectedPin;
        private CustomPin _from;
        private CustomPin _to;
        private Station _selectedStation;
        private Station _fromStation;
        private Station _toStation;
        private bool _showInfo;

        private IRidesService _ridesService;

        public CustomRideViewModel(IRidesService ridesService)
        {
            _ridesService = ridesService;

            CustomPins = new ObservableCollection<CustomPin>();
        }

        public ObservableCollection<CustomPin> CustomPins
        {
            get { return _customPins; }
            set
            {
                _customPins = value;
                RaisePropertyChanged(() => CustomPins);
            }
        }

        public CustomPin SelectedPin
        {
            get { return _selectedPin; }
            set
            {
                _selectedPin = value;

                if (_selectedPin != null)
                {
                    SelectedStation = SetStation(_selectedPin);
                }

                RaisePropertyChanged(() => SelectedPin);
                RaisePropertyChanged(() => HasRoute);
            }
        }

        public bool HasRoute
        {
            get { return From != null || To != null; }
        }

        public CustomPin From
        {
            get { return _from; }
            set
            {
                _from = value;

                if (_from != null)
                {
                    _from.Type = CustomPin.AnnotationType.From;
                    FromStation = SetStation(_from);
                }

                RaisePropertyChanged(() => From);
            }
        }

        public CustomPin To
        {
            get { return _to; }
            set
            {
                _to = value;

                if (_to != null)
                {
                    _to.Type = CustomPin.AnnotationType.To;
                    ToStation = SetStation(_to);
                }

                RaisePropertyChanged(() => To);
            }
        }

        public bool ShowInfo
        {
            get { return _showInfo; }
            set
            {
                _showInfo = value;
                RaisePropertyChanged(() => ShowInfo);
            }
        }

        public Station SelectedStation
        {
            get
            {
                return _selectedStation;
            }

            set
            {
                _selectedStation = value;
                RaisePropertyChanged(() => SelectedStation);
            }
        }

        public Station FromStation
        {
            get
            {
                return _fromStation;
            }

            set
            {
                _fromStation = value;
                RaisePropertyChanged(() => FromStation);
                RaisePropertyChanged(() => HasRoute);
            }
        }

        public Station ToStation
        {
            get
            {
                return _toStation;
            }

            set
            {
                _toStation = value;
                RaisePropertyChanged(() => ToStation);
                RaisePropertyChanged(() => HasRoute);
            }
        }

        public ICommand GoCommand => new Command(Go);

        public ICommand MakeBookingCommand => new Command(MakeBookingAsync);

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            if (navigationData is NavigationParameter)
            {
                await EstablishFromToStationsFromNavigation(navigationData as NavigationParameter);
            }
            else
            {
                await LoadData(navigationData);
            }

            IsBusy = false;
        }

        private async Task LoadData(object navigationData)
        {
            try
            {
                _stations = await _ridesService.GetNearestStations();
                InitializePinsFromStations(_stations);
            }
            catch (Exception ex) when (ex is WebException || ex is HttpRequestException)
            {
                await DialogService.ShowAlertAsync("Communication error", "Error", "Ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading data in: {ex}");
            }
        }

        private CustomPin SetCustomPin(string name)
        {
            var customPinNameIndex = name.IndexOf("(");
            var customPinName = name.Substring(0, customPinNameIndex - 1);
            return CustomPins.FirstOrDefault(s => s.Label == customPinName);
        }

        private Station SetStation(CustomPin customPin)
        {
            if (SelectedPin != customPin)
            {
                SelectedPin = customPin;
            }

            return _stations
                .FirstOrDefault(s => s.Latitude == customPin.Position.Latitude &&
                s.Longitude == customPin.Position.Longitude);
        }

        private async void Go()
        {
            if (From == null || To == null)
            {
                return;
            }

            await Task.Delay(500);

            ShowInfo = true;
            SelectedPin = null;
        }

        private async void MakeBookingAsync(object obj)
        {
            IsBusy = true;

            try
            {
                Booking booking = await _ridesService.RequestBikeBooking(FromStation, ToStation);
                MessagingCenter.Send(booking, MessengerKeys.BookingRequested);

                var navParameter = new BookingViewModel.BookingViewModelNavigationParameter
                {
                    ShowThanks = true,
                    BookingId = booking.Id
                };

                await NavigationService.NavigateToAsync<BookingViewModel>(navParameter);
                await NavigationService.RemoveLastFromBackStackAsync();
            }
            catch (NoAvailableBikesException)
            {
                await DialogService.ShowAlertAsync("We are sorry, there are no bikes in origin station", "No bike available", "Ok");
            }
            catch (Exception ex) when (ex is WebException || ex is HttpRequestException)
            {
                await DialogService.ShowAlertAsync("Communication error", "Error", "Ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading data in: {ex}");
            }

            IsBusy = false;
        }

        private async Task EstablishFromToStationsFromNavigation(NavigationParameter parameter)
        {
            try
            {
                var nearestFrom = await _ridesService.GetInfoForNearestStation();
                var toGeoLocation = new GeoLocation
                {
                    Latitude = parameter.Latitude,
                    Longitude = parameter.Longitude
                };

                var nearestTo = await _ridesService.GetInfoForNearestStationTo(toGeoLocation);
                _stations = new[] { nearestFrom, nearestTo };

                InitializePinsFromStations(_stations);

                From = CustomPins[0];
                To = CustomPins[1];
            }
            catch (Exception ex) when (ex is WebException || ex is HttpRequestException)
            {
                await DialogService.ShowAlertAsync("Communication error", "Error", "Ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading data in: {ex}");
            }
        }

        private void InitializePinsFromStations(IEnumerable<Station> allStations)
        {
            if (allStations != null)
            {
                var tempStations = new ObservableCollection<CustomPin>();

                int counter = 1;
                foreach (var station in allStations)
                {
                    tempStations.Add(new CustomPin
                    {
                        Id = counter,
                        PinIcon = "pushpin",
                        Label = station.Name,
                        Address = string.Format("{0}, {1}", station.Latitude, station.Longitude),
                        Position = new Xamarin.Forms.Maps.Position(station.Latitude, station.Longitude)
                    });

                    counter++;
                }

                CustomPins = new ObservableCollection<CustomPin>(tempStations);
            }
        }

        public class NavigationParameter
        {
            public float Latitude { get; set; }

            public float Longitude { get; set; }
        }
    }
}