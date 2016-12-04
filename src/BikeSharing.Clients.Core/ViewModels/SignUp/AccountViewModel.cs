using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Users;
using BikeSharing.Clients.Core.Validations;
using BikeSharing.Clients.Core.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.ViewModels.SignUp
{
    public class AccountViewModel : ViewModelBase
    {
        private ValidatableObject<string> _email;
        private ValidatableObject<string> _skype;
        private bool _isValid;

        public AccountViewModel()
        {
            _email = new ValidatableObject<string>();
            _skype = new ValidatableObject<string>();

            AddValidations();
        }

        public ValidatableObject<string> Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        public ValidatableObject<string> Skype
        {
            get
            {
                return _skype;
            }
            set
            {
                _skype = value;
                RaisePropertyChanged(() => Skype);
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }

            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        public ICommand CloseCommand
        {
            get { return new Command(() => Close()); }
        }

        public ICommand NextCommand
        {
            get { return new Command<object>((n) => Next(n)); }
        }

        public ICommand ValidateCommand
        {
            get { return new Command(() => Validate()); }
        }

        public bool Validate()
        {
            bool isValidEmail = _email.Validate();
            bool isValidSkype = _skype.Validate();

            return isValidEmail && isValidSkype;
        }

        private void AddValidations()
        {
            _email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Email should not be empty" });
            _email.Validations.Add(new EmailRule<string> { ValidationMessage = "Invalid Email" });
            _skype.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Skype Account should not be empty" });
        }

        private void Close()
        {
            MessagingCenter.Send(this, MessengerKeys.CloseCard);
        }

        private void Next(object navigate)
        {
            IsBusy = true;
            IsValid = true;
            bool isValid = Validate();

            if (isValid)
            {
                MessagingCenter.Send(this, MessengerKeys.NextCard, new Models.SignUp
                {
                    Navigate = bool.Parse(navigate.ToString()),
                    Profile = new UserProfile
                    {
                        Email = Email.Value,
                        Skype = Skype.Value
                    }
                });
            }
            else
            {
                IsValid = false;
            }

            IsBusy = false;
        }
    }
}
