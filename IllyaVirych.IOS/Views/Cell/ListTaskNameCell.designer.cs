// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace IllyaVirych.IOS.Views.Cell
{
    [Register ("ListTaskNameCell")]
    partial class ListTaskNameCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NameTaskLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (NameTaskLabel != null) {
                NameTaskLabel.Dispose ();
                NameTaskLabel = null;
            }
        }
    }
}