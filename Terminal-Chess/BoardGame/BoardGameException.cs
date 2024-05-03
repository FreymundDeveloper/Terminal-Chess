using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame
{
    internal class BoardGameException : Exception
    {
        public BoardGameException(string message) : base(message) { }
    }
}
