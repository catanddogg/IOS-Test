using IllyaVirych.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

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

            LabelNetworkAccessLoginView.BackgroundColor = UIColor.Gray;

            var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(LabelNetworkAccessLoginView).For(v => v.Hidden).To(vm => vm.NetworkAccess).WithConversion("Status");
            set.Bind(LoginButton).To(vm => vm.LoginWebViewCommand);
            set.Apply();
        }       
    }
}