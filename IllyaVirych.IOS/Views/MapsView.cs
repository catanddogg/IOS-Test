using CoreGraphics;
using CoreLocation;
using Foundation;
using IllyaVirych.Core.ViewModels;
using IllyaVirych.IOS.MapKit;
using MapKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace IllyaVirych.IOS.Views
{
    [MvxModalPresentation(WrapInNavigationController = true)]
    public partial class MapsView : MvxViewController<MapsViewModel>, IMKMapViewDelegate
    {
        private UIButton _buttonBack, _buttonSavePin;
        private double _lalitude;
        private double _longitude;
        private bool _isDragging;

        const string AnnotationIdentifierDefaultClusterPin = "TKDefaultClusterPin";

        public MapsView () : base (nameof(MapsView), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Map";

            _buttonBack = new UIButton(UIButtonType.Custom);
            _buttonBack.Frame = new CGRect(0, 0, 40, 40);
            _buttonBack.SetImage(UIImage.FromBundle("icons8-back-filled-30.png"), UIControlState.Normal);
            this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(_buttonBack), false);

            _buttonSavePin = new UIButton(UIButtonType.Custom);
            _buttonSavePin.Frame = new CGRect(0, 0, 40, 40);
            _buttonSavePin.SetImage(UIImage.FromBundle("baseline_add_location_black_48dp.png"), UIControlState.Normal);
            this.NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(_buttonSavePin), false);
            _buttonSavePin.TouchUpInside += ButtonGoogleMarkerSaveClick;

            MapViewIOS = new MKMapView();
            View = MapViewIOS;
            MapViewIOS.ZoomEnabled = true;
            MapViewIOS.ScrollEnabled = true;
            CLLocationManager locationManager = new CLLocationManager();
            locationManager.RequestWhenInUseAuthorization();
            MapViewIOS.ShowsUserLocation = true;            

            var longGesture = new UILongPressGestureRecognizer(LongPress);            
            longGesture.MinimumPressDuration = 1.5;
            MapViewIOS.AddGestureRecognizer(longGesture);

            MapViewIOS.GetViewForAnnotation += GetViewForAnnotation;

            if (ViewModel.LalitudeGoogleMarker != 0)
            {
                _lalitude = this.ViewModel.LalitudeGoogleMarker;
                _longitude = this.ViewModel.LongitudeGoogleMarker;
                MapViewIOS.AddAnnotations(new MKPointAnnotation()
                {
                    Coordinate = new CLLocationCoordinate2D(_lalitude, _longitude)
                });
            }      

            var set = this.CreateBindingSet<MapsView, MapsViewModel>();
            set.Bind(_buttonBack).To(vm => vm.BackTaskCommand);
            set.Bind(_buttonSavePin).To(vm => vm.SaveGoogleMapPointCommand);
            set.Apply();           
        }

        private void ButtonGoogleMarkerSaveClick(object sender, EventArgs e)
        {
            var lalitudeGoogleMarker = this.ViewModel.LalitudeGoogleMarker;
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
            ViewModel.LalitudeGoogleMarker = _lalitude;
            ViewModel.LongitudeGoogleMarker = _longitude;
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

        //private void OnChangedDragState(object sender, MKMapViewDragStateEventArgs e)
        //{
        //    if(e.NewState == MKAnnotationViewDragState.Starting)
        //    {
        //        _isDragging = true;
        //    }
        //    _lalitude = e.AnnotationView.Annotation.Coordinate.Latitude;
        //    _longitude = e.AnnotationView.Annotation.Coordinate.Longitude;

        //}

        //public virtual void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        //{
        //    _lalitude = e.View.Annotation.Coordinate.Latitude;
        //    _longitude = e.View.Annotation.Coordinate.Longitude;
        //    MapViewIOS.AddAnnotations(new MKPointAnnotation()
        //    {
        //        Coordinate = new CLLocationCoordinate2D(_lalitude, _longitude)
        //    });
        //}          
    }
}