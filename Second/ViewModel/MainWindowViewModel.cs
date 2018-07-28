using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using SecondTask;
using SecondTask.Model;

namespace SecondTask.ViewModel {
    class MainWindowViewModel : INotifyPropertyChanged {
        #region cities
        private String city1;
        private String city2;
        private String city3;
        private String city4;
        #endregion

        #region coordinates
        private String coord1;
        private String coord2;
        private String coord3;
        private String coord4;
        #endregion

        #region properties of cities
        public String City1 {
            get { return city1; }
            set {
                city1 = value;
                if (value != "")
                    try {
                        Cities[0] = new City(value);
                        Coord1 = Cities[0].StrCoords;
                    }
                    catch (Exception e) {
                        Cities[0] = null;
                        Coord1 = e.Message;
                    }
                else
                    Cities[0] = null;

                OnPropertyChanged("City1");
            }
        }
        public String City2 {
            get { return city2; }
            set {
                city2 = value;
                if (value != "")
                    try {
                        Cities[1] = new City(value);
                        Coord2 = Cities[1].StrCoords;
                    }
                    catch (Exception e) {
                        Cities[1] = null;
                        Coord2 = e.Message;
                    }
                else
                    Cities[1] = null;
                OnPropertyChanged("City2");
            }
        }
        public String City3 {
            get { return city3; }
            set {
                city3 = value;
                if (value != "")
                    try {
                        Cities[2] = new City(value);
                        Coord3 = Cities[2].StrCoords;
                    }
                    catch (Exception e) {
                        Cities[2] = null;
                        Coord3 = e.Message;
                    }
                else
                    Cities[2] = null;
                OnPropertyChanged("City3");
            }
        }
        public String City4 {
            get { return city4; }
            set {
                city4 = value;
                if (value != "")
                    try {
                        Cities[3] = new City(value);
                        Coord4 = Cities[3].StrCoords;
                    }
                    catch (Exception e) {
                        Cities[3] = null;
                        Coord4 = e.Message;
                    }
                else
                    Cities[3] = null;
                OnPropertyChanged("City4");
            }
        }
        #endregion

        #region properties of coordinates
        public String Coord1 {
            get { return coord1; }
            set {
                coord1 = value;
                OnPropertyChanged("Coord1");
            }
        }
        public String Coord2 {
            get { return coord2; }
            set {
                coord2 = value;
                OnPropertyChanged("Coord2");
            }
        }
        public String Coord3 {
            get { return coord3; }
            set {
                coord3 = value;
                OnPropertyChanged("Coord3");
            }
        }
        public String Coord4 {
            get { return coord4; }
            set {
                coord4 = value;
                OnPropertyChanged("Coord4");
            }
        }
        #endregion

        private String route;
        private String aproxTime;
        private String routeLength;        

        public String Route {
            get { return route; }
            set {
                route = value;
                OnPropertyChanged("Route");
            }
        }
        public String AproxTime {
            get { return aproxTime; }
            set {
                aproxTime = value;
                OnPropertyChanged("AproxTime");
            }
        }
        public String RouteLength {
            get { return routeLength; }
            set {
                routeLength = value;
                OnPropertyChanged("RouteLength");
            }
        }

        
        private Travel NewTravel { get; set; }
        private City[] Cities { get; set; }


        public MainWindowViewModel() {
            Coord1 = Coord2 = Coord3 = Coord4 = "Coordinates";
            Route = "";
            AproxTime = "00 h 00 m";
            RouteLength = "";

            Cities = new City[4];
            GetRouteCommand = new DelegateCommand(GetRoute, CanGetRoute);
            ShowMapCommand = new DelegateCommand(ShowMap, CanShowMap);
        }

        public ICommand GetRouteCommand { get; private set; }
        public ICommand ShowMapCommand { get; private set; }
                
        private void GetRoute(object obj) {
            try {
                List<City> newList = Cities.Distinct().ToList();
                for (int i = 0; i < newList.Count; i++)
                    if (newList[i] == null)
                        newList.RemoveAt(i);

                NewTravel = new Travel();
                foreach (var city in newList)
                    NewTravel.AddCity(city);
                
                NewTravel.ComputeRoute();

                Route = NewTravel.GetRouteStr();
                AproxTime = NewTravel.AproxTime;
                RouteLength = (NewTravel.RouteLength / 1000).ToString();               
            }
            catch (Exception er) {
                Route = AproxTime = RouteLength = "Ooops, error: " + er.Message;
            }
        }

        private bool CanGetRoute(object arg) {
            if (Cities != null) {
                List<City> newList = Cities.Distinct().ToList();
                int i = 0;
                foreach (var city in newList) 
                    if (city != null && city.Name != "" && city.Name != null)
                        i++;

                if (i >= 2)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        //TODO: Реализовать
        private void ShowMap(object obj) 
        {
            try {
                View.Map routeMap = new View.Map(NewTravel);
                routeMap.ShowDialog();
            }
            catch (Exception er) {
                System.Windows.MessageBox.Show("Ooops, error: " + er.Message);
            }
        }
        
        private bool CanShowMap(object arg) {
            if (NewTravel != null)
                return NewTravel.GetRouteStr() != "";
            else
                return false;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        
    }
}
