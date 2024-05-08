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
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        public bool Check {  get; private set; }
        private HashSet<Part> Parts;
        private HashSet<Part> Captureds;

        public MatchChess()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            Parts = new HashSet<Part>();
            Captureds = new HashSet<Part>();
            PutParts();
        }

        // Moves Methods
        public Part ExecuteMove(Position origin, Position destination)
        {
            Part part = Board!.RemovePart(origin)!;
            part.IncreaseNumberOfMoves();
            Part partTaked = Board!.RemovePart(destination)!;
            Board.PutPart(part, destination);

            if (partTaked != null) Captureds.Add(partTaked);

            // # Special - Short Castling
            if (part is King && destination.Column == origin.Column + 2)
            {
                Position originTower = new Position(origin.Row, origin.Column + 3);
                Position destinationTower = new Position(origin.Row, origin.Column + 1);

                Part tower = Board.RemovePart(originTower)!;
                tower.IncreaseNumberOfMoves();
                Board.PutPart(tower, destinationTower);
            }

            // # Special - Long Castling
            if (part is King && destination.Column == origin.Column - 2)
            {
                Position originTower = new Position(origin.Row, origin.Column - 4);
                Position destinationTower = new Position(origin.Row, origin.Column - 1);

                Part tower = Board.RemovePart(originTower)!;
                tower.IncreaseNumberOfMoves();
                Board.PutPart(tower, destinationTower);
            }

            return partTaked!;
        }

        public void UndoMove(Position origin, Position destination, Part partTaked) 
        {
            Part part = Board!.RemovePart(destination)!;
            part.DecrementNumberOfMoves();
            if (partTaked != null)
            {
                Board.PutPart(partTaked, destination);
                Captureds.Remove(partTaked);
            }
            Board.PutPart(part, origin);

            // # Special - Short Castling
            if (part is King && destination.Column == origin.Column + 2)
            {
                Position originTower = new Position(origin.Row, origin.Column + 3);
                Position destinationTower = new Position(origin.Row, origin.Column + 1);

                Part tower = Board.RemovePart(destinationTower)!;
                tower.DecrementNumberOfMoves();
                Board.PutPart(tower, originTower);
            }

            // # Special - Long Castling
            if (part is King && destination.Column == origin.Column - 2)
            {
                Position originTower = new Position(origin.Row, origin.Column - 4);
                Position destinationTower = new Position(origin.Row, origin.Column - 1);

                Part tower = Board.RemovePart(destinationTower)!;
                tower.DecrementNumberOfMoves();
                Board.PutPart(tower, originTower);
            }
        }

        public void MakePlay(Position origin, Position destination)
        {
            Part partTaked = ExecuteMove(origin, destination);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMove(origin, destination, partTaked);
                throw new BoardGameException("You cannot put yourself in check!");
            }

            if (IsInCheck(Rival(CurrentPlayer))) Check = true;
            else Check = false;

            if (IsACheckmate(Rival(CurrentPlayer))) Finished = true;
            else
            {
                Turn++;
                ChangePlayer();
            }
        }

        // Validate Exceptions Methods
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

        // Miscellaneous Methods
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

        private Color Rival(Color color)
        {
            if (color == Color.White) return Color.Black;
            else return Color.White;
        }

        private Part TheKing(Color color)
        {
            foreach (Part x in InGameParts(color)) 
            {
                if (x is King) return x;   
            }
            return null!;
        }

        public bool IsInCheck(Color color)
        {
            Part theKing = TheKing(color);

            if (theKing == null) throw new BoardGameException($"have no King with the color {color} on the board!");

            foreach (Part x in InGameParts(Rival(color)))
            {
                bool[,] matrix = x.PossibleMoves();

                if (matrix[theKing.Position!.Row, theKing.Position.Column]) return true;
            }
            return false;
        }

        public bool IsACheckmate(Color color)
        {
            if (!IsInCheck(color)) return false;

            foreach (Part x in InGameParts(color))
            {
                bool[,] matrix = x.PossibleMoves();

                for (int i = 0; i < Board!.Rows; i++)
                {
                    for (int j = 0; j < Board!.Columns; j++)
                    {
                        if (matrix[i, j])
                        {
                            Position origin = x.Position!;
                            Position destination = new Position(i, j);

                            Part partTaked = ExecuteMove(origin, destination);
                            bool checkTest = IsInCheck(color);

                            UndoMove(origin, destination, partTaked);

                            if (!checkTest) return false;
                        }
                    }
                }
            }
            return true;
        }

        // Parts Manipulate Methods
        public void PutNewPart(char column, int row, Part part)
        {
            Board!.PutPart(part, new PositionChess(column, row).ToPosition());
            Parts.Add(part);
        }

        private void PutParts()
        {
            // White Parts
            PutNewPart('a', 1, new Tower(Board!, Color.White));
            PutNewPart('b', 1, new Horse(Board!, Color.White));
            PutNewPart('c', 1, new Bishop(Board!, Color.White));
            PutNewPart('d', 1, new Queen(Board!, Color.White));
            PutNewPart('e', 1, new King(Board!, Color.White, this));
            PutNewPart('f', 1, new Bishop(Board!, Color.White));
            PutNewPart('g', 1, new Horse(Board!, Color.White));
            PutNewPart('h', 1, new Tower(Board!, Color.White));

            PutNewPart('a', 2, new Pawn(Board!, Color.White));
            PutNewPart('b', 2, new Pawn(Board!, Color.White));
            PutNewPart('c', 2, new Pawn(Board!, Color.White));
            PutNewPart('d', 2, new Pawn(Board!, Color.White));
            PutNewPart('e', 2, new Pawn(Board!, Color.White));
            PutNewPart('f', 2, new Pawn(Board!, Color.White));
            PutNewPart('g', 2, new Pawn(Board!, Color.White));
            PutNewPart('h', 2, new Pawn(Board!, Color.White));

            // Black Parts
            PutNewPart('a', 8, new Tower(Board!, Color.Black));
            PutNewPart('b', 8, new Horse(Board!, Color.Black));
            PutNewPart('c', 8, new Bishop(Board!, Color.Black));
            PutNewPart('d', 8, new Queen(Board!, Color.Black));
            PutNewPart('e', 8, new King(Board!, Color.Black, this));
            PutNewPart('f', 8, new Bishop(Board!, Color.Black));
            PutNewPart('g', 8, new Horse(Board!, Color.Black));
            PutNewPart('h', 8, new Tower(Board!, Color.Black));

            PutNewPart('a', 7, new Pawn(Board!, Color.Black));
            PutNewPart('b', 7, new Pawn(Board!, Color.Black));
            PutNewPart('c', 7, new Pawn(Board!, Color.Black));
            PutNewPart('d', 7, new Pawn(Board!, Color.Black));
            PutNewPart('e', 7, new Pawn(Board!, Color.Black));
            PutNewPart('f', 7, new Pawn(Board!, Color.Black));
            PutNewPart('g', 7, new Pawn(Board!, Color.Black));
            PutNewPart('h', 7, new Pawn(Board!, Color.Black));
        }
    }
}
