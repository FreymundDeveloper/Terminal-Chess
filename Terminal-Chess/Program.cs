using BoardGame;
using Chess;
using System;

namespace TerminalChess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Position Position = new Position(3, 4);
            Board Board = new Board(8, 8);

            Board.PutPart(new Tower(Board, Color.Black), new Position(0, 0));
            Board.PutPart(new Tower(Board, Color.Black), new Position(1, 3));
            Board.PutPart(new King(Board, Color.Black), new Position(2, 4));

            Screen.PrintBoard(Board);

            //Console.WriteLine("Test: " + Position.ReturnData());
        }
    }
}