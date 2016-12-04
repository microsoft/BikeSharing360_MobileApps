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
    [Register ("MapDetailController")]
    partial class MapDetailController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        WatchKit.WKInterfaceLabel FromLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        WatchKit.WKInterfaceButton GoButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        WatchKit.WKInterfaceMap Map { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        WatchKit.WKInterfaceLabel ToLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        WatchKit.WKInterfaceButton ZoomInButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        WatchKit.WKInterfaceButton ZoomOutButton { get; set; }

        [Action ("Clear")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Clear ();

        [Action ("ZoomIn")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ZoomIn ();

        [Action ("ZoomOut")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ZoomOut ();

        void ReleaseDesignerOutlets ()
        {
            if (FromLabel != null) {
                FromLabel.Dispose ();
                FromLabel = null;
            }

            if (GoButton != null) {
                GoButton.Dispose ();
                GoButton = null;
            }

            if (Map != null) {
                Map.Dispose ();
                Map = null;
            }

            if (ToLabel != null) {
                ToLabel.Dispose ();
                ToLabel = null;
            }

            if (ZoomInButton != null) {
                ZoomInButton.Dispose ();
                ZoomInButton = null;
            }

            if (ZoomOutButton != null) {
                ZoomOutButton.Dispose ();
                ZoomOutButton = null;
            }
        }
    }
}