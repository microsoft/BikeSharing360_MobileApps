using BikeSharing.Clients.Core.Controls;
using BikeSharing.Clients.Windows.Renderers;
using System.ComponentModel;
using Xamarin.Forms.Platform.UWP;
using Xamarin.Forms;
using Windows.UI.Xaml.Controls;

[assembly: ExportRenderer(typeof(NoBarsScrollViewer), typeof(NoBarsScrollViewerRenderer))]
namespace BikeSharing.Clients.Windows.Renderers
{
    public class NoBarsScrollViewerRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ScrollView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
            {
                return;
            }

            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;
            }

            e.NewElement.PropertyChanged += OnElementPropertyChanged;
        }

        private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ContentSize")
            {
                var scrollViewer = (ScrollViewer)Control;
                scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            }
        }
    }
}
