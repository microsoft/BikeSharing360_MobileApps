using BikeSharing.Clients.Core.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using System.ComponentModel;
using BikeSharing.Clients.Windows.Effects;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

[assembly: ExportEffect(typeof(MaxLinesEffect), "MaxLinesEffect")]
namespace BikeSharing.Clients.Windows.Effects
{
    public class MaxLinesEffect : PlatformEffect
    {
        TextBlock _control;

        protected override void OnAttached()
        {
            _control = Control as TextBlock;
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
                _control.TextWrapping = TextWrapping.Wrap;
                _control.MaxLines = maxLines;
            }
        }
    }
}
