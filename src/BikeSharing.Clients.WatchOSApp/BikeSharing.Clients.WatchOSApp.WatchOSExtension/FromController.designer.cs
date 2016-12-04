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
    [Register ("FromController")]
    partial class FromController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        WatchKit.WKInterfaceTable FromTable { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (FromTable != null) {
                FromTable.Dispose ();
                FromTable = null;
            }
        }
    }
}