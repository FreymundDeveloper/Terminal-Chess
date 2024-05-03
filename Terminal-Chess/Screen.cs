using BoardGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalChess
{
    internal class Screen
    {
        public static void PrintBoard(Board Board)
        {
            for (int i = 0; i < Board.Rows;  i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (Board.Part(j, i) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(Board.Part(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
