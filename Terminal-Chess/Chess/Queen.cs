using BoardGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Queen : Part
    {
        public Queen(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "Q";
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
            while (Board.ValidPosition(positionNow) && CanMove(positionNow))
            {
                matrix[positionNow.Row, positionNow.Column] = true;
                if (Board.Part(positionNow) != null && Board.Part(positionNow).Color != Color) break;
                positionNow.DefineValues(positionNow.Row - 1, positionNow.Column);
            }

            // South
            positionNow.DefineValues(Position!.Row + 1, Position!.Column);
            while (Board.ValidPosition(positionNow) && CanMove(positionNow))
            {
                matrix[positionNow.Row, positionNow.Column] = true;
                if (Board.Part(positionNow) != null && Board.Part(positionNow).Color != Color) break;
                positionNow.DefineValues(positionNow.Row + 1, positionNow.Column);
            }

            // East
            positionNow.DefineValues(Position!.Row, Position!.Column + 1);
            while (Board.ValidPosition(positionNow) && CanMove(positionNow))
            {
                matrix[positionNow.Row, positionNow.Column] = true;
                if (Board.Part(positionNow) != null && Board.Part(positionNow).Color != Color) break;
                positionNow.DefineValues(positionNow.Row, positionNow.Column + 1);
            }

            // West
            positionNow.DefineValues(Position!.Row, Position!.Column - 1);
            while (Board.ValidPosition(positionNow) && CanMove(positionNow))
            {
                matrix[positionNow.Row, positionNow.Column] = true;
                if (Board.Part(positionNow) != null && Board.Part(positionNow).Color != Color) break;
                positionNow.DefineValues(positionNow.Row, positionNow.Column - 1);
            }

            // Northwest
            positionNow.DefineValues(Position!.Row - 1, Position!.Column - 1);
            while (Board.ValidPosition(positionNow) && CanMove(positionNow))
            {
                matrix[positionNow.Row, positionNow.Column] = true;
                if (Board.Part(positionNow) != null && Board.Part(positionNow).Color != Color) break;
                positionNow.DefineValues(positionNow.Row - 1, positionNow.Column - 1);
            }

            // Northeast
            positionNow.DefineValues(Position!.Row - 1, Position!.Column + 1);
            while (Board.ValidPosition(positionNow) && CanMove(positionNow))
            {
                matrix[positionNow.Row, positionNow.Column] = true;
                if (Board.Part(positionNow) != null && Board.Part(positionNow).Color != Color) break;
                positionNow.DefineValues(positionNow.Row - 1, positionNow.Column + 1);
            }

            // Southeast
            positionNow.DefineValues(Position!.Row + 1, Position!.Column + 1);
            while (Board.ValidPosition(positionNow) && CanMove(positionNow))
            {
                matrix[positionNow.Row, positionNow.Column] = true;
                if (Board.Part(positionNow) != null && Board.Part(positionNow).Color != Color) break;
                positionNow.DefineValues(positionNow.Row + 1, positionNow.Column + 1);
            }

            // Southwest
            positionNow.DefineValues(Position!.Row + 1, Position!.Column - 1);
            while (Board.ValidPosition(positionNow) && CanMove(positionNow))
            {
                matrix[positionNow.Row, positionNow.Column] = true;
                if (Board.Part(positionNow) != null && Board.Part(positionNow).Color != Color) break;
                positionNow.DefineValues(positionNow.Row + 1, positionNow.Column - 1);
            }

            return matrix;
        }
    }
}
