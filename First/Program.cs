using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask {
    class Program {    
        static void Main(string[] args) {
            string sel;

            do {
                bool aFlag, bFlag;
                char[,] table = new char[10, 10];

                do {
                    aFlag = false;
                    bFlag = false;

                    Console.WriteLine("Hello, input your maze (10x10), please.");
                    Console.WriteLine("('0' for empty cell, '1' for wall, 'A' for start, 'B' for finish. Wrong symbols will be ignored)");

                    for (byte i = 0; i < 10; i++) {
                        string str;
                        bool flag;

                        do {
                            str = Console.ReadLine();
                            DeleteWrongSymbols(ref str);

                            flag = true;
                            bool aInLine = false, bInLine = false;

                            if (str.Length != 10) {
                                Console.WriteLine("Oops, less or more than 10 correct symbols per line, try again:");
                                flag = false;
                                continue;
                            }

                            foreach (char ch in str) {
                                if (ch == 'A' && !aFlag && !aInLine)
                                    aInLine = true;
                                else if (ch == 'A' && (aInLine || aFlag)) {
                                    flag = false;
                                    Console.WriteLine("Oops, 'A' has alredy been used, or more than one 'A' per line, try again:");
                                    break;
                                }

                                if (ch == 'B' && !bFlag && !bInLine)
                                    bInLine = true;
                                else if (ch == 'B' && (bInLine || bFlag)) {
                                    flag = false;
                                    Console.WriteLine("Oops, 'B' has alredy been used, or more than one 'B' per line, try again:");
                                    break;
                                }
                            }

                            if (flag && aInLine)
                                aFlag = true;
                            if (flag && bInLine)
                                bFlag = true;

                        } while (!flag);

                        byte col = 0;
                        foreach (char ch in str)
                            table[i, col++] = ch;
                    }
                    if (!aFlag || !bFlag)
                        Console.WriteLine("You forgot to indicate the beginning or end! Try again");

                    Console.WriteLine();
                } while (!aFlag || !bFlag);


                Maze maze = new Maze(table);
                Console.WriteLine("Your Maze:");

                byte newLine = 1;
                foreach (char ch in maze.Table) {
                    Console.Write(ch + " ");
                    if (newLine++ % 10 == 0)
                        Console.WriteLine();
                }
                Console.WriteLine();

                Console.WriteLine("Maze with paved way:");
                newLine = 1;
                table = maze.PaveWay();
                foreach (char ch in table) {
                    Console.Write(ch + " ");
                    if (newLine++ % 10 == 0)
                        Console.WriteLine();
                }
                Console.WriteLine();


                Console.WriteLine("Try again? (y/n)");
                sel = Console.ReadLine();
                while (sel != "n" && sel != "N" && sel != "y" && sel != "Y") {
                    Console.WriteLine("Wrong symbol!");
                    sel = Console.ReadLine();
                }

            } while (sel == "y" || sel == "Y");
            
            Console.ReadKey(true);
        }

        static void DeleteWrongSymbols(ref string str) {
            string tmp = "";
            foreach (char ch in str) {
                if (Maze.OneOfTrueSymbol(ch) || ch == '1')
                    tmp += ch;
            }
            str = tmp;
        }
    }
}
