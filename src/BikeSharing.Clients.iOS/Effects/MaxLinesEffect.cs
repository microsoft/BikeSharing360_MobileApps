using BikeSharing.Clients.Core.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using BikeSharing.Clients.iOS.Effects;
using UIKit;

[assembly: ExportEffect(typeof(MaxLinesEffect), "MaxLinesEffect")]
namespace BikeSharing.Clients.iOS.Effects
{
    public class MaxLinesEffect : PlatformEffect
    {
        UILabel _control;

        protected override void OnAttached()
        {
            _control = Control as UILabel;
            SetMaxLines();
        }

        protected override void OnDetached()
        {
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == NumberOfLinesEffect.NumberOfLinesProperty.PropertyName)
            {
                SetMaxLines();
            }
        }

        private void SetMaxLines()
        {
            var maxLines = NumberOfLinesEffect.GetNumberOfLines(Element);

            if (_control != null)
            {
                _control.Lines = maxLines;
            }
        }
    }
}
