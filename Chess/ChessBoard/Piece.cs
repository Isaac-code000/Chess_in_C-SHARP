using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChessBoard
{
    class Piece
    {
        public Colors Color { get; set; }
        public Position Position { get; protected set; }
        public int MoveCount { get; protected set; }
        public Board Board { get; protected set; }

        public string Name { get; set; }    

        public Piece(Colors color, Board board)
        {
            Color = color;
            Board = board;
            Position = null;
            MoveCount = 0;
        }

        public void newPosition(Position position)
        {
            Position = position;
            
        }

        public void increaseMoveCount()
        {
            MoveCount++;
        }   




    }
}
