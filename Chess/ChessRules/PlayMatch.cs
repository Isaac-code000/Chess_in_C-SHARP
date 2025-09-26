using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.ChessBoard;
using ChessBoard;



namespace ChessRules
{
    internal class PlayMatch
    {
        private Board Board = new Board(8, 8);
        public Colors CurrentPlayer { get; private set; }
        public int Turn { get; set; }
        public bool Check { get; private set; }
        public bool End { get; set; }

        public HashSet<Piece> CapturedPieces;
        public HashSet<Piece> PiecesInGame;

        public PlayMatch()
        {
            Turn = 1;
            CurrentPlayer = Colors.white;
            PiecesInGame = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            Check = false;
            SetLayout(Board);
        }

        public Board GetBoard() => Board;

        public void SwitchPlayer() => CurrentPlayer = Opponent(CurrentPlayer);

        public Colors Opponent(Colors color) => (color == Colors.white) ? Colors.black : Colors.white;

        public Piece Player(Colors color)
        {
            foreach (Piece p in PiecesInGame)
                if (p is King && p.Color == color)
                    return p;

            throw new BoardException("King not exists");
        }

        public bool InCheck(Colors color)
        {
            Piece king = Player(color);
            foreach (Piece p in PiecesInGameByColor(Opponent(color)))
            {
                bool[,] moves = p.PossibleMoves();
                if (moves[king.Position.Lines, king.Position.Columns])
                    return true;
            }
            return false;
        }

        public bool InCheckMate(Colors color)
        {
            if (!InCheck(color))
                return false;

            foreach (Piece p in PiecesInGameByColor(color))
            {
                bool[,] mat = p.PossibleMoves();

                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = p.Position;
                            Position destiny = new Position(i, j);

                          
                            Piece captured = Board.GetPiece(destiny);
                            Board.PutPiece(p, destiny);
                            Board.RemovePiece(origin);

                            bool isCheck = InCheck(color);

                            
                            Board.PutPiece(p, origin);
                            Board.RemovePiece(destiny);
                            if (captured != null)
                                Board.PutPiece(captured, destiny);

                            if (!isCheck)
                                return false;
                        }
                    }
                }
            }

            return true;
        }

        public void PerformMovement(Position origin, Position destiny)
        {
            Piece captured = MovePiece(origin, destiny);

            if (InCheck(CurrentPlayer))
            {
                CancelMovement(origin, destiny, captured);
                throw new BoardException("You can't put your king in check");
            }

            Check = InCheck(Opponent(CurrentPlayer));
            End = InCheckMate(Opponent(CurrentPlayer));

            SwitchPlayer();
            Turn++;
        }

        public Piece MovePiece(Position origin, Position destiny)
        {
            

            Piece movingPiece = Board.GetPiece(origin);
            if (movingPiece == null)
                throw new BoardException("There is no piece in this position");

            Piece capturedPiece = Board.GetPiece(destiny);

            if (capturedPiece != null)
            {
                if (capturedPiece.Color == movingPiece.Color)
                    throw new BoardException("You cannot capture your own piece");

                CapturedPieces.Add(capturedPiece);
                PiecesInGame.Remove(capturedPiece);
                Board.RemovePiece(destiny);
            }

            Board.RemovePiece(origin);
            Board.PutPiece(movingPiece, destiny);
            movingPiece.IncreaseMoveCount();

            if (movingPiece is Pawn && (destiny.Lines == 0 || destiny.Lines == 7))
                ((Pawn)movingPiece).ExecutePromotion();

            if (movingPiece is King )
            {
                if (movingPiece.Color == Colors.black)
                {
                    King king = (King)movingPiece;
                    king.SmallCastling(Colors.black);
                }
                else
                {
                    King king = (King)movingPiece;
                    king.SmallCastling(Colors.white);
                }
            }

            return capturedPiece;
        }

        public void CancelMovement(Position origin, Position destiny, Piece captured)
        {
            Piece movedPiece = Board.GetPiece(destiny);
            if (movedPiece != null)
            {
                Board.RemovePiece(destiny);
                Board.PutPiece(movedPiece, origin);
                movedPiece.DecressMoveCount();
            }

            if (captured != null)
            {
                Board.PutPiece(captured, destiny);
                PiecesInGame.Add(captured);
                CapturedPieces.Remove(captured);
            }
        }

        public void MoveIsPossible(Position origin, Position destiny)
        {
            if (!Board.GetPiece(origin).PossibleMoves()[destiny.Lines, destiny.Columns])
                throw new BoardException("Invalid move for this piece");
        }

        public void PutNewPiece(char column, int line, Piece piece)
        {
            Board.PutPiece(piece, new Position(line - 1, column - 'a'));
            PiecesInGame.Add(piece);
        }

        public HashSet<Piece> CapturedPiecesByColor(Colors color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in CapturedPieces)
                if (piece.Color == color)
                    aux.Add(piece);
            return aux;
        }

        public HashSet<Piece> PiecesInGameByColor(Colors color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in PiecesInGame)
                if (piece.Color == color)
                    aux.Add(piece);

            aux.ExceptWith(CapturedPiecesByColor(color));
            return aux;
        }

        public void SetLayout(Board board)
        {
            // Exemplo completo padrão de xadrez
            PutNewPiece('a', 1, new Tower(Colors.black, board));
            PutNewPiece('b', 1, new Horse(Colors.black, board));
            PutNewPiece('c', 1, new Bishop(Colors.black, board));
            PutNewPiece('d', 1, new Queen(Colors.black, board));
            PutNewPiece('e', 1, new King(Colors.black, board));
            
            PutNewPiece('h', 1, new Tower(Colors.black, board));

            for (int i = 0; i < 8; i++)
                PutNewPiece((char)('a' + i), 2, new Pawn(Colors.black, board));

            PutNewPiece('a', 8, new Tower(Colors.white, board));
            PutNewPiece('b', 8, new Horse(Colors.white, board));
            PutNewPiece('c', 8, new Bishop(Colors.white, board));
            PutNewPiece('d', 8, new Queen(Colors.white, board));
            PutNewPiece('e', 8, new King(Colors.white, board));
            
            PutNewPiece('h', 8, new Tower(Colors.white, board));

            for (int i = 0; i < 8; i++)
                PutNewPiece((char)('a' + i), 7, new Pawn(Colors.white, board));
        }
    }



}
