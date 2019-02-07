using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLocation;
using Foundation;
using MapKit;
using UIKit;

namespace IllyaVirych.IOS.MapKit
{
    [Preserve(AllMembers = true)]
    public class MKAnnotationClass : MKAnnotation
    {
        private CLLocationCoordinate2D _coordinate;
        private CLLocationCoordinate2D _coordinate2D;

        public override CLLocationCoordinate2D Coordinate => _coordinate;

        public CLLocationCoordinate2D Coordinate2D
        {
            get
            {
                return _coordinate2D;
            }
            set
            {
                _coordinate2D = value;              
            }
        }
        public override void SetCoordinate(CLLocationCoordinate2D value)
        {
            _coordinate2D = value;
        }

        public void BasicPinAnnotation(CLLocationCoordinate2D coordinate)
        {
            SetCoordinate(coordinate);
        }
    }
}