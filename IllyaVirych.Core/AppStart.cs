using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
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
            //if (_iLoginSrvice.FindAccount != null)
            //{
            //    CurrentInstagramUser.CurrentInstagramUserId = _iLoginSrvice.FindAccount.Properties["id"];
            //    NavigationService.Navigate<MainViewModel>();
            //    return _navigationService.Navigate<ListTaskViewModel>();
            //}
            //NavigationService.Navigate<MainViewModel>();
            //return _navigationService.Navigate<LoginViewModel>();
            return NavigationService.Navigate<MainViewModel>();
            //return _navigationService.Navigate<LoginViewModel>();
        }
    }
}
