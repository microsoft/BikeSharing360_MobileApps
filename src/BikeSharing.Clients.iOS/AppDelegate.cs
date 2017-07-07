using BikeSharing.Clients.Core;
using BikeSharing.Clients.Core.Services;
using BikeSharing.Clients.Core.Services.Interfaces;
using BikeSharing.Clients.Core.ViewModels.Base;
using FFImageLoading.Forms.Touch;
using Foundation;
using HockeyApp.iOS;
using UIKit;

namespace BikeSharing.Clients.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.BlackOpaque, false);
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
            {
                TextColor = UIColor.White
            });

			InitHockeyApp();

			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
			#endif

            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsMaps.Init();
            CachedImageRenderer.Init();

            ViewModelLocator.Instance.RegisterSingleton<INavigationService, iOSNavigationService>();
			ViewModelLocator.Instance.Register<IOperatingSystemVersionProvider, OperatingSystemVersionProvider>();

            LoadApplication(new App());

            return base.FinishedLaunching(application, launchOptions);
        }

        private static void InitHockeyApp()
        {
            var manager = BITHockeyManager.SharedHockeyManager;
            manager.Configure(GlobalSettings.HockeyAppAPIKeyForiOS);
			manager.CrashManager.CrashManagerStatus = BITCrashManagerStatus.AutoSend;
            manager.StartManager();
        }
    }
}