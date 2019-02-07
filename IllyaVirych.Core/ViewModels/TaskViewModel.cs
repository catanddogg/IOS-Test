using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Messenger;
using IllyaVirych.Core.Models;
using IllyaVirych.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        public IMvxCommand DeleteMarkerGoogleMapCommand { get; set; }
        public IMvxCommand GoogleMapCommand { get; set; }
        private int _idTask;
        private string _nameTask;
        private string _descriptionTask;
        private bool _statusTask;
        private bool _enableStatusNameTask;
        private string _userId;
        private double _lalitudeGoogleMarkerResult;
        private double _longitudeGoogleMarkerResult;       
        private MvxSubscriptionToken _token;

        public TaskViewModel(IMvxNavigationService navigationService, ITaskService iTaskService, IMvxMessenger messenger)
        {            
            _navigationService = navigationService;
            _iTaskService = iTaskService;
            _messenger = messenger;
            _token = messenger.Subscribe<GoogleMapMessenger>(OnLocationMessage);
            SaveTaskCommand = new MvxAsyncCommand(SaveTask);
            DeleteTaskCommand = new MvxAsyncCommand(DeleteTask);
            BackTaskCommand = new MvxAsyncCommand(BackTask);
            DeleteMarkerGoogleMapCommand = new MvxCommand(DeleteMarkerGoogleMap);
            GoogleMapCommand = new MvxAsyncCommand(CreateMarkerGoogleMap);
        }

        private void OnLocationMessage(GoogleMapMessenger googleMap)
        {
            IdTask = googleMap.IdTask;
            LalitudeGoogleMarkerResult = googleMap.LalitudeGoogleMarkerResult;
            LongitudeGoogleMarkerResult = googleMap.LongitudeGoogleMarkerResult;
            NameTask = googleMap.NameTaskResult;
            DescriptionTask = googleMap.DescriptionTaskResult;
            StatusTask = googleMap.StatusTaskResult;
            EnableStatusNameTask = true;
        }

        private void DeleteMarkerGoogleMap()
        {
            LalitudeGoogleMarkerResult = 0;
            LongitudeGoogleMarkerResult = 0;
        }

        private async Task CreateMarkerGoogleMap()
        {           
            await _navigationService.Navigate<MapsViewModel>();

            var message = new GoogleMapMessenger(this, 
                IdTask,
                LalitudeGoogleMarkerResult,
                LongitudeGoogleMarkerResult,
                NameTask,
                DescriptionTask,
                StatusTask                 
                  );
            UserId = CurrentInstagramUser.CurrentInstagramUserId;
            _messenger.Publish(message);
            _messenger.Unsubscribe<GoogleMapMessenger>(_token);
        }       

        private async Task BackTask()
        {
            var result = await _navigationService.Navigate<ListTaskViewModel>();            
        }

        private async Task DeleteTask()
        {
            _iTaskService.DeleteTask(IdTask);
            await _navigationService.Close(this);
        }

        private async Task SaveTask()
        {
            if (NameTask == null & NameTask != string.Empty)
            {
                return;
            }
            if (NameTask != null & NameTask != string.Empty)
            {
                //UserId = CurrentInstagramUser.CurrentInstagramUserId;
                TaskItem taskItem = new TaskItem(IdTask, NameTask, DescriptionTask, StatusTask,UserId, LalitudeGoogleMarkerResult, LongitudeGoogleMarkerResult);
                _iTaskService.InsertTask(taskItem);               
            }
            await _navigationService.Navigate<ListTaskViewModel>();
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
                LalitudeGoogleMarkerResult = parameter.LalitudeGoogleMarker;
                LongitudeGoogleMarkerResult = parameter.LongitudeGoogleMarker;
                return;
            }
            _userId = CurrentInstagramUser.CurrentInstagramUserId;
            EnableStatusNameTask = true;
        }
              
        public double LalitudeGoogleMarkerResult
        {
            get
            {
                return _lalitudeGoogleMarkerResult;
            }
            set
            {
                _lalitudeGoogleMarkerResult = value;
                RaisePropertyChanged(() => LalitudeGoogleMarkerResult);
            }
        }

        public double LongitudeGoogleMarkerResult
        {
            get
            {
                return _longitudeGoogleMarkerResult;
            }
            set
            {
                _longitudeGoogleMarkerResult = value;
                RaisePropertyChanged(() => LongitudeGoogleMarkerResult);
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
