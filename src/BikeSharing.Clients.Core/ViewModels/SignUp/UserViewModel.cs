using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Users;
using BikeSharing.Clients.Core.Validations;
using BikeSharing.Clients.Core.ViewModels.Base;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.ViewModels.SignUp
{
    public class UserViewModel : ViewModelBase
    {
        private ValidatableObject<string> _firstName;
        private ValidatableObject<string> _lastName;
        private DateTime _birthDate;
        private bool _isValid;

        public UserViewModel()
        {
            _firstName = new ValidatableObject<string>();
            _lastName = new ValidatableObject<string>();

            BirthDate = DateTime.Today;

            AddValidations();
        }

        public ValidatableObject<string> FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                RaisePropertyChanged(() => FirstName);
            }
        }

        public ValidatableObject<string> LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                _lastName = value;
                RaisePropertyChanged(() => LastName);
            }
        }

        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }

            set
            {
                _birthDate = value;
                RaisePropertyChanged(() => BirthDate);
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
                        BirthDate = BirthDate,
                        FirstName = FirstName.Value,
                        LastName = LastName.Value
                    }
                });
            }
            else
            {
                IsValid = false;
            }

            IsBusy = false;
        }

        public bool Validate()
        {
            bool isValidFirstName = _firstName.Validate();
            bool isValidLastName = _lastName.Validate();

            return isValidFirstName && isValidLastName;
        }

        private void AddValidations()
        {
            _firstName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "First Name should not be empty" });
            _lastName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Last Name should not be empty" });
        }
    }
}