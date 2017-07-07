using BikeSharing.Clients.Core.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BikeSharing.Clients.Core.Pages
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();

			var osVersionString = ViewModelLocator.Instance.Resolve<IOperatingSystemVersionProvider>()
                .GetOperatingSystemVersionString();

			if (Device.OS == TargetPlatform.iOS && osVersionString == "10.0.2")
			{
				SignInButton.BackgroundColor = Color.Black;
			}
		}
    }
}
