using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IllyaVirych.Xamarin.UI.Droid.Renderer;
using IllyaVirych.Xamarin.UI.RendererComponent;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Android.Support.V4.App;
using System.Threading.Tasks;
using Android;
using IllyaVirych.Core.ViewModels;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(RendererMap), typeof(MapViewRenderer))]
namespace IllyaVirych.Xamarin.UI.Droid.Renderer
{
    public class MapViewRenderer : MapRenderer, IOnMapReadyCallback, INotifyPropertyChanged
    {
        private List<RendererPin> _rendererPins;
        private RendererMap _rendererMap;
        private GoogleMap _googleMap;
        private MarkerOptions _markerOptions;
        private double _lalitude, _longitude;
        private LatLng _latLng;
        private Marker _marker;

        public event PropertyChangedEventHandler PropertyChanged;

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);
            //if (e.OldElement != null)
            //{
                _rendererMap = (RendererMap)e.NewElement;
                _rendererPins = _rendererMap.RendererPins;
                Control.GetMapAsync(this);

                GoogleMapOptions mapOptions = new GoogleMapOptions()
                .InvokeMapType(GoogleMap.MapTypeSatellite)
                .InvokeZoomControlsEnabled(false)
                .InvokeCompassEnabled(true);
            //}
        }

        protected override void OnMapReady(GoogleMap googleMap)
        {
            base.OnMapReady(googleMap);
            _googleMap = googleMap;
            _googleMap.UiSettings.ZoomControlsEnabled = true;
            _googleMap.UiSettings.CompassEnabled = true;

            //if (ActivityCompat.CheckSelfPermission(Application.Context, Manifest.Permission.AccessFineLocation) == (Android.Content.PM.Permission.Denied) ||
            //   ActivityCompat.CheckSelfPermission(Application.Context, Manifest.Permission.AccessCoarseLocation) == (Android.Content.PM.Permission.Denied))
            //{
            //    GetPermissionAsync();
            //}
            //_googleMap.MyLocationEnabled = true;

            LatLng location = new LatLng(49.99181, 36.23572);

            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(15);
            CameraPosition cameraPosition = builder.Build();

            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            _googleMap.MoveCamera(cameraUpdate);

            _markerOptions = new MarkerOptions();
            _markerOptions.Draggable(true);
            if (_rendererMap.UserLalitude != 0 & _rendererMap.UserLongitude != 0)
            {
                _lalitude = _rendererMap.UserLalitude;
                _longitude = _rendererMap.UserLongitude;
                _latLng = new LatLng(_lalitude, _longitude);
                _marker = _googleMap.AddMarker(new MarkerOptions().SetPosition(_latLng));
            }

            _googleMap.MapClick += MapOptionsClick;
            _googleMap.MarkerDragEnd += MarkerOptionLongClick;

            //this.ViewModel.LalitudeMarker = _lalitude;
            //this.ViewModel.LongitudeMarker = _longitude;
        }

        private void MarkerOptionLongClick(object sender, GoogleMap.MarkerDragEndEventArgs e)
        {
            _lalitude = e.Marker.Position.Latitude;
            _longitude = e.Marker.Position.Longitude;
            _rendererMap.UserLalitude = _lalitude;
            _rendererMap.UserLongitude = _longitude;
            //ViewModel.LalitudeMarker = _lalitude;
            //ViewModel.LongitudeMarker = _longitude;
        }

        private void MapOptionsClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            _lalitude = e.Point.Latitude;
            _longitude = e.Point.Longitude;
            _rendererMap.UserLalitude = _lalitude; 
            _rendererMap.UserLongitude = _longitude;
            _googleMap.Clear();
            _latLng = new LatLng(_lalitude, _longitude);
            _marker = _googleMap.AddMarker(new MarkerOptions().SetPosition(_latLng));
            _googleMap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(_latLng, _googleMap.CameraPosition.Zoom));
        }

        private async void GetPermissionAsync()
        {
            await Task.Run(() => GetLocationPermission());
        }


        private void GetLocationPermission()
        {
            //if (ActivityCompat.CheckSelfPermission(Application.Context, Manifest.Permission.AccessFineLocation) == (Android.Content.PM.Permission.Denied))
            //{
            //    ActivityCompat.RequestPermissions(ParentActivity, new String[] { Manifest.Permission.AccessFineLocation }, REQUEST_STORAGE);
            //}
            //if (ActivityCompat.CheckSelfPermission(Application.Context, Manifest.Permission.AccessCoarseLocation) == (Android.Content.PM.Permission.Denied))
            //{
            //    ActivityCompat.RequestPermissions(ParentActivity, new String[] { Manifest.Permission.AccessCoarseLocation }, REQUEST_STORAGE);
            //}
        }

        public MapViewRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }

        public double  Lalitude
        {
            get
            {
                return _lalitude;
            }
            set
            {
                if (_lalitude != value)
                {
                    _lalitude = value;
                    OnPropertyChanged("Lalitude");
                    SetSavePin();
                }
            }
        }

        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                if(_longitude != value)
                {
                    _longitude = value;
                    OnPropertyChanged("Longlitude");
                    SetSavePin();
                }
            }
        }

        private void SetSavePin()
        {
            _latLng = new LatLng(Lalitude, Longitude);
            _marker = _googleMap.AddMarker(new MarkerOptions().SetPosition(_latLng));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}