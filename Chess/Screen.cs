using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using ChessBoard;

namespace Chess
{
    internal class Screen
    {
        public static void PrintBoard(Board board,Piece origin = null)
        {
            int linecount = 9;
           

            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("   --------------------------");
            Console.ForegroundColor = aux;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.BackgroundColor = ConsoleColor.Black;

                aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" " + --linecount + " |");
                Console.ForegroundColor = aux;

                for (int j = 0; j < board.Columns; j++)
                {
                    Console.Write("");
                    if ((i + j) % 2 == 0)
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    else 
                        Console.BackgroundColor = ConsoleColor.Black;
                    if(origin != null && origin.PossibleMoves()[i, j])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }
                    PrintPiece(board.GetPiece(i, j));  
                }

                Console.BackgroundColor = ConsoleColor.Black;
                aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("|");
                Console.ForegroundColor = aux;

            }
            Console.BackgroundColor = ConsoleColor.Black;
            aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("   --------------------------");
            Console.WriteLine("     A " + " B " + " C " + " D " + " E " + " F " + " G " + " H ");
            Console.ForegroundColor = aux;

        }
        public void ShowPossibleMoves(Piece piece)
        {
            bool[,] mat = piece.PossibleMoves();
        }

        public static Position ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new Position(8 - line, column - 'A');
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write(" - ");

            }
            else
            {
                if (piece.Color == Colors.black)
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(" " + piece + " ");
                    Console.ForegroundColor = aux;
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" " + piece + " ");
                    Console.ForegroundColor = aux;
                }

            }
        }
    }

}


