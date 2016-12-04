using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace BikeSharing.Clients.Core.Controls
{
    public class CustomMap : Map
    {
        public static readonly BindableProperty SelectedPinProperty =
            BindableProperty.Create("SelectedPin",             
                typeof(CustomPin), typeof(CustomMap), null);

        public CustomPin SelectedPin
        {
            get { return (CustomPin)base.GetValue(SelectedPinProperty); }
            set { base.SetValue(SelectedPinProperty, value); }
        }

        public static readonly BindableProperty FromProperty =
            BindableProperty.Create("From",
                typeof(CustomPin), typeof(CustomMap), null);

        public CustomPin From
        {
            get { return (CustomPin)base.GetValue(FromProperty); }
            set { base.SetValue(FromProperty, value); }
        }

        public static readonly BindableProperty ToProperty = 
            BindableProperty.Create("To",    
                typeof(CustomPin), typeof(CustomMap), null);

        public CustomPin To
        {
            get { return (CustomPin)base.GetValue(ToProperty); }
            set { base.SetValue(ToProperty, value); }
        }

        public static readonly BindableProperty CustomPinsProperty =
            BindableProperty.Create("CustomPins",
                typeof(IEnumerable<CustomPin>), typeof(CustomMap), default(IEnumerable<CustomPin>), 
                BindingMode.TwoWay);

        public IEnumerable<CustomPin> CustomPins
        {
            get { return (IEnumerable<CustomPin>)base.GetValue(CustomPinsProperty); }
            set { base.SetValue(CustomPinsProperty, value); }
        }
    }
}