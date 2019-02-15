using CoreGraphics;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;
using Xamarin.Essentials;

namespace IllyaVirych.IOS.Views
{
    [MvxModalPresentation(WrapInNavigationController = true)]
    public partial class TaskView : MvxViewController<TaskViewModel>
    {
        public TaskView () : base (nameof(TaskView), null)
        {
        }

        UIButton _buttonBack;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(0, 127, 70);
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.Black };

            _buttonBack = new UIButton(UIButtonType.Custom);
            _buttonBack.Frame = new CGRect(0, 0, 40, 40);
            _buttonBack.SetImage(UIImage.FromBundle("BackIcon"), UIControlState.Normal);
            this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(_buttonBack), false);          

            var hideKeybord = new UITapGestureRecognizer(() => View.EndEditing(true));
            View.AddGestureRecognizer(hideKeybord);

            LabelNetworkAccessTaskView.BackgroundColor = UIColor.Red;

            DeleteMarkerButton.TouchUpInside += ButtonDeleteMarkerClick;
            SaveTaskButton.TouchUpInside += ButtonSaveTaskClick;
            DeleteTaskButton.TouchUpInside += ButtonDeleteTaskClick;

            var set = this.CreateBindingSet<TaskView, TaskViewModel>();
            set.Bind(NameTask).To(vm => vm.NameTask);            
            set.Bind(StatusTask).To(vm => vm.StatusTask);
            set.Bind(DescriptionTask).To(vm => vm.DescriptionTask);
            set.Bind(SaveTaskButton).To(vm => vm.SaveTaskCommand);
            set.Bind(DeleteTaskButton).To(vm => vm.DeleteTaskCommand);
            set.Bind(LabelNetworkAccessTaskView).For(v => v.Hidden).To(vm => vm.NetworkAccess).WithConversion("Status");
            set.Bind(MapButton).To(vm => vm.MapCommand);
            set.Bind(DeleteMarkerButton).To(vm => vm.DeleteMarkerMapCommand);
            set.Bind(_buttonBack).To(vm => vm.BackTaskCommand);
            set.Apply();
        }

        private void ButtonDeleteMarkerClick(object sender, EventArgs e)
        {
            var networkAccess = this.ViewModel.NetworkAccess;
            if (networkAccess != NetworkAccess.Internet)
            {
                var AllertSave = UIAlertController.Create("", "You do not have network access!", UIAlertControllerStyle.Alert);
                AllertSave.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(AllertSave, true, null);
                return;
            }
            var LalitudeMarker = this.ViewModel.LalitudeMarkerResult;
            if (LalitudeMarker == 0)
            {
                var AllertDeleteMarker_1 = UIAlertController.Create("", "Task have not marker!", UIAlertControllerStyle.Alert);
                AllertDeleteMarker_1.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(AllertDeleteMarker_1, true, null);
                return;
            }
            var AllertDeleteMarker_2 = UIAlertController.Create("", "Task marker has been deleted!", UIAlertControllerStyle.Alert);
            AllertDeleteMarker_2.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            PresentViewController(AllertDeleteMarker_2, true, null);
        }

        private void ButtonSaveTaskClick(object sender, EventArgs e)
        {
            var networkAccess = this.ViewModel.NetworkAccess;
            if (networkAccess != NetworkAccess.Internet)
            {
                var AllertSave = UIAlertController.Create("", "You do not have network access!", UIAlertControllerStyle.Alert);
                AllertSave.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(AllertSave, true, null);
                return;
            }
            var NameTask = this.ViewModel.NameTask;
            if (NameTask == null)
            {
                var AllertSave = UIAlertController.Create("", "Enter name task!", UIAlertControllerStyle.Alert);
                AllertSave.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(AllertSave, true, null);
            }
        }

        private void ButtonDeleteTaskClick(object sender, EventArgs e)
        {
            var networkAccess = this.ViewModel.NetworkAccess;
            if (networkAccess != NetworkAccess.Internet)
            {
                var AllertSave = UIAlertController.Create("", "You do not have network access!", UIAlertControllerStyle.Alert);
                AllertSave.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(AllertSave, true, null);
            }
        }
    }
}