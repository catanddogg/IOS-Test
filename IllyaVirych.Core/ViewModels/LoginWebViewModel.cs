using IllyaVirych.Core.Helper;
using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace IllyaVirych.Core.ViewModels
{
    public class LoginWebViewModel : BaseViewModel
    {
        #region Variables
        public IMvxCommand ListTaskTaskCommand { get; set; }
        private ILoginService _loginService;
        private IUserService _userService;
        private string _userId;
        private bool _changedNetworkAccess;

        #endregion

        #region Constructors
        public LoginWebViewModel(IMvxNavigationService navigationService, ILoginService loginService, IUserService userService)
            : base(navigationService)
        {
            _loginService = loginService;
            _userService = userService;
            ListTaskTaskCommand = new MvxAsyncCommand(BackTask);
            LoginCommand = new MvxCommand(_loginService.LoginInstagram);
            LoginNaVigationAndCreateCommand = new MvxAsyncCommand(NavigationAndCreate);

        }
        #endregion

        #region Lifecycle
        public override void ViewAppearing()
        {
            if (UserInstagramId.GetUserId() == string.Empty)
            {
                return;
            }
            User user = _userService.GetUser(UserInstagramId.GetUserId());
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
        #endregion

        #region Commands
        public IMvxCommand LoginCommand { get; set; }
        public IMvxCommand LoginNaVigationAndCreateCommand { get; set; }
        #endregion

        #region Methods
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
            {
                _changedNetworkAccess = true;
                return;
            }
            _changedNetworkAccess = false;
        }

        private async Task NavigationAndCreate()
        {
            CreateNewUser();
            await _navigationService.Navigate<ListTaskViewModel>();
        }



        public void CreateNewUser()
        {
            UserId = UserInstagramId.GetUserId();
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



        private async Task BackTask()
        {
            await _navigationService.Navigate<ListTaskViewModel>();
        }
        #endregion
    }
}
