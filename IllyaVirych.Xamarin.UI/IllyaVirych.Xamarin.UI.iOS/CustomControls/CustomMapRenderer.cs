using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using CoreGraphics;
using CoreLocation;
using Foundation;
using IllyaVirych.Xamarin.UI.CustomControls;
using IllyaVirych.Xamarin.UI.iOS.CustomControls;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace IllyaVirych.Xamarin.UI.iOS.CustomControls
{
    public class CustomMapRenderer : MapRenderer, INotifyPropertyChanged
    {
        #region Variables
        private double _lalitude;
        private double _longitude;
        private MKMapView _mKMapView;
        private CustomMap _customMap;
        private readonly string _annotationIdentifierDefaultClusterPin = "TKDefaultClusterPin";
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Override
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                _mKMapView = Control as MKMapView;
                _mKMapView.GetViewForAnnotation = null;
            }
            if (e.NewElement != null)
            {
                e.NewElement.PropertyChanged += NewElement_PropertyChanged;
                var formsMap = (CustomMap)e.NewElement;
                _mKMapView = Control as MKMapView;
                _customMap = (CustomMap)e.NewElement;
                SetUpCustomMap();
            }
        }
        #endregion
       
     
        #region Properties
        public double Lalitude
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
                if (_longitude != value)
                {
                    _longitude = value;
                    OnPropertyChanged("Longlitude");
                    SetSavePin();
                }
            }
        }
        #endregion

        #region Methods
        private void SetUpCustomMap()
        {
            _mKMapView.ZoomEnabled = true;
            _mKMapView.ScrollEnabled = true;
            CLLocationManager locationManager = new CLLocationManager();
            locationManager.RequestWhenInUseAuthorization();
            _mKMapView.ShowsUserLocation = true;


            _mKMapView.DidUpdateUserLocation += delegate
            {
                if (_mKMapView.UserLocation != null)
                {
                    CLLocationCoordinate2D coordinateUser = _mKMapView.UserLocation.Coordinate;
                    MKCoordinateSpan coordinateSpanUser = new MKCoordinateSpan(0.02, 0.02);
                    _mKMapView.Region = new MKCoordinateRegion(coordinateUser, coordinateSpanUser);
                }
            };
            if (!_mKMapView.UserLocationVisible)
            {
                CLLocationCoordinate2D coords = new CLLocationCoordinate2D(49.99181, 36.23572);
                MKCoordinateSpan span = new MKCoordinateSpan(0.05, 0.05);
                _mKMapView.Region = new MKCoordinateRegion(coords, span);
            }

            var longGesture = new UILongPressGestureRecognizer(LongPress);
            longGesture.MinimumPressDuration = 0.5;
            _mKMapView.AddGestureRecognizer(longGesture);

            _mKMapView.GetViewForAnnotation += GetViewForAnnotation;

        }

        private MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            var customAnnotation = annotation as MKAnnotationClass;

            if (customAnnotation == null) return null;

            var annotationView = mapView.DequeueReusableAnnotation(_annotationIdentifierDefaultClusterPin);

            if (annotationView == null)
            {
                annotationView = new MKAnnotationView(customAnnotation, _annotationIdentifierDefaultClusterPin);
            }
            else
            {
                annotationView.Annotation = customAnnotation;
            }
            annotationView.CanShowCallout = true;
            annotationView.Selected = true;
            annotationView.Draggable = true;

            return annotationView;
        }

        private void LongPress(UILongPressGestureRecognizer gesture)
        {
            _mKMapView.RemoveAnnotations(_mKMapView.Annotations);
            CGPoint touchPoint = gesture.LocationInView(_mKMapView);
            CLLocationCoordinate2D touchMapCoordinate = _mKMapView.ConvertPoint(touchPoint, _mKMapView);

            MKAnnotationClass annotation = new MKAnnotationClass();
            annotation.Coordinate2D = touchMapCoordinate;
            _lalitude = annotation.Coordinate2D.Latitude;
            _longitude = annotation.Coordinate2D.Longitude;
            _customMap.UserLalitude = _lalitude;
            _customMap.UserLongitude = _longitude;
            _mKMapView.AddAnnotation(annotation);

            _mKMapView.AddAnnotations(new MKPointAnnotation()
            {
                Coordinate = new CLLocationCoordinate2D(_lalitude, _longitude)
            });
        }

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

        private void SetSavePin()
        {
            _mKMapView.AddAnnotations(new MKPointAnnotation()
            {
                Coordinate = new CLLocationCoordinate2D(Lalitude, Longitude)
            });
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}