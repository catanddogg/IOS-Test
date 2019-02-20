using IllyaVirych.Core.Helper;
using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Essentials;

namespace IllyaVirych.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Variables
        private readonly IMvxNavigationService _navigationService;
        private ILoginService _loginService;
        private ITaskService _taskService;
      
        private string _userId;
        private IUserService _userService;
        private IAlertService _alertService;
        private bool _changedNetworkAccess;
        private readonly string _networkAccessAlert = "You do not have network access!";
        #endregion

        #region Constructors
        public LoginViewModel(IMvxNavigationService navigationService, ILoginService loginService, ITaskService taskService, IUserService userService, IAlertService alertService)
        {
            _taskService = taskService;
            _loginService = loginService;
            _navigationService = navigationService;
            _userService = userService;
            _alertService = alertService;
            LoginCommand = new MvxCommand(_loginService.LoginInstagram);
            ShowListTaskViewCommand = new MvxAsyncCommand(LoginWebView);
            LoginWebViewCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<LoginWebViewModel>());
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                ChangedNetworkAccess = true;
            }
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            _loginService.OnLoggedInHandler = new Action(() =>
            {
                CreateNewUser();
                ShowListTaskViewCommand.Execute(null);
            });
        }
        #endregion
   
        #region Lifecycle
        public override void ViewAppearing()
        {
            if (UserInstagramId.UserId() == string.Empty)
            {
                return;
            }
            User user = _userService.GetUser(UserInstagramId.UserId());
            if (user != null)
            {
                UserId = user.UserId;
            }
        }
        #endregion

        #region Properties
        public string UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
                RaisePropertyChanged(() => UserId);
            }
        }

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

        public OAuth2Authenticator Authhenticator
        {
            get
            {
                return _loginService.Authhenticator();
            }
        }
        #endregion

        #region Commands
        public IMvxCommand ShowListTaskViewCommand { get; set; }
        public IMvxCommand LoginCommand { get; set; }
        public IMvxCommand LoginWebViewCommand { get; set; }
        #endregion

        #region Methods
        private async Task LoginWebView()
        {
            if (_changedNetworkAccess == true)
            {
                await _navigationService.Navigate<ListTaskViewModel>();
                return;
            }
            _alertService.ShowAlert(_networkAccessAlert);
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


        public void CreateNewUser()
        {
            UserId = UserInstagramId.UserId();
            List<User> users = _userService.GetAllUsers();
            User user = new User(UserId);
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].UserId == user.UserId)
                {
                    return;
                }
            }
            _userService.InsertUser(user);
        }
        #endregion
    }
}
