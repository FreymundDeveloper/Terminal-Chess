using BoardGame;
using Chess;
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
                    PrintPart(Board.Part(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintBoard(Board Board, bool[,] possiblePositions)
        {
            ConsoleColor displayOrigin = Console.BackgroundColor;
            ConsoleColor displayChaged = ConsoleColor.DarkGray;

            for (int i = 0; i < Board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (possiblePositions[i, j]) Console.BackgroundColor = displayChaged;
                    else Console.BackgroundColor = displayOrigin;

                    PrintPart(Board.Part(i, j));
                    Console.BackgroundColor = displayOrigin;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = displayOrigin;
        }

        public static PositionChess ReadPositionChess()
        {
            string digit = Console.ReadLine()!;
            char column = digit[0];
            int row = int.Parse(digit[1] + "");
            return new PositionChess(column, row);
        }

        public static void PrintPart(Part part) 
        {
            if (part == null) Console.Write("- ");
            else
            {
                if (part.Color == Color.White) Console.Write(part);
                else
                {
                    ConsoleColor aid = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(part);
                    Console.ForegroundColor = aid;

                }
                Console.Write(" ");
            }
        }
    }
}
