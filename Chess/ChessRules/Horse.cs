using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessBoard;

namespace ChessRules
{
    internal class Horse : Piece
    {
        public Horse(Colors color, Board board) : base(color, board)
        {
        }
        private bool canMove(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece == null || piece.Color != Color;
        }

        private void TestPosition(Position position, bool[,] mat)
        {
            if (Board.ValidPosition(position) && canMove(position))
            {
                mat[position.Lines, position.Columns] = true;
            }
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position position = new Position(0, 0);

            position.SetValues(Position.Lines - 2, Position.Columns + 1);
            TestPosition(position, mat);
            position.SetValues(Position.Lines - 2, Position.Columns - 1);
            TestPosition(position, mat);

            position.SetValues(Position.Lines - 1, Position.Columns + 2);
            TestPosition(position, mat);
            position.SetValues(Position.Lines - 1, Position.Columns - 2);
            TestPosition(position, mat);

            position.SetValues(Position.Lines + 1, Position.Columns + 2);
            TestPosition(position, mat);
            position.SetValues(Position.Lines + 1, Position.Columns - 2);
            TestPosition(position, mat);

            position.SetValues(Position.Lines + 2, Position.Columns + 1);
            TestPosition(position, mat);
            position.SetValues(Position.Lines + 2, Position.Columns - 1);
            TestPosition(position, mat);
            return mat;
        }

        public bool IsLocked(bool[,] mat)
        {
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    if (mat[i, j])
                    {
                        return false;
                    }
                }
            }
            Console.WriteLine("This piece no have possible moves, try again");
            return true;
        }
        public override string ToString()
        {
            return "H";
        }
    }

}
