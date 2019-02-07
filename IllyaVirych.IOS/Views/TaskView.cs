using CoreGraphics;
using Foundation;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

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

            //_buttonBack = new UIBarButtonItem(UIBarButtonSystemItem.Reply, null);
            //NavigationItem.SetLeftBarButtonItem(_buttonBack, false);

            _buttonBack = new UIButton(UIButtonType.Custom);
            _buttonBack.Frame = new CGRect(0, 0, 40, 40);
            _buttonBack.SetImage(UIImage.FromBundle("icons8-back-filled-30.png"), UIControlState.Normal);
            this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(_buttonBack), false);          

            var hideKeybord = new UITapGestureRecognizer(() => View.EndEditing(true));
            View.AddGestureRecognizer(hideKeybord);

            var set = this.CreateBindingSet<TaskView, TaskViewModel>();
            set.Bind(NameTask).To(vm => vm.NameTask);            
            set.Bind(StatusTask).To(vm => vm.StatusTask);
            set.Bind(DescriptionTask).To(vm => vm.DescriptionTask);
            set.Bind(SaveTaskButton).To(vm => vm.SaveTaskCommand);
            set.Bind(DeleteTaskButton).To(vm => vm.DeleteTaskCommand);
            set.Bind(MapButton).To(vm => vm.GoogleMapCommand);
            set.Bind(DeleteMarkerButton).To(vm => vm.DeleteMarkerGoogleMapCommand);
            set.Bind(_buttonBack).To(vm => vm.BackTaskCommand);
            set.Apply();
        }            
    }
}