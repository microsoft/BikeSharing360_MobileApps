using BikeSharing.Clients.Core.Utils;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Pages
{
    public partial class HomePage : ContentPage
    {
        private const int ScrollMinLimit = 0;
        private const int ScrollMaxLimit = 190;

        public HomePage()
        {
            InitializeComponent();

            this.mainScrollView.Scrolled += ScrollView_Scrolled;

            if (Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.Windows)
            {
                ToolbarItems.Remove(ShowCustomRideToolbarItem);
            }
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            var val = MathHelper.ReMap(e.ScrollY, ScrollMinLimit, ScrollMaxLimit, 1, 0);

            this.infoPanel.Scale = val;
            this.infoPanel.Opacity = val;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            SuggestionsList.ListOrientation = Device.Idiom == TargetIdiom.Phone ? StackOrientation.Vertical : StackOrientation.Horizontal;
        }
    }
}
