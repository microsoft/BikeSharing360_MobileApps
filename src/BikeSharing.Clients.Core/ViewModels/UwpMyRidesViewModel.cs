using BikeSharing.Clients.Core.Controls;
using BikeSharing.Clients.Core.DataServices.Interfaces;
using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Events;
using BikeSharing.Clients.Core.Models.Rides;
using BikeSharing.Clients.Core.Utils;
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
using static BikeSharing.Clients.Core.Controls.CustomPin;

namespace BikeSharing.Clients.Core.ViewModels
{
    public class UwpMyRidesViewModel : ViewModelBase
    {
        private DateTime _currentDate;
        private string _location = "-";
        private string _temp = "-";
        private ObservableRangeCollection<Ride> _myRides;
        private Ride _selectedRide;
        private ObservableCollection<CustomPin> _customPins;
        private CustomPin _selectedPin;
        private CustomPin _from;
        private CustomPin _to;
        private Station _fromStation;
        private Station _toStation;
        private Event _event;

        private readonly IWeatherService _weatherService;
        private readonly IRidesService _ridesService;
        private readonly IEventsService _eventsService;

        public UwpMyRidesViewModel(IRidesService ridesService,
            IWeatherService weatherService,
            IEventsService eventsService)
        {
            _ridesService = ridesService;
            _weatherService = weatherService;
            _eventsService = eventsService;

            MyRides = new ObservableRangeCollection<Ride>();
        }

        public DateTime CurrentDate
        {
            get
            {
                return _currentDate;
            }

            set
            {
                _currentDate = value;
                RaisePropertyChanged(() => CurrentDate);
            }
        }

        public string Location
        {
            get
            {
                return _location;
            }

            set
            {
                _location = value;
                RaisePropertyChanged(() => Location);
            }
        }

        public string Temp
        {
            get
            {
                return _temp;
            }

            set
            {
                _temp = value;
                RaisePropertyChanged(() => Temp);
            }
        }

        public ObservableRangeCollection<Ride> MyRides
        {
            get
            {
                return _myRides;
            }
            set
            {
                _myRides = value;
                RaisePropertyChanged(() => MyRides);
            }
        }

        public Ride SelectedRide
        {
            get
            {
                return _selectedRide;
            }
            set
            {
                _selectedRide = value;

                if(_selectedRide != null)
                {
                    LoadRideDetails(_selectedRide);
                }

                RaisePropertyChanged(() => SelectedRide);
            }
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
                RaisePropertyChanged(() => SelectedPin);
            }
        }

        public CustomPin From
        {
            get { return _from; }
            set
            {
                _from = value;

                if (_from != null)
                {
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
                    ToStation = SetStation(_to);
                }

                RaisePropertyChanged(() => To);
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
            }
        }

        public Event Event
        {
            get
            {
                return _event;
            }

            set
            {
                _event = value;
                RaisePropertyChanged(() => Event);
            }
        }

        public ICommand MakeBookingCommand => new Command(MakeBookingAsync);

        public override async Task InitializeAsync(object navigationData)
        {
            CurrentDate = DateTime.Now;
            IsBusy = true;

            try
            {
                var weather = await _weatherService.GetWeatherInfoAsync();

                if (weather is WeatherInfo)
                {
                    var weatherInfo = weather as WeatherInfo;
                    Location = weatherInfo.LocationName;
                    Temp = Math.Round(weatherInfo.Temp).ToString();
                }

                var ridesResult = await _ridesService.GetUserRides();
                MyRides = new ObservableRangeCollection<Ride>(ridesResult);
                SelectedRide = MyRides.FirstOrDefault();
            }
            catch (Exception ex) when (ex is WebException || ex is HttpRequestException)
            {
                await DialogService.ShowAlertAsync("Communication error", "Error", "Ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in: {ex}");
            }

            IsBusy = false;
        }

        private async void LoadRideDetails(Ride selectedRide)
        {
            if (selectedRide != null)
            {
                var tempCustomPins = new List<CustomPin>();
                var fromPin = new CustomPin
                {
                    Id = 1,
                    Type = AnnotationType.From,
                    Label = selectedRide.FromStation.Name,
                    Address = string.Format("{0}, {1}", selectedRide.FromStation.Latitude, selectedRide.FromStation.Longitude),
                    Position = new Xamarin.Forms.Maps.Position(selectedRide.FromStation.Latitude, selectedRide.FromStation.Longitude)
                };

                tempCustomPins.Add(fromPin);

                var toPin = new CustomPin
                {
                    Id = 2,
                    Type = AnnotationType.To,
                    Label = selectedRide.ToStation.Name,
                    Address = string.Format("{0}, {1}", selectedRide.ToStation.Latitude, selectedRide.ToStation.Longitude),
                    Position = new Xamarin.Forms.Maps.Position(selectedRide.ToStation.Latitude, selectedRide.ToStation.Longitude)
                };

                tempCustomPins.Add(toPin);

                CustomPins = new ObservableCollection<CustomPin>(tempCustomPins);
                From = fromPin;
                To = toPin;

                if (selectedRide.EventId.HasValue)
                {
                    Event = await _eventsService.GetEventById(selectedRide.EventId.Value);
                }
                else
                {
                    Event = null;
                }
            }

            if(MyRides.Any())
            {
                selectedRide.IsSelected = true;

                foreach (var ride in _myRides)
                {
                    ride.IsSelected = false;

                    if (ride.Equals(selectedRide))
                        ride.IsSelected = true;
                }
            }
        }

        private Station SetStation(CustomPin customPin)
        {
            if(SelectedRide == null)
            {
                return null;
            }

            if(customPin.Type.Equals(AnnotationType.From))
                return SelectedRide.FromStation;

            if (customPin.Type.Equals(AnnotationType.To))
                return SelectedRide.ToStation;

            return null;
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
    }
}