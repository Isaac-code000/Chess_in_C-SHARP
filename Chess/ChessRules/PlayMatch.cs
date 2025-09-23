using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.ChessBoard;
using ChessBoard;



namespace ChessRules
{
    internal class PlayMatch
    {
        private Board Board = new Board(8,8);
        public Colors CurrentPlayer { get; private set; }
        public int Turn { get; private set; }

        public HashSet<Piece> CapturedPieces;
        public HashSet<Piece> PiecesInGame;

        public PlayMatch()
        {
            Turn = 1;
            CurrentPlayer = Colors.white;
            PiecesInGame = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
        }

        public void NewTurn()
        {
            Turn++;
            ChangePlayer(CurrentPlayer);
        }

        public Colors ChangePlayer(Colors player)
        {
            if (CurrentPlayer == Colors.white)
            {
                return CurrentPlayer = Colors.black;
            }
            else
            {
                return CurrentPlayer = Colors.white;
            }
        }

        public Board GetBoard()
        {
            return Board;
        }



        

        public void MoveIsPossible(Position origin, Position destiny)
        {
            if(!Board.GetPiece(origin).PossibleMoves()[destiny.Lines, destiny.Columns])
            {
                throw new BoardException("Invalid move for this piece");
            }
        }

        public void MovePiece(Position origin, Position destiny)
        {
            
            if (!Board.ThereIsAPiece(origin))
            {
                throw new BoardException("There is no piece in this position");
            }
            else
            {
                if (Board.ThereIsAPiece(destiny))
                {
                    if (Board.GetPiece(origin).Color == Board.GetPiece(destiny).Color)
                    {
                        throw new BoardException("You cannot capture your own piece");
                    }
                    else
                    {
                        
                        Board.RemovePiece(destiny);
                        Board.PutPiece(Board.GetPiece(origin), destiny);
                        CapturedPieces.Add(Board.GetPiece(destiny));    
                        Board.RemovePiece(origin);
                        if (Board.GetPiece(origin).Name == 'P' && destiny.Lines == 0 || destiny.Lines == 7)
                        {
                            Pawn aux = (Pawn)Board.GetPiece(destiny);
                            aux.ExecutePromotion();
                        }

                        Board.GetPiece(destiny).IncreaseMoveCount();
                    }
                }
                else
                {
                    Board.PutPiece(Board.GetPiece(origin), destiny);
                    Board.RemovePiece(origin);
                    Board.GetPiece(destiny).IncreaseMoveCount();
                }
            }
        }

        public void PutNewPiece(char column, int line, Piece piece)
        {
            Board.PutPiece(piece, new Position(line - 1, column - 'a'));
            PiecesInGame.Add(piece);
        }

        public void SetLayout(Board board)
        {

            PutNewPiece('a', 1, new Tower(Colors.black, board));
            PutNewPiece('b', 1, new Horse(Colors.black, board));
            PutNewPiece('c', 1, new Bishop(Colors.black, board));
            PutNewPiece('d', 1, new Queen(Colors.black, board));
            PutNewPiece('e', 1, new King(Colors.black, board));
            PutNewPiece('f', 1, new Bishop(Colors.black, board));
            PutNewPiece('g', 1, new Horse(Colors.black, board));
            PutNewPiece('h', 1, new Tower(Colors.black, board));

            for (int i = 0; i < 7; i++)
            {
            PutNewPiece((char)('a' + i), 2, new Pawn(Colors.black, board));
            PutNewPiece((char)('e' + i), 7, new Pawn(Colors.white, board));
            }
            // Setting up white pieces
            PutNewPiece('a', 8, new Tower(Colors.white, board));
            PutNewPiece('b', 8, new Horse(Colors.white, board));
            PutNewPiece('c', 8, new Bishop(Colors.white, board));
            PutNewPiece('d', 8, new Queen(Colors.white, board));
            PutNewPiece('e', 8, new King(Colors.white, board));
            PutNewPiece('f', 8, new Bishop(Colors.white, board));
            PutNewPiece('g', 8, new Horse(Colors.white, board));
            PutNewPiece('h', 8, new Tower(Colors.white, board));
        }

        public HashSet<Piece> CapturedPiecesByColor(Colors color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in CapturedPieces)
            {
                if (piece.Color == color)
                {
                    aux.Add(piece);
                }
            }
            return aux;
        }

        public HashSet<Piece> PiecesInGameByColor(Colors color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in PiecesInGame)
            {
                if (piece.Color == color)
                {
                    aux.Add(piece);
                }
            }
            aux.ExceptWith(CapturedPiecesByColor(color));
            return aux;
        }   

        public bool CheckMate()
        {
            return true;
        }   
    }
}
