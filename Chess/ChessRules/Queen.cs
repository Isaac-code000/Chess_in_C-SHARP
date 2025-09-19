using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessBoard;

namespace ChessRules
{
    internal class Queen : Piece
    {
        public Queen(Colors color, Board board) : base(color, board)
        {
        }
        public override string ToString()
        {
            return "Q";
        }
    }
   
}
