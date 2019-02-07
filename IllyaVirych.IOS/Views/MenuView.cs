using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using TZStackView;
using UIKit;

namespace IllyaVirych.IOS.Views
{
    public partial class MenuView : MvxViewController<MenuViewModel>
    {
        public MenuView () : base (nameof(MenuView), null)
        {
        }
        //private StackView _options;
        //private MenuOption _listTaskMenu, _aboutTaskMenu, _layoutMenu;
        //public static UIView f;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //View.BackgroundColor = UIColor.Black;

            Title = "Menu";

            //f = MenuViewController;
                        
            ImageViewMenu.Image = UIImage.FromBundle("mqse9xro.jpg");

            //var view = MenuViewController;
            //view.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;

            //View.BackgroundColor = UIColor.Clear;
            //MenuViewController.Opaque = true;       
            NavigationViewMenu.BackgroundColor = UIColor.Clear;
            MenuViewController.BackgroundColor= UIColor.FromRGBA(0, 0, 0, 1F);
            UITapGestureRecognizer tapGesture = new UITapGestureRecognizer(() =>
            {
                ViewModel.ListTaskViewCommand.Execute(null);
            });
            NavigationViewMenu.UserInteractionEnabled = true;
            NavigationViewMenu.AddGestureRecognizer(tapGesture);

            UIView.Transition(this.View, 0.75, UIViewAnimationOptions.CurveEaseOut, () => {
                
            }, null);

            var set = this.CreateBindingSet<MenuView, MenuViewModel>();
            set.Bind(CreateTaskButton).To(vm => vm.TaskCreateViewCommand);
            set.Bind(NavigationViewMenu).For("Clicked").To(vm => vm.ListTaskViewCommand);
                     
            set.Apply();


            //var controller = this.View;
            //StackViewMenu = new UIStackView
            //{
            //    Axis = UILayoutConstraintAxis.Vertical
            //};

            //_listTaskMenu = new MenuOption();
            //_listTaskMenu.Image.Image = UIImage.FromBundle("iconsdelete48");
            //_listTaskMenu.Label.Text = "Create Task";
            //_aboutTaskMenu = new MenuOption();
            //_aboutTaskMenu.Image.Image = UIImage.FromBundle("iconsdelete48");
            //_aboutTaskMenu.Label.Text = "About Task";
            //_layoutMenu = new MenuOption();
            //_layoutMenu.Image.Image = UIImage.FromBundle("iconsdelete48");
            //_layoutMenu.Label.Text = "Logout";

            ////View.AddSubviews(_options);
            ////View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            //StackViewMenu.AddArrangedSubview(_listTaskMenu);
            //StackViewMenu.AddArrangedSubview(_aboutTaskMenu);
            //StackViewMenu.AddArrangedSubview(_layoutMenu);


        }

        //public override void ViewWillAppear(bool animated)
        //{
        //    base.ViewWillAppear(animated);
        //    View.BackgroundColor = UIColor.Red.ColorWithAlpha(0.5F);
        //}
    }
}