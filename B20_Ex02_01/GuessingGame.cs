﻿using System;

namespace B20_Ex02_01
{
    public class GuessingGame
    {
        //Members
        private Board m_Board;
        private Player[] m_Players = new Player[2] {null, null};
        private object[] m_ObjectArray;
        //----------------------------------------------------------------------//
        //SetGet
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
        public Player[] Players
        {
            get
            {
                return this.m_Players;
            }
            set
            {
                this.m_Players = value;
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
        //Functions
        public void createRandomBoard()
        {
            int[,] arrayForTileCreation = new int[2, 18];

            for (int i = 0; i < 18; ++i)
            {
                arrayForTileCreation[0, i] = i;
            }

            int maxNumber = (this.Board.RowBorder * this.Board.ColumnBorder) / 2;
            Random random = new Random();
            int randomNumber = random.Next(0, maxNumber);
            int ContentOfTile;

            for (int i = 0; i < this.Board.RowBorder; ++i)
            {
                for (int j = 0; j < this.Board.ColumnBorder; ++j)
                {
                    if (arrayForTileCreation[1, randomNumber] < 2)
                    {
                        ContentOfTile = arrayForTileCreation[0, randomNumber];
                        this.Board[i, j] = new Board.Tile(ContentOfTile, i, j);
                        ++arrayForTileCreation[1, randomNumber];
                    }
                    else
                    {
                        --j;
                    }

                    randomNumber = random.Next(0, maxNumber);
                }
            }
        }
        //----------------------------------------------------------------------//
        public object HashObject(int i_Index)
        {
            return this.m_ObjectArray[i_Index];
        }

        public void MakeAMove(Player i_CurrentPlayer)
        {
            //Get from UI 

            //If Q STOP GAME
        }
    }
}
