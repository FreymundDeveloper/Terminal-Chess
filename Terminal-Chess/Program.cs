﻿using BoardGame;
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
                    Console.Clear();
                    Screen.PrintBoard(MatchChess.Board!);

                    Console.Write("\nOrigin: ");
                    Position origin = Screen.ReadPositionChess().ToPosition();
                    Console.Write("Destination: ");
                    Position destination = Screen.ReadPositionChess().ToPosition();

                    MatchChess.ExecuteMove(origin, destination);
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