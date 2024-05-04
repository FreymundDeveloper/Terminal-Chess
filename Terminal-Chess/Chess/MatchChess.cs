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
        public int Turn { get; private set; }
        public Color? CurrentPlayer { get; private set; }
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

        public void MakePlay(Position origin, Position destination)
        {
            ExecuteMove(origin, destination);
            Turn++;
            ChangePlayer();
        }

        public void ValidateOriginPosition(Position position) 
        {
            if (Board?.Part(position) == null) throw new BoardGameException("There is no part in the origin position of the choice!");
            if (CurrentPlayer != Board?.Part(position).Color) throw new BoardGameException("The origin part chosen is't yours!");
            if (!(Board!.Part(position).HavePossibleMoves())) throw new BoardGameException("There is no moves in the origin part chosen!");
        }

        public void ValidateDestinationPosition(Position origin, Position destination) 
        {
            if (!(Board!.Part(origin).CanMoveTo(destination))) throw new BoardGameException("Destination position invalid!");
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White) CurrentPlayer = Color.Black;
            else CurrentPlayer = Color.White;
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
