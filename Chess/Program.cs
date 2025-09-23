using System;
using Chess.ChessBoard;

using ChessBoard;
using ChessRules;
using static Chess.Screen;




namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PlayMatch play = new PlayMatch();
                while (play.XequeMate())
                {
                    
                    PrintBoard(play.GetBoard());
                    Console.WriteLine();
                    Console.WriteLine(" Turn: " + play.Turn + "\n Play " + play.CurrentPlayer );
                    play.NewTurn();
                    Console.WriteLine();
                    Console.WriteLine("Informe a origem");
                    Position origin = ReadChessPosition();
                    play.GetBoard().ValidatePosition(origin);
                    PrintBoard(play.GetBoard(), play.GetBoard().GetPiece(origin));

                    Console.WriteLine("Informe o destino");
                    Position destiny = ReadChessPosition();
                    play.GetBoard().ValidatePosition(destiny);
                    play.MoveIsPossible(origin, destiny);

                    play.MovePiece(origin, destiny);
                    Console.Clear();
                }

            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
