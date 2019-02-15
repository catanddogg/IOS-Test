using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using Xamarin.Auth;
using Xamarin.Essentials;

namespace IllyaVirych.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private ILoginService _loginService;
        private ITaskService _iTaskService;
        public IMvxCommand ShowListTaskViewCommand { get; set; } 
        public IMvxCommand LoginCommand { get; set; }
        public IMvxCommand LoginWebViewCommand { get; set; }
        private string _userId;
        private NetworkAccess _networkAccess;

        public LoginViewModel(IMvxNavigationService navigationService, ILoginService loginService, ITaskService iTaskService)
        {            
            _iTaskService = iTaskService;
            _loginService = loginService;
            _navigationService = navigationService;
            _networkAccess = Connectivity.NetworkAccess;
            LoginCommand = new MvxCommand(_loginService.LoginInstagram);
            ShowListTaskViewCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<ListTaskViewModel>());
            LoginWebViewCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<LoginWebViewModel>());
            _loginService.OnLoggedInHandler = new Action(() =>
            {
                CreateNewUser();
                ShowListTaskViewCommand.Execute(null);
            });
        }
        public override void ViewAppearing()
        {
            if (CurrentInstagramUser.CurrentInstagramUserId == string.Empty)
            {
                return;
            }
            User user = _iTaskService.GetUser(CurrentInstagramUser.CurrentInstagramUserId);
            if (user != null)
            {
                UserId = user.UserId;
            }
        }
        public void CreateNewUser()
        {
            UserId = CurrentInstagramUser.CurrentInstagramUserId;
            List<User> users = _iTaskService.GetAllUsers();
            User user = new User(UserId);
            for(int i = 0; i <users.Count; i++)
            {
                if (users[i].UserId == user.UserId)
                {
                    return;
                }
            }
            _iTaskService.InsertUser(user);            
        }

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

        public OAuth2Authenticator Authhenticator
        {
            get
            {
                return _loginService.Authhenticator();
            }
        }
    }
}
