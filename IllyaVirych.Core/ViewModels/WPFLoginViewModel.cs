using IllyaVirych.Core.Interface;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using static IllyaVirych.Core.Models.Enums;

namespace IllyaVirych.Core.ViewModels
{
    //WPF
    public class WPFLoginViewModel : BaseViewModel
    {
        private const string ServerURI = "http://localhost:65015/chat";
        private ISingnalRPersistensConnectionService _singnalRPersistensConnectionService;

        public WPFLoginViewModel(IMvxNavigationService navigationService, ISingnalRPersistensConnectionService singnalRPersistensConnectionService)
            : base(navigationService)
        {
            _singnalRPersistensConnectionService = singnalRPersistensConnectionService;
            LoginCommand = new MvxCommand(Login);
        }

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        private string _statusText;
        public string StatusText
        {
            get
            {
                return _statusText;
            }
            set
            {
                _statusText = value;
                RaisePropertyChanged(() => StatusText);
            }
        }

        private bool _statusTextVisibility;
        public bool StatusTextVisibility
        {
            get
            {
                return _statusTextVisibility;
            }
            set
            {
                _statusTextVisibility = value;
                RaisePropertyChanged(() => StatusTextVisibility);
            }
        }

        public IMvxCommand LoginCommand { get; set; }

        private void Login()
        {
            if (!String.IsNullOrEmpty(UserName))
            {
                StatusTextVisibility = true;
                StatusText = "Connecting to server...";
                ConnectAsync();
            }
        }

        private async void ConnectAsync()
        {
            try
            {
                await _singnalRPersistensConnectionService.ConnectServerAsync(ServerURI);
                await _singnalRPersistensConnectionService.SendMessageAsync(UserName, null, MethodType.AddUser, null, ReceiverType.None);
                await _navigationService.Navigate<TestWPFViewModel, string>(UserName);
            }
            catch (Exception e)
            {
                StatusText = "Unable to connect to server: Start server before connecting clients.";
                return;
            }
        }
    }
}
