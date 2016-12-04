using BikeSharing.Clients.Core.Animations;
using BikeSharing.Clients.Core.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BikeSharing.Clients.Core.Pages
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class PaymentPage : ContentView
    {
        public PaymentPage()
        {
            InitializeComponent();

            PaymentImage.IsVisible = Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.iOS;

            var cloudCard1Animation = this.Resources["CloudCard1Animation"] as StoryBoard;

            if (cloudCard1Animation != null)
            {
                CloudCard1.Animate(cloudCard1Animation);
            }

            var cloudCard2Animation = this.Resources["CloudCard2Animation"] as StoryBoard;

            if (cloudCard2Animation != null)
            {
                CloudCard2.Animate(cloudCard2Animation);
            }
        }
    }
}
