using BikeSharing.Clients.Core.Controls;
using BikeSharing.Clients.iOS.Renderers;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomProgressBar), typeof(CustomProgressBarRenderer))]
namespace BikeSharing.Clients.iOS.Renderers
{
    public class CustomProgressBarRenderer : ProgressBarRenderer
    {
        protected override void OnElementChanged(
         ElementChangedEventArgs<ProgressBar> e)
        {
            base.OnElementChanged(e);

            Control.ProgressTintColor = Color.FromRgb(255, 255, 255).ToUIColor();
            var semiTransparentColor = new Color(255, 255, 255, 0.5);
            Control.TrackTintColor = semiTransparentColor.ToUIColor();
        }


        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
                        
            this.ClipsToBounds = true;
            this.Layer.MasksToBounds = true;
            this.Layer.CornerRadius = 5; 

            var X = 1.0f;
            var Y = 1.2f;

            CGAffineTransform transform = CGAffineTransform.MakeScale(X, Y);
            transform.TransformSize(this.Frame.Size);
            this.Transform = transform;
        }
    }
}
