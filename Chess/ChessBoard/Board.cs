using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.ChessBoard;

namespace ChessBoard
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] pieces;

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            pieces = new Piece[lines, columns];
        }

        public bool ValidPosition(Position position)
        {
            if (position.Lines < 0 || position.Lines >= Lines || position.Columns < 0 || position.Columns >= Columns)
            {
                return false;
            }
            return true;
        }

        public bool ThereIsAPiece(Position position)
        {
            ValidPosition(position);
            if (GetPiece(position.Lines, position.Columns) != null)
            {
                return true;
            }
            return false;
        }

        public bool ValidatePosition(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new BoardException("Invalid Position");
            }
            return true;
        }



        public void NewPiece(Piece piece, Position position)
        {
            ValidatePosition(position);
            pieces[position.Lines, position.Columns] = piece;
            piece.newPosition(position);
        }   



        public Piece GetPiece(int line, int column)
        {
            return pieces[line, column];
        }

        public Piece GetPiece(Position position)
        {
            return pieces[position.Lines, position.Columns];
        }

        public void RemovePiece(Position? position)
        {
            ValidatePosition(position);
            if (GetPiece(position) == null)
            {
                throw new BoardException("There is no piece in this position");
            }
            pieces[position.Lines, position.Columns] = null;
            position = null;
        }

        public void MovePiece(Position origin, Position destiny)
        {
            
            if (!ThereIsAPiece(origin))
            {
                throw new BoardException("There is no piece in this position");
            }
            else 
            {
                if(ThereIsAPiece(destiny))
                {
                    if(GetPiece(origin).Color == GetPiece(destiny).Color)
                    {
                        throw new BoardException("You cannot capture your own piece");
                    }
                    else
                    {
                        RemovePiece(destiny);
                        Piece p = GetPiece(origin);
                        p.increaseMoveCount();  
                        RemovePiece(origin);
                        NewPiece(p, destiny);
                    }
                }
                else
                {
                    Piece p = GetPiece(origin);
                    p.increaseMoveCount();
                    RemovePiece(origin);
                    NewPiece(p, destiny);
                }
            }
            

        }   











    }
}
