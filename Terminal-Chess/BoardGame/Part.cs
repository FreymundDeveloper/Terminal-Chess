using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame
{
    internal class Part
    {
        public Position? Position { get; set; } 
        public Color? Color { get; protected set; }
        public int NumberOfMoves { get; protected set; }
        public Board? BoardGame { get; protected set; }

        public Part(Position position, Board boardGame, Color color) {
            Position = position;
            BoardGame = boardGame;
            Color = color;
            NumberOfMoves = 0;
        }

    }
}
