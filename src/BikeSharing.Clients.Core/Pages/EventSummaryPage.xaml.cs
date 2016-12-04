using System;
using BikeSharing.Clients.Core.Models.Events;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Pages
{
    public partial class EventSummaryPage : ContentPage, IPageWithParameters
    {
        public EventSummaryPage()
        {
            InitializeComponent();
        }

		public void InitializeWith(object parameter)
		{
			Event @event = parameter as Event;

			if (@event != null)
			{
				// assign background before page appears to 
				// avoid image replacing during navigation
				EventBackground.Source = @event.ImagePath;
			}
		}

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            MainScrollView.Margin = Device.Idiom == TargetIdiom.Desktop ? new Thickness(45, 72, 0, 0) : new Thickness(0);
            RideSummary.Margin = Device.Idiom == TargetIdiom.Desktop ? new Thickness(0, 23, 45, 0) : new Thickness(0, 5, 0, 0);
        }
    }
}
