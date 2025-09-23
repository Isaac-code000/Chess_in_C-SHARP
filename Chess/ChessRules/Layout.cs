using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessBoard;

namespace ChessRules
{
    static class Layout
    {
        public static void SetLayout(Board board)
        {
            board.NewPiece(new Tower(Colors.black, board), new Position(0, 0));
            board.NewPiece(new Horse(Colors.black, board), new Position(0, 1));
            board.NewPiece(new Bishop(Colors.black, board), new Position(0, 2));
            board.NewPiece(new Queen(Colors.black, board), new Position(0, 3));
            board.NewPiece(new King(Colors.black, board), new Position(0, 4));
            board.NewPiece(new Bishop(Colors.black, board), new Position(0, 5));
            board.NewPiece(new Horse(Colors.black, board), new Position(0, 6));
            board.NewPiece(new Tower(Colors.black, board), new Position(0, 7));

            for (int i = 0; i < 8; i++) {
                board.NewPiece(new Tower(Colors.black, board), new Position(1, i));
                board.NewPiece(new Queen(Colors.white, board), new Position(6, i));
            }
            // Setting up white pieces
            board.NewPiece(new Tower(Colors.white, board), new Position(7, 0));
            board.NewPiece(new Horse(Colors.white, board), new Position(7, 1));
            board.NewPiece(new Bishop(Colors.white, board), new Position(7, 2));
            board.NewPiece(new Queen(Colors.white, board), new Position(7, 3));
            board.NewPiece(new King(Colors.white, board), new Position(7, 4));
            board.NewPiece(new Bishop(Colors.white, board), new Position(7, 5));
            board.NewPiece(new Horse(Colors.white, board), new Position(7, 6));
            board.NewPiece(new Tower(Colors.white, board), new Position(7, 7));

           
        }   
    }
}
