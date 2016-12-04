using BikeSharing.Clients.Core.DataServices;
using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Users;
using BikeSharing.Clients.Core.Services.Interfaces;
using BikeSharing.Clients.Core.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private UserProfile _profile;

        private readonly IProfileService _profileService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMediaPickerService _mediaPickerService;

        public UserProfile Profile
        {
            get
            {
                return _profile;
            }

            set
            {
                _profile = value;
                RaisePropertyChanged(() => Profile);
            }
        }
        
        public ICommand LogoutCommand => new Command(OnLogout);

        public ICommand UpdatePhotoCommand => new Command(OnUpdatePhoto);

        public ProfileViewModel(IProfileService profileService, IAuthenticationService authenticationService, IMediaPickerService mediaPickerService)
        {
            _profileService = profileService;
            _authenticationService = authenticationService;
            _mediaPickerService = mediaPickerService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            try
            {
                var profile = await _profileService.GetCurrentProfileAsync();

                if (!string.IsNullOrEmpty(profile.PhotoUrl))
                {
                    // Force photo reload
                    profile.PhotoUrl += $"?t={DateTime.Now.Ticks}";
                }

                Profile = profile;
                MessagingCenter.Send(Profile, MessengerKeys.ProfileUpdated);
            }
            catch (Exception ex) when (ex is WebException || ex is HttpRequestException)
            {
                await DialogService.ShowAlertAsync("Communication error", "Error", "Ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching profile with exception: {ex}");
            }

            IsBusy = false;
        }

        private async void OnLogout(object obj)
        {
            await _authenticationService.LogoutAsync();
            await NavigationService.NavigateToAsync<LoginViewModel>();
        }

        private async void OnUpdatePhoto(object obj)
        {
            IsBusy = true;

            try
            {
                string imageAsBase64 = await _mediaPickerService.PickImageAsBase64String();
                await _profileService.UploadUserImageAsync(Profile, imageAsBase64);

                Profile = null;
                await InitializeAsync(null);
            }
            catch (Exception ex) when (ex is WebException || ex is HttpRequestException)
            {
                await DialogService.ShowAlertAsync("Communication error", "Error", "Ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error uploading profile image with exception: {ex}");
            }

            IsBusy = false;
        }
    }
}
