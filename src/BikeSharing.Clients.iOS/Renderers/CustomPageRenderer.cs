using BikeSharing.Clients.iOS.Renderers;
using CoreGraphics;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Page), typeof(CustomPageRenderer))]
namespace BikeSharing.Clients.iOS.Renderers
{
    public class CustomPageRenderer : PageRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            nfloat width = UIScreen.MainScreen.Bounds.Size.Width;
            nfloat height = UIScreen.MainScreen.Bounds.Size.Height;

            var image = UIImage.FromBundle("background");
            var imageView = new UIImageView(new CGRect(0, 0, width, height));
            imageView.Image = image;
            imageView.ContentMode = UIViewContentMode.ScaleAspectFill;
            View.AddSubview(imageView);
            View.SendSubviewToBack(imageView);
        }
    }
}
