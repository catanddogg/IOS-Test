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
    [Register ("MenuView")]
    partial class MenuView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton AbouTaskButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CreateTaskButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ImageViewMenu { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton LogoutButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView MenuViewController { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView MenuViewN { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView NavigationViewMenu { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AbouTaskButton != null) {
                AbouTaskButton.Dispose ();
                AbouTaskButton = null;
            }

            if (CreateTaskButton != null) {
                CreateTaskButton.Dispose ();
                CreateTaskButton = null;
            }

            if (ImageViewMenu != null) {
                ImageViewMenu.Dispose ();
                ImageViewMenu = null;
            }

            if (LogoutButton != null) {
                LogoutButton.Dispose ();
                LogoutButton = null;
            }

            if (MenuViewController != null) {
                MenuViewController.Dispose ();
                MenuViewController = null;
            }

            if (MenuViewN != null) {
                MenuViewN.Dispose ();
                MenuViewN = null;
            }

            if (NavigationViewMenu != null) {
                NavigationViewMenu.Dispose ();
                NavigationViewMenu = null;
            }
        }
    }
}