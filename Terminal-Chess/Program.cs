using BoardGame;
using System;

namespace TerminalChess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Position Position = new Position(3, 4);
            Board Board = new Board(8, 8);

            Console.WriteLine("Test: " + Position.ReturnData());
        }
    }
}