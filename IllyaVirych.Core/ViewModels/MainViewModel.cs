using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Plugin.Settings;
using System.Threading.Tasks;

namespace IllyaVirych.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ILoginService _iLoginService;
        private readonly string _currentLoadingId = "id";
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
            if (CrossSettings.Current.Contains(_currentLoadingId) == true)
            {
                CurrentInstagramUser.CurrentInstagramUserId = CrossSettings.Current.GetValueOrDefault("id", string.Empty).ToString();                
                await _navigationService.Navigate<ListTaskViewModel>();
                return;
            }
            await _navigationService.Navigate<LoginViewModel>();
        }        
    }
}
