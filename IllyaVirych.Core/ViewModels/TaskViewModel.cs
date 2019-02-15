using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Messenger;
using IllyaVirych.Core.Models;
using IllyaVirych.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace IllyaVirych.Core.ViewModels
{
    public class TaskViewModel : BaseViewModel<TaskItem>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IMvxMessenger _messenger;
        private readonly ITaskService _iTaskService;
        public IMvxCommand SaveTaskCommand { get; set; }
        public IMvxCommand DeleteTaskCommand { get; set; }
        public IMvxCommand BackTaskCommand { get; set; }
        public IMvxCommand DeleteMarkerMapCommand { get; set; }
        public IMvxCommand MapCommand { get; set; }
        private int _idTask;
        private string _nameTask;
        private string _descriptionTask;
        private bool _statusTask;
        private bool _enableStatusNameTask;
        private string _userId;
        private double _lalitudeMarkerResult;
        private double _longitudeMarkerResult;       
        private readonly MvxSubscriptionToken _token;
        private IWebApiService _iWebApiService;
        private NetworkAccess _networkAccess;

        public TaskViewModel(IMvxNavigationService navigationService, ITaskService iTaskService, IMvxMessenger messenger, IWebApiService iWebApiService)
        {            
            _navigationService = navigationService;
            _iTaskService = iTaskService;
            _iWebApiService = iWebApiService; 
            _messenger = messenger;
            _networkAccess = Connectivity.NetworkAccess;
            _token = messenger.Subscribe<MapMessenger>(OnLocationMessage);
            SaveTaskCommand = new MvxAsyncCommand(SaveTask);
            DeleteTaskCommand = new MvxAsyncCommand(DeleteTask);
            BackTaskCommand = new MvxAsyncCommand(BackTask);
            DeleteMarkerMapCommand = new MvxCommand(DeleteMarkerMap);
            MapCommand = new MvxAsyncCommand(CreateMarkerMap);
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
            if (_networkAccess == NetworkAccess.Internet)
            {
                LalitudeMarkerResult = 0;
                LongitudeMarkerResult = 0;
            }
        }

        private async Task CreateMarkerMap()
        {
            if (_networkAccess == NetworkAccess.Internet)
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
                UserId = CurrentInstagramUser.CurrentInstagramUserId;
                _messenger.Publish(message);
                _messenger.Unsubscribe<MapMessenger>(_token);
            }
        }

        private async Task BackTask()
        {
            var result = await _navigationService.Navigate<ListTaskViewModel>();            
        }

        private async Task DeleteTask()
        {
            if (_networkAccess == NetworkAccess.Internet)
            {
                await _iWebApiService.DeleteTaskItem(IdTask);
                await _navigationService.Close(this);
            }
        }

        private async Task SaveTask()
        {
            if (_networkAccess == NetworkAccess.Internet)
            {
                if (NameTask == null & NameTask != string.Empty)
                {
                    return;
                }
                if (NameTask != null & NameTask != string.Empty)
                {
                    UserId = CurrentInstagramUser.CurrentInstagramUserId;
                    TaskItem taskItem = new TaskItem(IdTask, NameTask, DescriptionTask, StatusTask, UserId, LalitudeMarkerResult, LongitudeMarkerResult);
                    await _iWebApiService.SaveTaskItem(taskItem, IdTask);
                }
                await _navigationService.Navigate<ListTaskViewModel>();
            }
        }

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
                UserId = CurrentInstagramUser.CurrentInstagramUserId;
                DescriptionTask = parameter.DescriptionTask;
                StatusTask = parameter.StatusTask;
                LalitudeMarkerResult = parameter.LalitudeMarker;
                LongitudeMarkerResult = parameter.LongitudeMarker;
                return;
            }
            _userId = CurrentInstagramUser.CurrentInstagramUserId;
            EnableStatusNameTask = true;
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
    }
}
