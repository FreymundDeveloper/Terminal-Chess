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
        public King(Board board, Color color) : base(board, color) 
        {
            
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
