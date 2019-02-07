using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Auth;

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

        public LoginViewModel(IMvxNavigationService navigationService, ILoginService loginService, ITaskService iTaskService)
        {            
            _iTaskService = iTaskService;
            _loginService = loginService;
            _navigationService = navigationService;            
            LoginCommand = new MvxCommand(_loginService.LoginInstagram);
            ShowListTaskViewCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<ListTaskViewModel>());
            LoginWebViewCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<TestLoginViewModel>());
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

        public OAuth2Authenticator Authhenticator
        {
            get
            {
                return _loginService.Authhenticator();
            }
        }
    }
}
