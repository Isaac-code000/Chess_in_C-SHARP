using System;
using System.Collections.Generic;
using System.Drawing;
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
            if (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Lines, position.Columns] = true;
            }
        }

        public void SmallCastling(Colors color)
        {
            if (!ValidSmallCastling(Color, PossibleMoves()))
            {
                return;
            }
            if (color == Colors.black)
            {
                Piece king = Board.GetPiece(Position);
                Piece tower = Board.GetPiece(new Position(0, 7));
                Board.RemovePiece(Position);
                Board.RemovePiece(new Position(0, 7));
                Board.PutPiece(king, new Position(0, 6));
                Board.PutPiece(tower, new Position(0, 5));
            }
            else
            {
                Piece king = Board.GetPiece(Position);
                Piece tower = Board.GetPiece(new Position(7, 7));
                Board.RemovePiece(Position);
                Board.RemovePiece(new Position(7, 7));
                Board.PutPiece(king, new Position(7, 6));
                Board.PutPiece(tower, new Position(7, 5));
            }
        }

        public void BigCastling(Colors color)
        {
            if (!ValidBigCastling(Color, PossibleMoves()))
            {
                return;
            }
            if (color == Colors.black)
            {
                Piece king = Board.GetPiece(Position);
                Piece tower = Board.GetPiece(new Position(0, 0));
                Board.RemovePiece(Position);
                Board.RemovePiece(new Position(0, 0));
                Board.PutPiece(king, new Position(0, 2));
                Board.PutPiece(tower, new Position(0, 3));
            }
            else
            {
                Piece king = Board.GetPiece(Position);
                Piece tower = Board.GetPiece(new Position(7, 0));
                Board.RemovePiece(Position);
                Board.RemovePiece(new Position(7, 0));
                Board.PutPiece(king, new Position(7, 2));
                Board.PutPiece(tower, new Position(7, 3));
            }
        }

        

        private bool ValidSmallCastling(Colors color, bool[,] mat)
        {
            if (color == Colors.black)
            {
                Piece blackT = Board.GetPiece(new Position(0, 7));
                Piece blackK = Board.GetPiece(new Position(0, 4));
                if (blackK is King  && blackT is Tower)
                {
                    if (blackK.MoveCount == 0 && blackT.MoveCount == 0)
                    {
                        if (CanMove(new Position(0, 5)) && CanMove(new Position(0, 6)))
                        {
                            mat[0, 6] = true;
                            return true;
                        }
                        
                    }
                }
            }
            else
            {
                Piece whiteT = Board.GetPiece(new Position(7, 7));
                Piece whiteK = Board.GetPiece(new Position(7, 4));
                if (whiteK is King && whiteT is Tower)
                {
                    if (whiteK.MoveCount == 0 && whiteT.MoveCount == 0)
                    {
                        if (CanMove(new Position(7, 5)) && CanMove(new Position(7, 6)))
                        {
                            mat[7, 6] = true;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool ValidBigCastling(Colors color, bool[,] mat)
        {
            if (color == Colors.black)
            {
                Piece blackT = Board.GetPiece(new Position(0, 0));
                Piece blackK = Board.GetPiece(new Position(0, 4));
                if (blackK is King  && blackT is Tower)
                {
                    if (blackK.MoveCount == 0 && blackT.MoveCount == 0)
                    {
                        if (CanMove(new Position(0, 3)) && CanMove(new Position(0, 2)))
                        {
                            mat[0, 2] = true;
                            return true;
                        }

                    }
                }
            }
            else
            {
                Piece whiteT = Board.GetPiece(new Position(7, 0));
                Piece whiteK = Board.GetPiece(new Position(7, 4));
                if (whiteK is King && whiteT is Tower)
                {
                    if (whiteK.MoveCount == 0 && whiteT.MoveCount == 0)
                    {
                        if (CanMove(new Position(7, 3)) && CanMove(new Position(7, 2)))
                        {
                            mat[7, 2] = true;
                            return true;
                        }
                    }
                }
            }
            return false;
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
            ValidSmallCastling(Color, mat);
            ValidBigCastling(Color, mat);
            return mat;
        }
    }
}
