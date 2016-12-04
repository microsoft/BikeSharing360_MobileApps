using System;
using System.ComponentModel;
using BikeSharing.Clients.Core.Controls;
using BikeSharing.Clients.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NoBarsScrollViewer), typeof(NoBarsScrollViewerRenderer))]
namespace BikeSharing.Clients.Droid.Renderers
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

            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;
            }

            e.NewElement.PropertyChanged += OnElementPropertyChanged;
        }

        private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ContentSize" && ChildCount > 0)
            {
                Android.Views.View child = GetChildAt(0);
                child.VerticalScrollBarEnabled = false;
                child.HorizontalScrollBarEnabled = false;
            }    
        } 
    }
}