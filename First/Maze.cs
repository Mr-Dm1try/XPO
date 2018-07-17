using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask {
    class Maze {
        public char[,] Table { get; }
        Int16[,] AdjList { get; }
        byte Start { get; }
        byte Finish { get; }

        public Maze(char[,] table) {
            AdjList = new Int16[100, 4];
            Table = table;

            bool aFlag = false, bFlag = false;
            byte vert = 0;
            foreach (char ch in Table) {
                if (ch == 'A' && !aFlag) {
                    aFlag = true;
                    Start = vert;
                } 
                else if (ch == 'B' && !bFlag) {
                    bFlag = true;
                    Finish = vert;
                }
                vert++;
                if (aFlag && bFlag)
                    break;
            }

            if (!aFlag || !bFlag)
                throw new Exception("Start or finish point not found!");

            TableToAdjList();
        }

        private void TableToAdjList() {
            for (byte i = 0; i < 10; i++) {
                for (byte j = 0; j < 10; j++) {
                    byte currVert = (byte)(i * 10 + j);
                    byte column = 0;

                    if (j > 0 && OneOfTrueSymbol(Table[i, j - 1])) 
                        AdjList[currVert, column++] = (byte)(currVert - 1);                    

                    if (i > 0 && OneOfTrueSymbol(Table[i - 1, j])) 
                        AdjList[currVert, column++] = (byte)(currVert - 10);
                    
                    if (j < 9 && OneOfTrueSymbol(Table[i, j + 1]))
                        AdjList[currVert, column++] = (byte)(currVert + 1);

                    if (i < 9 && OneOfTrueSymbol(Table[i + 1, j]))
                        AdjList[currVert, column++] = (byte)(currVert + 10);

                    if (column < 4)
                        for (byte c = column; c < 4; c++)
                            AdjList[currVert, c] = Int16.MaxValue;
                }
            }  
        }

        public char[,] PaveWay() {
            Int16[] way = Dijkstra.GetWay(Start, Finish, AdjList);
            char[,] newTable;

            if (way.Length == 1 && way[0] == -1) {
                byte i = 0;
                string str = "ThereIsNoWayFromAToB";

                newTable = new char[1, str.Length];
                foreach (char ch in str)
                    newTable[0, i++] = ch;
            }
            else {

                newTable = Table;

                for (short i = 1; i < way.Length - 1; i++) {
                    newTable[way[i] / 10, way[i] % 10] = '*';
                }
            }
            return newTable;
        }    
        
        public static Boolean OneOfTrueSymbol(char symb) {
            return symb == '0' || symb == 'A' || symb == 'B';
        }
    }
}
