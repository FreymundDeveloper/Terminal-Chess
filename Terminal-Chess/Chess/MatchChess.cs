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
        private HashSet<Part> Parts;
        private HashSet<Part> Captureds;

        public MatchChess()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Parts = new HashSet<Part>();
            Captureds = new HashSet<Part>();
            PutParts();
        }

        public void ExecuteMove(Position origin, Position destination)
        {
            Part part = Board!.RemovePart(origin)!;
            part.IncreaseNumberOfMoves();
            Part partTaked = Board!.RemovePart(destination)!;
            Board.PutPart(part, destination);

            if (partTaked != null) Captureds.Add(partTaked);
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

        public HashSet<Part> CapturedsParts(Color color)
        {
            HashSet<Part> aid = new HashSet<Part>();
            foreach (Part x in Captureds) {
                if (x.Color == color) aid.Add(x);
            }
            return aid;
        }

        public HashSet<Part> InGameParts(Color color)
        {
            HashSet<Part> aid = new HashSet<Part>();
            foreach (Part x in Parts)
            {
                if (x.Color == color) aid.Add(x);
            }
            aid.ExceptWith(CapturedsParts(color));
            return aid;
        }

        public void PutNewPart(char column, int row, Part part)
        {
            Board!.PutPart(part, new PositionChess(column, row).ToPosition());
            Parts.Add(part);
        }

        private void PutParts()
        {
            // White Parts
            PutNewPart('c', 1, new Tower(Board!, Color.White));
            PutNewPart('c', 2, new Tower(Board!, Color.White));
            PutNewPart('d', 2, new Tower(Board!, Color.White));
            PutNewPart('e', 2, new Tower(Board!, Color.White));
            PutNewPart('e', 1, new Tower(Board!, Color.White));

            PutNewPart('d', 1, new King(Board!, Color.White));

            // Black Parts
            PutNewPart('c', 7, new Tower(Board!, Color.Black));
            PutNewPart('c', 8, new Tower(Board!, Color.Black));
            PutNewPart('d', 7, new Tower(Board!, Color.Black));
            PutNewPart('e', 7, new Tower(Board!, Color.Black));
            PutNewPart('e', 8, new Tower(Board!, Color.Black));

            PutNewPart('d', 8, new King(Board!, Color.Black));
        }
    }
}
