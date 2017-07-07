using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using BikeSharing.Clients.Core.ViewModels.Base;
using FFImageLoading.Forms.Droid;
using Xamarin.Forms;
using BikeSharing.Clients.Core;

namespace BikeSharing.Clients.Droid
{
    [Activity(
        Label = "BikeSharing.Clients.Core",
        Icon = "@drawable/icon",
        Theme = "@style/MainTheme",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            UserDialogs.Init(this);
            CachedImageRenderer.Init();
			ViewModelLocator.Instance.Register<IOperatingSystemVersionProvider, OperatingSystemVersionProvider>();

            LoadApplication(new App());
        }
    }
}