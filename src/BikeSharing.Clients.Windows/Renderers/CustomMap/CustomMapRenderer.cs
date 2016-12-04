using BikeSharing.Clients.Core.Controls;
using BikeSharing.Clients.Core.Helpers;
using BikeSharing.Clients.Windows.Renderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace BikeSharing.Clients.Windows.Renderers
{
    public class CustomMapRenderer : MapRenderer
    {
        private RandomAccessStreamReference NormalResource = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/pushpin.png"));
        private RandomAccessStreamReference FromResource = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/pushpin_origen.png"));
        private RandomAccessStreamReference ToResource = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/pushpin_destiny.png"));

        private CustomMap _customMap;
        private List<CustomPin> _customPins;
        private List<CustomMapIcon> _tempMapIcons;

        public CustomMapRenderer()
        {
            _customPins = new List<CustomPin>();
            _tempMapIcons = new List<CustomMapIcon>();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var windowsMapView = (MapControl)Control;
            _customMap = (CustomMap)sender;

            if (e.PropertyName.Equals("CustomPins"))
            {
                windowsMapView.MapElementClick += OnMapElementClick;
                _customPins = _customMap.CustomPins.ToList();
                ClearPushPins(windowsMapView);

                AddPushPins(windowsMapView, _customMap.CustomPins);
                PositionMap();
            }

            if (e.PropertyName.Equals("From"))
            {
                AddFromPushPin(windowsMapView, _customMap.From);
            }

            if (e.PropertyName.Equals("To"))
            {
                AddToPushPin(windowsMapView, _customMap.To);
            }
        }

        private void OnMapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var myMap = this.Element as CustomMap;
            var mapIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;

            if (mapIcon != null)
            {
                var customMapIcon = _tempMapIcons.FirstOrDefault(
                    m => m.MapIcon.MapTabIndex == mapIcon.MapTabIndex);

                var formsPin = new CustomPin
                { 
                    Id = mapIcon.MapTabIndex,
                    Label = customMapIcon?.Label,
                    Position = new Position(
                        mapIcon.Location.Position.Latitude, 
                        mapIcon.Location.Position.Longitude)
                };

                myMap.SelectedPin = formsPin;

                if (!_tempMapIcons.Any(p => p.MapIcon.Image == FromResource))
                {
                    formsPin.Type = CustomPin.AnnotationType.From;
                    myMap.From = formsPin;
                }
                else
                {
                    var from = _tempMapIcons.FirstOrDefault(p => p.MapIcon.Image == FromResource);

                    if (from != null               
                        && !_tempMapIcons.Any(p => p.MapIcon.Image == ToResource)       
                        && from.MapIcon.Location.Position.Latitude == myMap.SelectedPin.Position.Latitude 
                        && from.MapIcon.Location.Position.Longitude == myMap.SelectedPin.Position.Longitude)
                    {
                        myMap.From = null;
                    }
                    else
                    {
                        if (_tempMapIcons.Any(p => p.MapIcon.Image == FromResource) &&
                            !_tempMapIcons.Any(p => p.MapIcon.Image == ToResource))
                        {
                            formsPin.Type = CustomPin.AnnotationType.To;
                            myMap.To = formsPin;
                        }
                        else
                        {
                            var to = _tempMapIcons.FirstOrDefault(p => p.MapIcon.Image == ToResource);

                            if (to != null)
                            {
                                myMap.To = null;
                            }
                        }
                    }

                    myMap.SelectedPin = null;

                    if (formsPin.Type != CustomPin.AnnotationType.Normal)
                        myMap.SelectedPin = formsPin;
                }
            }
        }

        private void ClearPushPins(MapControl mapControl)
        {
            mapControl.MapElements.Clear();
        }

        private void AddPushPins(MapControl mapControl, IEnumerable<CustomPin> pins)
        {
            if(_tempMapIcons != null)
            {
                _tempMapIcons.Clear();
            }

            foreach (var pin in pins)
            {
                var snPosition = new BasicGeoposition { Latitude = pin.Position.Latitude, Longitude = pin.Position.Longitude };
                var snPoint = new Geopoint(snPosition);

                var mapIcon = new CustomMapIcon();
                mapIcon.Label = pin.Label;
                mapIcon.MapIcon = new MapIcon();
                mapIcon.MapIcon.MapTabIndex = pin.Id;
                mapIcon.MapIcon.CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible;
                mapIcon.MapIcon.Location = snPoint;
                mapIcon.MapIcon.NormalizedAnchorPoint = new Point(0.5, 1.0);
                mapIcon.MapIcon.ZIndex = 0;

                switch (pin.Type)
                {
                    case CustomPin.AnnotationType.From:
                        mapIcon.MapIcon.Image = FromResource;
                        break;
                    case CustomPin.AnnotationType.To:
                        mapIcon.MapIcon.Image = ToResource;
                        break;
                    default:
                        mapIcon.MapIcon.Image = NormalResource;
                        break;
                }

                mapControl.MapElements.Add(mapIcon.MapIcon);
                _tempMapIcons.Add(mapIcon);
            }
        }

        private void AddPushPins(MapControl mapControl, IEnumerable<CustomMapIcon> mapIcons)
        {
            foreach (var mapIcon in mapIcons)
            {
                mapControl.MapElements.Add(mapIcon.MapIcon);
            }
        }

        private void AddFromPushPin(MapControl mapControl, CustomPin from)
        {
            // Reset previous From pushpin
            var fromMapIcon = _tempMapIcons
                .FirstOrDefault(p => p.MapIcon.Image == FromResource);

            if (fromMapIcon != null)
            {
                fromMapIcon.MapIcon.Image = NormalResource;
            }

            // Set new From pushpin
            if (from != null)
            {
                from.Type = CustomPin.AnnotationType.From;

                var newMapIcon = _tempMapIcons
                    .FirstOrDefault(p => 
                    p.MapIcon.Location.Position.Latitude == from.Position.Latitude && 
                    p.MapIcon.Location.Position.Longitude == from.Position.Longitude);

                if (newMapIcon != null)
                {
                    newMapIcon.MapIcon.Image = FromResource;
                }
            }

            ClearPushPins(mapControl);
            AddPushPins(mapControl, _tempMapIcons);
        }

        private void AddToPushPin(MapControl mapControl, CustomPin to)
        {
            // Reset previous To pushpin
            var toMapIcon = _tempMapIcons
                .FirstOrDefault(p => p.MapIcon.Image == ToResource);

            if (toMapIcon != null)
            {
                toMapIcon.MapIcon.Image = NormalResource;
            }

            // Set new To pushpin
            if (to != null)
            {
                to.Type = CustomPin.AnnotationType.To;

                var newMapIcon = _tempMapIcons
                    .FirstOrDefault(p => 
                    p.MapIcon.Location.Position.Latitude == to.Position.Latitude &&
                    p.MapIcon.Location.Position.Longitude == to.Position.Longitude);

                if (newMapIcon != null)
                {
                    newMapIcon.MapIcon.Image = ToResource;
                }
            }

            ClearPushPins(mapControl);
            AddPushPins(mapControl, _tempMapIcons);
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
