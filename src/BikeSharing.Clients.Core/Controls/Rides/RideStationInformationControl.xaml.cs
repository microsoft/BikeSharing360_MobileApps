using BikeSharing.Clients.Core.Models.Rides;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Controls.Rides
{
    public partial class RideStationInformationControl
    {
        public static readonly BindableProperty StationProperty =
            BindableProperty.Create("Station", typeof(Station), typeof(RideStationInformationControl), null);

        public Station Station
        {
            get { return (Station)GetValue(StationProperty); }
            set { SetValue(StationProperty, value); }
        }

        public static readonly BindableProperty IsFromStationProperty =
            BindableProperty.Create("IsFromStation", typeof(bool), typeof(RideStationInformationControl), false);

        public bool IsFromStation
        {
            get { return (bool)GetValue(IsFromStationProperty); }
            set { SetValue(IsFromStationProperty, value); }
        }

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create("TextColor", typeof(Color), typeof(RideStationInformationControl), Color.Default);

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static readonly BindableProperty StationTextColorProperty =
            BindableProperty.Create("StationTextColor", typeof(Color), typeof(RideStationInformationControl), Color.Default);

        public Color StationTextColor
        {
            get { return (Color)GetValue(StationTextColorProperty); }
            set { SetValue(StationTextColorProperty, value); }
        }

        public RideStationInformationControl()
        {
            InitializeComponent();
        }
    }
}