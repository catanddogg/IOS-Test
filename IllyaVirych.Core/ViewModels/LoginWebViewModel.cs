using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IllyaVirych.Core.ViewModels
{
    public class LoginWebViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public IMvxCommand ListTaskTaskCommand { get; set; }
        private ILoginService _iLoginService;
        private ITaskService _iTaskService;
        public IMvxCommand LoginCommand { get; set; }
        public IMvxCommand LoginNaVigationAndCreateCommand { get; set; }
        private string _userId;

        public LoginWebViewModel(IMvxNavigationService navigationService, ILoginService iLoginService, ITaskService iTaskService)
        {
            _iTaskService = iTaskService;
            _iLoginService = iLoginService;
            _navigationService = navigationService;
            ListTaskTaskCommand = new MvxAsyncCommand(BackTask);
            LoginCommand = new MvxCommand(_iLoginService.LoginInstagram);
            LoginNaVigationAndCreateCommand = new MvxAsyncCommand(NavigationAndCreate);
           
        }

        private async Task NavigationAndCreate()
        {
            CreateNewUser();
            await _navigationService.Navigate<ListTaskViewModel>();
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
            for (int i = 0; i < users.Count; i++)
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

        private async Task BackTask()
        {            
            await _navigationService.Navigate<ListTaskViewModel>();
        }
    }
}
