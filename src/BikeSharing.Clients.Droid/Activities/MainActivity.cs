using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Services.Interfaces;
using BikeSharing.Clients.Core.ViewModels.Base;
using Card.IO;
using FFImageLoading.Forms.Droid;
using Xamarin.Forms;
using BikeSharing.Clients.Core;
using BikeSharing.Clients.Droid.Services;

namespace BikeSharing.Clients.Droid
{
    [Activity(
        Label = "BikeSharing.Clients.Core",
        Icon = "@drawable/icon",
        Theme = "@style/MainTheme",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            UserDialogs.Init(this);
            CachedImageRenderer.Init();
            ViewModelLocator.Instance.Register<ICreditCardScannerService, CreditCardScannerService>();

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (data != null)
            {
                var card = data.GetParcelableExtra(CardIOActivity.ExtraScanResult).JavaCast<CreditCard>();

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