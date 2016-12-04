using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BikeSharing.Clients.Core.Pages
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            //LogoutButton.Margin = Device.Idiom == TargetIdiom.Desktop ? new Thickness(14, 0, 0, 0) : new Thickness(14, 0, 0, 14);
        }
    }
}
