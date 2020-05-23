using System;

namespace B20_Ex02_01
{
    public class UserInterface
    {
        //----------------------------------------------------------------------//
        public void InitProgram()
        {
            GuessingGame game = new GuessingGame();
            this.CreateObjectArray(game);
            this.CreatePlayers(game);
            this.CreateBoard(game);
            game.createRandomBoard();

            

            game.Board.printBoard();

        }
        //----------------------------------------------------------------------//
        public void CreateObjectArray(GuessingGame i_Game)
        {
            i_Game.ObjectArray = new object[18]
                { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R' };
        }
        //----------------------------------------------------------------------//
        public void CreatePlayers(GuessingGame i_Game)
        {
            Console.Write("Hello player, please enter your name: ");
            string name = Console.ReadLine();
            i_Game.Players[0] = new Player(name, false);

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
                i_Game.Players[1] = new Player(name, false);
            }
            else
            {
                i_Game.Players[1] = new Player("PC", true);
            }
        }
        //----------------------------------------------------------------------//
        public void CreateBoard(GuessingGame i_Game)
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

            i_Game.Board = new Board(int.Parse(rowSize), int.Parse(colSize));
        }
        //----------------------------------------------------------------------//
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
