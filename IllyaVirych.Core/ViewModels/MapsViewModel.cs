using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Messenger;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System.Threading.Tasks;

namespace IllyaVirych.Core.ViewModels
{
    public class MapsViewModel : BaseViewModel
    {        
        private readonly IMvxNavigationService _navigationService;
        private readonly ITaskService _iTaskService;
        private readonly IMvxMessenger _messenger;
        public IMvxCommand BackTaskCommand { get; set; }
        public IMvxAsyncCommand SaveMapPointCommand { get; set; }
        private double _lalitudeMarker;
        private double _longitudeMarker;
        private int _idTask;
        private string _nameTaskBackResult;
        private string _descriptionTaskBackResult;
        private bool _statusTaskBackResult;
        private readonly MvxSubscriptionToken _token;
        private double _lalitudeMarkerBack;
        private double _longlitudeMarkerBack;

        public MapsViewModel(IMvxNavigationService navigationService, ITaskService iTaskService, IMvxMessenger messenger)
        {
            _navigationService = navigationService;
            _iTaskService = iTaskService;
            _messenger = messenger;
            _token = messenger.Subscribe<MapMessenger>(OnLosationMessage);          
            BackTaskCommand = new MvxAsyncCommand(BackMap);
            SaveMapPointCommand = new MvxAsyncCommand(SaveMapPoint);                 
        }
        
        private async Task BackMap()
        {
            await _navigationService.Navigate<TaskViewModel>();

            LalitudeMarker = _lalitudeMarkerBack;
            LongitudeMarker = _longlitudeMarkerBack;
            var message = new MapMessenger(this,
                _idTask,
             LalitudeMarker,
             LongitudeMarker,
             _nameTaskBackResult,
             _descriptionTaskBackResult,
             _statusTaskBackResult
                );
            _messenger.Publish(message);
            _messenger.Unsubscribe<MapMessenger>(_token);
        }

        private void OnLosationMessage(MapMessenger mapMesseger)
        {
            _idTask = mapMesseger.IdTask;
            LalitudeMarker = mapMesseger.LalitudeMarkerResult;
            LongitudeMarker= mapMesseger.LongitudeMarkerResult;
            _nameTaskBackResult = mapMesseger.NameTaskResult;
            _descriptionTaskBackResult = mapMesseger.DescriptionTaskResult;
            _statusTaskBackResult = mapMesseger.StatusTaskResult;
            _lalitudeMarkerBack = mapMesseger.LalitudeMarkerResult;
            _longlitudeMarkerBack = mapMesseger.LongitudeMarkerResult;
        }

        private async Task SaveMapPoint()
        {
            if (LalitudeMarker == 0 & LongitudeMarker == 0)
            {
                return;
            }
            if (LalitudeMarker != 0 & LongitudeMarker != 0)
            {             
                await _navigationService.Navigate<TaskViewModel>();

                var message = new MapMessenger(this,
                    _idTask,
              LalitudeMarker,
              LongitudeMarker,
              _nameTaskBackResult,
              _descriptionTaskBackResult,
              _statusTaskBackResult
                 );
                _messenger.Publish(message);
                _messenger.Unsubscribe<MapMessenger>(_token);
            }
        }       

        public double LalitudeMarker
        {
            get
            {
                return _lalitudeMarker;
            }
            set
            {
                _lalitudeMarker = value;
                RaisePropertyChanged(() => LalitudeMarker);
            }
        }

        public double LongitudeMarker 
        {
            get
            {
                return _longitudeMarker;
            }
            set
            {
                _longitudeMarker = value;
                RaisePropertyChanged(() => LongitudeMarker);
            }
        }        
    }
}
