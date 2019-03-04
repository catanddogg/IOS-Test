using IllyaVirych.Core.Helper;
using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using IllyaVirych.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.UI;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace IllyaVirych.Core.ViewModels
{
    public class ListTaskViewModel : BaseViewModel
    {
        #region Variables
        private MvxObservableCollection<TaskItem> _items;
        private bool _refreshTaskCollection;
        private readonly ITaskService _taskService;
        private ILoginService _loginService;
        private IWebApiService _wepApiService;
        private IAlertService _alertService;
        private bool _changedNetworkAccess;
        private readonly string _networkAccessAlert = "You do not have network access!";
        #endregion

        #region Constructors
        public ListTaskViewModel(IMvxNavigationService navigationService, ITaskService taskService, ILoginService loginService, IWebApiService wepApiService, IAlertService alertService) 
            :base(navigationService)
        {
            _loginService = loginService;
            _taskService = taskService;
            _wepApiService = wepApiService;
            _alertService = alertService;
            Items = new MvxObservableCollection<TaskItem>();
            TaskCreateCommand = new MvxAsyncCommand<TaskItem>(TaskCreate);
            TaskChangeCommand = new MvxAsyncCommand<TaskItem>(TaskChange);
            ShowAboutCommand = new MvxAsyncCommand(async () => await navigationService.Navigate<AboutTaskViewModel>());
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await navigationService.Navigate<MenuViewModel>());
            LoginViewCommand = new MvxAsyncCommand(LogoutInstagram);
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                ChangedNetworkAccess = true;
            }
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        #endregion

        #region Lifecycle
        public override void ViewAppearing()
        {
            base.ViewAppearing();
            AppearingData();            
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

        public bool RefreshTaskCollection
        {
            get
            {
                return _refreshTaskCollection;
            }
            set
            {
                _refreshTaskCollection = value;
                RaisePropertyChanged(() => RefreshTaskCollection);
            }
        }
        public MvxObservableCollection<TaskItem> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }
        #endregion

        #region Commands
        public IMvxCommand ShowMenuViewModelCommand { get; set; }
        public IMvxCommand<TaskItem> TaskCreateCommand { get; set; }
        public IMvxCommand<TaskItem> TaskChangeCommand { get; set; }
        public IMvxCommand ShowAboutCommand { get; set; }
        public IMvxCommand LoginViewCommand { get; set; }
        private MvxCommand _refreshTaskCommand;

        public MvxCommand RefreshTaskCommand
        {
            get
            {
                return _refreshTaskCommand = _refreshTaskCommand ?? new MvxCommand(RefreshTask);
            }
        }
        #endregion

        #region Methods

        public async void AppearingData()
        {
            RefreshTaskCollection = true;
            if (_changedNetworkAccess == true)
            {
                await _wepApiService.RefreshDataAsync();
                LoadData();
            }
            if (_changedNetworkAccess == false)
            {
                LoadData();
            }
            RefreshTaskCollection = false;
        }

        private void LoadData()
        {
            var list = _taskService.GetUserTasks(UserInstagramId.GetUserId());
            Items = new MvxObservableCollection<TaskItem>(list);
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
            await _navigationService.Navigate<LoginViewModel>();
        }

        private async Task TaskCreate(TaskItem task)
        {
            if (_changedNetworkAccess == true)
            {
                var result = await _navigationService.Navigate<TaskViewModel, TaskItem>(task);
                return;
            }
            _alertService.ShowAlert(_networkAccessAlert);
        }

        private async Task TaskChange(TaskItem task)
        {
                var result = await _navigationService.Navigate<TaskViewModel, TaskItem>(task);
        }

        public async void RefreshTask()
        {
            RefreshTaskCollection = true;
            if (_changedNetworkAccess == true)
            {
                await _wepApiService.RefreshDataAsync();
                LoadData();
            }
            if (_changedNetworkAccess == false)
            {
                LoadData();
            }
            RefreshTaskCollection = false;
        }
        #endregion 
    }
}
