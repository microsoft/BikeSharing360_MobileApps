using Android.Widget;
using BikeSharing.Clients.Droid.Effects;
using BikeSharing.Clients.Core.Effects;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(DatePickerLineColorEffect), "DatePickerLineColorEffect")]
namespace BikeSharing.Clients.Droid.Effects
{
    public class DatePickerLineColorEffect : PlatformEffect
    {
        EditText control;

        protected override void OnAttached()
        {
            control = Control as EditText;
            UpdateLineColor();
        }

        protected override void OnDetached()
        {
            control = null;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == LineColorEffect.LineColorProperty.PropertyName)
            {
                UpdateLineColor();
            }
        }

        private void UpdateLineColor()
        {
            try
            {
                control.Background.SetColorFilter(LineColorEffect.GetLineColor(Element).ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcAtop);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }
    }
}