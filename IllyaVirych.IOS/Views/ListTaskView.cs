using CoreGraphics;
using Firebase.RemoteConfig;
using Foundation;
using Google.MobileAds;
using iAd;
using IllyaVirych.Core.Interface;
using IllyaVirych.Core.ViewModels;
using IllyaVirych.IOS.Views.Cell;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using UIKit;
using Xamarin.Essentials;

namespace IllyaVirych.IOS.Views
{
    [MvxModalPresentation(WrapInNavigationController = true)]
    public partial class ListTaskView : MvxViewController<ListTaskViewModel>, IInterstitialDelegate
    {
        #region Variables
        private UIButton _buttonMenu, _buttonAdd;
        private UIButton _createTaskButton, _aboutTaskButton, _logoutTaskButton;
        private UIImageView _imageViewMenu;
        private UIView _menuView, _navigationView;
        private bool _statusMenuView;
        private UICollectionViewFlowLayout _listTaskCollectionViewFlowLayout;
        private TaskListCollectionViewSource _source;
        private MvxUIRefreshControl _refreshListTaskControl;
        private Interstitial _adsInterstitial;
        private bool _showAds;
        #endregion

        #region Constructors
        public ListTaskView() : base(nameof(ListTaskView), null)
        {
        }
        #endregion

        #region Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "TaskyDrop";
            NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(0, 127, 70);
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.Black };

            _buttonAdd = new UIButton(UIButtonType.Custom);
            _buttonAdd.Frame = new CGRect(0, 0, 40, 40);
            _buttonAdd.SetImage(UIImage.FromBundle("AddTaskIcon"), UIControlState.Normal);
            this.NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(_buttonAdd), false);

            //set.Bind(_buttonAdd).To(vm => vm.TaskCreateCommand);

            _buttonMenu = new UIButton(UIButtonType.Custom);
            _buttonMenu.Frame = new CGRect(0, 0, 40, 40);
            _buttonMenu.SetImage(UIImage.FromBundle("MenuIcon"), UIControlState.Normal);
            this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(_buttonMenu), false);

            _adsInterstitial = new Interstitial(adUnitID: "ca-app-pub-3940256099942544/4411468910") {
                Delegate = this
            };
            _adsInterstitial.LoadRequest(Google.MobileAds.Request.GetDefaultRequest());
            //_adsInterstitial.Delegate = this;

            var firebasePredictionsService = Mvx.Resolve<IFirebasePredictionsService>();
            _showAds = firebasePredictionsService.InitializeFirebasePredictions();
            SetUpCollectionView();

            _buttonMenu.TouchUpInside += delegate
            {
                _statusMenuView = true;
                MenuViewController();
            };
            LabelNetworkAccessListTask.BackgroundColor = UIColor.Red;

            var set = this.CreateBindingSet<ListTaskView, ListTaskViewModel>();
            set.Bind(_buttonAdd).To(vm => vm.TaskCreateCommand);
            set.Bind(LabelNetworkAccessListTask).For(v => v.Hidden).To(vm => vm.ChangedNetworkAccess);
            set.Bind(_source).To(m => m.Items);
            set.Bind(_source).For(v => v.SelectionChangedCommand).To(vm => vm.TaskChangeCommand);
            set.Bind(_refreshListTaskControl).For(v => v.IsRefreshing).To(vm => vm.RefreshTaskCollection);
            set.Bind(_refreshListTaskControl).For(v => v.RefreshCommand).To(vm => vm.RefreshTaskCommand);
            //Interaction
            set.Bind(this).For(view => view.Interaction).To(viewModel => viewModel.Interaction).OneWay();
            // 
            set.Apply();
        }
        #endregion

        private IMvxInteraction<object> _interaction;
        public IMvxInteraction<object> Interaction
        {
            get => _interaction;
            set
            {
                if (_interaction != null)
                    _interaction.Requested -= OnInteractionRequested;

                _interaction = value;
                _interaction.Requested += OnInteractionRequested;
            }
        }

        private void OnInteractionRequested(object sender, MvxValueEventArgs<object> eventArgs)
        {
            if (_showAds)
            {
                if (_adsInterstitial.IsReady)
                {
                    var window = UIApplication.SharedApplication.KeyWindow;
                    var vc = window.RootViewController;
                    while (vc.PresentedViewController != null)
                    {
                        vc = vc.PresentedViewController;
                    }
                    _adsInterstitial.PresentFromRootViewController(vc);
                    //_adsInterstitial.PresentFromRootViewController(rootViewController: this);
                }
            }
        }

        #region Override 
        public override void ViewWillTransitionToSize(CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
        {
            base.ViewWillTransitionToSize(toSize, coordinator);
            _listTaskCollectionViewFlowLayout.ItemSize = new CGSize(47 * toSize.Width / 100, 47 * toSize.Width / 100);
            if (_statusMenuView == true)
            {
                if (toSize.Width > toSize.Height || toSize.Width < toSize.Height)
                {
                    _menuView.Frame = new CGRect(0, 0, 75 * toSize.Width / 100, 736);
                    _navigationView.Frame = new CGRect(75 * toSize.Width / 100, 0, 25 * toSize.Width / 100, 736);
                    _imageViewMenu.Frame = new CGRect(0, 0, 75 * toSize.Width / 100, 200);
                }
            }
        }
        #endregion

        #region Methods
        private void MenuViewController()
        {
            _menuView = new UIView();
            _menuView.BackgroundColor = UIColor.DarkGray;
            _menuView.Frame = new CGRect(0, 0, 75 * UIScreen.MainScreen.Bounds.Width / 100, 736);

            _navigationView = new UIView();
            _navigationView.BackgroundColor = UIColor.FromWhiteAlpha(1, 0.75F);
            _navigationView.Frame = new CGRect(75 * UIScreen.MainScreen.Bounds.Width / 100, 0, 25 * UIScreen.MainScreen.Bounds.Width / 100, 736);

            _imageViewMenu = new UIImageView();
            _imageViewMenu.Image = UIImage.FromBundle("MenuTitleIcon");
            _imageViewMenu.Frame = new CGRect(0, 0, 75 * UIScreen.MainScreen.Bounds.Width / 100, 170);

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

        private void SetUpCollectionView()
        {
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
            TaskListCollectionView.ReloadData();
        }

        [Export("interstitialDidReceiveAd:")]
        public void DidReceiveAd(Interstitial ad)
        {
        }

        [Export("interstitial:didFailToReceiveAdWithError:")]
        public void DidFailToReceiveAd(Interstitial sender, RequestError error)
        {
        }

        [Export("interstitialWillPresentScreen:")]
        public void WillPresentScreen(Interstitial ad)
        {
        }

        [Export("interstitialDidFailToPresentScreen:")]
        public void DidFailToPresentScreen(Interstitial ad)
        {
        }

        [Export("interstitialWillDismissScreen:")]
        public void WillDismissScreen(Interstitial ad)
        {
        }

        [Export("interstitialDidDismissScreen:")]
        public void DidDismissScreen(Interstitial ad)
        {
            //ViewModel.TaskChangeCommand.Execute(null);
        }

        [Export("interstitialWillLeaveApplication:")]
        public void WillLeaveApplication(Interstitial ad)
        {
        }
        #endregion
    }
}