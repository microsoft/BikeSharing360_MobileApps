using Android.App;
using Android.Content;
using Android.OS;
using Android.Content.PM;
using Android.Support.V7.App;

namespace BikeSharing.Clients.Droid.Activities
{
    [Activity(
        Label = "BikesSharing", 
        Icon = "@drawable/icon", 
        Theme = "@style/Theme.Splash", 
        MainLauncher = true,
        NoHistory = true,
        ScreenOrientation=ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            InvokeMainActivity();
        }

        private void InvokeMainActivity()
        {
            var mainActivityIntent = new Intent(this, typeof(MainActivity));
            StartActivity(mainActivityIntent);
        }
    }
}