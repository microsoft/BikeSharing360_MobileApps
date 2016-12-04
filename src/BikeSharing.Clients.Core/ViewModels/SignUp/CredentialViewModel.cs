using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Users;
using BikeSharing.Clients.Core.Validations;
using BikeSharing.Clients.Core.ViewModels.Base;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.ViewModels.SignUp
{
    public class CredentialViewModel : ViewModelBase
    {
        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _password;
        private ValidatableObject<string> _repeatPassword;
        private bool _isValid;

        public CredentialViewModel()
        {
            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            _repeatPassword = new ValidatableObject<string>();

            AddValidations();
        }

        public ValidatableObject<string> UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public ValidatableObject<string> Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public ValidatableObject<string> RepeatPassword
        {
            get
            {
                return _repeatPassword;
            }

            set
            {
                _repeatPassword = value;
                RaisePropertyChanged(() => RepeatPassword);
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
            get { return new Command(() => ValidatePassword()); }
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
                        User = new User
                        {
                            UserName = UserName.Value,
                            Password = Password.Value
                        }
                    }
                });
            }
            else
            {
                IsValid = false;
            }

            IsBusy = false;
        }

        private void Close()
        {
            MessagingCenter.Send(this, MessengerKeys.CloseCard);
        }

        public bool Validate()
        {
            AddDynamicValidations();

            bool isValidUsername = _userName.Validate();
            bool isValidPassword = _password.Validate();
            bool isValidRepeatedPassword = _repeatPassword.Validate();

            return isValidUsername && isValidPassword && isValidRepeatedPassword;
        }

        private bool ValidatePassword()
        {
            AddDynamicValidations();

            bool isValidPassword = _password.Validate();
            bool isValidRepeatedPassword = _repeatPassword.Validate();

            return isValidPassword && isValidRepeatedPassword;
        }

        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Username should not be empty" });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password should not be empty" });
            _repeatPassword.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Repeat password should not be empty" });
        }

        private void AddDynamicValidations()
        {
            if (_repeatPassword.Validations
                .Any(v => v.GetType().Equals(typeof(RepeatPasswordRule<string>))))
            {
                var validation = _repeatPassword.Validations.First(v => v.GetType().Equals(typeof(RepeatPasswordRule<string>)));
                _repeatPassword.Validations.Remove(validation);
            }

            _repeatPassword.Validations.Add(new RepeatPasswordRule<string> { ValidationMessage = "The passwords do not match", Password = Password.Value });
        }
    }
}
