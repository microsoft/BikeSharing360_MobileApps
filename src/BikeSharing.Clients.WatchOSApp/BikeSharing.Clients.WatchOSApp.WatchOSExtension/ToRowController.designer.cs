// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System.CodeDom.Compiler;

namespace BikeSharing.Clients.WatchOSApp.WatchOSExtension
{
    [Register ("ToRowController")]
    partial class ToRowController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        public WatchKit.WKInterfaceLabel ToLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ToLabel != null) {
                ToLabel.Dispose ();
                ToLabel = null;
            }
        }
    }
}