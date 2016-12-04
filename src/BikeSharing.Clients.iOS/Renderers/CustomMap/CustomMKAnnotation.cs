using CoreLocation;
using MapKit;

namespace BikeSharing.Clients.iOS.Renderers
{
    public class CustomMKAnnotation : MKAnnotation
    {
        private CLLocationCoordinate2D _coord;
        private string _title;

        public override CLLocationCoordinate2D Coordinate
        {
            get { return _coord; }
        }

        public override string Title
        {
            get { return _title; }
        }

        public override void SetCoordinate(CLLocationCoordinate2D coord)
        {
            _coord = coord;
        }

        public CustomMKAnnotation(
            CLLocationCoordinate2D coord,
            string title)
        {
            _coord = coord;
            _title = title;
        }
    }
}