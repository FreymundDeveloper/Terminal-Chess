﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame
{
    internal class Position
    {
        public int Row {  get; set; }
        public int Column { get; set; }

        public Position(int row, int column) {  
            Row = row; 
            Column = column; 
        }

        public void DefineValues(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public string ReturnData()
        {
            return $"{Row}, {Column}";
        }
    }
}
