using BikeSharing.Clients.Core.Controls.Cells;
using BikeSharing.Clients.Core.Models.Rides;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.TemplateSelectors
{
    public class MyRidesDataTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate _eventDataTemplate;
        private readonly DataTemplate _customRideDataTemplate;
        private readonly DataTemplate _suggestedRideDataTemplate;

        public MyRidesDataTemplateSelector()
        {
            _eventDataTemplate = new DataTemplate(typeof(EventRideCell));
            _customRideDataTemplate = new DataTemplate(typeof(CustomRideCell));
            _suggestedRideDataTemplate = new DataTemplate(typeof(SuggestedRideCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var ride = item as Ride;

            if (ride == null)
            {
                return null;
            }

            switch (ride.RideType)
            {
                case RideType.Event:
                    return _eventDataTemplate;
                case RideType.Suggestion:
                    return _suggestedRideDataTemplate;
                case RideType.Custom:
                    return _customRideDataTemplate;
                default:
                    return null;
            }
        }
    }
}
