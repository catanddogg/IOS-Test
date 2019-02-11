using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace IllyaVirych.Core.ViewModels
{
    public class AboutTaskViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public IMvxCommand BackTaskCommand { get; set; }

        public AboutTaskViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            BackTaskCommand = new MvxAsyncCommand(BackTask);
        }

        private async Task BackTask()
        {
            await _navigationService.Navigate<ListTaskViewModel>();           
        }
    }
}
