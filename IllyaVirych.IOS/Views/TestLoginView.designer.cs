// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace IllyaVirych.IOS
{
    [Register ("TestLoginView")]
    partial class TestLoginView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        WebKit.WKWebView LoginWebView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView TestLoginViewController { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LoginWebView != null) {
                LoginWebView.Dispose ();
                LoginWebView = null;
            }

            if (TestLoginViewController != null) {
                TestLoginViewController.Dispose ();
                TestLoginViewController = null;
            }
        }
    }
}