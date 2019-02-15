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
        public LoginView () : base (nameof(LoginView), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.SetNavigationBarHidden(true, false);

            LabelNetworkAccessLoginView.BackgroundColor = UIColor.Red;
            LoginButton.TouchUpInside += ButtonLoginClick;

            var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(LabelNetworkAccessLoginView).For(v => v.Hidden).To(vm => vm.NetworkAccess).WithConversion("Status");
            set.Bind(LoginButton).To(vm => vm.LoginWebViewCommand);
            set.Apply();
        }

        private void ButtonLoginClick(object sender, EventArgs e)
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