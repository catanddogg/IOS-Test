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
        #region Variables
        private UIButton _buttonBack;
        #endregion

        #region Constructors
        public TaskView() : base(nameof(TaskView), null)
        {
        }
        #endregion
  
        #region Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(0, 127, 70);
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.Black };

            _buttonBack = new UIButton(UIButtonType.Custom);
            _buttonBack.Frame = new CGRect(0, 0, 40, 40);
            _buttonBack.SetImage(UIImage.FromBundle("BackIcon"), UIControlState.Normal);
            this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(_buttonBack), false);

            DescriptionTask.Font = UIFont.FromName("MyCustomFont", 12.0f);

            var hideKeybord = new UITapGestureRecognizer(() => View.EndEditing(true));
            View.AddGestureRecognizer(hideKeybord);

            LabelNetworkAccessTaskView.BackgroundColor = UIColor.Red;

            var set = this.CreateBindingSet<TaskView, TaskViewModel>();
            set.Bind(NameTask).To(vm => vm.NameTask);
            set.Bind(StatusTask).To(vm => vm.StatusTask);
            set.Bind(DescriptionTask).To(vm => vm.DescriptionTask);
            set.Bind(SaveTaskButton).To(vm => vm.SaveTaskCommand);
            set.Bind(DeleteTaskButton).To(vm => vm.DeleteTaskCommand);
            set.Bind(LabelNetworkAccessTaskView).For(v => v.Hidden).To(vm => vm.ChangedNetworkAccess);
            set.Bind(MapButton).To(vm => vm.MapCommand);
            set.Bind(DeleteMarkerButton).To(vm => vm.DeleteMarkerMapCommand);
            set.Bind(_buttonBack).To(vm => vm.BackTaskCommand);
            set.Apply();
        }
        #endregion
    }
}