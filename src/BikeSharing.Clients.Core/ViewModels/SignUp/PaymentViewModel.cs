using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Users;
using BikeSharing.Clients.Core.Services.Interfaces;
using BikeSharing.Clients.Core.Validations;
using BikeSharing.Clients.Core.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.ViewModels.SignUp
{
    public class PaymentViewModel : ViewModelBase
    {
        private const int Year = 2000;

        private ObservableCollection<string> _creditCardTypes;
        private string _creditCardType;
        private ValidatableObject<string> _creditCardNumber;
        private ValidatableObject<string> _creditCardMonth;
        private ValidatableObject<string> _creditCardYear;
        private bool _isValid;

        private ICreditCardScannerService _creditCardScannerService;

        public PaymentViewModel(ICreditCardScannerService creditCardScannerService)
        {
            _creditCardScannerService = creditCardScannerService;

            _creditCardNumber = new ValidatableObject<string>();
            _creditCardMonth = new ValidatableObject<string>();
            _creditCardYear = new ValidatableObject<string>();

            LoadCreditCardTypes();
            AddValidations();
            InitializeMessaging();
        }

        public ObservableCollection<string> CreditCardTypes
        {
            get { return _creditCardTypes; }
            set
            {
                _creditCardTypes = value;
                RaisePropertyChanged(() => CreditCardTypes);
            }
        }

        public string CreditCardType
        {
            get { return _creditCardType; }
            set
            {
                _creditCardType = value;
                RaisePropertyChanged(() => CreditCardType);
            }
        }

        public ValidatableObject<string> CreditCardNumber
        {
            get
            {
                return _creditCardNumber;
            }
            set
            {
                _creditCardNumber = value;
                RaisePropertyChanged(() => CreditCardNumber);
            }
        }

        public ValidatableObject<string> CreditCardMonth
        {
            get
            {
                return _creditCardMonth;
            }
            set
            {
                _creditCardMonth = value;
                RaisePropertyChanged(() => CreditCardMonth);
            }
        }

        public ValidatableObject<string> CreditCardYear
        {
            get
            {
                return _creditCardYear;
            }
            set
            {
                _creditCardYear = value;
                RaisePropertyChanged(() => CreditCardYear);
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

        public ICommand ScanCreditCardCommand
        {
            get { return new Command(() => ScanCreditCard()); }
        }

        public ICommand ValidateCommand
        {
            get { return new Command(() => Validate()); }
        }

        private void InitializeMessaging()
        {
            // First step
            MessagingCenter.Subscribe<CreditCardInformation>(this, MessengerKeys.CreditCardScanned, async (sender) =>
            {
                var creditCardInformation = sender;

                if (creditCardInformation != null)
                {
                    CreditCardNumber.Value = creditCardInformation.CardNumber;
                    CreditCardMonth.Value = creditCardInformation.ExpirationMonth;
                    CreditCardYear.Value = creditCardInformation.ExpirationYear.Substring(2);
                }
                else
                {
                    await DialogService.ShowAlertAsync("An error ocurred reading your credit card", "Error", "Ok");
                }
            });
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
                        Payment = new Payment
                        {
                            CreditCard = CreditCardNumber.Value,
                            CreditCardType = 0,
                            ExpirationDate = new DateTime(
                            Year + Convert.ToInt32(CreditCardYear.Value),
                            Convert.ToInt32(CreditCardMonth.Value),
                            1)
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

        private void LoadCreditCardTypes()
        {
            CreditCardTypes = new ObservableCollection<string>
            {
                "None",
                "Amex",
                "Visa",
                "Masterdcard"
            };

            CreditCardType = CreditCardTypes.First();
        }

        public bool Validate()
        {
            bool isValidCreditCardNumber = _creditCardNumber.Validate();
            bool isValidCreditCardMonth = _creditCardMonth.Validate();
            bool isCreditCardYear = _creditCardYear.Validate();

            return isValidCreditCardNumber && isValidCreditCardMonth && isCreditCardYear;
        }

        private void AddValidations()
        {
            _creditCardNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Credit Card number should not be empty" });
            _creditCardMonth.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Month should not be empty" });
            _creditCardMonth.Validations.Add(new MonthRule<string> { ValidationMessage = "Month is not valid" });
            _creditCardYear.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Year should not be empty" });
            _creditCardMonth.Validations.Add(new YearRule<string> { ValidationMessage = "Year is not valid" });
        }

        private void ScanCreditCard()
        {
            _creditCardScannerService.StartScanning();
        }
    }
}
