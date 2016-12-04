using BikeSharing.Clients.Core.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;
using System.ComponentModel;
using BikeSharing.Clients.Droid.Renderers;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using BikeSharing.Clients.Core.Helpers;
using System.Linq;
using Xamarin.Forms.Maps;
using System.Collections.Generic;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace BikeSharing.Clients.Droid.Renderers
{
    class CustomMapRenderer : MapRenderer
    {
        private const int NormalResource = Resource.Drawable.pushpin;
        private const int FromResource = Resource.Drawable.pushpin_origin;
        private const int ToResource = Resource.Drawable.pushpin_destiny;

        private BitmapDescriptor _pinIcon;
        private BitmapDescriptor _fromPinIcon;
        private BitmapDescriptor _toPinIcon;
        private List<CustomMarkerOptions> _tempMarkers;
        private bool _isDrawnDone;

        public CustomMapRenderer()
        {
            _tempMarkers = new List<CustomMarkerOptions>();
            _pinIcon = BitmapDescriptorFactory.FromResource(NormalResource);
            _fromPinIcon = BitmapDescriptorFactory.FromResource(FromResource);
            _toPinIcon = BitmapDescriptorFactory.FromResource(ToResource);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var androidMapView = (MapView)Control;
            var formsMap = (CustomMap)sender;

            if (e.PropertyName.Equals("CustomPins") && !_isDrawnDone)
            {
                ClearPushPins(androidMapView);

                androidMapView.Map.MarkerClick += HandleMarkerClick;
                androidMapView.Map.MyLocationEnabled = formsMap.IsShowingUser;

                AddPushPins(androidMapView, formsMap.CustomPins);

                PositionMap();

                _isDrawnDone = true;
            }

            if (e.PropertyName.Equals("From"))
            {
                AddFromPushPin(androidMapView, formsMap.From);
            }

            if (e.PropertyName.Equals("To"))
            {
                AddToPushPin(androidMapView, formsMap.To);
            }
        }

        void HandleMarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            var marker = e.Marker;
            marker.ShowInfoWindow();

            var myMap = this.Element as CustomMap;

            var tempMarker = _tempMarkers
                .FirstOrDefault(m => m.MarkerOptions.Position.Latitude 
                == marker.Position.Latitude &&
                m.MarkerOptions.Position.Longitude == marker.Position.Longitude);

            var formsPin = new CustomPin
            {
                Id = tempMarker.Id,
                Label = marker.Title,
                Address = marker.Snippet,
                Position = new Position(marker.Position.Latitude, marker.Position.Longitude)
            };

            myMap.SelectedPin = formsPin;

            if (!_tempMarkers.Any(p => p.MarkerOptions.Icon == _fromPinIcon))
            {
                formsPin.Type = CustomPin.AnnotationType.From;
                myMap.From = formsPin;
            }
            else
            {
                var from = _tempMarkers.FirstOrDefault(p => p.MarkerOptions.Icon == _fromPinIcon);

                if (from != null
                    && !_tempMarkers.Any(p => p.MarkerOptions.Icon == _toPinIcon)
                    && from.Id == myMap.SelectedPin.Id)
                {
                    myMap.From = null;
                }
                else
                {
                    if (_tempMarkers.Any(p => p.MarkerOptions.Icon == _fromPinIcon) &&
                        !_tempMarkers.Any(p => p.MarkerOptions.Icon == _toPinIcon))
                    {
                        formsPin.Type = CustomPin.AnnotationType.To;
                        myMap.To = formsPin;
                    }
                    else
                    {
                        var to = _tempMarkers.FirstOrDefault(p => p.MarkerOptions.Icon == _toPinIcon);

                        if (to != null)
                        {
                            myMap.To = null;
                        }
                    }
                }

                myMap.SelectedPin = null;

                if(formsPin.Type != CustomPin.AnnotationType.Normal)
                    myMap.SelectedPin = formsPin;
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            if (changed)
            {
                _isDrawnDone = false;
            }
        }

        private void ClearPushPins(MapView mapView)
        {
            mapView.Map.Clear();
        }

        private void AddPushPins(MapView mapView, IEnumerable<CustomPin> pins)
        {
            foreach (var formsPin in pins)
            {
                var markerWithIcon = new MarkerOptions();

                markerWithIcon.SetPosition(new LatLng(formsPin.Position.Latitude, formsPin.Position.Longitude));
                markerWithIcon.SetTitle(formsPin.Label);
                markerWithIcon.SetSnippet(formsPin.Address);

                if (!string.IsNullOrEmpty(formsPin.PinIcon))
                    markerWithIcon.SetIcon(_pinIcon);
                else
                    markerWithIcon.SetIcon(BitmapDescriptorFactory.DefaultMarker());

                mapView.Map.AddMarker(markerWithIcon);

                _tempMarkers.Add(new CustomMarkerOptions
                {
                    Id = formsPin.Id,
                    MarkerOptions = markerWithIcon
                });
            }
        }

        private void AddPushPins(MapView mapView, IEnumerable<CustomMarkerOptions> markers)
        {
            foreach (var marker in markers)
            {
                mapView.Map.AddMarker(marker.MarkerOptions);
            }
        }

        private void AddFromPushPin(MapView mapView, CustomPin from)
        {
            // Reset previous From pushpin
            var fromTempMarker = _tempMarkers
                .FirstOrDefault(p => p.MarkerOptions.Icon == _fromPinIcon);

            if (fromTempMarker != null)
            {
                fromTempMarker.MarkerOptions.SetIcon(_pinIcon);
            }

            // Set new From pushpin
            if (from != null)
            {
                from.Type = CustomPin.AnnotationType.From;

                var newFromTempMarker = _tempMarkers
                    .FirstOrDefault(p => p.Id == from.Id);

                if (newFromTempMarker != null)
                {
                    newFromTempMarker.MarkerOptions.SetIcon(_fromPinIcon);
                }
            }

            ClearPushPins(mapView);
            AddPushPins(mapView, _tempMarkers);
        }

        private void AddToPushPin(MapView mapView, CustomPin to)
        {
            // Reset previous To pushpin
            var toTempMarker = _tempMarkers
                .FirstOrDefault(p => p.MarkerOptions.Icon == _toPinIcon);

            if (toTempMarker != null)
            {
                toTempMarker.MarkerOptions.SetIcon(_pinIcon);
            }

            // Set new To pushpin
            if (to != null)
            {
                to.Type = CustomPin.AnnotationType.To;

                var newToTempMarker = _tempMarkers
                    .FirstOrDefault(p => p.Id == to.Id);

                if (newToTempMarker != null)
                {
                    newToTempMarker.MarkerOptions.SetIcon(_toPinIcon);
                }
            }

            ClearPushPins(mapView);
            AddPushPins(mapView, _tempMarkers);
        }

        private void PositionMap()
        {
            var myMap = this.Element as CustomMap;
            var formsPins = myMap.CustomPins;

            if (formsPins == null || formsPins.Count() == 0)
            {
                return;
            }

            var centerPosition = new Position(formsPins.Average(x => x.Position.Latitude), formsPins.Average(x => x.Position.Longitude));

            var minLongitude = formsPins.Min(x => x.Position.Longitude);
            var minLatitude = formsPins.Min(x => x.Position.Latitude);

            var maxLongitude = formsPins.Max(x => x.Position.Longitude);
            var maxLatitude = formsPins.Max(x => x.Position.Latitude);

            var distance = MapHelper.CalculateDistance(minLatitude, minLongitude,
                maxLatitude, maxLongitude, 'M') / 2;

            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromMiles(distance)));
        }
    }
}