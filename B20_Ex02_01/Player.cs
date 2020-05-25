using System;
using System.Collections.Generic;

namespace B20_Ex02_01
{
    public class Player
    {
        //Members
        private readonly string m_Name;
        private int m_PointsForCorrectGuesses;
        //----------------------------------------------------------------------//
        //C'tor
        public Player(string i_Name)
        {
            this.m_Name = i_Name;
            this.m_PointsForCorrectGuesses = 0;
        }
        //----------------------------------------------------------------------//
        //Get
        public string Name
        {
            get
            {
                return this.m_Name;
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

    }

}
