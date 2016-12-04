using BikeSharing.Clients.Core.Helpers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Xamarin.Forms;
using Size = Windows.Foundation.Size;

namespace BikeSharing.Clients.Windows.Services
{
    public class WindowStatusService
    {
        public async Task Load()
        {
            if (Device.Idiom == TargetIdiom.Desktop)
            {
                string serialized = Settings.UwpWindowSize;

                if (!string.IsNullOrEmpty(serialized))
                {
                    Rect bounds = await Task.Run(() => JsonConvert.DeserializeObject<Rect>(serialized));
                    ApplicationView.PreferredLaunchViewSize = new Size(bounds.Width, bounds.Height);
                    ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
                }
            }
        }

        public async Task Save()
        {
            if (Device.Idiom == TargetIdiom.Desktop)
            {
                ApplicationView appView = ApplicationView.GetForCurrentView();
                Rect bounds = appView.VisibleBounds;

                string serialized = await Task.Run(() => JsonConvert.SerializeObject(bounds));
                Settings.UwpWindowSize = serialized;
            }
        }
    }
}
