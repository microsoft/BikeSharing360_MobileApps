using BikeSharing.Clients.Core.Effects;
using BikeSharing.Clients.iOS.Effects;
using CoreAnimation;
using CoreGraphics;
using System;
using System.ComponentModel;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(EntryLineColorEffect), "EntryLineColorEffect")]
namespace BikeSharing.Clients.iOS.Effects
{
    public class EntryLineColorEffect : PlatformEffect
    {
        UITextField control;

        protected override void OnAttached()
        {
            try
            {
                control = Control as UITextField;
                UpdateLineColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            control = null;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == LineColorEffect.LineColorProperty.PropertyName || 
                args.PropertyName == "Height")
            {
                Initialize();
                UpdateLineColor();
            }
        }

        private void Initialize()
        {
            var entry = Element as Entry;
            if (entry != null)
            {
                Control.Bounds = new CGRect(0, 0, entry.Width, entry.Height);
            }
        }

        private void UpdateLineColor()
        {
            BorderLineLayer lineLayer = control.Layer.Sublayers.OfType<BorderLineLayer>()
                                                             .FirstOrDefault();
            
            if (lineLayer == null)
            {
                lineLayer = new BorderLineLayer();
                lineLayer.MasksToBounds = true;
                lineLayer.BorderWidth = 1.0f;
                control.Layer.AddSublayer(lineLayer);
                control.BorderStyle = UITextBorderStyle.None;
            }

            lineLayer.Frame = new CGRect(0f, Control.Frame.Height - 1f, Control.Bounds.Width, 1f);
            lineLayer.BorderColor = LineColorEffect.GetLineColor(Element).ToCGColor();
			control.TintColor = control.TextColor;
        }

        private class BorderLineLayer : CALayer
        {
        }
    }
}
