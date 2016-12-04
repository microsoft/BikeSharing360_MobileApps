using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Users;
using BikeSharing.Clients.Core.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.ViewModels.SignUp
{
    public class GenreViewModel : ViewModelBase
    {
        public enum Genre
        {
            Male,
            Female
        };

        private bool _isMale;
        private bool _isFemale;

        public GenreViewModel()
        {
            IsMale = false;
            IsFemale = false;
        }

        public bool IsMale
        {
            get { return _isMale; }
            set
            {
                _isMale = value;
                RaisePropertyChanged(() => IsMale);
            }
        }

        public bool IsFemale
        {
            get { return _isFemale; }
            set
            {
                _isFemale = value;
                RaisePropertyChanged(() => IsFemale);
            }
        }

        public ICommand MaleCommand
        {
            get { return new Command<string>((g) => SetGenre(Genre.Male)); }
        }

        public ICommand FemaleCommand
        {
            get { return new Command<string>((g) => SetGenre(Genre.Female)); }
        }

        public ICommand CloseCommand
        {
            get { return new Command(() => Close()); }
        }

        public ICommand SkipCommand
        {
            get { return new Command(() => Skip()); }
        }

        public ICommand NextCommand
        {
            get { return new Command<object>((n) => Next(n)); }
        }

        private void SetGenre(Genre genre)
        {
            if (genre.Equals(Genre.Male))
            {
                if (IsFemale)
                    IsFemale = false;
            }
            if (genre.Equals(Genre.Female))
            {                
                if (IsMale)
                    IsMale = false;
            }
        }

        private void Close()
        {
            MessagingCenter.Send(this, MessengerKeys.CloseCard);
        }

        private void Skip()
        {
            Models.SignUp nullSignUp = null;
            MessagingCenter.Send(this, MessengerKeys.NextCard, nullSignUp);
        }

        private void Next(object navigate)
        {
            MessagingCenter.Send(this, MessengerKeys.NextCard, new Models.SignUp
            {
                Navigate = bool.Parse(navigate.ToString()),
                Profile = new UserProfile
                {
                    Gender = IsMale ? 0 : 1
                }
            });
        }
    }
}