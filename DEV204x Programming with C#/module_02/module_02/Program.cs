using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace module_02
{
    class Program
    {
        static void Main(string[] args)
        {
            const int boardSize = 8;
            string[][] board = new string[boardSize][];

            for (int i = 0; i < boardSize; i++)
            {
                string oddChar;
                string evenChar;
                if (i % 2 == 1)
                {
                    oddChar = "X";
                    evenChar = "O";
                }
                else
                {
                    oddChar = "O";
                    evenChar = "X";
                }

                string[] row = new string[boardSize];
                for (int j = 0; j < boardSize; j++)
                {
                    string j_type;
                    if (j%2 == 1)
                        row[j] = oddChar;
                    else
                        row[j] = evenChar;
                }
                board[i] = row;
                Console.WriteLine(string.Join("",row));
            }

        }
    }
}
