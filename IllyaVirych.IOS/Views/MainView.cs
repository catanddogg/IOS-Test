using IllyaVirych.Core.ViewModels;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;

namespace IllyaVirych.IOS.Views
{     
    [MvxRootPresentation]
    public class MainView : MvxViewController<MainViewModel>
    {
        #region Variables
        private bool _firstTimePresented = true;
        #endregion

        #region Constructors
        public MainView()
        {
        }
        #endregion

        #region Lifecycle
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            if (_firstTimePresented)
            {
                _firstTimePresented = false;
                ViewModel.CurrentMainViewCommand.Execute(null);
            }
        }
        #endregion
    }
}