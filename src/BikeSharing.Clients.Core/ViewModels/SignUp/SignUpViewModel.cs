using System.Windows.Input;
using System.Threading.Tasks;
using BikeSharing.Clients.Core.ViewModels.SignUp;
using Xamarin.Forms;
using System;
using BikeSharing.Clients.Core.ViewModels.Base;
using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.DataServices;
using BikeSharing.Clients.Core.Models.Users;
using System.Diagnostics;

namespace BikeSharing.Clients.Core.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private CredentialViewModel _credentialViewModel;
        private AccountViewModel _accountViewModel;
        private GenreViewModel _genreViewModel;
        private UserViewModel _userViewModel;
        private PaymentViewModel _paymentViewModel;
        private SubscriptionViewModel _subscriptionViewModel;
        private UserProfile _profile;
        private double _progress;
        private bool _isInitialized = false;

        private IProfileService _profileService;
        private IAuthenticationService _authenticationService;

        public event EventHandler OnCardChanged;

        public SignUpViewModel(
            CredentialViewModel credentialViewModel,
            AccountViewModel accountViewModel,
            GenreViewModel genreViewModel,
            UserViewModel userViewModel,
            PaymentViewModel paymentViewModel,
            SubscriptionViewModel subscriptionViewModel,
            IProfileService profileService,
            IAuthenticationService authenticationService)
        {
            _credentialViewModel = credentialViewModel;
            _accountViewModel = accountViewModel;
            _genreViewModel = genreViewModel;
            _userViewModel = userViewModel;
            _paymentViewModel = paymentViewModel;
            _subscriptionViewModel = subscriptionViewModel;

            _profileService = profileService;
            _authenticationService = authenticationService;
        }

        public CredentialViewModel CredentialViewModel
        {
            get { return _credentialViewModel; }
            set
            {
                _credentialViewModel = value;
                RaisePropertyChanged(() => CredentialViewModel);
            }
        }

        public AccountViewModel AccountViewModel
        {
            get { return _accountViewModel; }
            set
            {
                _accountViewModel = value;
                RaisePropertyChanged(() => AccountViewModel);
            }
        }

        public GenreViewModel GenreViewModel
        {
            get { return _genreViewModel; }
            set
            {
                _genreViewModel = value;
                RaisePropertyChanged(() => GenreViewModel);
            }
        }

        public UserViewModel UserViewModel
        {
            get { return _userViewModel; }
            set
            {
                _userViewModel = value;
                RaisePropertyChanged(() => UserViewModel);
            }
        }

        public PaymentViewModel PaymentViewModel
        {
            get { return _paymentViewModel; }
            set
            {
                _paymentViewModel = value;
                RaisePropertyChanged(() => PaymentViewModel);
            }
        }

        public SubscriptionViewModel SubscriptionViewModel
        {
            get { return _subscriptionViewModel; }
            set
            {
                _subscriptionViewModel = value;
                RaisePropertyChanged(() => SubscriptionViewModel);
            }
        }

        public UserProfile Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                RaisePropertyChanged(() => Profile);
            }
        }

        public double Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                RaisePropertyChanged(() => Progress);
            }
        }

        public ICommand SwipeCardCommand
        {
            get { return new Command<int>((i) => SwipeCard(i)); }
        }

        public ICommand CloseCommand
        {
            get { return new Command(() => Close()); }
        }

        public ICommand NextCommand
        {
            get { return new Command(() => Next()); }
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (!_isInitialized)
            {
                _isInitialized = true;

                Profile = new UserProfile();
                SwipeCard();
                InitializeMessaging();
            }

            return base.InitializeAsync(navigationData);
        }

        private void InitializeMessaging()
        {
            // First step
            MessagingCenter.Subscribe<CredentialViewModel>(this, MessengerKeys.CloseCard, (sender) =>
            {
                CloseCommand.Execute(null);
            });

            MessagingCenter.Subscribe<CredentialViewModel, Models.SignUp>(this, MessengerKeys.NextCard, (sender, args) =>
            {
                var signUp = args;

                if (signUp != null)
                {
                    if(Profile.User == null)
                    {
                        Profile.User = new User();
                    }

                    Profile.User.UserName = signUp.Profile.User.UserName;
                    Profile.User.Password = signUp.Profile.User.Password;

                    if (signUp.Navigate)
                        NextCommand.Execute(null);
                }
            });

            // Second step
            MessagingCenter.Subscribe<AccountViewModel>(this, MessengerKeys.CloseCard, (sender) =>
            {
                CloseCommand.Execute(null);
            });

            MessagingCenter.Subscribe<AccountViewModel, Models.SignUp>(this, MessengerKeys.NextCard, (sender, args) =>
            {
                var signUp = args;

                if (signUp != null)
                {
                    Profile.Email = signUp.Profile.Email;
                    Profile.Skype = signUp.Profile.Skype;

                    if (signUp.Navigate)
                        NextCommand.Execute(null);
                }
            });

            // Third step
            MessagingCenter.Subscribe<GenreViewModel>(GenreViewModel, MessengerKeys.CloseCard, (sender) =>
            {
                CloseCommand.Execute(null);
            });

            MessagingCenter.Subscribe<GenreViewModel, Models.SignUp>(GenreViewModel, MessengerKeys.NextCard, (sender, args) =>
            {
                var signUp = args;

                if (signUp != null)
                {
                    Profile.Gender = signUp.Profile.Gender;

                    if (signUp.Navigate)
                        NextCommand.Execute(null);
                }
            });

            // Fourth step
            MessagingCenter.Subscribe<UserViewModel>(UserViewModel, MessengerKeys.CloseCard, (sender) =>
            {
                CloseCommand.Execute(null);
            });

            MessagingCenter.Subscribe<UserViewModel, Models.SignUp>(UserViewModel, MessengerKeys.NextCard, (sender, args) =>
            {
                var signUp = args;

                if (signUp != null)
                {
                    Profile.FirstName = signUp.Profile.FirstName;
                    Profile.LastName = signUp.Profile.LastName;
                    Profile.BirthDate = signUp.Profile.BirthDate;

                    if (signUp.Navigate)
                        NextCommand.Execute(null);
                }
            });

            // Fifth step   
            MessagingCenter.Subscribe<PaymentViewModel>(PaymentViewModel, MessengerKeys.CloseCard, (sender) =>
            {
                CloseCommand.Execute(null);
            });

            MessagingCenter.Subscribe<PaymentViewModel, Models.SignUp>(PaymentViewModel, MessengerKeys.NextCard, (sender, args) =>
            {
                var signUp = args;

                if (signUp != null)
                {
                    Profile.Payment = new Payment
                    {
                        CreditCard = signUp.Profile.Payment.CreditCard,
                        CreditCardType = signUp.Profile.Payment.CreditCardType,
                        ExpirationDate = signUp.Profile.Payment.ExpirationDate
                    };

                    if (signUp.Navigate)
                        NextCommand.Execute(null);
                }
            });

            // Last step
            MessagingCenter.Subscribe<SubscriptionViewModel>(SubscriptionViewModel, MessengerKeys.CloseCard, (sender) =>
            {
                CloseCommand.Execute(null);
            });

            MessagingCenter.Subscribe<SubscriptionViewModel, Models.SignUp>(SubscriptionViewModel, MessengerKeys.LastCard, async (sender, args) =>
            {
                if (IsBusy)
                {
                    return;
                }

                try
                {
                    var signUp = args;

                    if (signUp == null)
                    {
                        return;
                    }

                    string gender = "NotSpecified";

                    if(Profile.Gender != null)
                    {
                        if (Profile.Gender == 0)
                            gender = "Male";
                        else
                            gender = "Female";
                    }

                    var userAndProfile = new UserAndProfileModel
                    {
                        UserName = Profile.User.UserName,
                        Password = Profile.User.Password,
                        Gender = gender,
                        BirthDate = Profile.BirthDate,
                        FirstName = Profile.FirstName,
                        LastName = Profile.LastName,
                        Email = Profile.Email,
                        Skype = Profile.Skype,
                        TenantId = GlobalSettings.TenantId
                    };

                    IsBusy = true;

                    UserAndProfileModel result = await _profileService.SignUp(userAndProfile);

                    if (result != null)
                    {
                        bool isAuthenticated =
                            await _authenticationService.LoginAsync(userAndProfile.UserName, userAndProfile.Password);

                        if (isAuthenticated)
                        {
                            await NavigationService.NavigateToAsync<MainViewModel>();
                        }
                        else
                        {
                            await DialogService.ShowAlertAsync("Invalid credentials", "Login failure", "Try again");
                        }
                    }
                    else
                    {
                        await DialogService.ShowAlertAsync("Invalid data", "Sign Up failure", "Try again");
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine($"Exception in sign up {ex}");
                    await DialogService.ShowAlertAsync("Invalid data", "Sign Up failure", "Try again");
                }

                IsBusy = false;
            });
        }

        private void SwipeCard(int cardIndex = 0)
        {
            switch (cardIndex)
            {
                case 0:
                    Progress = 0.16f;
                    break;
                case 1:
                    Progress = 0.32f;
                    break;
                case 2:
                    Progress = 0.48f;
                    CredentialViewModel.NextCommand.Execute(false);
                    break;
                case 3:
                    Progress = 0.64f;
                    GenreViewModel.NextCommand.Execute(false);
                    break;
                case 4:
                    Progress = 0.8f;
                    UserViewModel.NextCommand.Execute(false);
                    break;
                case 5:
                    Progress = 1.0f;
                    PaymentViewModel.NextCommand.Execute(false);
                    break;
                default:
                    Progress = 0.0f;
                    break;
            }
        }

        private async void Close()
        {
            await NavigationService.NavigateBackAsync();
        }

        private void Next()
        {
            OnCardChanged?.Invoke(this, new EventArgs());
        }

        public bool Validate(int cardIndex = 0)
        {
            switch (cardIndex)
            {
                case 0:
                    return CredentialViewModel.Validate();
                case 1:
                    return AccountViewModel.Validate();
                case 2:
                    return true;    // Optional
                case 3:
                    return UserViewModel.Validate();
                case 4:
                    return PaymentViewModel.Validate();
                case 5:
                    return false;
                default:
                    return false;
            }
        }
    }
}