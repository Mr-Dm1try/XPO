using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace SecondTask.Model {
    class OSRM_PairOfCities {
        private readonly String url = "http://router.project-osrm.org/route/v1/car";
        private PairOfCities Pair { get; set; }
        public String FirstCoords { get; private set; }
        public String SecondCoords { get; private set; }

        public OSRM_PairOfCities(Double x1, Double y1, Double x2, Double y2) {
            FirstCoords = x1.ToString().Replace(',', '.') + ',' + y1.ToString().Replace(',', '.');
            SecondCoords = x2.ToString().Replace(',', '.') + ',' + y2.ToString().Replace(',', '.');

            DoRequest();
        }

        public OSRM_PairOfCities(String x1, String y1, String x2, String y2) {
            FirstCoords = x1 + ',' + y1;
            SecondCoords = x2 + ',' + y2;

            DoRequest();
        }

        public OSRM_PairOfCities(String coords1, String coords2) {
            FirstCoords = coords1;
            SecondCoords = coords2;

            DoRequest();
        }

        public OSRM_PairOfCities(City city1, City city2) {
            FirstCoords = city1.GetStrCoords();
            SecondCoords = city2.GetStrCoords();

            DoRequest();
        }

        private void DoRequest() {
            Boolean flag;
            Int32 i = 0;
            do {
                flag = false;
                try {
                    HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url + "/" + FirstCoords + ";" + SecondCoords);
                    HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                    String response;

                    using (StreamReader reader = new StreamReader(httpResponse.GetResponseStream())) {
                        response = reader.ReadToEnd();
                    }

                    Pair = JsonConvert.DeserializeObject<PairOfCities>(response);
                }
                catch (Exception) {
                    flag = true;
                }
                i++;
            } while (flag && i < 250);

            if (i == 250)
                throw new Exception("Server is not responding");
        }

        public Double GetDistance() {
            if (Pair.code == "NoRoute")
                return -1;
            else
                return Pair.routes[0].distance;
        }

        public Double GetDuration() {
            if(Pair.code == "NoRoute")
                return -1;
            else
                return Pair.routes[0].duration;
        }
    }

    class PairOfCities {
        public List<Route> routes { get; set; }
        public string code { get; set; }
    }

    class Route {
        public string geometry { get; set; }
        public List<Leg> legs { get; set; }
        public string weight_name { get; set; }
        public double weight { get; set; }
        public double duration { get; set; }
        public double distance { get; set; }
    }

    class Leg {
        public string summary { get; set; }
        public double weight { get; set; }
        public double duration { get; set; }
        public List<object> steps { get; set; }
        public double distance { get; set; }
    }
}
