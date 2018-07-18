using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GMap.NET;
using GMap;
using GMap.NET.MapProviders;
using SecondTask;
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
        }

        private Travel CurrTravel { get; set; }

        private void InitMap() {
            gMapCtrl.DragButton = MouseButton.Left;
            gMapCtrl.Focus();

            gMapCtrl.MapProvider = GMapProviders.GoogleMap;

            PointLatLng startPoint = new PointLatLng(CurrTravel.Cities[0].Coord.latitude, CurrTravel.Cities[0].Coord.longitude);
            gMapCtrl.Position = startPoint;

            List<PointLatLng> path = new List<PointLatLng>();
            foreach (City c in CurrTravel.GetRouteCts()) {
                path.Add(new PointLatLng(c.Coord.latitude, c.Coord.longitude));

                GMapMarker marker = new GMapMarker(new PointLatLng(c.Coord.latitude, c.Coord.longitude)) {
                    Shape = new System.Windows.Shapes.Ellipse { Stroke = new SolidColorBrush(Colors.DeepSkyBlue), StrokeThickness = 12 }, 
                    Offset = new Point(-6, -6),
                    ZIndex = 1
                };
                gMapCtrl.Markers.Add(marker);
            }            

            GMapRoute route = new GMapRoute(path) {                
                Shape = new Path() { Stroke = new SolidColorBrush(Colors.Purple), StrokeThickness = 4, },
                ZIndex = 0
            };            
            gMapCtrl.Markers.Add(route);

            /*
                GMapMarker marker = new GMapMarker(startPoint);
                marker.Shape = new Control();
                gMapCtrl.Markers.Add(marker);

                gMapCtrl.Markers[0].Shape.Visibility = Visibility.Visible;


                List<Point> locPath = new List<Point>();
                List<PointLatLng> path = new List<PointLatLng>();
                foreach (City c in CurrTravel.Cities) {
                    path.Add(new PointLatLng(c.Coord.latitude, c.Coord.longitude));
                    locPath.Add(new Point(c.Coord.latitude, c.Coord.longitude));
                    gMapCtrl.Markers.Add(new GMapMarker(new PointLatLng(c.Coord.latitude, c.Coord.longitude)));
                }

                foreach (GMapMarker marker in gMapCtrl.Markers) {
                    if (marker.Shape != null)
                        marker.Shape.Visibility = Visibility.Visible;
                }

                MapRoute route = new MapRoute(path, "First");


                Path path2 = gMapCtrl.CreateRoutePath(locPath);
                path2.BeginInit();
                path2.ApplyTemplate();
                path2.BringIntoView();
                gMapCtrl.BringIntoView(); 
            */
            
        }

        private void Window_Activated(object sender, EventArgs e) {
            InitMap();
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            gMapCtrl = null;
        }
    }
}
