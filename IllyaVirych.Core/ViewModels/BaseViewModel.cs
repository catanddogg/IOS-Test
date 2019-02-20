using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace IllyaVirych.Core.ViewModels
{
    public abstract class BaseViewModel: MvxViewModel
    {
        protected readonly IMvxNavigationService _navigationService;
        protected BaseViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }        
    }
    public abstract class BaseViewModel <TParameter> : MvxViewModel <TParameter>
        where TParameter : class
        
    {
        protected readonly IMvxNavigationService _navigationService;
        protected BaseViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
