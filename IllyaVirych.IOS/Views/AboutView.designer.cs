// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace IllyaVirych.IOS.Views
{
    [Register ("AboutView")]
    partial class AboutView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AboutTaskLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelNetworkAccessAboutView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AboutTaskLabel != null) {
                AboutTaskLabel.Dispose ();
                AboutTaskLabel = null;
            }

            if (LabelNetworkAccessAboutView != null) {
                LabelNetworkAccessAboutView.Dispose ();
                LabelNetworkAccessAboutView = null;
            }
        }
    }
}