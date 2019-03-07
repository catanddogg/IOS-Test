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
using IllyaVirych.Xamarin.UI.Droid.CustomControls;
using IllyaVirych.Xamarin.UI.CustomControls;

[assembly: ExportRenderer(typeof(CustomMap), typeof(AndroidCustomMap))]
namespace IllyaVirych.Xamarin.UI.Droid.CustomControls
{
    public class AndroidCustomMap : MapRenderer, IOnMapReadyCallback, INotifyPropertyChanged
    {
        #region Variables
        private CustomMap _customMap;
        private GoogleMap _googleMap;
        private MarkerOptions _markerOptions;
        private double _lalitude, _longitude;
        private LatLng _latLng;
        private Marker _marker;
        static readonly int REQUEST_STORAGE = 0;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Override
        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);
            if(e.OldElement != null)
            {
                e.NewElement.PropertyChanged -= NewElement_PropertyChanged;
                _googleMap.MapClick -= MapOptionsClick;
                _googleMap.MarkerDragEnd -= MarkerOptionLongClick;
            }
            if (e.NewElement != null)
            {
                e.NewElement.PropertyChanged += NewElement_PropertyChanged;
                _customMap = (CustomMap)e.NewElement;
                Control.GetMapAsync(this);

                GoogleMapOptions mapOptions = new GoogleMapOptions()
                .InvokeMapType(GoogleMap.MapTypeSatellite)
                .InvokeZoomControlsEnabled(false)
                .InvokeCompassEnabled(true);
            }
        }

        protected override void OnMapReady(GoogleMap googleMap)
        {
            base.OnMapReady(googleMap);
            _googleMap = googleMap;
            _googleMap.UiSettings.ZoomControlsEnabled = true;
            _googleMap.UiSettings.CompassEnabled = true;
            _googleMap.UiSettings.ZoomGesturesEnabled = true;
            _googleMap.UiSettings.ScrollGesturesEnabled = true;
            _googleMap.UiSettings.MyLocationButtonEnabled = true;

            if (ActivityCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.AccessFineLocation) == (Android.Content.PM.Permission.Denied) ||
               ActivityCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.AccessCoarseLocation) == (Android.Content.PM.Permission.Denied))
            {
                GetPermissionAsync();
                System.Threading.Thread.Sleep(5000);
            }
            _googleMap.MyLocationEnabled = true;
            _markerOptions = new MarkerOptions();
            _markerOptions.Draggable(true);
            if (_customMap.UserLalitude != 0 & _customMap.UserLongitude != 0)
            {
                _lalitude = _customMap.UserLalitude;
                _longitude = _customMap.UserLongitude;
                _latLng = new LatLng(_lalitude, _longitude);
                _marker = _googleMap.AddMarker(new MarkerOptions().SetPosition(_latLng));
            }

            _googleMap.MapClick += MapOptionsClick;
            _googleMap.MarkerDragEnd += MarkerOptionLongClick;
        }
        #endregion

        #region Methods
        private void NewElement_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "UserLalitude")
            {
                Lalitude = ((CustomMap)sender).UserLalitude;
            }
            if (e.PropertyName == "UserLongitude")
            {
                Longitude = ((CustomMap)sender).UserLongitude;
            }
        }

        private void MarkerOptionLongClick(object sender, GoogleMap.MarkerDragEndEventArgs e)
        {
            _lalitude = e.Marker.Position.Latitude;
            _longitude = e.Marker.Position.Longitude;
            _customMap.UserLalitude = _lalitude;
            _customMap.UserLongitude = _longitude;
        }
     

        private void MapOptionsClick(object sender, GoogleMap.MapClickEventArgs e)
        { 
            _lalitude = e.Point.Latitude;
            _longitude = e.Point.Longitude;
            _customMap.UserLalitude = _lalitude;
            _customMap.UserLongitude = _longitude;
            _googleMap.Clear();
            _latLng = new LatLng(_lalitude, _longitude);
            _marker = _googleMap.AddMarker(new MarkerOptions().SetPosition(_latLng));
            _googleMap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(_latLng, _googleMap.CameraPosition.Zoom));
        }

        private async void GetPermissionAsync()
        {
            await Task.Run(() => GetLocationPermission());
            try
            {

            }
            catch
            {

            }
        }

        private void GetLocationPermission()
        {
            if (ActivityCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.AccessFineLocation) == (Android.Content.PM.Permission.Denied))
            {
                ActivityCompat.RequestPermissions((Activity)Context, new String[] { Manifest.Permission.AccessFineLocation }, REQUEST_STORAGE);
            }
            if (ActivityCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.AccessCoarseLocation) == (Android.Content.PM.Permission.Denied))
            {
                ActivityCompat.RequestPermissions((Activity)Context, new String[] { Manifest.Permission.AccessCoarseLocation }, REQUEST_STORAGE);
            }
        }

        public AndroidCustomMap(Context context) : base(context)
        {
            AutoPackage = false;
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
        #endregion

        #region Properties
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
        #endregion
    }
}