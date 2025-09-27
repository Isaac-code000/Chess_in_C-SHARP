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
        private PlayMatch play;
        public Pawn(Colors color, Board board,PlayMatch play) : base(color, board)
        {
            Name = 'P';
            this.play = play;
        }

        private bool canMove(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece == null || piece.Color != Color;
        }

        private bool IsEnemy(Position p)
        {
            if(p == null) return false;
            if(Board.GetPiece(p).Color == play.Opponent(Board.GetPiece(p).Color)) return true;
            return false;
        }

        private bool TestPosition(Position position, bool[,] mat)
        {
            if (Board.ValidPosition(position) && canMove(position))
            {
                mat[position.Lines, position.Columns] = true;
            }
            return true;
        }

        private void Promotion(Piece p)
        {
            Position aux = Position;
            p.MoveCount = MoveCount;
            Board.RemovePiece(Position);
            Board.PutPiece(p, aux);
        }

        private void ChosenPromotion()
        {
            Console.WriteLine("Chosen the promotion of pawn");
            Console.WriteLine("{ Q } { T } { B } { H }");
            char op = char.Parse(Console.ReadLine().ToUpper());

            switch (op)
            {
                case 'Q':Promotion(new Queen(Color, Board)); break;
                case 'T':Promotion(new Tower(Color, Board)); break;
                case 'H':Promotion(new Horse(Color, Board)); break;
                case 'B':Promotion(new Bishop(Color, Board)); break;
            }
        }

        public void ExecutePromotion()
        {
            ChosenPromotion();
        }


       
        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            if (Board.GetPiece(GetPosition()).Color == Colors.white)
            {
                pos.SetValues(Position.Lines - 1, Position.Columns);
                if (Board.ValidPosition(pos) && Board.GetPiece(pos) == null)
                {
                    mat[pos.Lines, pos.Columns] = true;
                }

                pos.SetValues(Position.Lines - 2, Position.Columns);
                if (Board.ValidPosition(pos) && MoveCount == 0 && Board.GetPiece(pos) == null)
                {
                    TestPosition(pos, mat);
                }

                pos.SetValues(Position.Lines - 1, Position.Columns - 1);
                if (Board.ValidPosition(pos) && Board.GetPiece(pos) != null)
                {
                    TestPosition(pos, mat);
                }
                pos.SetValues(Position.Lines - 1, Position.Columns + 1);
                if (Board.ValidPosition(pos) && Board.GetPiece(pos) != null)
                {
                    TestPosition(pos, mat);
                }
                if (Position.Lines == 3)
                {
                    Position left = new Position(Position.Lines, Position.Columns - 1);
                    Position right = new Position(Position.Lines, Position.Columns + 1);
                    if (Board.ValidPosition(left) && IsEnemy(left) && Board.GetPiece(left) == play.EnpassantPossible)
                    {
                        TestPosition(left, mat);
                    }
                    if (Board.ValidPosition(right) && IsEnemy(right) && Board.GetPiece(right) == play.EnpassantPossible)
                    {
                        TestPosition(right, mat);
                    }
                }
        }
            else
            {
                pos.SetValues(Position.Lines + 1, Position.Columns);
                if (Board.ValidPosition(pos) && Board.GetPiece(pos) == null)
                {
                    mat[pos.Lines, pos.Columns] = true;
                }

                pos.SetValues(Position.Lines + 2, Position.Columns);
                if (Board.ValidPosition(pos) && MoveCount == 0 && Board.GetPiece(pos) == null)
                {
                    TestPosition(pos, mat);
                }

                pos.SetValues(Position.Lines + 1, Position.Columns + 1);
                if (Board.ValidPosition(pos) && Board.GetPiece(pos) != null)
                {
                    TestPosition(pos, mat);
                }
                pos.SetValues(Position.Lines + 1, Position.Columns - 1);
                if (Board.ValidPosition(pos) && Board.GetPiece(pos) != null)
                {
                    TestPosition(pos, mat);
                }

                if (Position.Lines == 4)
                {
                    Position left = new Position(Position.Lines, Position.Columns - 1);
                    Position right = new Position(Position.Lines, Position.Columns + 1);
                    if (Board.ValidPosition(left) && IsEnemy(left) && Board.GetPiece(left) == play.EnpassantPossible)
                    {
                        TestPosition(left, mat);
                    }
                    if (Board.ValidPosition(right) && IsEnemy(right) && Board.GetPiece(right) == play.EnpassantPossible)
                    {
                        TestPosition(right, mat);
                    }
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

