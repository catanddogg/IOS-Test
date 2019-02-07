using CoreGraphics;
using Foundation;
using IllyaVirych.Core.Services;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace IllyaVirych.IOS.Views
{
    [MvxModalPresentation(WrapInNavigationController = true/*, ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve*/)]
    public partial class ListTaskView : MvxViewController<ListTaskViewModel>
    {        
        private UIButton _buttonMenu, _buttonAdd;       
        private UIButton _createTaskButton, _aboutTaskButton, _logoutTaskButton;
        private UIImageView _imageViewMenu;
        private UIView _menuView, _navigationView;

        public ListTaskView () : base (nameof(ListTaskView), null)
        {          
        }

        public sealed override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TaskListCollectionView.RegisterNibForCell(ListTaskNameCell.Nib, ListTaskNameCell.Key);
            var source = new TaskListCollectionViewSource(TaskListCollectionView, ListTaskNameCell.Key );
            TaskListCollectionView.Source = source;

            Title = "TaskyDrop";

            //_buttonAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add, null);
            //NavigationItem.SetRightBarButtonItem(_buttonAdd, false);

            _buttonAdd = new UIButton(UIButtonType.Custom);
            _buttonAdd.Frame = new CGRect(0, 0, 40, 40);
            _buttonAdd.SetImage(UIImage.FromBundle("icons8-plus-math-filled-30.png"), UIControlState.Normal);
            this.NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(_buttonAdd), false);

            _buttonMenu = new UIButton(UIButtonType.Custom);
            _buttonMenu.Frame = new CGRect(0, 0, 40, 40);
            _buttonMenu.SetImage(UIImage.FromBundle("icons8-menu-filled-30.png"), UIControlState.Normal);
            this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(_buttonMenu), false);             

            _buttonMenu.TouchUpInside += (sender, e) =>
            {
                MenuViewController();
            };

            var set = this.CreateBindingSet<ListTaskView, ListTaskViewModel>();
            set.Bind(_buttonAdd).To(vm => vm.TaskCreateCommand);            
            set.Bind(source).To(m => m.Items);
            set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.TaskCreateCommand);            
            set.Apply();       

            TaskListCollectionView.ReloadData();
        }

        private void MenuViewController()
        {
            _menuView = new UIView();
            _menuView.BackgroundColor = UIColor.DarkGray;
            _menuView.Frame = new CGRect(0, 0, 300, 736);

            _navigationView = new UIView();
            _navigationView.BackgroundColor = UIColor.FromWhiteAlpha(1, 0.75F);
            _navigationView.Frame = new CGRect(300, 0, 115, 736);

            _imageViewMenu = new UIImageView();
            _imageViewMenu.Image = UIImage.FromBundle("mqse9xro.jpg");
            _imageViewMenu.Frame = new CGRect(0, 0, 300, 200);

            _createTaskButton = new UIButton();
            _createTaskButton.SetTitle("Create Task", UIControlState.Normal);
            _createTaskButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            _createTaskButton.LayoutIfNeeded();
            _createTaskButton.Frame = new CGRect(10, 220, 290, 30);

            _aboutTaskButton = new UIButton();
            _aboutTaskButton.SetTitle("About Task", UIControlState.Normal);
            _aboutTaskButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            _aboutTaskButton.LayoutIfNeeded();
            _aboutTaskButton.Frame = new CGRect(10, 260, 290, 30);

            _logoutTaskButton = new UIButton();
            _logoutTaskButton.SetTitle("Logout", UIControlState.Normal);
            _logoutTaskButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            _logoutTaskButton.LayoutIfNeeded();
            _logoutTaskButton.Frame = new CGRect(10, 300, 290, 30);

            this.View.Window.AddSubviews(_menuView, _imageViewMenu, _navigationView, _createTaskButton,
                _aboutTaskButton, _logoutTaskButton);

            UITapGestureRecognizer tapGestureRecognizer = new UITapGestureRecognizer(() =>
            {
                _menuView.RemoveFromSuperview();
                _imageViewMenu.RemoveFromSuperview();
                _navigationView.RemoveFromSuperview();
                _createTaskButton.RemoveFromSuperview();
                _aboutTaskButton.RemoveFromSuperview();
                _logoutTaskButton.RemoveFromSuperview();
            });
            _navigationView.UserInteractionEnabled = true;
            _navigationView.AddGestureRecognizer(tapGestureRecognizer);

            var set1 = this.CreateBindingSet<ListTaskView, ListTaskViewModel>();
            set1.Bind(_createTaskButton).To(vm => vm.TaskCreateCommand);
            set1.Bind(_aboutTaskButton).To(vm => vm.ShowAboutCommand);
            set1.Bind(_logoutTaskButton).To(vm => vm.LoginViewCommand);
            set1.Apply();
        }
    }   
}