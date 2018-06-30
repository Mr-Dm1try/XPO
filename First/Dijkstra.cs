using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask {
    class Dijkstra {
        public static Int16[] Algoritm(byte start, ref Int16[,] adjList, out short [] prev) {
            const byte vertCount = 100;

            short[] dist;
            short index, min, i;
            bool[] visited;

            dist = new short[vertCount];
            prev = new short[vertCount];
            visited = new bool[vertCount];

            //инициализация
            for (i = 0; i < vertCount; i++) {
                dist[i] = Int16.MaxValue;
                visited[i] = false;
            }
            dist[start] = 0;

            do {
                index = Int16.MaxValue;
                min = Int16.MaxValue;

                //поиск непройденой вершины с минимальной длиной пути до нее
                for (i = 0; i < vertCount; i++) {
                    if (!visited[i] && dist[i] < min) {
                        min = dist[i];
                        index = i;
                    }
                }

                if (index != Int16.MaxValue) {
                    for (i = 0; i < vertCount; i++) {
                        if (adjList[index, 0] == i ||
                            adjList[index, 1] == i ||
                            adjList[index, 2] == i ||
                            adjList[index, 3] == i) {
                            short tmp = (short) (min + 1);
                            if (tmp < dist[i]) {
                                dist[i] = tmp;
                                prev[i] =  index;
                            }
                        }
                    }
                    visited[index] = true;
                }
            } while (index < Int16.MaxValue);
            return dist;
        }

        public static Int16[] GetWay(byte start, byte finish, Int16[,] adjList) {
            Int16[] temp, way, prev = { 0 };
            short currVert, count = 1;

            temp = Algoritm(start, ref adjList, out prev);
            if (temp[finish] == Int16.MaxValue) {
                way = new Int16[1];
                way[0] = -1;
                return way;
            }

            currVert = finish;
            do {
                currVert = prev[currVert];
                count++;
            } while (currVert != start);

            way = new Int16[count];
            way[--count] = finish;

            currVert = finish;
            while (currVert != start) {
                currVert = prev[currVert];
                way[--count] = currVert;
            }

            return way;
        }
    }
}
