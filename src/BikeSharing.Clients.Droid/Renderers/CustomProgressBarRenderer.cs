using BikeSharing.Clients.Core.Controls;
using BikeSharing.Clients.Droid.Renderers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomProgressBar), typeof(CustomProgressBarRenderer))]
namespace BikeSharing.Clients.Droid.Renderers
{
    public class CustomProgressBarRenderer : ProgressBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ProgressBar> e)
        {
            base.OnElementChanged(e);

            try
            {
                var solidTransparentColor = new Color(255, 255, 255, 1.0);
                Control.ProgressDrawable.SetColorFilter(solidTransparentColor.ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcIn);
                Control.ProgressTintList = Android.Content.Res.ColorStateList.ValueOf(solidTransparentColor.ToAndroid());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}