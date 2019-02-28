using IllyaVirych.Core.Helper;
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
        #region Variables
        private readonly ILoginService _loginService;
        private readonly string _currentLoadingId = "id";
        #endregion

        #region Constructors

        public MainViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            CurrentMainViewCommand = new MvxAsyncCommand(CurrentMainView);
            ListTaskViewCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<ListTaskViewModel>());
            MenuViewCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MenuViewModel>());
        }
        #endregion

        #region Commands
        public IMvxCommand CurrentMainViewCommand { get; set; }
        public IMvxCommand ListTaskViewCommand { get; set; }
        public IMvxCommand MenuViewCommand { get; set; }
        #endregion

        #region Methods
        private async Task CurrentMainView()
        {
            if (CrossSettings.Current.Contains(_currentLoadingId) == true)
            {
                await _navigationService.Navigate<ListTaskViewModel>();
                return;
            }              
            await _navigationService.Navigate<LoginViewModel>();
        }
        #endregion
    }
}
