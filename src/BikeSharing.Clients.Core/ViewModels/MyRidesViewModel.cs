using BikeSharing.Clients.Core.DataServices.Interfaces;
using BikeSharing.Clients.Core.Models.Rides;
using BikeSharing.Clients.Core.Utils;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.ViewModels
{
    public class MyRidesViewModel : ViewModelBase
    {
        private ObservableRangeCollection<Ride> _myRides;
        private readonly IRidesService _ridesService;

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

        public ICommand RefreshCommand => new Command(RefreshRidesCommand);

        public MyRidesViewModel(IRidesService ridesService)
        {
            _ridesService = ridesService;

            _myRides = new ObservableRangeCollection<Ride>();
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await LoadData();
        }

        private async void RefreshRidesCommand(object obj)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            IsBusy = true;

            try
            {
                var ridesResult = await _ridesService.GetUserRides();
                MyRides = new ObservableRangeCollection<Ride>(ridesResult);
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
    }
}