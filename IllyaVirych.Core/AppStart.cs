using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace IllyaVirych.Core
{
    public class AppStart : MvxAppStart 
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ILoginService _iLoginSrvice;

        public AppStart(IMvxApplication app, IMvxNavigationService navigationService, ILoginService iLoginService)
            : base(app, navigationService)
        {
            _navigationService = navigationService;
            _iLoginSrvice = iLoginService;
        }

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            return NavigationService.Navigate<MainViewModel>();
        }
    }
}
