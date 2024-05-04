using BoardGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Tower : Part
    {
        public Tower(Board board, Color color) : base(board, color){}

        public override string ToString()
        {
            return "T";
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
                positionNow.Row--;
            }

            // South
            positionNow.DefineValues(Position!.Row + 1, Position!.Column);
            while (Board.ValidPosition(positionNow) && CanMove(positionNow))
            {
                matrix[positionNow.Row, positionNow.Column] = true;
                if (Board.Part(positionNow) != null && Board.Part(positionNow).Color != Color) break;
                positionNow.Row++;
            }

            // East
            positionNow.DefineValues(Position!.Row, Position!.Column + 1);
            while (Board.ValidPosition(positionNow) && CanMove(positionNow))
            {
                matrix[positionNow.Row, positionNow.Column] = true;
                if (Board.Part(positionNow) != null && Board.Part(positionNow).Color != Color) break;
                positionNow.Column++;
            }

            // West
            positionNow.DefineValues(Position!.Row, Position!.Column - 1);
            while (Board.ValidPosition(positionNow) && CanMove(positionNow))
            {
                matrix[positionNow.Row, positionNow.Column] = true;
                if (Board.Part(positionNow) != null && Board.Part(positionNow).Color != Color) break;
                positionNow.Column--;
            }

            return matrix;
        }
    }
}
