using CoreGraphics;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace IllyaVirych.IOS.Views
{
    [MvxModalPresentation(WrapInNavigationController = true)]
    public partial class AboutView : MvxViewController<AboutTaskViewModel>
    {
        public AboutView () : base (nameof(AboutView), null)
        {
        }

        private UIButton _buttonBack;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "About Task";
            NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(0, 127, 70);
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.Black };

            _buttonBack = new UIButton(UIButtonType.Custom);
            _buttonBack.Frame = new CGRect(0, 0, 40, 40);
            _buttonBack.SetImage(UIImage.FromBundle("BackIcon"), UIControlState.Normal);
            this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(_buttonBack), false);

            var set = this.CreateBindingSet<AboutView, AboutTaskViewModel>();
            set.Bind(_buttonBack).To(vm => vm.BackTaskCommand);
            set.Apply();
        }
    }
}