using System;
using System.Collections.Generic;

namespace B20_Ex02_01
{
    public class AI
    {
        private List<Board.Tile> m_RememberFlips;
        private int m_PointsForCorrectGuesses;
        //----------------------------------------------------------------------//
        public AI()
        {
            this.m_RememberFlips = new List<Board.Tile>(36);
        }
        //----------------------------------------------------------------------//
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
        //----------------------------------------------------------------------//
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
        //----------------------------------------------------------------------//
        internal void MakeAIMove(UserInterface i_UI, out Board.Tile o_FirstTile, out Board.Tile o_SecondTile, Player i_Player)
        {
            bool foundAMatch = this.findAMatchingSet(out o_FirstTile, out o_SecondTile, i_UI.Game);

            if (!foundAMatch)
            {
                o_FirstTile = this.ReturnARandomTile(i_UI.Game);
                o_SecondTile = this.ReturnARandomTile(i_UI.Game);
            }
            this.OpenAndPrintBoard(o_FirstTile, o_SecondTile, i_UI, i_Player);
            if (!i_UI.Game.IsMatchingFlip(o_FirstTile, o_SecondTile))
            {
                insertTileIntoListByIndex(o_FirstTile, i_UI.Game);
                insertTileIntoListByIndex(o_SecondTile, i_UI.Game);
            }
        }
        //----------------------------------------------------------------------//
        private void insertTileIntoListByIndex(Board.Tile i_Tile, GuessingGame i_Game)
        {
            int numOfElementsInList = this.RememberFlip.Count;
            int index = 0;
            if (!this.m_RememberFlips.Contains(i_Tile))
            {
                for (index = 0; index < numOfElementsInList; ++index)
                {
                    if (i_Game.IsMatchingFlip(i_Tile, this.m_RememberFlips[index]))
                    {
                        break;
                    }
                }
                this.m_RememberFlips.Insert(index, i_Tile);
            }
        }
        internal Board.Tile ReturnARandomTile(GuessingGame i_Game)
        {
            //Random col = this.m_RandomNumber.Next(0,)
            //Random row = new Random();
            //row.Next(0, i_Board.RowBorder);
            //col.Next(0, i_Board.ColumnBorder);
            int rowIndex = i_Game.RandomNumber.Next(0, i_Game.Board.RowBorder);
            int colIndex = i_Game.RandomNumber.Next(0, i_Game.Board.ColumnBorder);
            while (i_Game.Board[rowIndex, colIndex].IsOpen)
            {
                rowIndex = i_Game.RandomNumber.Next(0, i_Game.Board.RowBorder);
                colIndex = i_Game.RandomNumber.Next(0, i_Game.Board.ColumnBorder);

                //rowIndex = row.Next(0, i_Board.RowBorder);
                //colIndex = col.Next(0, i_Board.ColumnBorder);
            }
            
            return i_Game.Board[rowIndex, colIndex];
        }
        //----------------------------------------------------------------------//
        internal bool findAMatchingSet(out Board.Tile o_FirstTile, out Board.Tile o_SecondTile, GuessingGame i_Game)
        {
            o_FirstTile = null;
            o_SecondTile = null;
            bool isMatchFound = false;
            int ListCapacity = this.RememberFlip.Count;
            for (int i = 0; i < ListCapacity - 1; ++i)
            {
                if (i_Game.IsMatchingFlip(this.m_RememberFlips[i], this.m_RememberFlips[i + 1]))
                {
                    o_FirstTile = m_RememberFlips[i];
                    o_SecondTile = m_RememberFlips[i + 1];
                    isMatchFound = true;
                    break;
                }
            }
            return isMatchFound;
        }
        //----------------------------------------------------------------------//
        internal void OpenAndPrintBoard(Board.Tile i_FirstTile, Board.Tile i_SecondTile, UserInterface i_UI, Player i_Player)
        {
            i_FirstTile.OpenTile();
            i_UI.ClearScreenShowBoard(i_Player);
            System.Threading.Thread.Sleep(2000);
            i_SecondTile.OpenTile();
            i_UI.ClearScreenShowBoard(i_Player);
        }
    }
}
