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

        public void PutPiece(Piece piece, Position position)
        {
            NewPiece(piece, position);
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
    }
}
