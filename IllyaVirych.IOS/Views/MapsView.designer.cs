// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using MapKit;
using System;
using System.CodeDom.Compiler;

namespace IllyaVirych.IOS.Views
{
    [Register ("MapsView")]
    partial class MapsView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView MapViewIOS { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (MapViewIOS != null) {
                MapViewIOS.Dispose ();
                MapViewIOS = null;
            }
        }
    }
}