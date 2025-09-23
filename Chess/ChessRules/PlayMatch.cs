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

        public PlayMatch()
        {
            Turn = 1;
            CurrentPlayer = Colors.white;
            Layout.SetLayout(Board);
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
                        Board.RemovePiece(origin);
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

        public bool XequeMate()
        {
            return true;
        }

    }
}
