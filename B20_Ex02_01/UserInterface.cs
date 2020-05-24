using System;

namespace B20_Ex02_01
{
    public class UserInterface
    {
        private GuessingGame m_Game;

        //----------------------------------------------------------------------//
        public UserInterface()
        {
            m_Game = new GuessingGame();
        }

        //----------------------------------------------------------------------//
        public void StartGame()
        {
            
            this.CreateObjectArray();
            this.CreatePlayers();
            this.CreateBoard();
            this.m_Game.CreateRandomBoard();
            this.PlayGame();

        }
        //----------------------------------------------------------------------//
        public void CreateObjectArray() //For this specific assignment we used chars so UI created the object array with chars
        {
            this.m_Game.ObjectArray = new object[18]
                { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R' };
        }
        //----------------------------------------------------------------------//
        public void CreatePlayers()
        {
            Console.Write("Hello player, please enter your name: ");
            string name = Console.ReadLine();
            this.m_Game.Players[0] = new Player(name, false);

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
                Console.Write("Please Enter the 2nd players name: ");
                name = Console.ReadLine();
                this.m_Game.Players[1] = new Player(name, false);
            }
            else
            {
                this.m_Game.Players[1] = new Player("PC", true);
            }
        }
        //----------------------------------------------------------------------//
        public void CreateBoard()
        {
            Console.WriteLine("Please enter the board size, range of the board is minimum 4x4 and maximum 6x6");
            Console.Write("Rows: ");
            string rowSize = Console.ReadLine();
            Console.Write("Columns: ");
            string colSize = Console.ReadLine();

            while (checkForValidInput(rowSize, colSize) == false)
            {
                Console.WriteLine("Invalid size, please enter the board size, range of the board is minimum 4x4 and maximum 6x6");
                Console.Write("Rows: ");
                rowSize = Console.ReadLine();
                Console.Write("Columns: ");
                colSize = Console.ReadLine();
            }

            this.m_Game.Board = new Board(int.Parse(rowSize), int.Parse(colSize));
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

        public void PlayGame()
        {
            Player currentPlayer = this.m_Game.Players[0];
            bool userQuit;
            Board.Tile firstFlipTile, secondFlipTile;
            showBoardForTwoSeconds();
            //PRINT BOARD AND SHOW FOW A MOMENT - function

            while (GameEnd() == false)
            {
                Console.Write("Please enter a first card to flip: ");
                this.makeAPlayerMove(out firstFlipTile, out userQuit);
                if (userQuit == true)
                {
                    break;
                }

                Console.Write("Please enter a second card to flip: ");
                this.makeAPlayerMove(out secondFlipTile, out userQuit);
                if (userQuit == true)
                {
                    break;
                }

                if (firstFlipTile.ContentOfTile != secondFlipTile.ContentOfTile)
                {
                    firstFlipTile.IsOpen = false;
                    secondFlipTile.IsOpen = false;
                    this.m_Game.ForwardPlayer(ref currentPlayer);
                }
                else
                {
                    this.m_Game.Board.NumberOfOpenTiles += 2;
                    ++currentPlayer.PointsForCorrectGuesses;
                }
            }
        }
        //----------------------------------------------------------------------//
        private void makeAPlayerMove(out Board.Tile io_Tile, out bool io_userQuit)
        {
            string userMoveInput;
            userMoveInput = Console.ReadLine();
            io_userQuit = (userMoveInput == "Q");
            while (this.checkForValidMove(userMoveInput, out io_Tile) == false && !io_userQuit)
            {
                Console.Write("No such choice, please re-enter a card to flip: ");
                userMoveInput = Console.ReadLine();
                io_userQuit = (userMoveInput == "Q");
            }

            if (io_userQuit == false)
            {
                io_Tile.IsOpen = true;
                this.PrintBoard();
            }
        }
        //----------------------------------------------------------------------//
        bool checkForValidMove(string i_UserMove, out Board.Tile io_TileToFlip) ///Incorrect atm
        {
            bool isValidMove = false, validRows, validColumns;
            int rowFromInput = 0;
            int colFromInput = 0;
            io_TileToFlip = null;

            if (i_UserMove.Length == 2 && char.IsUpper(i_UserMove[0]) && char.IsDigit(i_UserMove[1]))
            {
                
                rowFromInput = Convert.ToInt32(i_UserMove[0] - 'A');
                colFromInput = Convert.ToInt32(i_UserMove[1] - '0');
                validRows = (rowFromInput <= this.m_Game.Board.RowBorder && rowFromInput >= 0);
                validColumns = (colFromInput <= this.m_Game.Board.ColumnBorder && colFromInput >= 0);
                isValidMove = validRows && validColumns;
            }

            if (isValidMove)
            {
                io_TileToFlip = this.m_Game.Board[rowFromInput, colFromInput];

                if (io_TileToFlip.IsOpen == true)
                {
                    isValidMove = false;
                    io_TileToFlip = null;
                }
            }

            return isValidMove;
        }
        //----------------------------------------------------------------------//
        public bool GameEnd()
        {
            return this.m_Game.Board.NumberOfOpenTiles == (this.m_Game.Board.ColumnBorder * this.m_Game.Board.RowBorder);
        }
        //----------------------------------------------------------------------//
        public void showBoardForTwoSeconds()
        {
            this.PrintBoard();
            System.Threading.Thread.Sleep(2000);
            Ex02.ConsoleUtils.Screen.Clear();
            this.m_Game.ChangeAllTiles();
        }
        public void PrintBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            
            int rowNumber = 1;
            Console.Write("   ");

            char columnChar = 'A';

            for (int j = 0; j < this.m_Game.Board.ColumnBorder; ++j)
            {
                Console.Write(" {0}  ", columnChar);
                ++columnChar;
            }

            Console.WriteLine();
            this.printLine();

            for (int i = 0; i < this.m_Game.Board.RowBorder; ++i)
            {
                Console.Write("{0} |", rowNumber);
                ++rowNumber;
                for (int j = 0; j < this.m_Game.Board.RowBorder; ++j)
                {
                    if(this.m_Game.Board[i, j].ContentOfTile == -1)
                    {
                        Console.Write("   |");
                    }
                    else
                    {
                        Console.Write(" {0} |",this.m_Game.HashObject(this.m_Game.Board[i,j].ContentOfTile));
                    }
                }
                Console.WriteLine();
                this.printLine();
            }
        }

        private void printLine()
        {
            Console.Write("  ");

            for (int i = 0; i < this.m_Game.Board.RowBorder * this.m_Game.Board.RowBorder + 1; ++i)
            {
                Console.Write("=");
            }
            
            Console.WriteLine();
        }
    }
}
