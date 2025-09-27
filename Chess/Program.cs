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
           
                PlayMatch play = new PlayMatch();
                while (!play.End)
                {
                try
                {
                    PrintMatch(play);
                }
                catch (BoardException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Tente novamente");
                }
                }
            }
           
        }
    }

