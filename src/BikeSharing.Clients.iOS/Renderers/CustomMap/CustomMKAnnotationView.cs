using MapKit;

namespace BikeSharing.Clients.iOS.Renderers
{
    public class CustomMKAnnotationView : MKAnnotationView
    {
        public int Id { get; set; }

        public CustomMKAnnotationView(IMKAnnotation annotation, int id)
            : base(annotation, id.ToString())
        {
        }
    }
}