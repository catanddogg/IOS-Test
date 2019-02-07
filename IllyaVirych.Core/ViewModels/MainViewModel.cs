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
    public class MainViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ILoginService _iLoginService;         
        public IMvxCommand CurrentMainViewCommand { get; set; }   
        public IMvxCommand TestIOSCommand { get; set; }
        public IMvxCommand MenuViewCommand { get; set; }

        public MainViewModel(IMvxNavigationService navigationService, ILoginService iLoginService)
        {
            _navigationService = navigationService;
            _iLoginService = iLoginService;
            
            CurrentMainViewCommand = new MvxAsyncCommand(CurrentMainView);
            TestIOSCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<LoginViewModel>());
            MenuViewCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<ListTaskViewModel>());
        }

        private async Task CurrentMainView()
        {
            if (_iLoginService.FindAccount == null)
            {
                await _navigationService.Navigate<LoginViewModel>();
            }
            if(_iLoginService.FindAccount != null)
            {
                await _navigationService.Navigate<ListTaskViewModel>();                
            }
        }     
   
    }
}
