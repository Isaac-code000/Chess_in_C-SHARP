using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessBoard;

namespace ChessRules
{
    internal class Pawn : Piece
    {
        public Pawn(Colors color, Board board) : base(color, board)
        {
        }

        private bool canMove(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece == null || piece.Color != Color;
        }

        private bool TestPosition(Position position, bool[,] mat)
        {
            if(Board.ValidPosition(position) && canMove(position)){
                mat[position.Lines, position.Columns] = true;
            }
            return true;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            if (Board.GetPiece(GetPosition()).Color == Colors.white)
            {
                pos.SetValues(Position.Lines - 1, Position.Columns);
                if(Board.GetPiece(pos) == null)
                {
                    mat[pos.Lines, pos.Columns] = true;
                }
               
                pos.SetValues(Position.Lines - 2, Position.Columns);
                if (MoveCount == 0 && Board.GetPiece(pos) == null)
                {
                    TestPosition(pos, mat);
                }

                pos.SetValues(Position.Lines - 1, Position.Columns - 1);
                if (Board.GetPiece(pos) != null)
                {
                    TestPosition(pos, mat);
                }
                pos.SetValues(Position.Lines - 1, Position.Columns + 1);
                if (Board.GetPiece(pos) != null)
                {
                    TestPosition(pos, mat);
                }
            }
            else
            {
                pos.SetValues(Position.Lines + 1, Position.Columns);
                TestPosition(pos, mat);
                if (MoveCount == 0)
                {
                    pos.SetValues(Position.Lines + 2, Position.Columns);
                    TestPosition(pos, mat);
                }
                pos.SetValues(Position.Lines + 1, Position.Columns - 1);
                if (Board.GetPiece(pos) != null)
                {
                    TestPosition(pos, mat);
                }
                pos.SetValues(Position.Lines + 1, Position.Columns + 1);
                if (Board.GetPiece(pos) != null)
                {
                    TestPosition(pos, mat);
                }
            }
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
            return "P";
        }
    }

}

