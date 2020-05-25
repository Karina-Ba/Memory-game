using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace B20_Ex02_01
{
    public class UserInterface
    {
        private GuessingGame m_Game;
        private AI m_AIPlayer;

        //----------------------------------------------------------------------//
        public UserInterface()
        {
            this.m_Game = new GuessingGame();
            this.m_AIPlayer = null;
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
            this.m_Game.Players.Add(new Player(name));

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
                this.m_Game.Players.Add(new Player(name));
            }
            else
            {
                this.m_AIPlayer = new AI();
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
        //----------------------------------------------------------------------//
        public void PlayGame()
        {
            Board.Tile firstFlipTile, secondFlipTile;
            PrintBoard();

            while (GameEnd() == false)
            {
                foreach (Player currentPlayer in this.m_Game.Players)
                {
                    Console.Write("Please enter a first card to flip: ");
                    this.makeAPlayerMove(out firstFlipTile);

                    Console.Write("Please enter a second card to flip: ");
                    this.makeAPlayerMove(out secondFlipTile);

                    if (firstFlipTile.ContentOfTile != secondFlipTile.ContentOfTile)
                    {
                        Console.WriteLine("Oops... not a match!");
                        this.showBoardForTwoSeconds();
                        firstFlipTile.IsOpen = secondFlipTile.IsOpen = false;
                    }
                    else
                    {
                        this.m_Game.Board.NumberOfOpenTiles += 2;
                        ++currentPlayer.PointsForCorrectGuesses;
                    }

                }

                if (this.m_AIPlayer != null)
                {
                    this.AITurn();
                }
            }
        }
        //----------------------------------------------------------------------//
        private void makeAPlayerMove(out Board.Tile io_Tile)
        {
            string userMoveInput;
            userMoveInput = Console.ReadLine();
            bool userQuit = (userMoveInput == "Q");
            while (this.checkForValidMove(userMoveInput, out io_Tile) == false && !userQuit)
            {
                Console.Write("No such choice, please re-enter a card to flip: ");
                userMoveInput = Console.ReadLine();
                userQuit = (userMoveInput == "Q");
            }

            if (userQuit == false)
            {
                io_Tile.IsOpen = true;
                this.PrintBoard();
            }
            else
            {
                Console.WriteLine("Thank you for playing!");
                Environment.Exit(0);
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

                colFromInput = Convert.ToInt32(i_UserMove[0] - 'A');
                rowFromInput= Convert.ToInt32(i_UserMove[1] - '0');
                validRows = (rowFromInput <= this.m_Game.Board.RowBorder && rowFromInput >= 0);
                validColumns = (colFromInput <= this.m_Game.Board.ColumnBorder && colFromInput >= 0);
                isValidMove = validRows && validColumns;
            }

            if (isValidMove)
            {
                io_TileToFlip = this.m_Game.Board[rowFromInput - 1, colFromInput];

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
        }
        //----------------------------------------------------------------------//
        public void PrintBoard()
        {
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
        //----------------------------------------------------------------------//
        private void printLine()
        {
            Console.Write("  ");

            for (int i = 0; i < this.m_Game.Board.RowBorder * this.m_Game.Board.RowBorder + 1; ++i)
            {
                Console.Write("=");
            }
            
            Console.WriteLine();
        }
        //----------------------------------------------------------------------//
        private void AITurn()
        {
            bool foundAMatch = this.m_AIPlayer.findAMatchingSet();
            Board.Tile firstFlip, secondFlip;

            if (!foundAMatch)
            {
                firstFlip = this.m_AIPlayer.ReturnARandomTile(this.m_Game.Board);
                firstFlip.IsOpen = true;
                this.PrintBoard();
                secondFlip = this.m_AIPlayer.ReturnARandomTile(this.m_Game.Board);
                secondFlip.IsOpen = true;
                Ex02.ConsoleUtils.Screen.Clear();
                this.PrintBoard();

                if (firstFlip.ContentOfTile == secondFlip.ContentOfTile)
                {
                    ++this.AIPlayer.PointsForCorrectGuesses;
                }
                else
                {
                    firstFlip.IsOpen = secondFlip.IsOpen = false;
                    this.AIPlayer.RememberFlip.Add(firstFlip);
                    this.AIPlayer.RememberFlip.Add(secondFlip);
                }
            }
        }

        public AI AIPlayer
        {
            get
            {
                return this.m_AIPlayer;
            }
        }

        public class AI
        {
            private List<Board.Tile> m_RememberFlips;
            private int m_PointsForCorrectGuesses;

            public AI()
            {
                this.m_RememberFlips = new List<Board.Tile>(36);
            }

            internal List<Board.Tile> RememberFlip
            {
                get
                {
                    return this.m_RememberFlips;
                }
                set
                {
                    this.m_RememberFlips = value;
                }
            }

            public int PointsForCorrectGuesses
            {
                get
                {
                    return this.m_PointsForCorrectGuesses;
                }
                set
                {
                    this.m_PointsForCorrectGuesses = value;
                }
            }

            internal Board.Tile ReturnARandomTile(Board i_Board)
            {
                Random col = new Random();
                Random row = new Random();
                row.Next(0, i_Board.RowBorder);
                col.Next(0, i_Board.ColumnBorder);
                int rowIndex = row.Next(0, i_Board.RowBorder);
                int colIndex = col.Next(0, i_Board.ColumnBorder);
                while (i_Board[rowIndex, colIndex].IsOpen)
                {
                    rowIndex = row.Next(0, i_Board.RowBorder);
                    colIndex = col.Next(0, i_Board.ColumnBorder);
                }
                rowIndex = row.Next(0, i_Board.RowBorder);
                colIndex = col.Next(0, i_Board.ColumnBorder);
                Board.Tile firstFlip = i_Board[rowIndex, colIndex];
                return i_Board[rowIndex, colIndex];
            }

            internal bool findAMatchingSet()
            {
                foreach (Board.Tile currentTile in this.m_RememberFlips)
                {
                }
                return true;
            }

        }
    }
}
