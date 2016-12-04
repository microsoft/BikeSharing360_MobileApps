using BikeSharing.Clients.Core.Utils;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Pages
{
    public partial class UwpMyRidesPage : ContentPage
    {
        private const double MinWidth = 1024;

        public UwpMyRidesPage()
        {
            InitializeComponent();

            DemoHelper.CenterMapInDefaultLocation(Map);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.SizeChanged += UwpMyRidesSizeChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            this.SizeChanged -= UwpMyRidesSizeChanged;
        }

        private void UwpMyRidesSizeChanged(object sender, System.EventArgs e)
        {
            if(Width < MinWidth)
            {
                Grid.SetColumnSpan(MyRidesList, 3);
                MyRidesMap.IsVisible = false;
                MyRidesDetail.IsVisible = false;
            }
            else
            {
                Grid.SetColumnSpan(MyRidesList, 1);
                MyRidesMap.IsVisible = true;
                MyRidesDetail.IsVisible = true;
            }
        }
    }
}
