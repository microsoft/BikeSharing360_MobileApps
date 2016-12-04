// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace BikeSharing.Clients.WatchOSApp.WatchOSExtension
{
    [Register ("FromRowController")]
    partial class FromRowController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        public WatchKit.WKInterfaceLabel FromLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (FromLabel != null) {
                FromLabel.Dispose ();
                FromLabel = null;
            }
        }
    }
}