using CoreGraphics;
using CoreLocation;
using Foundation;
using MapKit;
using System;
using WatchKit;
using System.Linq;

namespace BikeSharing.Clients.WatchOSApp.WatchOSExtension
{
	public partial class MapDetailController : WKInterfaceController
	{
		private Station _from;
		private Station _to;
		private MKCoordinateRegion _currentRegion;
		private MKCoordinateSpan _currentSpan;

		public MapDetailController(IntPtr handle) : base(handle)
		{
			_currentSpan = new MKCoordinateSpan(1.0, 1.0);
		}

		public override void Awake(NSObject context)
		{
			base.Awake(context);

			// Configure interface objects here.
			Console.WriteLine("{0} awake with context", this);

			var stations = Stations.All;

			if (context is NSString)
			{
				var parameter = context.ToString();
				var index = parameter.IndexOf(",", StringComparison.InvariantCultureIgnoreCase);
				var fromId = Convert.ToInt32(parameter.Substring(0, index));
				var toId = Convert.ToInt32(parameter.Substring(index + 1));

				_from = stations.FirstOrDefault(f => f.Id == fromId);
				_to = stations.FirstOrDefault(t => t.Id == toId);
			}

			if (_from == null)
			{
				_from = stations.First();
			}

			if (_to == null)
			{
				_to = stations.Last();
			}

			FromLabel.SetText(_from.Name);
			ToLabel.SetText(_to.Name);
		}

		public override void WillActivate()
		{
			// This method is called when the controller is about to be visible to the wearer.
			Console.WriteLine("{0} will activate", this);

			GoToFrom(null);
		}

		public override void DidDeactivate()
		{
			// This method is called when the controller is no longer visible.
			Console.WriteLine("{0} did deactivate", this);
		}

		private void GoToFrom(NSObject obj)
		{
			if (_from == null)
			{
				return;
			}

			var coordinate = new CLLocationCoordinate2D(_from.Latitude, _from.Longitude);

			SetMapToCoordinate(coordinate);

			AddImageAnnotations();
		}

		private void SetMapToCoordinate(CLLocationCoordinate2D coordinate)
		{
			var region = new MKCoordinateRegion(coordinate, _currentSpan);
			_currentRegion = region;

			var newCenterPoint = MKMapPoint.FromCoordinate(coordinate);

			Map.SetVisible(new MKMapRect(
				newCenterPoint.X,
				newCenterPoint.Y,
				_currentSpan.LatitudeDelta,
				_currentSpan.LongitudeDelta));

			Map.SetRegion(region);
		}

		private void AddImageAnnotations()
		{
			var from = new CLLocationCoordinate2D(
				_currentRegion.Center.Latitude,
				_currentRegion.Center.Longitude);

			Map.AddAnnotation(from, "From", CGPoint.Empty);

			var to = new CLLocationCoordinate2D(
				_to.Latitude,
				_to.Longitude);

			Map.AddAnnotation(to, "To", CGPoint.Empty);
		}

		partial void ZoomIn()
		{
			var span = new MKCoordinateSpan(_currentSpan.LatitudeDelta * 0.5f, _currentSpan.LongitudeDelta * 0.5f);
			var region = new MKCoordinateRegion(_currentRegion.Center, span);

			_currentSpan = span;
			Map.SetRegion(region);
		}

		partial void ZoomOut()
		{
			var span = new MKCoordinateSpan(_currentSpan.LatitudeDelta * 2, _currentSpan.LongitudeDelta * 2);
			var region = new MKCoordinateRegion(_currentRegion.Center, span);

			_currentSpan = span;
			Map.SetRegion(region);
		}

		partial void Clear()
		{
			Map.RemoveAllAnnotations();
		}
	}
}
