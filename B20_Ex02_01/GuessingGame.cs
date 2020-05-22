using System;

namespace B20_Ex02_01
{
    class GuessingGame
    {
        private Board m_Board;
        private Player[] m_Players = new Player[2] {null, null};

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

        public void CreatePlayer(string i_Name, bool i_IsPC)
        {
             if (this.m_Players[0] == null)
             {
                 this.m_Players[0] = new Player(i_Name, i_IsPC);
             }
             else
             {
                 this.m_Players[1] = new Player(i_Name, i_IsPC);
             }
        }

        public void MakeAMove(Player i_CurrentPlayer)
        {
            //Get from UI 

            //If Q STOP GAME
        }
    }
}
