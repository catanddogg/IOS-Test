using Firebase.RemoteConfig;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using System.Collections.Generic;
using UIKit;
using Xamarin.Essentials;
using Google.MobileAds;
using CoreGraphics;
using MvvmCross;
using IllyaVirych.Core.Interface;

namespace IllyaVirych.IOS.Views
{
    [MvxModalPresentation(WrapInNavigationController = true)]
    public partial class LoginView : MvxViewController<LoginViewModel>
    {
        private BannerView _adsBannerView;

        #region Constructors
        public LoginView() : base(nameof(LoginView), null)
        {
        }
        #endregion

        #region Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.SetNavigationBarHidden(true, false);
            LabelNetworkAccessLoginView.BackgroundColor = UIColor.Red;

            var firebasePredictionsService = Mvx.Resolve<IFirebasePredictionsService>();
            bool showAds = firebasePredictionsService.InitializeFirebasePredictions();
            if (showAds)
            {
                UIViewController viewCtrl = null;
                foreach (UIWindow v in UIApplication.SharedApplication.Windows)
                {
                    if (v.RootViewController != null)
                    {
                        viewCtrl = v.RootViewController;
                    }
                }

                _adsBannerView = new BannerView(size: AdSizeCons.Banner, origin: new CGPoint(25, UIScreen.MainScreen.Bounds.Height - 50))
                {
                    AdUnitID = "ca-app-pub-3940256099942544/2934735716",
                    RootViewController = viewCtrl
                };

                View.AddSubview(_adsBannerView);
                _adsBannerView.LoadRequest(Google.MobileAds.Request.GetDefaultRequest());
            }
           
            var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(LabelNetworkAccessLoginView).For(v => v.Hidden).To(vm => vm.ChangedNetworkAccess);
            set.Bind(LoginButton).To(vm => vm.LoginWebViewCommand);
            set.Apply();
        }
        #endregion
    }
}