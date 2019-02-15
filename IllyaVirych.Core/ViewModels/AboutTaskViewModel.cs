using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace IllyaVirych.Core.ViewModels
{
    public class AboutTaskViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public IMvxCommand BackTaskCommand { get; set; }
        private NetworkAccess _networkAccess;

        public AboutTaskViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            _networkAccess = Connectivity.NetworkAccess;
            BackTaskCommand = new MvxAsyncCommand(BackTask);
        }

        private async Task BackTask()
        {
            await _navigationService.Navigate<ListTaskViewModel>();           
        }

        public NetworkAccess NetworkAccess
        {
            get
            {
                return _networkAccess;
            }
            set
            {
                _networkAccess = value;
                RaisePropertyChanged(() => NetworkAccess);
            }
        }
    }
}
