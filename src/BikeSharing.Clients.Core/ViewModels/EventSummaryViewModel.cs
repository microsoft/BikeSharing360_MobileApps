using BikeSharing.Clients.Core.DataServices;
using BikeSharing.Clients.Core.DataServices.Interfaces;
using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Events;
using BikeSharing.Clients.Core.Models.Rides;
using BikeSharing.Clients.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.ViewModels
{
    public class EventSummaryViewModel : ViewModelBase
    {
        private readonly IEventsService _eventsService;
        private readonly IRidesService _ridesService;

        private Event _event;

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

        private Station _fromStation;

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

        private Station _toStation;

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

        public ICommand MakeBookingCommand => new Command(MakeBookingAsync);

        public EventSummaryViewModel(IEventsService eventsService, IRidesService ridesService)
        {
            _eventsService = eventsService;
            _ridesService = ridesService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            try
            {
                Event = navigationData as Event;

                var toGeoLocation = new GeoLocation
                {
                    Latitude = Event.Venue.Latitude,
                    Longitude = Event.Venue.Longitude
                };

                var fromStationTask = _ridesService.GetInfoForNearestStation();
                var toStationTask = _ridesService.GetInfoForNearestStationTo(toGeoLocation);

                var tasks = new List<Task> { fromStationTask, toStationTask };
                while (tasks.Count > 0)
                {
                    var finishedTask = await Task.WhenAny(tasks);
                    tasks.Remove(finishedTask);

                    if (finishedTask.Status == TaskStatus.RanToCompletion)
                    {
                        if (finishedTask == fromStationTask)
                        {
                            var fromStationResult = await fromStationTask;
                            if (fromStationResult is Station)
                            {
                                FromStation = fromStationResult as Station;
                            }
                        }
                        else if (finishedTask == toStationTask)
                        {
                            var toStationResult = await toStationTask;
                            if (toStationResult is Station)
                            {
                                ToStation = toStationResult as Station;
                            }
                        }
                    }
                }
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

        private BookingViewModel.BookingViewModelNavigationParameter _navParameter;

        private async void MakeBookingAsync(object obj)
        {
            IsBusy = true;

            try
            {
                Booking booking = await _ridesService.RequestBikeBooking(FromStation, ToStation, Event);
                MessagingCenter.Send(booking, MessengerKeys.BookingRequested);

                _navParameter = new BookingViewModel.BookingViewModelNavigationParameter
                {
                    ShowThanks = true,
                    BookingId = booking.Id
                };

                await NavigationService.NavigateToAsync<BookingViewModel>(_navParameter);
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
    }
}
