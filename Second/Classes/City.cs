﻿using System;

namespace SecondTask.Classes {
    public struct Coordinates {
        public Double longitude;
        public Double latitude;
    }

    public class City {
        public String Name { get; }
        public Coordinates Coord { get; }

        public City (String name) {
            Name = name;
            Double tmp1 = 0, tmp2 = 0;

            YandexGC.GetCoordinates(Name, ref tmp1, ref tmp2);

            if (tmp1 == -1 || tmp2 == -1)
                throw new Exception();
            else {
                Coord = new Coordinates {
                    longitude = tmp1,
                    latitude = tmp2
                };
            }
        }

        public Int32 GetPathLength(City secondCity) {
            OSRM_PairOfCities newPair = new OSRM_PairOfCities(GetStrCoords(), secondCity.GetStrCoords());
            if (newPair.GetDistance() != -1)
                return (Int32)Math.Round(newPair.GetDistance());
            else
                return 0;
        }

        public String GetStrCoords() {
            return Coord.longitude.ToString().Replace(',', '.') + ',' + Coord.latitude.ToString().Replace(',', '.');
        }

        //for random filling
        //public City(string name, double x, double y) {  
        //    Name = name;
        //    Coord = new Coordinates {
        //        longitude = x,
        //        latitude = y
        //    };
        //} 
        //public Int32 GetPathLength() => MainWindow.GetRandom;  
    }
}
