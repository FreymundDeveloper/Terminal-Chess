using BoardGame;
using System;

namespace TerminalChess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Position Position = new Position(3, 4);

            Console.WriteLine("Test: " + Position.ReturnData());
        }
    }
}