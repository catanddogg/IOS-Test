using IllyaVirych.Core.Interface;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IllyaVirych.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ILoginService _iLoginService;
        public IMvxCommand TaskCreateViewCommand { get; set; }
        public IMvxCommand AboutViewCommand { get; set; }
        public IMvxCommand LoginViewCommand { get; set; }
        public IMvxCommand ListTaskViewCommand { get; set; }

        public MenuViewModel(IMvxNavigationService navigationService, ILoginService iLoginService)
        {
            _navigationService = navigationService;
            _iLoginService = iLoginService;
            TaskCreateViewCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<TaskViewModel>());
            AboutViewCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<AboutTaskViewModel>());
            ListTaskViewCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<ListTaskViewModel>());
            LoginViewCommand = new MvxAsyncCommand(LogoutInstagram);
        }
        private async Task LogoutInstagram()
        {
            _iLoginService.LogoutInstagram();
            var result = await _navigationService.Navigate<LoginViewModel>();
        }
    }
}
