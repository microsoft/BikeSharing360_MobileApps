using BikeSharing.Clients.Core.DataServices.Interfaces;
using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Events;
using BikeSharing.Clients.Core.Models.Rides;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IWeatherService _weatherService;
        private readonly IEventsService _eventsService;
        private readonly IRidesService _ridesService;

        DateTime currentDate;

        public DateTime CurrentDate
        {
            get
            {
                return currentDate;
            }

            set
            {
                currentDate = value;
                RaisePropertyChanged(() => CurrentDate);
            }
        }

        string location = "-";

        public string Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
                RaisePropertyChanged(() => Location);
            }
        }

        string temp = "-";

        public string Temp
        {
            get
            {
                return temp;
            }

            set
            {
                temp = value;
                RaisePropertyChanged(() => Temp);
            }
        }

        private ObservableCollection<Event> _events = new ObservableCollection<Event>();

        public ObservableCollection<Event> Events
        {
            get
            {
                return _events;
            }

            set
            {
                _events = value;
                RaisePropertyChanged(() => Events);
            }
        }

        private ObservableCollection<Suggestion> _suggestions = new ObservableCollection<Suggestion>();

        public ObservableCollection<Suggestion> Suggestions
        {
            get
            {
                return _suggestions;
            }

            set
            {
                _suggestions = value;
                RaisePropertyChanged(() => Suggestions);
            }
        }

        public ICommand ShowEventCommand => new Command<Event>(ShowEventAsync);

        public ICommand ShowCustomRideCommand => new Command(CustomRideAsync);

        public ICommand ShowRecommendedRideCommand => new Command<Suggestion>(RecommendedRideAsync);

        public HomeViewModel(IWeatherService weatherService, IEventsService eventsService, IRidesService ridesService)
        {
            _weatherService = weatherService;
            _eventsService = eventsService;
            _ridesService = ridesService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            CurrentDate = GlobalSettings.EventDate;
            IsBusy = true;

            try
            {
                var weather = _weatherService.GetDemoWeatherInfoAsync();
                var events = _eventsService.GetEvents();
                var suggestions = _ridesService.GetSuggestions();

                var tasks = new List<Task> { weather, events, suggestions };
                while (tasks.Count > 0)
                {
                    var finishedTask = await Task.WhenAny(tasks);
                    tasks.Remove(finishedTask);

                    if (finishedTask.Status == TaskStatus.RanToCompletion)
                    {
                        if (finishedTask == weather)
                        {
                            var weatherResults = await weather;
                            if (weatherResults is WeatherInfo)
                            {
                                var weatherInfo = weatherResults as WeatherInfo;
                                Location = weatherInfo.LocationName;
                                Temp = Math.Round(weatherInfo.Temp).ToString();
                            }
                        }
                        else if (finishedTask == events)
                        {
                            var eventsResults = await events;
                            Events = new ObservableCollection<Event>(eventsResults);
                        }
                        else if (finishedTask == suggestions)
                        {
                            var suggestionsResults = await suggestions;
                            Suggestions = new ObservableCollection<Suggestion>(suggestionsResults);
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

        private async void ShowEventAsync(Event @event)
        {
            if (@event != null)
            {
                await NavigationService.NavigateToAsync<EventSummaryViewModel>(@event);
            }
        }

        private async void CustomRideAsync()
        {
            await NavigationService.NavigateToAsync<CustomRideViewModel>();
        }

        private async void RecommendedRideAsync(object obj)
        {
            var suggestion = obj as Suggestion;

            if (suggestion != null)
            {
                var parameters = new CustomRideViewModel.NavigationParameter
                {
                    Latitude = suggestion.Latitude,
                    Longitude = suggestion.Longitude
                };

                await NavigationService.NavigateToAsync<CustomRideViewModel>(parameters);
            }
        }
    }
}
