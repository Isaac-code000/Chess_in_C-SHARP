using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessBoard;
using Chess.ChessBoard;

namespace ChessRules
{
    internal class ChessPosition
    {
        public char Column { get; set; }
        public int Line { get; set; }

        public ChessPosition(char column, int line)
        {
            if (column < 'A' || column > 'H' || line < 1 || line > 8)
            {
                throw new BoardException("Error instantiating ChessPosition. Valid values are from a1 to h8.");
            }
            Column = column;
            Line = line;
        }


        public Position ToPosition()
        {
            return new Position(8 - Line, Column - 'a');
        }

        public override string ToString()
        {
            return "" + Column + Line;
        }
    }
}
