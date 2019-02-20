using IllyaVirych.Core.Interface;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace IllyaVirych.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        #region Variables
        private readonly IMvxNavigationService _navigationService;
        private readonly ILoginService _loginService;
        private IAlertService _alertService;      
        private bool _changedNetworkAccess;
        private readonly string _networkAccessAlert = "You do not have network access!";
        #endregion

        #region Constructors
        public MenuViewModel(IMvxNavigationService navigationService, ILoginService loginService, IAlertService alertService)
            :base(navigationService)        
        {
            _navigationService = navigationService;
            _loginService = loginService;
            _alertService = alertService;
            TaskCreateViewCommand = new MvxAsyncCommand(TaskCreateView);
            AboutViewCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<AboutTaskViewModel>());
            ListTaskViewCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<ListTaskViewModel>());
            LoginViewCommand = new MvxAsyncCommand(LogoutInstagram);
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                ChangedNetworkAccess = true;
            }
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
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
        public IMvxCommand TaskCreateViewCommand { get; set; }
        public IMvxCommand AboutViewCommand { get; set; }
        public IMvxCommand LoginViewCommand { get; set; }
        public IMvxCommand ListTaskViewCommand { get; set; }
        #endregion

        #region Methods
        private async Task TaskCreateView()
        {
            if (_changedNetworkAccess == false)
            {
                _alertService.ShowAlert(_networkAccessAlert);
                return;
            }
            await _navigationService.Navigate<TaskViewModel>();
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
            {
                ChangedNetworkAccess = true;
                return;
            }
            ChangedNetworkAccess = false;
        }

        private async Task LogoutInstagram()
        {
            _loginService.LogoutInstagram();
            var result = await _navigationService.Navigate<LoginViewModel>();
        }
        #endregion
    }
}
