using IllyaVirych.Core.Helper;
using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Messenger;
using IllyaVirych.Core.Models;
using IllyaVirych.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace IllyaVirych.Core.ViewModels
{
    public class TaskViewModel : BaseViewModel<TaskItem>
    {
        #region Variables
        private readonly IMvxMessenger _messenger;     
        private int _idTask;
        private string _nameTask;
        private string _descriptionTask;
        private bool _statusTask;
        private bool _enableStatusNameTask;
        private string _userId;
        private double _lalitudeMarkerResult;
        private double _longitudeMarkerResult;
        private readonly MvxSubscriptionToken _token;
        private IWebApiService _webApiService;
        private IAlertService _alertService;
        private bool _changedNetworkAccess;
        private ChatMessage _chatMessage;
        private string _roomName = "TaskHub";
        private readonly string _saveTaskAlert = "Enter Name Task!";
        private readonly string _deleteMarkerAlertHasMarker = "Task marker has been deleted!";
        private readonly string _deleteMarkerAlert = "Task have not marker!";
        private readonly string _networkAccessAlert = "You do not have network access!";
        #endregion

        #region Constructors
        public TaskViewModel(IMvxNavigationService navigationService, IMvxMessenger messenger, IWebApiService webApiService, IAlertService alertService/*, ISignalR signalR*/)
            : base (navigationService)
        {
            _webApiService = webApiService;
            _messenger = messenger;
            _alertService = alertService;
            _token = messenger.Subscribe<MapMessenger>(OnLocationMessage);
            SaveTaskCommand = new MvxAsyncCommand(SaveTask);
            DeleteTaskCommand = new MvxAsyncCommand(DeleteTask);
            BackTaskCommand = new MvxAsyncCommand(BackTask);
            DeleteMarkerMapCommand = new MvxCommand(DeleteMarkerMap);
            MapCommand = new MvxAsyncCommand(CreateMarkerMap);
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                ChangedNetworkAccess = true;
            }
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        #endregion

        #region Lifecycle
        public override Task Initialize()
        {
            return base.Initialize();
        }


        public override void Prepare(TaskItem parameter)
        {
            if (parameter != null)
            {
                EnableStatusNameTask = false;
                IdTask = parameter.Id;
                NameTask = parameter.NameTask;
                UserId = parameter.UserId;
                DescriptionTask = parameter.DescriptionTask;
                StatusTask = parameter.StatusTask;
                LalitudeMarkerResult = parameter.LalitudeMarker;
                LongitudeMarkerResult = parameter.LongitudeMarker;
                return;
            }
            _userId = UserInstagramId.GetUserId();
            EnableStatusNameTask = true;
        }
        #endregion

        #region Properties
        public double LalitudeMarkerResult
        {
            get
            {
                return _lalitudeMarkerResult;
            }
            set
            {
                _lalitudeMarkerResult = value;
                RaisePropertyChanged(() => LalitudeMarkerResult);
            }
        }

        public double LongitudeMarkerResult
        {
            get
            {
                return _longitudeMarkerResult;
            }
            set
            {
                _longitudeMarkerResult = value;
                RaisePropertyChanged(() => LongitudeMarkerResult);
            }

        }

        public int IdTask
        {
            get => _idTask;
            set
            {
                _idTask = value;
                RaisePropertyChanged(() => IdTask);
            }
        }

        public string NameTask
        {
            get => _nameTask;
            set
            {
                _nameTask = value;
                RaisePropertyChanged(() => NameTask);
            }
        }

        public string DescriptionTask
        {
            get => _descriptionTask;
            set
            {
                _descriptionTask = value;
                RaisePropertyChanged(() => DescriptionTask);
            }
        }

        public string UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                RaisePropertyChanged(() => UserId);
            }
        }

        public bool StatusTask
        {
            get => _statusTask;
            set
            {
                _statusTask = value;
                RaisePropertyChanged(() => StatusTask);
            }
        }

        public bool EnableStatusNameTask
        {
            get => _enableStatusNameTask;
            set
            {
                _enableStatusNameTask = value;
                RaisePropertyChanged(() => EnableStatusNameTask);
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
        #endregion

        #region Commands
        public IMvxCommand SaveTaskCommand { get; set; }
        public IMvxCommand DeleteTaskCommand { get; set; }
        public IMvxCommand BackTaskCommand { get; set; }
        public IMvxCommand DeleteMarkerMapCommand { get; set; }
        public IMvxCommand MapCommand { get; set; }
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

        private void OnLocationMessage(MapMessenger mapMesseger)
        {
            IdTask = mapMesseger.IdTask;
            LalitudeMarkerResult = mapMesseger.LalitudeMarkerResult;
            LongitudeMarkerResult = mapMesseger.LongitudeMarkerResult;
            NameTask = mapMesseger.NameTaskResult;
            DescriptionTask = mapMesseger.DescriptionTaskResult;
            StatusTask = mapMesseger.StatusTaskResult;
            EnableStatusNameTask = true;
        }

        private void DeleteMarkerMap()
        {
            if (_changedNetworkAccess == false)
            {
                _alertService.ShowAlert(_networkAccessAlert);
                return;
            }
            if (_lalitudeMarkerResult != 0 & _longitudeMarkerResult != 0)
            {
                _alertService.ShowAlert(_deleteMarkerAlertHasMarker);
                LalitudeMarkerResult = 0;
                LongitudeMarkerResult = 0;
                return;
            }
            _alertService.ShowAlert(_deleteMarkerAlert); 
        }

        private async Task CreateMarkerMap()
        {
            await _navigationService.Navigate<MapsViewModel>();
            var message = new MapMessenger(this,
                IdTask,
                LalitudeMarkerResult,
                LongitudeMarkerResult,
                NameTask,
                DescriptionTask,
                StatusTask
                  );            
            _messenger.Publish(message);
            _messenger.Unsubscribe<MapMessenger>(_token);
        }

        private async Task BackTask()
        {
            var result = await _navigationService.Navigate<ListTaskViewModel>();
        }

        private async Task DeleteTask()
        {
            if (_changedNetworkAccess == false)
            {
                _alertService.ShowAlert(_networkAccessAlert);
                return;
            }
            if (_changedNetworkAccess == true)
            {
                await _webApiService.DeleteTaskItem(IdTask);
                await _navigationService.Close(this);
            }
        }

        private async Task SaveTask()
        {
            if (_changedNetworkAccess == false)
            {
                _alertService.ShowAlert(_networkAccessAlert);
                return;
            }
            if (NameTask == null || NameTask.Trim() == string.Empty)
            {
                _alertService.ShowAlert(_saveTaskAlert);
                return;
            }
            if (NameTask != null & NameTask.Trim() != string.Empty)
            {
                UserId = UserInstagramId.GetUserId();
                TaskItem taskItem = new TaskItem(IdTask, NameTask.Trim(), DescriptionTask, StatusTask, UserId, LalitudeMarkerResult, LongitudeMarkerResult);
                await _webApiService.SaveTaskItem(taskItem, IdTask);
            }
            await _navigationService.Navigate<ListTaskViewModel>();

        }

        private void _chatServices_OnMessageReceived(object sender, ChatMessage e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
