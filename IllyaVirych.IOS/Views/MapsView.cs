using CoreGraphics;
using CoreLocation;
using IllyaVirych.Core.ViewModels;
using IllyaVirych.IOS.MapKit;
using MapKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;
using Xamarin.Essentials;

namespace IllyaVirych.IOS.Views
{
    [MvxModalPresentation(WrapInNavigationController = true)]
    public partial class MapsView : MvxViewController<MapsViewModel>, IMKMapViewDelegate
    {
        #region Variables
        private UIButton _buttonBack, _buttonSavePin;
        private double _lalitude, _longitude;
        private readonly string _annotationIdentifierDefaultClusterPin = "TKDefaultClusterPin";
        #endregion

        #region Constructors
        public MapsView() : base(nameof(MapsView), null)
        {
        }
        #endregion

        #region Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Map";
            NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(0, 127, 70);
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.Black };

            _buttonBack = new UIButton(UIButtonType.Custom);
            _buttonBack.Frame = new CGRect(0, 0, 40, 40);
            _buttonBack.SetImage(UIImage.FromBundle("BackIcon"), UIControlState.Normal);
            this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(_buttonBack), false);

            _buttonSavePin = new UIButton(UIButtonType.Custom);
            _buttonSavePin.Frame = new CGRect(0, 0, 40, 40);
            _buttonSavePin.SetImage(UIImage.FromBundle("AddLocationIcon"), UIControlState.Normal);
            this.NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(_buttonSavePin), false);

            SetUpMapView();

            var set = this.CreateBindingSet<MapsView, MapsViewModel>();
            set.Bind(_buttonBack).To(vm => vm.BackTaskCommand);
            set.Bind(_buttonSavePin).To(vm => vm.SaveMapPointCommand);
            //set.Bind(LabelNetworkAccessTaskView).For(v => v.Hidden).To(vm => vm.NetworkAccess).WithConversion("TrueToFalse");
            set.Apply();
        }
        #endregion

        #region Methods
        private void SetUpMapView()
        {
            MapKitView = new MKMapView();
            View = MapKitView;
            MapKitView.ZoomEnabled = true;
            MapKitView.ScrollEnabled = true;
            CLLocationManager locationManager = new CLLocationManager();
            locationManager.RequestWhenInUseAuthorization();
            MapKitView.ShowsUserLocation = true;

            MapKitView.DidUpdateUserLocation += delegate
            {
                if (MapKitView.UserLocation != null)
                {
                    CLLocationCoordinate2D coordinateUser = MapKitView.UserLocation.Coordinate;
                    MKCoordinateSpan coordinateSpanUser = new MKCoordinateSpan(0.02, 0.02);
                    MapKitView.Region = new MKCoordinateRegion(coordinateUser, coordinateSpanUser);
                }
            };
            if (!MapKitView.UserLocationVisible)
            {
                CLLocationCoordinate2D coords = new CLLocationCoordinate2D(49.99181, 36.23572);
                MKCoordinateSpan span = new MKCoordinateSpan(0.05, 0.05);
                MapKitView.Region = new MKCoordinateRegion(coords, span);
            }

            var longGesture = new UILongPressGestureRecognizer(LongPress);
            longGesture.MinimumPressDuration = 0.5;
            MapKitView.AddGestureRecognizer(longGesture);

            MapKitView.GetViewForAnnotation += GetViewForAnnotation;

            if (ViewModel.LalitudeMarker != 0)
            {
                _lalitude = this.ViewModel.LalitudeMarker;
                _longitude = this.ViewModel.LongitudeMarker;
                MapKitView.AddAnnotations(new MKPointAnnotation()
                {
                    Coordinate = new CLLocationCoordinate2D(_lalitude, _longitude)
                });
            }
        }

        private void LongPress(UILongPressGestureRecognizer gesture)
        {
            MapKitView.RemoveAnnotations(MapKitView.Annotations);
            CGPoint touchPoint = gesture.LocationInView(MapKitView);
            CLLocationCoordinate2D touchMapCoordinate = MapKitView.ConvertPoint(touchPoint, MapKitView);

            MKAnnotationClass annotation = new MKAnnotationClass();
            annotation.Coordinate2D = touchMapCoordinate;
            _lalitude = annotation.Coordinate2D.Latitude;
            _longitude = annotation.Coordinate2D.Longitude;
            ViewModel.LalitudeMarker = _lalitude;
            ViewModel.LongitudeMarker = _longitude;
            MapKitView.AddAnnotation(annotation);

            MapKitView.AddAnnotations(new MKPointAnnotation()
            {
                Coordinate = new CLLocationCoordinate2D(_lalitude, _longitude)
            });

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
        #endregion
    }
}