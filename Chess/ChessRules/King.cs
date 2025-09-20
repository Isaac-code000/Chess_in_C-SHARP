using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessBoard;

namespace ChessRules
{
    internal class King : Piece
    {
        public King(Colors color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position position)
        {
            Piece p = Board.GetPiece(position);
            return p == null || p.Color != Color;
        }

        private void TestMove(Position position, bool[,] mat)
        {
            if (Board.ValidatePosition(position) && CanMove(position))
            {
                mat[position.Lines, position.Columns] = true;
            }
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            pos.SetValues(Position.Lines - 1, Position.Columns - 1);
            TestMove(pos, mat);
            pos.SetValues(Position.Lines - 1, Position.Columns);
            TestMove(pos, mat);
            pos.SetValues(Position.Lines - 1, Position.Columns + 1);
            TestMove(pos, mat);

            pos.SetValues(Position.Lines, Position.Columns - 1);
            TestMove(pos, mat);
            pos.SetValues(Position.Lines, Position.Columns + 1);
            TestMove(pos, mat);

            pos.SetValues(Position.Lines + 1, Position.Columns + 1);
            TestMove(pos, mat);
            pos.SetValues(Position.Lines + 1, Position.Columns);
            TestMove(pos, mat);
            pos.SetValues(Position.Lines + 1, Position.Columns - 1);
            TestMove(pos, mat);
            return mat;
        }
    }
}
