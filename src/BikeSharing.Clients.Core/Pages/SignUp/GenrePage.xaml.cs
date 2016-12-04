using BikeSharing.Clients.Core.Animations;
using BikeSharing.Clients.Core.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BikeSharing.Clients.Core.Pages
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class GenrePage : ContentView
    {
        public GenrePage()
        {
            InitializeComponent();

            var sunAnimation = this.Resources["SunAnimation"] as RotateToAnimation;

            if (sunAnimation != null && Device.OS != TargetPlatform.Windows)
            {
                Sun.Animate(sunAnimation);
            }

            var cloud1Animation = this.Resources["Cloud1Animation"] as StoryBoard;

            if (cloud1Animation != null)
            {
                Cloud1.Animate(cloud1Animation);
            }

            var cloud2Animation = this.Resources["Cloud2Animation"] as StoryBoard;

            if (cloud2Animation != null)
            {
                Cloud2.Animate(cloud2Animation);
            }
        }
    }
}