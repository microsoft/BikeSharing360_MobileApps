using BikeSharing.Clients.Core.Services.Interfaces;
using UIKit;

namespace BikeSharing.Clients.iOS
{
    public class CreditCardScannerService : ICreditCardScannerService
	{
		public void StartScanning()
		{
			UIViewController controller = GetTopViewController();

			controller.PresentViewController(new CardIOController(), false, null);
		}

		private UIViewController GetTopViewController()
		{
			var window = UIApplication.SharedApplication.KeyWindow;
			var vc = window.RootViewController;

			while (vc.PresentedViewController != null)
			{
				vc = vc.PresentedViewController;
			}

			return vc;
		}
	}
}
