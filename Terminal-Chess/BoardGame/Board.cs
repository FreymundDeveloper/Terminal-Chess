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
        public Part[,]? Parts;

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
    }
}
