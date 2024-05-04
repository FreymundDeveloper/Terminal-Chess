using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame
{
    internal class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public Part[,] Parts;

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Parts = new Part[Rows, Columns];
        }

        public Part Part(int row, int column)
        {
            return Parts![row, column];
        }

        public Part Part(Position position)
        {
            return Parts![position.Row, position.Column];
        }

        // Part Moves
        public void PutPart(Part part, Position position)
        {
            if (HavePart(position)) throw new BoardGameException("The Board already has this Position!");
            Parts![position.Row, position.Column] = part;
            part.Position = position;
        }

        public Part? RemovePart(Position position) 
        {
            if (Part(position) == null) return null;

            Part aid = Part(position);
            aid.Position = null;
            Parts[position.Row, position.Column] = null!;
            return aid;

        }

        // Validations
        public bool HavePart(Position position)
        {
            ValidatePosition(position);
            return Part(position) != null;
        }

        public bool ValidPosition(Position position)
        {
            if (position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position position)
        {
            if (!ValidPosition(position)) throw new BoardGameException("Invalid Position!");
        }
    }
}
