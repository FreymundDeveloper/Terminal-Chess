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
                MatchChess MatchChess = new MatchChess();

                while (!MatchChess.Finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintBoard(MatchChess.Board!);
                        Console.WriteLine();
                        Console.WriteLine("Turn: " + MatchChess.Turn);
                        Console.WriteLine("Waiting play: " + MatchChess.CurrentPlayer);

                        Console.Write("\nOrigin: ");
                        Position origin = Screen.ReadPositionChess().ToPosition();
                        MatchChess.ValidateOriginPosition(origin);

                        bool[,] possiblePositions = MatchChess.Board!.Part(origin).PossibleMoves();
                        Console.Clear();
                        Screen.PrintBoard(MatchChess.Board!, possiblePositions);

                        Console.Write("\nDestination: ");
                        Position destination = Screen.ReadPositionChess().ToPosition();
                        MatchChess.ValidateDestinationPosition(origin, destination);

                        MatchChess.MakePlay(origin, destination);
                    }
                    catch (BoardGameException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

            }
            catch (BoardGameException e) {
                Console.WriteLine(e.Message);
            }

            //PositionChess PositionChess = new PositionChess('c', 7);
            //Console.WriteLine(PositionChess.ToPosition().ReturnData());
        }
    }
}