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
    public partial class LoginView : MvxViewController<LoginViewModel>
    {
    
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

            var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(LabelNetworkAccessLoginView).For(v => v.Hidden).To(vm => vm.ChangedNetworkAccess);
            set.Bind(LoginButton).To(vm => vm.LoginWebViewCommand);
            set.Apply();
        }
        #endregion
    }
}