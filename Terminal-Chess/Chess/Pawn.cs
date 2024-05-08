using BoardGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Pawn : Part
    {
        private MatchChess MatchChess;

        public Pawn(Board board, Color color, MatchChess matchChess) : base(board, color) 
        {
            MatchChess = matchChess;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool HasAnEnemy(Position position)
        {
            Part part = Board!.Part(position);
            return part != null && part!.Color != Color;
        }

        private bool FreeToGo(Position position)
        {
            return Board!.Part(position) == null;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[Board!.Rows, Board!.Columns];

            Position positionNow = new Position(0, 0);

            if (Color == BoardGame.Color.White) // White Pawns
            {
                // Move
                positionNow.DefineValues(Position!.Row - 1, Position!.Column);
                if (Board.ValidPosition(positionNow) && FreeToGo(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

                positionNow.DefineValues(Position!.Row - 2, Position!.Column);
                if (Board.ValidPosition(positionNow) && FreeToGo(positionNow) && NumberOfMoves == 0) matrix[positionNow.Row, positionNow.Column] = true;

                // Attack
                positionNow.DefineValues(Position!.Row - 1, Position!.Column - 1);
                if (Board.ValidPosition(positionNow) && HasAnEnemy(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

                positionNow.DefineValues(Position!.Row - 1, Position!.Column + 1);
                if (Board.ValidPosition(positionNow) && HasAnEnemy(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

                // # Special - En Passant
                if (Position!.Row == 3)
                {
                    Position left = new Position(Position!.Row, Position!.Column - 1);
                    if (Board!.ValidPosition(left) && HasAnEnemy(left) && Board!.Part(left) == MatchChess.VulnerableEnPassant) matrix[left.Row - 1, left.Column] = true;

                    Position right = new Position(Position!.Row, Position!.Column + 1);
                    if (Board!.ValidPosition(right) && HasAnEnemy(right) && Board!.Part(right) == MatchChess.VulnerableEnPassant) matrix[right.Row - 1, right.Column] = true;
                }
            }
            else // Black Pawns
            {
                // Move
                positionNow.DefineValues(Position!.Row + 1, Position!.Column);
                if (Board.ValidPosition(positionNow) && FreeToGo(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

                positionNow.DefineValues(Position!.Row + 2, Position!.Column);
                if (Board.ValidPosition(positionNow) && FreeToGo(positionNow) && NumberOfMoves == 0) matrix[positionNow.Row, positionNow.Column] = true;

                // Attack
                positionNow.DefineValues(Position!.Row + 1, Position!.Column - 1);
                if (Board.ValidPosition(positionNow) && HasAnEnemy(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

                positionNow.DefineValues(Position!.Row + 1, Position!.Column + 1);
                if (Board.ValidPosition(positionNow) && HasAnEnemy(positionNow)) matrix[positionNow.Row, positionNow.Column] = true;

                // # Special - En Passant
                if (Position!.Row == 4)
                {
                    Position left = new Position(Position!.Row, Position!.Column - 1);
                    if (Board!.ValidPosition(left) && HasAnEnemy(left) && Board!.Part(left) == MatchChess.VulnerableEnPassant) matrix[left.Row + 1, left.Column] = true;

                    Position right = new Position(Position!.Row, Position!.Column + 1);
                    if (Board!.ValidPosition(right) && HasAnEnemy(right) && Board!.Part(right) == MatchChess.VulnerableEnPassant) matrix[right.Row + 1, right.Column] = true;
                }

            }

            return matrix;
        }
    }
}
