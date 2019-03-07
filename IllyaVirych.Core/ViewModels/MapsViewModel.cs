using Foundation;
using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Messenger;
using IllyaVirych.Core.MvxInteraction;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using WebKit;
using Xamarin.Essentials;

namespace IllyaVirych.Core.ViewModels
{
    public class MapsViewModel : BaseViewModel
    {
        #region Variables
        private readonly ITaskService _taskService;
        private IAlertService _alertService;
        private readonly IMvxMessenger _messenger;
        private double _lalitudeMarker;
        private double _longitudeMarker;
        private int _idTask;
        private string _nameTaskBackResult;
        private string _descriptionTaskBackResult;
        private bool _statusTaskBackResult;
        private readonly MvxSubscriptionToken _token;
        private double _lalitudeMarkerBack;
        private double _longlitudeMarkerBack;
        private bool _changedNetworkAccess;
        private readonly string _networkAccessAlert = "You do not have network access!";
        private readonly string _putMarkerGoogleMapAlert = "Put marker in google map!";
        public static double _testLalitude;
        public MvxInteraction<CoordinateAction> Interaction { get; set; } = new MvxInteraction<CoordinateAction>();
        #endregion

        #region Constructors
        public MapsViewModel(IMvxNavigationService navigationService, ITaskService taskService, IMvxMessenger messenger, IAlertService alertService)
            : base(navigationService)
        {

            _taskService = taskService;
            _messenger = messenger;
            _alertService = alertService;
            _token = messenger.Subscribe<MapMessenger>(OnLocationMessage);
            BackTaskCommand = new MvxAsyncCommand(BackMap);
            SaveMapPointCommand = new MvxAsyncCommand(SaveMapPoint);
            NativeSaveMapPointCommand = new MvxAsyncCommand(NativeSaveMapPoint);

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                ChangedNetworkAccess = true;
            }
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
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
        #endregion

        #region Commands
        public IMvxCommand BackTaskCommand { get; set; }
        public IMvxAsyncCommand SaveMapPointCommand { get; set; }
        public IMvxAsyncCommand NativeSaveMapPointCommand { get; set; }
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

        private void OnLocationMessage(MapMessenger mapMesseger)
        {
            _idTask = mapMesseger.IdTask;
            LalitudeMarker = mapMesseger.LalitudeMarkerResult;
            LongitudeMarker = mapMesseger.LongitudeMarkerResult;
            _nameTaskBackResult = mapMesseger.NameTaskResult;
            _descriptionTaskBackResult = mapMesseger.DescriptionTaskResult;
            _statusTaskBackResult = mapMesseger.StatusTaskResult;
            _lalitudeMarkerBack = mapMesseger.LalitudeMarkerResult;
            _longlitudeMarkerBack = mapMesseger.LongitudeMarkerResult;

            var request = new CoordinateAction
            {
                LalitudePin = LalitudeMarker,
                LongitudePin = LongitudeMarker
            };
            Interaction.Raise(request);
        }

        private async Task SaveMapPoint()
        {
            if (_changedNetworkAccess == false)
            {
                _alertService.ShowAlert(_networkAccessAlert);
                return;
            }
            if (LalitudeMarker == 0 & LongitudeMarker == 0)
            {
                _alertService.ShowAlert(_putMarkerGoogleMapAlert);
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

        private async Task NativeSaveMapPoint()
        {
            if (_changedNetworkAccess == false)
            {
                _alertService.ShowAlert(_networkAccessAlert);
                return;
            }
            if (LalitudeMarker == 0 & LongitudeMarker == 0)
            {
                _alertService.ShowAlert(_putMarkerGoogleMapAlert);
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
        #endregion
    }
}
