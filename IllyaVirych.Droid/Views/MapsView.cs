using System;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Views;
using Android.Widget;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Xamarin.Essentials;

namespace IllyaVirych.Droid.ViewModels
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    public class MapsView : BaseFragment<MapsViewModel>, IOnMapReadyCallback
    {
        protected override int FragmentId => Resource.Layout.MapsView;
        private GoogleMap _googleMap;
        private MapView _mapView;
        private MarkerOptions _markerOptions;
        private LatLng _latLng;
        private double _lalitude;
        private double _longitude;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            
            _mapView = (MapView)view.FindViewById(Resource.Id.map);
            _mapView.OnCreate(savedInstanceState);
            _mapView.OnResume();
            _mapView.GetMapAsync(this);

            var buttonMarkerSave = view.FindViewById<ImageButton>(Resource.Id.savegooglemarker);
            buttonMarkerSave.Click += ButtonMarkerSaveClick;

            GoogleMapOptions mapOptions = new GoogleMapOptions()
            .InvokeMapType(GoogleMap.MapTypeSatellite)
            .InvokeZoomControlsEnabled(false)
            .InvokeCompassEnabled(true);           

            return view;
        }

        private void ButtonMarkerSaveClick(object sender, EventArgs e)
        {
            var networkAccess = this.ViewModel.NetworkAccess;
            if (networkAccess != NetworkAccess.Internet)
            {
                Toast.MakeText(this.Context, "You do not have network access!", ToastLength.Short).Show();
                return;
            }
            var lalitudeGoogleMarker = this.ViewModel.LalitudeMarker;
            if (lalitudeGoogleMarker == 0)
            {
                Toast.MakeText(this.Context, "Put marker in google map!", ToastLength.Short).Show();
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {  
            _googleMap = googleMap;
            _googleMap.UiSettings.ZoomControlsEnabled = true;
            _googleMap.UiSettings.CompassEnabled = true;
            
            LatLng location = new LatLng(49.99181, 36.23572);

            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(15); 
            CameraPosition cameraPosition = builder.Build();

            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            _googleMap.MoveCamera(cameraUpdate);

            _markerOptions = new MarkerOptions();                       
            _markerOptions.Draggable(true);
            if (ViewModel.LalitudeMarker != 0)
            {
                _lalitude = this.ViewModel.LalitudeMarker;
                _longitude = this.ViewModel.LongitudeMarker;
                _latLng = new LatLng(_lalitude, _longitude);
                _markerOptions.SetPosition(_latLng);
                _googleMap.AddMarker(_markerOptions);
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
            _markerOptions.SetPosition(_latLng);
            _googleMap.AddMarker(_markerOptions);
            _googleMap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(_latLng, _googleMap.CameraPosition.Zoom));
        }             
    }
}