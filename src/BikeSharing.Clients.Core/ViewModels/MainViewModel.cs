using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private MenuViewModel _menuViewModel;

        public MenuViewModel MenuViewModel
        {
            get
            {
                return _menuViewModel;
            }

            set
            {
                _menuViewModel = value;
                RaisePropertyChanged(() => MenuViewModel);
            }
        }

        public MainViewModel(MenuViewModel menuViewModel)
        {
            _menuViewModel = menuViewModel;
        }

        public override Task InitializeAsync(object navigationData)
        {
            return Task.WhenAll
                (
                    _menuViewModel.InitializeAsync(navigationData),
                    NavigationService.NavigateToAsync<HomeViewModel>()
                );
        }
    }
}