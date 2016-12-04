using BikeSharing.Clients.Core.Models.Rides;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Controls.Rides
{
    public partial class RideSummaryControl
    {
        public event EventHandler AddedToParent;

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(RideSummaryControl), null);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty SubtitleProperty =
            BindableProperty.Create("Subtitle", typeof(string), typeof(RideSummaryControl), null);

        public string Subtitle
        {
            get { return (string)GetValue(SubtitleProperty); }
            set { SetValue(SubtitleProperty, value); }
        }
                
        public static readonly BindableProperty FromProperty =
          BindableProperty.Create("From", typeof(string), typeof(RideSummaryControl), null);

        public string From
        {
            get { return (string)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        public static readonly BindableProperty ToProperty =
          BindableProperty.Create("To", typeof(string), typeof(RideSummaryControl), null);

        public string To
        {
            get { return (string)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        public static readonly BindableProperty DateProperty =
          BindableProperty.Create("Date", typeof(DateTime), typeof(RideSummaryControl), DateTime.Now);

        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static readonly BindableProperty LocationProperty =
            BindableProperty.Create("Location", typeof(string), typeof(RideSummaryControl), string.Empty);

        public string Location
        {
            get { return (string)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }

        public static readonly BindableProperty FromStationProperty =
            BindableProperty.Create("FromStation", typeof(Station), typeof(RideSummaryControl), null);

        public Station FromStation
        {
            get { return (Station)GetValue(FromStationProperty); }
            set { SetValue(FromStationProperty, value); }
        }

        public static readonly BindableProperty ToStationProperty =
            BindableProperty.Create("ToStation", typeof(Station), typeof(RideSummaryControl), null);

        public Station ToStation
        {
            get { return (Station)GetValue(ToStationProperty); }
            set { SetValue(ToStationProperty, value); }
        }

        public static readonly BindableProperty BookingCommandProperty =
            BindableProperty.Create("BookingCommand", typeof(ICommand), typeof(RideSummaryControl), null);

        public ICommand BookingCommand
        {
            get { return (ICommand)GetValue(BookingCommandProperty); }
            set { SetValue(BookingCommandProperty, value); }
        }

        public static readonly BindableProperty IsStationsVisibleProperty =
          BindableProperty.Create("IsStationsVisible", typeof(bool), typeof(RideSummaryControl), true);

        public bool IsStationsVisible
        {
            get { return (bool)GetValue(IsStationsVisibleProperty); }
            set { SetValue(IsStationsVisibleProperty, value); }
        }

        public RideSummaryControl()
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.Windows ||
                Device.OS == TargetPlatform.WinPhone)
            {
                StationsContainer.WidthRequest = 432;
                StationsContainer.HorizontalOptions = LayoutOptions.Start;
            }
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            AddedToParent?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            BookingButton.Margin = Device.Idiom == TargetIdiom.Desktop ? new Thickness(0, 52, 0, 0) : new Thickness(-2, 20, 0, 0);
            StationsContainer.Margin = Device.Idiom == TargetIdiom.Desktop ? new Thickness(0, 45, 0, 10) : new Thickness(0);
        }
    }
}