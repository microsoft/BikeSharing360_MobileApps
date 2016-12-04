using System;
using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.ViewModels.Base;
using Card.IO;
using UIKit;
using Xamarin.Forms;

namespace BikeSharing.Clients.iOS
{
	public class CardIOController : UIViewController, ICardIOPaymentViewControllerDelegate
	{
		private bool _appeared;

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			if (_appeared)
			{
				DismissViewController(false, null);
				return;
			}

			_appeared = true;
	
			var paymentViewController = new CardIOPaymentViewController(this);

			paymentViewController.CollectExpiry = true;
			paymentViewController.CollectCVV = false;
			paymentViewController.CollectPostalCode = false;
			paymentViewController.HideCardIOLogo = true;
			paymentViewController.UseCardIOLogo = false;
			paymentViewController.DisableManualEntryButtons = true;

			paymentViewController.AllowFreelyRotatingCardGuide = true;
			PresentViewController(paymentViewController, true, null);
		}

		public void UserDidCancelPaymentViewController(CardIOPaymentViewController paymentViewController)
		{
			paymentViewController.DismissViewController(true, null);
		}

		public void UserDidProvideCreditCardInfo(CreditCardInfo card, CardIOPaymentViewController paymentViewController)
		{
			paymentViewController.DismissViewController(true, null);

			if (card != null)
			{
				var creditCardInfo = new CreditCardInformation
				{
					CardNumber = card.CardNumber,
					ExpirationMonth = card.ExpiryMonth.ToString(),
					ExpirationYear = card.ExpiryYear.ToString()
				};

				MessagingCenter.Send(creditCardInfo, MessengerKeys.CreditCardScanned);
			}
		}
	}
}
