using System;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Android.Widget;
using static Android.Content.ClipData;
using Android.Support.V4.Content;
using Android;
using Android.Content.PM;
using Android.App;
using Android.Support.V4.App;
using Android.Util;
using Android.Support.Design.Widget;
using IllyaVirych.Core.ViewModels;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Runtime;
using Android.Webkit;
using System.Threading.Tasks;

namespace IllyaVirych.Droid.ViewModels
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    public class MapsView : BaseFragment<MapsViewModel>, IOnMapReadyCallback
    {
        #region Variables
        private GoogleMap _googleMap;
        private MapView _mapView;
        private MarkerOptions _markerOptions;
        private LatLng _latLng;
        private double _lalitude;
        private double _longitude;
        private Marker _marker; 
        static readonly int REQUEST_STORAGE = 0;
        #endregion

        #region Lifecycle
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);
            _mapView = (MapView)view.FindViewById(Resource.Id.map);
            _mapView.OnCreate(savedInstanceState);
            _mapView.OnResume();
            _mapView.GetMapAsync(this);
            
            GoogleMapOptions mapOptions = new GoogleMapOptions()
            .InvokeMapType(GoogleMap.MapTypeSatellite)
            .InvokeZoomControlsEnabled(false)
            .InvokeCompassEnabled(true);

            return view;
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();
        }
        #endregion

        #region Override   
        protected override int FragmentId => Resource.Layout.MapsView;

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (requestCode != REQUEST_STORAGE)
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
                return;
            }
            if (grantResults.Length == 1 && grantResults[0] == Android.Content.PM.Permission.Granted)
            {
                Snackbar.Make(this.View, Resource.String.AccessLocationPermission, Snackbar.LengthShort).Show();
                return;
            }
            Snackbar.Make(this.View, Resource.String.DeniedLocationPermission, Snackbar.LengthShort).Show();
        }
        #endregion

        #region Methods
        private async void GetPermissionAsync()
        {
            await Task.Run(() => GetLocationPermission());
        }
         
        private void GetLocationPermission()
        {
            if (ActivityCompat.CheckSelfPermission(Application.Context, Manifest.Permission.AccessFineLocation) == (Android.Content.PM.Permission.Denied))
            {                
                 ActivityCompat.RequestPermissions(ParentActivity, new String[] { Manifest.Permission.AccessFineLocation}, REQUEST_STORAGE);
            }
            if(ActivityCompat.CheckSelfPermission(Application.Context, Manifest.Permission.AccessCoarseLocation) == (Android.Content.PM.Permission.Denied))
            {
                ActivityCompat.RequestPermissions(ParentActivity, new String[] {Manifest.Permission.AccessCoarseLocation }, REQUEST_STORAGE);
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            _googleMap = googleMap;
            _googleMap.UiSettings.ZoomControlsEnabled = true;
            _googleMap.UiSettings.CompassEnabled = true;

            if (ActivityCompat.CheckSelfPermission(Application.Context, Manifest.Permission.AccessFineLocation) == (Android.Content.PM.Permission.Denied) ||
                ActivityCompat.CheckSelfPermission(Application.Context, Manifest.Permission.AccessCoarseLocation) == (Android.Content.PM.Permission.Denied))
            {
                GetPermissionAsync();
            }
            _googleMap.MyLocationEnabled = true;

            LatLng location = new LatLng(49.99181, 36.23572);

            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(15);
            CameraPosition cameraPosition = builder.Build();

            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            _googleMap.MoveCamera(cameraUpdate);

            _markerOptions = new MarkerOptions();
            _markerOptions.Draggable(true);
            if (ViewModel.LalitudeMarker != 0 & ViewModel.LongitudeMarker != 0)
            {
                _lalitude = this.ViewModel.LalitudeMarker;
                _longitude = this.ViewModel.LongitudeMarker;
                _latLng = new LatLng(_lalitude, _longitude);
                _marker = _googleMap.AddMarker(new MarkerOptions().SetPosition(_latLng));
            }
          
            _googleMap.MapClick += MapOptionsClick;
            _googleMap.MarkerDragEnd += MarkerOptionLongClick;

            this.ViewModel.LalitudeMarker = _lalitude;
            this.ViewModel.LongitudeMarker = _longitude;
        }

        private void MarkerOptionLongClick(object sender, GoogleMap.MarkerDragEndEventArgs e)
        {
            _lalitude = e.Marker.Position.Latitude;
            _longitude = e.Marker.Position.Longitude;
            this.ViewModel.LalitudeMarker = _lalitude;
            this.ViewModel.LongitudeMarker = _longitude;
        }

        private void MapOptionsClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            _lalitude = e.Point.Latitude;
            _longitude = e.Point.Longitude;
            this.ViewModel.LalitudeMarker = _lalitude;
            this.ViewModel.LongitudeMarker = _longitude;
            _googleMap.Clear();
            _latLng = new LatLng(_lalitude, _longitude);
            _marker = _googleMap.AddMarker(new MarkerOptions().SetPosition(_latLng));
            _googleMap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(_latLng, _googleMap.CameraPosition.Zoom));
        }
        #endregion
    }
}