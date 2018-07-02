using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTask.Classes {
    class Travel {
        public List<City> Cities { get; private set; }
        private Int32[,] Graph { get; set; }

        public Int32 RouteLength { get; private set; }
        private List<Int32> Route { get; set; }
        public String AproxTime { get; private set; }
        
        public Travel() {
            Cities = new List<City>();
            Graph = new Int32[4, 4];
            RouteLength = Int32.MaxValue;
            
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Graph[i, j] = 0;
            
        }

        public void AddCity(String name, Double x, Double y) {
            Cities.Add(new City(name, x, y));
            if(Cities.Count > 1)
                AddPath(Cities.Last());            
        }

        private void AddPath(City newCity) {
            for (int i = 0; i < Cities.Count - 1; i++) {
                Int32 temp = newCity.GetPathLength(Cities[i]);
                Graph[Cities.Count - 1, i] = temp;
                Graph[i, Cities.Count - 1] = temp;
            }
        }

        public void ComputeRoute() {
            Int32 vertCount = Cities.Count;
            Int32[,] adjMatx = Graph;

            adjMatx[0, 0] = 1;

            Int32[] currRoute = new Int32[vertCount];

            Algorithm(1, 0, currRoute, vertCount, adjMatx);

            Int32 sec = (Int32) Math.Round(RouteLength / 0.025); //90 км/ч в км/с

            AproxTime = (sec / 3600).ToString() + " h  " + ((sec % 3600) / 60).ToString() + " m";
        }

        private void Algorithm(Int32 step, Int32 currVert, Int32[] currRoute, Int32 vertCount, Int32[,] matx) {
            for (int i = 0; i < vertCount; i++) {
                if (i != currVert && matx[currVert, i] != 0) {                 
                    if (matx[i, i] == 0) {
                        currRoute[step] = i;
                        matx[i, i] = 1;
                        Algorithm(step + 1, i, currRoute, vertCount, matx);
                        matx[i, i] = 0;
                    }

                    if (step == vertCount) {
                        CheckSum(currRoute);
                        break;
                    }
                }
            }
        }

        private void CheckSum(Int32[] currRoute) {
            Int32 sum = 0;

            for (int i = 0; i < currRoute.Length - 1; i++)
                sum += Graph[currRoute[i], currRoute[i + 1]];

            if (sum < RouteLength) {
                RouteLength = sum;
                Route = new List<Int32>();
                foreach (Int32 i in currRoute)
                    Route.Add(i);
            }
        }

        public String GetRoute() {
            String route = "";
            for (Int32 i = 0; i < Route.Count; i++) {
                if (i < Route.Count - 1)
                    route += Cities[Route[i]].Name + " -> ";
                else
                    route += Cities[Route[i]].Name;
            }

            return route;
        }
    }
}
