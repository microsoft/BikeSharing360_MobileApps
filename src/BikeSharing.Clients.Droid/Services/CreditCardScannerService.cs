using Android.Content;
using BikeSharing.Clients.Core.Services.Interfaces;
using Card.IO;
using Plugin.CurrentActivity;

namespace BikeSharing.Clients.Droid.Services
{
    public class CreditCardScannerService : ICreditCardScannerService
    {
        public void StartScanning()
        {
            var currentActivity = CrossCurrentActivity.Current.Activity;

            var intent = new Intent(currentActivity, typeof(CardIOActivity));
            intent.PutExtra(CardIOActivity.ExtraRequireExpiry, true);
            intent.PutExtra(CardIOActivity.ExtraRequireCvv, false);
            intent.PutExtra(CardIOActivity.ExtraRequirePostalCode, false);
            intent.PutExtra(CardIOActivity.ExtraUseCardioLogo, false);
            intent.PutExtra(CardIOActivity.ExtraUsePaypalActionbarIcon, false);
            intent.PutExtra(CardIOActivity.ExtraHideCardioLogo, true);

            currentActivity.StartActivityForResult(intent, 101);
        }
    }
}