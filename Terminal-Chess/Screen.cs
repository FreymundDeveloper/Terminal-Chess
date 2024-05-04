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
                Console.Write(8 - i + " ");
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (Board.Part(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        PrintPart(Board.Part(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintPart(Part part) 
        {
            if (part.Color == Color.White) Console.Write(part);
            else
            {
                ConsoleColor aid = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(part);
                Console.ForegroundColor = aid;

            }
        }
    }
}
