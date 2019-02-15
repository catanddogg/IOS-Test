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
        private UIButton _buttonBack, _buttonSavePin;
        private double _lalitude, _longitude;      

        const string AnnotationIdentifierDefaultClusterPin = "TKDefaultClusterPin";

        public MapsView () : base (nameof(MapsView), null)
        {
        }

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
            _buttonSavePin.TouchUpInside += ButtonGoogleMarkerSaveClick;

            SetUpMapView();

            var set = this.CreateBindingSet<MapsView, MapsViewModel>();
            set.Bind(_buttonBack).To(vm => vm.BackTaskCommand);
            set.Bind(_buttonSavePin).To(vm => vm.SaveMapPointCommand);
            set.Apply();           
        }

        private void SetUpMapView()
        {
            MapViewIOS = new MKMapView();
            View = MapViewIOS;
            MapViewIOS.ZoomEnabled = true;
            MapViewIOS.ScrollEnabled = true;
            CLLocationManager locationManager = new CLLocationManager();
            locationManager.RequestWhenInUseAuthorization();
            MapViewIOS.ShowsUserLocation = true;

            MapViewIOS.DidUpdateUserLocation += delegate
            {
                if (MapViewIOS.UserLocation != null)
                {
                    CLLocationCoordinate2D coordinateUser = MapViewIOS.UserLocation.Coordinate;
                    MKCoordinateSpan coordinateSpanUser = new MKCoordinateSpan(0.02, 0.02);
                    MapViewIOS.Region = new MKCoordinateRegion(coordinateUser, coordinateSpanUser);
                }
            };
            if (!MapViewIOS.UserLocationVisible)
            {
                CLLocationCoordinate2D coords = new CLLocationCoordinate2D(49.99181, 36.23572);
                MKCoordinateSpan span = new MKCoordinateSpan(0.05, 0.05);
                MapViewIOS.Region = new MKCoordinateRegion(coords, span);
            }

            var longGesture = new UILongPressGestureRecognizer(LongPress);
            longGesture.MinimumPressDuration = 0.5;
            MapViewIOS.AddGestureRecognizer(longGesture);

            MapViewIOS.GetViewForAnnotation += GetViewForAnnotation;           

            if (ViewModel.LalitudeMarker != 0)
            {
                _lalitude = this.ViewModel.LalitudeMarker;
                _longitude = this.ViewModel.LongitudeMarker;
                MapViewIOS.AddAnnotations(new MKPointAnnotation()
                {
                    Coordinate = new CLLocationCoordinate2D(_lalitude, _longitude)
                });
            }
        }

        private void ButtonGoogleMarkerSaveClick(object sender, EventArgs e)
        {
            var networkAccess = this.ViewModel.NetworkAccess;
            if (networkAccess != NetworkAccess.Internet)
            {
                var AllertSave = UIAlertController.Create("", "You do not have network access!", UIAlertControllerStyle.Alert);
                AllertSave.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(AllertSave, true, null);
                return;
            }
            var lalitudeGoogleMarker = this.ViewModel.LalitudeMarker;
            if (lalitudeGoogleMarker == 0)
            {
                var AllertSave = UIAlertController.Create("", "Put marker in google map!", UIAlertControllerStyle.Alert);
                AllertSave.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(AllertSave, true, null);
            }
        }

        private void LongPress(UILongPressGestureRecognizer gesture)
        {
            MapViewIOS.RemoveAnnotations(MapViewIOS.Annotations);
            CGPoint touchPoint = gesture.LocationInView(MapViewIOS);
            CLLocationCoordinate2D touchMapCoordinate = MapViewIOS.ConvertPoint(touchPoint, MapViewIOS);

            MKAnnotationClass annotation = new MKAnnotationClass();
            annotation.Coordinate2D = touchMapCoordinate;
            _lalitude = annotation.Coordinate2D.Latitude;
            _longitude = annotation.Coordinate2D.Longitude;
            ViewModel.LalitudeMarker = _lalitude;
            ViewModel.LongitudeMarker = _longitude;
            MapViewIOS.AddAnnotation(annotation);            

            MapViewIOS.AddAnnotations(new MKPointAnnotation()
            {               
                Coordinate = new CLLocationCoordinate2D(_lalitude, _longitude)
            });

        }

        private MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            var customAnnotation = annotation as MKAnnotationClass;

            if (customAnnotation == null) return null;

            var annotationView = mapView.DequeueReusableAnnotation(AnnotationIdentifierDefaultClusterPin);

            if (annotationView == null)
            {
                annotationView = new MKAnnotationView(customAnnotation, AnnotationIdentifierDefaultClusterPin);
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
    }
}