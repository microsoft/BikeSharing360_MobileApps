using BikeSharing.Clients.Windows.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(DatePicker), typeof(CustomDatePickerRenderer))]
namespace BikeSharing.Clients.Windows.Renderers
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        private const double MaxDatePickerWidth = 200;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            var datePicker = Control;

            if (datePicker != null)
            {
                datePicker.MinWidth = MaxDatePickerWidth;
                datePicker.Width = MaxDatePickerWidth;
                datePicker.MaxWidth = MaxDatePickerWidth;
            }
        }
    }
}
