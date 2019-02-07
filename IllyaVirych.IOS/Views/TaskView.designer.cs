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
    [Register ("TaskView")]
    partial class TaskView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DeleteMarkerButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DeleteTaskButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView DescriptionTask { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton MapButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField NameTask { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SaveTaskButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch StatusTask { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DeleteMarkerButton != null) {
                DeleteMarkerButton.Dispose ();
                DeleteMarkerButton = null;
            }

            if (DeleteTaskButton != null) {
                DeleteTaskButton.Dispose ();
                DeleteTaskButton = null;
            }

            if (DescriptionTask != null) {
                DescriptionTask.Dispose ();
                DescriptionTask = null;
            }

            if (MapButton != null) {
                MapButton.Dispose ();
                MapButton = null;
            }

            if (NameTask != null) {
                NameTask.Dispose ();
                NameTask = null;
            }

            if (SaveTaskButton != null) {
                SaveTaskButton.Dispose ();
                SaveTaskButton = null;
            }

            if (StatusTask != null) {
                StatusTask.Dispose ();
                StatusTask = null;
            }
        }
    }
}