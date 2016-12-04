using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Pages
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();

            if (Device.OS != TargetPlatform.iOS)
            {
                ToolbarItems.Remove(LogoutToolbarItem);
            }
        }
    }
}