using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace B20_Ex02_01
{
    public class GuessingGame
    {
        public enum eGameType
        {
            PlayerVSPlayer = 1,
            PlayerVSPC = 2
        };
        //----------------------------------------------------------------------//
        private Board m_Board;
        private List<Player> m_Players;
        private object[] m_ObjectArray;
        private eGameType m_GameType;
        private Random m_RandomNumber;
        //----------------------------------------------------------------------//
        public GuessingGame()
        {
            this.m_Players = new List<Player>();
            this.m_RandomNumber = new Random();
            this.ObjectArray = new object[18]
                { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R' };
        }
        //----------------------------------------------------------------------//
        public Board Board
        {
            get
            {
                return this.m_Board;
            }
            set
            {
                this.m_Board = value;
            }
        }
        //----------------------------------------------------------------------//
        public List<Player> Players
        {
            get
            {
                return this.m_Players;
            }
        }
        //----------------------------------------------------------------------//
        public object[] ObjectArray
        {
            set
            {
                this.m_ObjectArray = value;
            }
        }
        //----------------------------------------------------------------------//
        public eGameType GameType
        {
            get
            {
                return this.m_GameType;
            }
            set
            {
                this.m_GameType = value;
            }
        }
        //----------------------------------------------------------------------//
        public Random RandomNumber 
        {
            get
            {
                return this.m_RandomNumber;
            }
            set
            {
                this.m_RandomNumber = value;

            }
        }
        //----------------------------------------------------------------------//
        public bool IsUserGameTypeInputValid(string i_UserInput)
        {
            return (eGameType.TryParse(i_UserInput, out this.m_GameType) && (this.m_GameType == eGameType.PlayerVSPlayer || this.m_GameType == eGameType.PlayerVSPC));
        }
        //----------------------------------------------------------------------//
        public bool CheckForValidInput(string i_RowSize, string i_ColSize)
        {
            bool boolToReturn = true;
            int numOfRows, numOfCols;
            if (!(int.TryParse(i_RowSize, out numOfRows)))
            {
                boolToReturn = false;
            }

            if (!(int.TryParse(i_ColSize, out numOfCols)))
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
        public void AddPlayerToList(string i_Name)
        {
            this.m_Players.Add(new Player(i_Name));
        }
        //----------------------------------------------------------------------//
        public void SetBoard(int i_RowSize, int i_ColumnSize)
        {
            this.m_Board = new Board(i_RowSize, i_ColumnSize);
            this.CreateRandomBoard();
        }
        //----------------------------------------------------------------------//
        private void CreateRandomBoard()
        {
            int[,] arrayForTileCreation = new int[2, 18];

            for (int i = 0; i < 18; ++i)
            {
                arrayForTileCreation[0, i] = i;
            }

            int maxNumber = (this.Board.RowBorder * this.Board.ColumnBorder) / 2;
            int randomNumber = this.m_RandomNumber.Next(0, maxNumber);

            for (int i = 0; i < this.Board.RowBorder; ++i)
            {
                for (int j = 0; j < this.Board.ColumnBorder; ++j)
                {
                    if (arrayForTileCreation[1, randomNumber] < 2)
                    {
                        int contentOfTile = arrayForTileCreation[0, randomNumber];
                        this.Board[i, j] = new Board.Tile(contentOfTile, i, j);
                        ++arrayForTileCreation[1, randomNumber];
                    }
                    else
                    {
                        --j;
                    }

                    randomNumber = this.m_RandomNumber.Next(0, maxNumber);
                }
            }
        }
        //----------------------------------------------------------------------//
        public object HashObject(int i_Index)
        {
            return this.m_ObjectArray[i_Index];
        }
        //----------------------------------------------------------------------//
        public bool GameEnd()
        {
            return this.Board.NumberOfOpenTiles == (this.Board.ColumnBorder * this.Board.RowBorder);
        }
        //----------------------------------------------------------------------//
        internal bool CheckForValidGameMove(string i_UserMove, out Board.Tile io_TileToFlip)
        {
            bool isValidMove = false, validRows, validColumns;
            int rowFromInput = 0;
            int colFromInput = 0;
            io_TileToFlip = null;

            if (i_UserMove.Length == 2 && char.IsUpper(i_UserMove[0]) && char.IsDigit(i_UserMove[1]))
            {
                colFromInput = Convert.ToInt32(i_UserMove[0] - 'A');
                rowFromInput = Convert.ToInt32(i_UserMove[1] - '0');
                validRows = (rowFromInput <= this.Board.RowBorder && rowFromInput >= 0);
                validColumns = (colFromInput <= this.Board.ColumnBorder && colFromInput >= 0);
                isValidMove = (validRows && validColumns);
            }

            if (isValidMove)
            {
                io_TileToFlip = this.Board[rowFromInput - 1, colFromInput];

                if (io_TileToFlip.IsOpen == true)
                {
                    isValidMove = false;
                    io_TileToFlip = null;
                }
            }

            return isValidMove;
        }
        //----------------------------------------------------------------------//
        public void DidUserQuit(string i_UserInput)
        {
            if (i_UserInput == "Q")
            {
                Console.WriteLine("Thank you for playing!");
                Environment.Exit(0);
            }
        }
        //----------------------------------------------------------------------//
        internal bool IsMatchingFlip(Board.Tile i_FirstTile, Board.Tile i_SecondTile)
        {
            return i_FirstTile.ContentOfTile == i_SecondTile.ContentOfTile;
        }
        //----------------------------------------------------------------------//
        public bool IsPlayerAI(Player i_Player)
        {
            return this.GameType == eGameType.PlayerVSPC && i_Player.Name == "PC";
        }
        //----------------------------------------------------------------------//
        public void SwitchPlayer(ref Player io_Player)
        {
            if (io_Player == this.Players.First())
            {
                io_Player = this.m_Players.Last();
            }
            else
            {
                io_Player = this.m_Players.First();
            }
        }
       
    }
}
