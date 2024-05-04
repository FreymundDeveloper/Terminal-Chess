using BoardGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class King : Part
    {
        public King(Board board, Color color) : base(board, color) {}

        public override string ToString()
        {
            return "R";
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

            return matrix;
        }
    }
}
