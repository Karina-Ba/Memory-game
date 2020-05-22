using System;

namespace B20_Ex02_01
{
    public class Player
    {
        private string m_Name;
        private int m_PointsForCorrectGuesses;
        private bool m_IsPC;

        public Player(string i_Name, bool i_IsPC)
        {
            this.m_Name = i_Name;
            this.m_PointsForCorrectGuesses = 0;
            this.m_IsPC = i_IsPC;
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
        }


        public class AI
        {
            
        }
    }
}
