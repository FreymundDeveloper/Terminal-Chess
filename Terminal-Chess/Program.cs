using BoardGame;
using Chess;
using System;

namespace TerminalChess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try {
                Board Board = new Board(8, 8);

                Board.PutPart(new Tower(Board, Color.Black), new Position(0, 0));
                Board.PutPart(new Tower(Board, Color.Black), new Position(1, 3));
                Board.PutPart(new King(Board, Color.Black), new Position(0, 2));

                Board.PutPart(new Tower(Board, Color.White), new Position(5, 3));

                Screen.PrintBoard(Board);
            }
            catch (BoardGameException e) {
                Console.WriteLine(e.Message);
            }

            //PositionChess PositionChess = new PositionChess('c', 7);
            //Console.WriteLine(PositionChess.ToPosition().ReturnData());
        }
    }
}