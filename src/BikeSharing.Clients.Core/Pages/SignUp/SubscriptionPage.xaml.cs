using BikeSharing.Clients.Core.Animations;
using BikeSharing.Clients.Core.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BikeSharing.Clients.Core.Pages.SignUp
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class SubscriptionPage : ContentView
    {
        public SubscriptionPage()
        {
            InitializeComponent();

            var sunAnimation = this.Resources["SunAnimation"] as RotateToAnimation;

            if (sunAnimation != null && Device.OS != TargetPlatform.Windows)
            {
                Sun.Animate(sunAnimation);
            }

            var cloudAnimation = this.Resources["CloudAnimation"] as StoryBoard;

            if (cloudAnimation != null)
            {
                Cloud.Animate(cloudAnimation);
            }

            var boyAnimation = this.Resources["BoyAnimation"] as StoryBoard;

            if (boyAnimation != null)
            {
                Boy.Animate(boyAnimation);
            }
        }
    }
}