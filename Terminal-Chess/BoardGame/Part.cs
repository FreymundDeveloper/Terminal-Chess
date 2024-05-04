using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame
{
    abstract class Part
    {
        public Position? Position { get; set; } 
        public Color? Color { get; protected set; }
        public int NumberOfMoves { get; protected set; }
        public Board? Board { get; protected set; }

        public Part(Board board, Color color) {
            Position = null;
            Board = board;
            Color = color;
            NumberOfMoves = 0;
        }

        public void IncreaseNumberOfMoves()
        {
            NumberOfMoves++;
        }

        public abstract bool[,] PossibleMoves();
    }
}
