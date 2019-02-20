using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace IllyaVirych.Core.ViewModels
{
    public class AboutTaskViewModel : BaseViewModel
    {
        #region Variables
        private readonly IMvxNavigationService _navigationService;
        private bool _changedNetworkAccess;
        #endregion

        #region Constructors
        public AboutTaskViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                ChangedNetworkAccess = true;
            }          
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            BackTaskCommand = new MvxAsyncCommand(BackTask);
        }
        #endregion
        
        #region Properties
        public bool ChangedNetworkAccess
        {
            get
            {

                return _changedNetworkAccess;

            }
            set
            {
                _changedNetworkAccess = value;
                RaisePropertyChanged(() => ChangedNetworkAccess);
            }
        }
        #endregion

        #region Commands
        public IMvxCommand BackTaskCommand { get; set; }
        #endregion

        #region Methods
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
            {
                ChangedNetworkAccess = true;
                return;
            }
            ChangedNetworkAccess = false;
        }

        private async Task BackTask()
        {
            await _navigationService.Navigate<ListTaskViewModel>();           
        }
        #endregion

    }
}
