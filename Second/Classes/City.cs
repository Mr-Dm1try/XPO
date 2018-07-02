using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTask.Classes {
    struct Coordinates {
        public Double longitude;
        public Double latitude;
    }

    class City {
        public String Name { get; }
        public Coordinates Coord { get; }

        public City(string name, double x, double y) {
            Name = name;
            Coord = new Coordinates {
                longitude = x,
                latitude = y
            };
        }

        public Int32 GetPathLength(City secCity) => MainWindow.GetRandom;
    }
}
