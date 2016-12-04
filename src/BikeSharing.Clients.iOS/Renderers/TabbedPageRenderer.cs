using BikeSharing.Clients.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageRenderer))]
namespace BikeSharing.Clients.iOS.Renderers
{
    public class TabbedPageRenderer : TabbedRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            TabBar.TintColor = UIColor.FromRGBA(0x30, 0x63, 0xf5, 0xff);
            TabBar.BarTintColor = UIColor.FromRGBA(0xf4, 0xf6, 0xfa, 0xff);
        }
    }
}
