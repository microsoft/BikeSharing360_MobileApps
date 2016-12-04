using BikeSharing.Clients.Core.Models.Users;
using BikeSharing.Clients.Core.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.ViewModels.SignUp
{
    public class SubscriptionViewModel : ViewModelBase
    {
        public enum Subscription
        {
            Monthly,
            ThreeMonthly,
            Annual
        };

        private bool _isMonth;
        private bool _isThreeMonthly;
        private bool _isAnnual;
        private bool _isValid;

        public bool IsMonth
        {
            get
            {
                return _isMonth;
            }
            set
            {
                _isMonth = value;
                RaisePropertyChanged(() => IsMonth);
            }
        }

        public bool IsThreeMonthly
        {
            get
            {
                return _isThreeMonthly;
            }
            set
            {
                _isThreeMonthly = value;
                RaisePropertyChanged(() => IsThreeMonthly);
            }
        }

        public bool IsAnnual
        {
            get
            {
                return _isAnnual;
            }
            set
            {
                _isAnnual = value;
                RaisePropertyChanged(() => IsAnnual);
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

        public ICommand MonthlyCommand
        {
            get { return new Command<string>((g) => SetSubscription(Subscription.Monthly)); }
        }

        public ICommand ThreeMonthlyCommand
        {
            get { return new Command<string>((g) => SetSubscription(Subscription.ThreeMonthly)); }
        }

        public ICommand AnnualCommand
        {
            get { return new Command<string>((g) => SetSubscription(Subscription.Annual)); }
        }

        public ICommand CloseCommand
        {
            get { return new Command(() => Close()); }
        }

        public ICommand NextCommand
        {
            get { return new Command<object>((n) => Next(n)); }
        }

        private void Close()
        {
            MessagingCenter.Send(this, MessengerKeys.CloseCard);
        }

        private void Next(object navigate)
        {
            IsBusy = true;
            IsValid = true;

            var isValid = Validate();

            if (isValid)
            {
                MessagingCenter.Send(this, MessengerKeys.LastCard, new Models.SignUp
                {
                    Navigate = bool.Parse(navigate.ToString()),
                    Profile = new UserProfile()
                });
            }
            else
            {
                IsValid = false;
            }

            IsBusy = false;
        }

        private void SetSubscription(Subscription subscription)
        {
            if (subscription.Equals(Subscription.Monthly))
            {
                if (IsThreeMonthly)
                    IsThreeMonthly = false;
                if (IsAnnual)
                    IsAnnual = false;
            }
            if (subscription.Equals(Subscription.ThreeMonthly))
            {
                if (IsMonth)
                    IsMonth = false;
                if (IsAnnual)
                    IsAnnual = false;
            }
            if (subscription.Equals(Subscription.Annual))
            {
                if (IsMonth)
                    IsMonth = false;
                if (IsThreeMonthly)
                    IsThreeMonthly = false;
            }

            Validate();
        }

        public bool Validate()
        {
            return IsMonth || IsThreeMonthly || IsAnnual;
        }
    }
}
