using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace FirstTask {
    class Program {    
        static void Main(string[] args) {
            string sel;
            Int16 selInpt;
            Console.WriteLine("\t### Hello! ###");
            do {
                Console.WriteLine("Select the input method: \n1) From keyboard \n2) From file");
                selInpt = Convert.ToInt16(Console.ReadLine());
                while (selInpt != 1 && selInpt != 2) {
                    Console.WriteLine("Wrong number!");
                    sel = Console.ReadLine();
                }
                Console.WriteLine();

                Maze maze = null;
                switch (selInpt) {
                    case 1: maze = new Maze(InputFromKeyboard()); break;
                    case 2: maze = new Maze(InputFromFile()); break;
                }               

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
                foreach (char ch in maze.PaveWay()) {
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

                Console.WriteLine();
            } while (sel == "y" || sel == "Y");
        }

        static Char[,] InputFromKeyboard() {
            bool aFlag, bFlag;
            Char[,] table = new Char[10, 10];

            do {
                aFlag = false;
                bFlag = false;

                Console.WriteLine("Input your maze (10x10), please.");
                Console.WriteLine("('0' for empty cell, '1' for wall, 'A' for start, 'B' for finish. Wrong symbols will be ignored)");

                for (byte i = 0; i < 10; i++) {
                    string str;
                    bool flag;

                    do {                                         
                        flag = true;
                        bool aInLine = false, bInLine = false;

                        str = Console.ReadLine();
                        DeleteWrongSymbols(ref str);

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
                    Console.WriteLine("You forgot to indicate the start or finish point! Try again");

                Console.WriteLine();
            } while (!aFlag || !bFlag);

            return table;
        }

        static Char[,] InputFromFile() {
            string fileName;
            FileStream file = null;
            string sel;
            Char[,] table = new Char[10, 10];

            //Открытие файла
            do {
                sel = "";
                Console.Write("Input file name: ");
                fileName = Console.ReadLine();

                try {
                    file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                }
                catch (FileNotFoundException) {
                    Console.WriteLine("File with this name not found. Enter 'c' to create or 'a' to try again.");
                    sel = Console.ReadLine();
                    while (sel != "c" && sel != "C" && sel != "a" && sel != "A") {
                        Console.WriteLine("Wrong symbol!");
                        sel = Console.ReadLine();
                    }

                    if (sel == "c" || sel == "C") {
                        file = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read);
                        file.Close();

                        Process.Start(fileName);
                        Console.WriteLine("Edit file and save changes, then press any key.");
                        Console.ReadKey(true);
                        
                        file = new FileStream(fileName, FileMode.Open, FileAccess.Read);                        
                    }
                    Console.WriteLine();
                }
            } while (sel == "a" || sel == "A");
            Console.WriteLine();
           
            //Считывание из файла
            StreamReader fileReader = new StreamReader(file);
            bool aFlag, bFlag;
            do {
                aFlag = false;
                bFlag = false;

                for (byte i = 0; i < 10; i++) {
                    string str;
                    bool flag = true;

                    do {
                        //Закрытие потоков и открытие файла для редактирования
                        if (!flag) {
                            fileReader.Close();
                            file.Close();

                            Process.Start(fileName);
                            Console.WriteLine("Edit file and save changes, then press any key.");
                            Console.ReadKey(true);
                            Console.WriteLine();

                            file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                            fileReader = new StreamReader(file);
                            for (int j = 0; j < i; j++)
                                fileReader.ReadLine();
                        }

                        flag = true;
                        bool aInLine = false, bInLine = false;

                        str = fileReader.ReadLine();                //Чтение строки из файла
                        if (str == null) {
                            Console.WriteLine("Oops, {0} line not found, edit file, please", i + 1);
                            flag = false;
                            continue;
                        }

                        DeleteWrongSymbols(ref str);
                        if (str.Length != 10) {
                            Console.WriteLine("Oops, less or more than 10 correct symbols in {0} line, edit file, please", i + 1);
                            flag = false;
                            continue;
                        }

                        foreach (char ch in str) {
                            if (ch == 'A' && !aFlag && !aInLine)
                                aInLine = true;
                            else if (ch == 'A' && (aInLine || aFlag)) {
                                flag = false;
                                Console.WriteLine("Oops, 'A' has alredy been used, or more than one 'A' in {0} line, edit file, please", i + 1);
                                break;
                            }

                            if (ch == 'B' && !bFlag && !bInLine)
                                bInLine = true;
                            else if (ch == 'B' && (bInLine || bFlag)) {
                                flag = false;
                                Console.WriteLine("Oops, 'B' has alredy been used, or more than one 'B' in {0} line, edit file, please", i + 1);
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
                if (!aFlag || !bFlag) {
                    Console.WriteLine("You forgot to indicate the start or finish point! Edit file, please");

                    fileReader.Close();
                    file.Close();

                    Process.Start(fileName);
                    Console.WriteLine("Edit file and save changes, then press any key.");
                    Console.ReadKey(true);
                    Console.WriteLine();

                    file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    fileReader = new StreamReader(file);
                }

                Console.WriteLine();
            } while (!aFlag || !bFlag);            

            return table;
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
