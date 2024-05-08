using BoardGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Horse : Part
    {
        public Horse(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "H";
        }

        private bool CanMove(Position position)
        {
            Part part = Board!.Part(position);
            return part == null || part.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[Board!.Rows, Board!.Columns];

            Position positionNow = new Position(0, 0);

            // South -> East
            positionNow.DefineValues(Position!.Row - 1, Position!.Column - 2);
            if (Board.ValidPosition(positionNow) && CanMove(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

            positionNow.DefineValues(Position!.Row - 2, Position!.Column - 1);
            if (Board.ValidPosition(positionNow) && CanMove(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

            // South -> West
            positionNow.DefineValues(Position!.Row - 2, Position!.Column + 1);
            if (Board.ValidPosition(positionNow) && CanMove(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

            positionNow.DefineValues(Position!.Row - 1, Position!.Column + 2);
            if (Board.ValidPosition(positionNow) && CanMove(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

            // North -> East
            positionNow.DefineValues(Position!.Row + 1, Position!.Column + 2);
            if (Board.ValidPosition(positionNow) && CanMove(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

            positionNow.DefineValues(Position!.Row + 2, Position!.Column + 1);
            if (Board.ValidPosition(positionNow) && CanMove(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

            // North -> West
            positionNow.DefineValues(Position!.Row + 2, Position!.Column - 1);
            if (Board.ValidPosition(positionNow) && CanMove(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

            positionNow.DefineValues(Position!.Row + 1, Position!.Column - 2);
            if (Board.ValidPosition(positionNow) && CanMove(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

            return matrix;
        }
    }
}
