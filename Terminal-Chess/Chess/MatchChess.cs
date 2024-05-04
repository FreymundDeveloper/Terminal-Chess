using BoardGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    internal class MatchChess
    {
        public Board? Board { get; private set; }
        private int Turn;
        private Color? CurrentPlayer;
        public bool Finished { get; private set; }

        public MatchChess()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            PutParts();
        }

        public void ExecuteMove(Position origin, Position destination)
        {
            Part part = Board!.RemovePart(origin)!;
            part.IncreaseNumberOfMoves();
            Part partTaked = Board!.RemovePart(destination)!;
            Board.PutPart(part, destination);
        }

        private void PutParts()
        {
            // White Parts
            Board!.PutPart(new Tower(Board, Color.White), new PositionChess('c', 1).ToPosition());
            Board!.PutPart(new Tower(Board, Color.White), new PositionChess('c', 2).ToPosition());
            Board!.PutPart(new Tower(Board, Color.White), new PositionChess('d', 2).ToPosition());
            Board!.PutPart(new Tower(Board, Color.White), new PositionChess('e', 2).ToPosition());
            Board!.PutPart(new Tower(Board, Color.White), new PositionChess('e', 1).ToPosition());

            Board!.PutPart(new King(Board, Color.White), new PositionChess('d', 1).ToPosition());

            // Black Parts
            Board!.PutPart(new Tower(Board, Color.Black), new PositionChess('c', 7).ToPosition());
            Board!.PutPart(new Tower(Board, Color.Black), new PositionChess('c', 8).ToPosition());
            Board!.PutPart(new Tower(Board, Color.Black), new PositionChess('d', 7).ToPosition());
            Board!.PutPart(new Tower(Board, Color.Black), new PositionChess('e', 7).ToPosition());
            Board!.PutPart(new Tower(Board, Color.Black), new PositionChess('e', 8).ToPosition());

            Board!.PutPart(new King(Board, Color.Black), new PositionChess('d', 8).ToPosition());
        }
    }
}
