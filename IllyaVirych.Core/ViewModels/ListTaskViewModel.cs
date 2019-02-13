﻿ using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using IllyaVirych.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace IllyaVirych.Core.ViewModels
{
    public class ListTaskViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ITaskService _iTaskService;
        private MvxObservableCollection<TaskItem> _items;
        public IMvxCommand ShowMenuViewModelCommand { get; set; }
        public IMvxCommand<TaskItem> TaskCreateCommand { get; set; }
        public IMvxCommand ShowAboutCommand { get; set; }
        public IMvxCommand LoginViewCommand { get; set; }
        private bool _refreshTaskCollection;
        private MvxCommand _refreshTaskCommand;
        private ILoginService _iLoginService;
        private IWebApiService _iWepApiService;

        public ListTaskViewModel(IMvxNavigationService navigationService, ITaskService iTaskService, ILoginService iLoginService, IWebApiService iWepApiService) 
        {
            _iLoginService = iLoginService;
            _navigationService = navigationService;
            _iTaskService = iTaskService;
            _iWepApiService = iWepApiService;
            Items = new MvxObservableCollection<TaskItem>();
            TaskCreateCommand = new MvxAsyncCommand<TaskItem>(TaskCreate);
            ShowAboutCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<AboutTaskViewModel>());
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MenuViewModel>());
            LoginViewCommand = new MvxAsyncCommand(LogoutInstagram);
        }

        private async Task LogoutInstagram()
        {
            _iLoginService.LogoutInstagram();
            await _navigationService.Navigate<LoginViewModel>();
        }

        public override void ViewAppearing()
        {
            _iWepApiService.RefreshDataAsync();
            var list = _iTaskService.GetUserTasks(CurrentInstagramUser.CurrentInstagramUserId);
            Items = new MvxObservableCollection<TaskItem>(list);
            RaisePropertyChanged(() => Items);
            base.ViewAppearing();
        }


        public MvxCommand RefreshTaskCommand
        {
            get
            {
                return _refreshTaskCommand = _refreshTaskCommand ?? new MvxCommand(RefreshTask);
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

        private async Task TaskCreate(TaskItem task)
        {
            var result = await _navigationService.Navigate<TaskViewModel, TaskItem>(task);
        }

        public void RefreshTask()
        {
            RefreshTaskCollection = true;
            var list = _iTaskService.GetUserTasks(CurrentInstagramUser.CurrentInstagramUserId);
            Items = new MvxObservableCollection<TaskItem>(list);
            RefreshTaskCollection = false;
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
    }
}
