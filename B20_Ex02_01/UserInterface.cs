using System;

namespace B20_Ex02_01
{
    public class UserInterface
    {
        private GuessingGame game;

        public void InitGame()
        {
            
            this.CreatePlayers();
            this.CreateBoard();
        }

        public void CreatePlayers()
        {
            Console.Write("Hello player, please enter your name: ");
            string name = Console.ReadLine();
            game.CreatePlayer(name, false);

            Console.WriteLine(@"
{0}, would you like to play against another player or against the PC?
1. Another player
2. Against the PC", name);
            int choiceNum;
            string choice = Console.ReadLine();
            while (!int.TryParse(choice, out choiceNum))
            {
                Console.WriteLine("Invalid input, please try again: ");
                choice = Console.ReadLine();
            }

            if (choiceNum == 1)
            {
                Console.WriteLine("Please Enter the 2nd players name: ");
                name = Console.ReadLine();
                game.CreatePlayer(name, false);
            }
            else
            {
                game.CreatePlayer("PC", true);
            }
        }

        public void CreateBoard()
        {
            Console.WriteLine("Please enter the board size, range of the board is minimum 4x4 and maximum 6x6");
            Console.Write("Rows: ");
            string rowSize = Console.ReadLine();
            Console.Write("Columns: ");
            string colSize = Console.ReadLine();

            while (checkForValidInput(rowSize, colSize) == false)
            {
                Console.WriteLine("Ïnvalid size, please enter the board size, range of the board is minimum 4x4 and maximum 6x6");
                Console.Write("Rows: ");
                rowSize = Console.ReadLine();
                Console.Write("Columns: ");
                colSize = Console.ReadLine();
            }

            game.Board = new Board(int.Parse(rowSize), int.Parse(colSize));
        }

        private bool checkForValidInput(string i_RowSize, string i_ColSize)
        {
            bool boolToReturn = true;
            int numOfRows, numOfCols;
            if (!(int.TryParse(i_RowSize, out numOfRows)))
            {
                boolToReturn = false;
            }

            if(!(int.TryParse(i_ColSize, out numOfCols)))
            {
                boolToReturn = false;
            }

            if (boolToReturn == true)
            {
                if ((numOfRows < 4 || numOfRows > 6) || (numOfCols < 4 || numOfCols > 6) || ((numOfCols * numOfRows) % 2 == 1))
                {
                    boolToReturn = false;
                }

            }

            return boolToReturn;
        }
    }
}
