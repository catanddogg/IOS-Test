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
    public partial class ListTaskView : MvxViewController<ListTaskViewModel>
    {        
        private UIButton _buttonMenu, _buttonAdd;       
        private UIButton _createTaskButton, _aboutTaskButton, _logoutTaskButton;
        private UIImageView _imageViewMenu;
        private UIView _menuView, _navigationView;
        private bool _statusMenuView;        
        private UICollectionViewFlowLayout _listTaskCollectionViewFlowLayout;
        private TaskListCollectionViewSource _source;
        private MvxUIRefreshControl _refreshListTaskControl;

        public ListTaskView () : base (nameof(ListTaskView), null)
        {          
        }

        public sealed override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetUpListTaskView();
        }

        private void SetUpListTaskView()
        {
            Title = "TaskyDrop";
            NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(0, 127, 70);
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.Black };

            _refreshListTaskControl = new MvxUIRefreshControl();
            TaskListCollectionView.AddSubview(_refreshListTaskControl);            

            TaskListCollectionView.RegisterNibForCell(ListTaskNameCell.Nib, ListTaskNameCell.Key);
            _source = new TaskListCollectionViewSource(TaskListCollectionView, ListTaskNameCell.Key);
            TaskListCollectionView.Source = _source;
            _listTaskCollectionViewFlowLayout = new UICollectionViewFlowLayout();
            _listTaskCollectionViewFlowLayout.ItemSize = new CGSize(47 * UIScreen.MainScreen.Bounds.Width / 100, 47 * UIScreen.MainScreen.Bounds.Width / 100);
            _listTaskCollectionViewFlowLayout.MinimumInteritemSpacing = 8;
            _listTaskCollectionViewFlowLayout.MinimumLineSpacing = 8;
            _listTaskCollectionViewFlowLayout.HeaderReferenceSize = new CGSize(0, 0);
            _listTaskCollectionViewFlowLayout.SectionInset = new UIEdgeInsets(5, 5, 5, 5);
            TaskListCollectionView.CollectionViewLayout = _listTaskCollectionViewFlowLayout;

            _buttonAdd = new UIButton(UIButtonType.Custom);
            _buttonAdd.Frame = new CGRect(0, 0, 40, 40);
            _buttonAdd.SetImage(UIImage.FromBundle("AddTaskIcon"), UIControlState.Normal);
            this.NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(_buttonAdd), false);
            _buttonAdd.TouchUpInside += ButtonAddTaskClick;

            _buttonMenu = new UIButton(UIButtonType.Custom);
            _buttonMenu.Frame = new CGRect(0, 0, 40, 40);
            _buttonMenu.SetImage(UIImage.FromBundle("MenuIcon"), UIControlState.Normal);
            this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(_buttonMenu), false);

            _buttonMenu.TouchUpInside += delegate
            {
                _statusMenuView = true;
                MenuViewController();
            };

            LabelNetworkAccessListTask.BackgroundColor = UIColor.Red;
            TaskListCollectionView.ReloadData();

            var set = this.CreateBindingSet<ListTaskView, ListTaskViewModel>();
            set.Bind(_buttonAdd).To(vm => vm.TaskCreateCommand);
            set.Bind(LabelNetworkAccessListTask).For(v => v.Hidden).To(vm => vm.NetworkAccess).WithConversion("Status");
            set.Bind(_source).To(m => m.Items);
            set.Bind(_source).For(v => v.SelectionChangedCommand).To(vm => vm.TaskChangeCommand);
            set.Bind(_refreshListTaskControl).For(v => v.IsRefreshing).To(vm => vm.RefreshTaskCollection);
            set.Bind(_refreshListTaskControl).For(v => v.RefreshCommand).To(vm => vm.RefreshTaskCommand);
            set.Apply();
        }

        private void ButtonAddTaskClick(object sender, EventArgs e)
        {
            var networkAccess = this.ViewModel.NetworkAccess;
            if (networkAccess != NetworkAccess.Internet)
            {
                var AllertSave = UIAlertController.Create("", "You do not have network access!", UIAlertControllerStyle.Alert);
                AllertSave.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(AllertSave, true, null);
            }
        }

        public override void ViewWillTransitionToSize(CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
        {
            base.ViewWillTransitionToSize(toSize, coordinator);
            _listTaskCollectionViewFlowLayout.ItemSize = new CGSize(47 * toSize.Width / 100, 47 * toSize.Width / 100);
            if (_statusMenuView == true)
            {
                if(toSize.Width > toSize.Height || toSize.Width < toSize.Height)
                {
                    _menuView.Frame = new CGRect(0, 0, 75 * toSize.Width/100, 736);
                    _navigationView.Frame = new CGRect(75 * toSize.Width / 100, 0, 25 * toSize.Width / 100, 736);
                    _imageViewMenu.Frame = new CGRect(0, 0, 75 * toSize.Width / 100, 200);
                }                
            }
        }
        
        private void MenuViewController()
        {
            _menuView = new UIView();
            _menuView.BackgroundColor = UIColor.DarkGray;
            _menuView.Frame = new CGRect(0, 0, 75 * UIScreen.MainScreen.Bounds.Width/100, 736);

            _navigationView = new UIView();
            _navigationView.BackgroundColor = UIColor.FromWhiteAlpha(1, 0.75F);
            _navigationView.Frame = new CGRect(75 * UIScreen.MainScreen.Bounds.Width/100, 0, 25 * UIScreen.MainScreen.Bounds.Width/100, 736);

            _imageViewMenu = new UIImageView();
            _imageViewMenu.Image = UIImage.FromBundle("MenuTitleIcon");
            _imageViewMenu.Frame = new CGRect(0, 0, 75 * UIScreen.MainScreen.Bounds.Width/100, 170);

            _createTaskButton = new UIButton();
            _createTaskButton.SetTitle("Create Task", UIControlState.Normal);
            _createTaskButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            _createTaskButton.LayoutIfNeeded();
            _createTaskButton.Frame = new CGRect(10, 200, 290, 30);

            _aboutTaskButton = new UIButton();
            _aboutTaskButton.SetTitle("About Task", UIControlState.Normal);
            _aboutTaskButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            _aboutTaskButton.LayoutIfNeeded();
            _aboutTaskButton.Frame = new CGRect(10, 240, 290, 30);

            _logoutTaskButton = new UIButton();
            _logoutTaskButton.SetTitle("Logout", UIControlState.Normal);
            _logoutTaskButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            _logoutTaskButton.LayoutIfNeeded();
            _logoutTaskButton.Frame = new CGRect(10, 280, 290, 30);

            this.View.Window.AddSubviews(_menuView, _imageViewMenu, _navigationView, _createTaskButton,
                _aboutTaskButton, _logoutTaskButton);

            UITapGestureRecognizer tapGestureRecognizer = new UITapGestureRecognizer(() =>
            {
                _statusMenuView = false;
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