using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.ChessBoard;
using ChessBoard;

namespace ChessRules
{
    internal class Bishop : Piece
    {
        public Bishop(Colors color, Board board) : base(color, board)
        {
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

        private void DiagonalMoves(Position pos, bool[,] mat, char op1, char op2)
        {
            while (true)
            {
                TestMove(pos, mat);
                if (Board.ThereIsAPiece(pos) && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }
                if (op1 == '+' && op2 == '+')
                    pos.SetValues(pos.Lines + 1, pos.Columns + 1);
                else if (op1 == '+' && op2 == '-')
                    pos.SetValues(pos.Lines + 1, pos.Columns - 1);
                else if (op1 == '-' && op2 == '+')
                    pos.SetValues(pos.Lines - 1, pos.Columns + 1);
                else
                    pos.SetValues(pos.Lines - 1, pos.Columns - 1);
            }
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);
            pos.SetValues(pos.Lines + 1, pos.Columns + 1);
            DiagonalMoves(pos, mat, '+', '+');
            pos.SetValues(pos.Lines - 1, pos.Columns - 1);
            DiagonalMoves(pos, mat, '-', '-');
            pos.SetValues(pos.Lines + 1, pos.Columns - 1);
            DiagonalMoves(pos, mat, '+', '-');
            pos.SetValues(pos.Lines - 1, pos.Columns + 1);
            DiagonalMoves(pos, mat, '-', '+');
            return mat;
        } 

        public bool IsLocked(bool[,] mat)
        {
            for(int i = 0; i < mat.GetLength(0); i++)
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
            return "B";
        }
    }
   
}
