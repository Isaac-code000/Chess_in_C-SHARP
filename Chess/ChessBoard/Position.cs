using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoard
{
    internal class Position
    {
        public int Lines { get; set; }
        public int Columns { get; set; }


        public Position(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
        }

        public override string ToString()
        {
            return Lines + ", " + Columns;
        }
    }
}
