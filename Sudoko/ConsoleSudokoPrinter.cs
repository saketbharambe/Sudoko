using System;

namespace Sudoko
{
    internal class ConsoleSudokoPrinter : ISudokoPrinter
    {
        public void PrintSudoko(int[][] data)
        {
            /* 
             *    ___ ___ ___ __ __
             *   | 0 | 1 | 2 | 
             * 
             */
            Console.Out.WriteLine("-------------------------------------");
            for (int row = 0; row < 9; row++)
            {
                Console.Out.Write("|");
                for (int col = 0; col < 9; col ++)
                {
                    Console.Out.Write($" {data[row][col]} |");
                }
                Console.Out.WriteLine();
                Console.Out.WriteLine("-------------------------------------");
            }
        }
    }
}