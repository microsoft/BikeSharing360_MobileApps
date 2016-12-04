using BikeSharing.Clients.Core.Models.Rides;
using BikeSharing.Clients.Core.ViewModels.Base;
using Xamarin.Forms;
using BikeSharing.Clients.Core.Animations;
using BikeSharing.Clients.Core.Extensions;

namespace BikeSharing.Clients.Core.Pages
{
    public partial class BookingPage : ContentPage
    {
        public BookingPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<Booking>(this, MessengerKeys.BookingReloadRequest, HideAndShowBookingData);
        }

        private void HideAndShowBookingData(Booking booking)
        {
            InformationPanel.Animate(new FadeToAnimation
            {
                Opacity = 0,
                Easing = EasingType.Linear,
                Duration = "500"
            }, () =>
            {
                InformationPanel.Animate(new FadeToAnimation
                {
                    Opacity = 1,
                    Easing = EasingType.Linear,
                    Duration = "500"
                });
            });
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            InformationPanel.Padding = Device.Idiom == TargetIdiom.Desktop ? new Thickness(50, 100) : new Thickness(45);
            CountdownContainer.Margin = Device.Idiom == TargetIdiom.Desktop ? new Thickness(0, 50) : new Thickness(0);
        }
    }
}
