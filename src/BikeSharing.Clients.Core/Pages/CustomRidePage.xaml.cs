using System;
using BikeSharing.Clients.Core.Animations;
using BikeSharing.Clients.Core.Extensions;
using BikeSharing.Clients.Core.Utils;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Pages
{
    public partial class CustomRidePage : ContentPage
    {
        public CustomRidePage()
        {
            InitializeComponent();
            InitializePlatformSpecifics();

            DemoHelper.CenterMapInDefaultLocation(Map);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

			if (Device.OS != TargetPlatform.iOS)
			{
            	RouteSelector.IsVisible = true;
				RouteSelector.Animate(new FadeInAnimation
				{
					Direction = FadeInAnimation.FadeDirection.Up,
					Easing = EasingType.Linear,
					Duration = "1000"
				}, () =>
				{
					if (Device.OS == TargetPlatform.Android)
					{

						AndroidGoImage.Animate(new FadeToAnimation
						{
							Delay = 1000,
							Opacity = 1,
							Duration = "500"
						});
					}
				});
			}

            InfoPanel.IsVisible = true;
        }

        private void Go(object sender, System.EventArgs e)
        {
            RideSummary.Animate(new FadeInAnimation
            {
                Direction = FadeInAnimation.FadeDirection.Up,
                Easing = EasingType.Linear,
                Duration = "1000"
            });
        }

        private void InitializePlatformSpecifics()
        {
			RouteSelector.IsVisible = Device.OS == TargetPlatform.iOS;

            if (Device.Idiom == TargetIdiom.Desktop && 
                Device.OS == TargetPlatform.Windows)
            {
                AbsoluteLayout.SetLayoutFlags(RouteSelector, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(RouteSelector, new Rectangle(0.0, 0.0, 400, 250));

                AbsoluteLayout.SetLayoutFlags(InfoPanel, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(InfoPanel, new Rectangle(0.0, 1.0, 400, AbsoluteLayout.AutoSize));
            }

			if (Device.OS == TargetPlatform.iOS)
			{
				AbsoluteLayout.SetLayoutBounds(RouteSelector, new Rectangle(0.1, 0, 1.0, 160));
				GoButtonContainer.TranslationY = 20;
			}

            // Depending on target device, we show more or less station controls
            if (Device.Idiom == TargetIdiom.Desktop)
            {
                InfoPanelContent.Children.RemoveAt(0);
            }
            else
            {
                InfoPanelContent.Children.RemoveAt(1);
                InfoPanelContent.Children.RemoveAt(1);
            }
        }
    }
}
