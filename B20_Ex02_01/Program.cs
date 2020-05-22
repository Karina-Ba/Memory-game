using System;

namespace B20_Ex02_01
{
    public class Program
    {
        public static void Main()
        {
            InitProgram();
        }

        public static void InitProgram()
        {
            Board b = new Board(6,6);

            b.printBoard();
        }
    }
}
