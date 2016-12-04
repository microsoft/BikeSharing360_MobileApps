using BikeSharing.Clients.Core.Controls;
using BikeSharing.Clients.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NoBarsScrollViewer), typeof(NoBarsScrollViewerRenderer))]
namespace BikeSharing.Clients.iOS
{
	public class NoBarsScrollViewerRenderer : ScrollViewRenderer
	{
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null || this.Element == null)
			{
				return;
			}

			ShowsHorizontalScrollIndicator = false;
			ShowsVerticalScrollIndicator = false;
		}
	}
}
