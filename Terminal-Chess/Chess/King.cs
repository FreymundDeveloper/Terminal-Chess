using BoardGame;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class King : Part
    {
        private MatchChess MatchChess;

        public King(Board board, Color color, MatchChess matchChess) : base(board, color) 
        {
            MatchChess = matchChess;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position position)
        {
            Part part = Board!.Part(position);
            return part == null || part.Color != Color;
        }

        private bool TowerCanDoCastling(Position position)
        {
            Part part = Board!.Part(position);
            return part != null && part is Tower && part.Color == Color && part.NumberOfMoves == 0;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[Board!.Rows, Board!.Columns];

            Position positionNow = new Position(0, 0);

            // North
            positionNow.DefineValues(Position!.Row - 1, Position!.Column);
            if (Board.ValidPosition(positionNow) && CanMove(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

            // Northeast
            positionNow.DefineValues(Position!.Row - 1, Position!.Column + 1);
            if (Board.ValidPosition(positionNow) && CanMove(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

            // East
            positionNow.DefineValues(Position!.Row, Position!.Column + 1);
            if (Board.ValidPosition(positionNow) && CanMove(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

            // Southeast
            positionNow.DefineValues(Position!.Row + 1, Position!.Column + 1);
            if (Board.ValidPosition(positionNow) && CanMove(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

            // South
            positionNow.DefineValues(Position!.Row + 1, Position!.Column);
            if (Board.ValidPosition(positionNow) && CanMove(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

            // Southwest
            positionNow.DefineValues(Position!.Row + 1, Position!.Column - 1);
            if (Board.ValidPosition(positionNow) && CanMove(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

            // West
            positionNow.DefineValues(Position!.Row, Position!.Column - 1);
            if (Board.ValidPosition(positionNow) && CanMove(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

            // Northwest
            positionNow.DefineValues(Position!.Row - 1, Position!.Column - 1);
            if (Board.ValidPosition(positionNow) && CanMove(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

            // #Specials Castling
            if (NumberOfMoves == 0 && !(MatchChess.Check))
            {
                // # Special - Short Castling
                Position positionTower1 = new Position(Position!.Row, Position!.Column + 3);
                if (TowerCanDoCastling(positionTower1))
                {
                    Position position1 = new Position(Position!.Row, Position!.Column + 1);
                    Position position2 = new Position(Position!.Row, Position!.Column + 2);

                    if (Board!.Part(position1) == null && Board!.Part(position2) == null) matrix[Position!.Row, Position!.Column + 2] = true;
                }

                // # Special - Long Castling
                Position positionTower2 = new Position(Position!.Row, Position!.Column - 4);
                if (TowerCanDoCastling(positionTower2))
                {
                    Position position1 = new Position(Position!.Row, Position!.Column - 1);
                    Position position2 = new Position(Position!.Row, Position!.Column - 2);
                    Position position3 = new Position(Position!.Row, Position!.Column - 3);

                    if (Board!.Part(position1) == null && Board!.Part(position2) == null && Board!.Part(position3) == null) matrix[Position!.Row, Position!.Column - 2] = true;
                }
            }

            return matrix;
        }
    }
}
