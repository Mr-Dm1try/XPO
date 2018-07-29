using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using GMap.NET;
using GMap.NET.MapProviders;
using SecondTask.Classes;
using GMap.NET.WindowsPresentation;

namespace Second {
    /// <summary>
    /// Логика взаимодействия для Map.xaml
    /// </summary>
    public partial class Map : Window {
        public Map(Travel currTravel) {
            InitializeComponent();
            CurrTravel = currTravel;

            InitMap();
        }

        private Travel CurrTravel { get; set; }

        private void InitMap() {
            gMapCtrl.DragButton = MouseButton.Left;
            gMapCtrl.Focus();

            gMapCtrl.MapProvider = GMapProviders.GoogleMap;

            PointLatLng startPoint = new PointLatLng(CurrTravel.Cities[0].Coord.latitude, CurrTravel.Cities[0].Coord.longitude);
            gMapCtrl.Position = startPoint;

            //Маркеры
            foreach (City c in CurrTravel.GetRouteCts()) {
                GMapMarker marker = new GMapMarker(new PointLatLng(c.Coord.latitude, c.Coord.longitude)) {
                    Shape = new Ellipse { Stroke = new SolidColorBrush(Colors.DeepSkyBlue), StrokeThickness = 12 }, 
                    Offset = new Point(-6, -6),
                    ZIndex = 2
                };
                gMapCtrl.Markers.Add(marker);
            }

            //Путь
            City prev = null;
            foreach (City c in CurrTravel.GetRouteCts()) {
                if (prev != null) {
                    PointLatLng start = new PointLatLng(prev.Coord.latitude, prev.Coord.longitude);
                    PointLatLng end = new PointLatLng(c.Coord.latitude, c.Coord.longitude);

                    GMapRoute route = null;
                    MapRoute path = GMapProviders.OpenStreetMap.GetRoute(start, end, false, false, 15);
                    if (path != null && path.Distance > 1) {
                        route = new GMapRoute(path.Points) {
                            Shape = new Path() { Stroke = new SolidColorBrush(Colors.DarkViolet), StrokeThickness = 4, Opacity = 0.65 },
                            ZIndex = 1
                        };                        
                    }
                    else {
                        route = new GMapRoute(new List<PointLatLng>() { start, end }) {
                            Shape = new Path() { Stroke = new SolidColorBrush(Colors.Orange), StrokeThickness = 4, Opacity = 0.65 },
                            ZIndex = 0
                        };
                    }
                    gMapCtrl.Markers.Add(route);
                }
                prev = c;
            }

            //Прямые линии между точками
            //GMapRoute route = new GMapRoute(path) {
            //    Shape = new Path() { Stroke = new SolidColorBrush(Colors.Orange), StrokeThickness = 4, },
            //    ZIndex = 0
            //};
            //gMapCtrl.Markers.Add(route)

            //Ненужное                
            //GMapMarker marker = new GMapMarker(startPoint);
            //marker.Shape = new Control();
            //gMapCtrl.Markers.Add(marker);             
            //gMapCtrl.Markers[0].Shape.Visibility = Visibility.Visible;                 
            //List<Point> locPath = new List<Point>();
            //List<PointLatLng> path = new List<PointLatLng>();
            //foreach (City c in CurrTravel.Cities) {
            //    path.Add(new PointLatLng(c.Coord.latitude, c.Coord.longitude));
            //    locPath.Add(new Point(c.Coord.latitude, c.Coord.longitude));
            //    gMapCtrl.Markers.Add(new GMapMarker(new PointLatLng(c.Coord.latitude, c.Coord.longitude)));
            //}
            //foreach (GMapMarker marker in gMapCtrl.Markers) {
            //    if (marker.Shape != null)
            //        marker.Shape.Visibility = Visibility.Visible;
            //}
            //MapRoute route = new MapRoute(path, "First");                  
            //Path path2 = gMapCtrl.CreateRoutePath(locPath);
            //path2.BeginInit();
            //path2.ApplyTemplate();
            //path2.BringIntoView();
            //gMapCtrl.BringIntoView();
        }
        
        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            gMapCtrl = null;
        }
    }
}
