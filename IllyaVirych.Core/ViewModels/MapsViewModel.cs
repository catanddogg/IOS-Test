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
    public class MapsViewModel : BaseViewModel
    {      
       
        private readonly IMvxNavigationService _navigationService;
        private readonly ITaskService _iTaskService;
        private readonly IMvxMessenger _messenger;
        public IMvxCommand BackTaskCommand { get; set; }
        public IMvxAsyncCommand SaveGoogleMapPointCommand { get; set; }
        private double _lalitudeGoogelMarker;
        private double _longitudeGoogleMarker;
        private int _idTask;
        private string _nameTaskBackResult;
        private string _descriptionTaskBackResult;
        private bool _statusTaskBackResult;
        private readonly MvxSubscriptionToken _token;
        private double _lalitudeGoogleMarkerBack;
        private double _longlitudeGoogleMarkerBack;

        public MapsViewModel(IMvxNavigationService navigationService, ITaskService iTaskService, IMvxMessenger messenger)
        {
            _navigationService = navigationService;
            _iTaskService = iTaskService;
            _messenger = messenger;
            _token = messenger.Subscribe<GoogleMapMessenger>(OnLosationMessage);          
            BackTaskCommand = new MvxAsyncCommand(BackGoogleMap);
            SaveGoogleMapPointCommand = new MvxAsyncCommand(SaveGoogleMapPoint);                 
        }
        
        private async Task BackGoogleMap()
        {
            await _navigationService.Navigate<TaskViewModel>();

            LalitudeGoogleMarker = _lalitudeGoogleMarkerBack;
            LongitudeGoogleMarker = _longlitudeGoogleMarkerBack;
            var message = new GoogleMapMessenger(this,
                _idTask,
             LalitudeGoogleMarker,
             LongitudeGoogleMarker,
             _nameTaskBackResult,
             _descriptionTaskBackResult,
             _statusTaskBackResult
                );
            _messenger.Publish(message);
            _messenger.Unsubscribe<GoogleMapMessenger>(_token);
        }

        private void OnLosationMessage(GoogleMapMessenger googleMap)
        {
            _idTask = googleMap.IdTask;
            LalitudeGoogleMarker = googleMap.LalitudeGoogleMarkerResult;
            LongitudeGoogleMarker= googleMap.LongitudeGoogleMarkerResult;
            _nameTaskBackResult = googleMap.NameTaskResult;
            _descriptionTaskBackResult = googleMap.DescriptionTaskResult;
            _statusTaskBackResult = googleMap.StatusTaskResult;
            _lalitudeGoogleMarkerBack = googleMap.LalitudeGoogleMarkerResult;
            _longlitudeGoogleMarkerBack = googleMap.LongitudeGoogleMarkerResult;
        }

        private async Task SaveGoogleMapPoint()
        {
            if (LalitudeGoogleMarker == 0 & LongitudeGoogleMarker == 0)
            {
                return;
            }
            if (LalitudeGoogleMarker != 0 & LongitudeGoogleMarker != 0)
            {             
                await _navigationService.Navigate<TaskViewModel>();

                var message = new GoogleMapMessenger(this,
                    _idTask,
              LalitudeGoogleMarker,
              LongitudeGoogleMarker,
              _nameTaskBackResult,
              _descriptionTaskBackResult,
              _statusTaskBackResult
                 );
                _messenger.Publish(message);
                _messenger.Unsubscribe<GoogleMapMessenger>(_token);
            }
        }       

        public double LalitudeGoogleMarker
        {
            get
            {
                return _lalitudeGoogelMarker;
            }
            set
            {
                _lalitudeGoogelMarker = value;
                RaisePropertyChanged(() => LalitudeGoogleMarker);
            }
        }

        public double LongitudeGoogleMarker 
        {
            get
            {
                return _longitudeGoogleMarker;
            }
            set
            {
                _longitudeGoogleMarker = value;
                RaisePropertyChanged(() => LongitudeGoogleMarker);
            }
        }        
    }
}
