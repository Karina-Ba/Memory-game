using System;

namespace B20_Ex02_01
{
    public class Board
    {
        //Members
        private Tile[,] m_Tile;
        private readonly int m_RowBorder;
        private readonly int m_ColumnBorder;

        private int m_NumberOfOpenTiles;
        //----------------------------------------------------------------------//
        //C'tor
        public Board(int i_RowSize, int i_ColumnSize)
        {
            this.m_ColumnBorder = i_ColumnSize;
            this.m_RowBorder = i_RowSize;
            this.m_NumberOfOpenTiles = 0;
            this.m_Tile = new Tile[i_RowSize, i_ColumnSize];
        }
        //----------------------------------------------------------------------//
        //SetGets
        public int RowBorder
        {
            get
            {
                return this.m_RowBorder;
            }
        }
        //----------------------------------------------------------------------//
        public int ColumnBorder
        {
            get
            {
                return this.m_ColumnBorder;
            }
        }
        //----------------------------------------------------------------------//
        internal Tile this[int i, int j]
        {
            get
            {
                return this.m_Tile[i, j];
            }
            set
            {
                this.m_Tile[i, j] = value;
            }
        }
        //----------------------------------------------------------------------//
        public int NumberOfOpenTiles
        {
            get
            {
                return this.m_NumberOfOpenTiles;
            }
            set
            {
                this.m_NumberOfOpenTiles = value;

            }
        }
        //----------------------------------------------------------------------//
        //Nested Class
        internal class Tile
        {
            private int m_ContentOfTile;
            private int m_Row;
            private int m_Column;
            private bool m_IsOpen;

            public Tile(int i_ContentOfTile, int i_Row, int i_Column)
            {
                this.m_Column = i_Column;
                this.m_Row = i_Row;
                this.m_ContentOfTile = i_ContentOfTile;
                this.m_IsOpen = false;
            }

            public int ContentOfTile
            {
                get
                {
                    if (this.m_IsOpen)
                    {
                        return this.m_ContentOfTile;
                    }
                    else
                    {
                        return -1;
                    }
                }
                set
                {
                    this.ContentOfTile = value;
                }
            }
            public bool IsOpen
            {
                get
                {
                    return this.m_IsOpen;
                }
                set
                {
                    this.m_IsOpen = value;
                }
            }

            public int Column
            {
                get
                {
                    return this.m_Column;
                }
                set
                {
                    this.m_Column = value;
                }
            }

            public int Row
            {
                get
                {
                    return this.m_Row;
                }
                set
                {
                    this.m_Row = value;
                }
            }
        }
        //----------------------------------------------------------------------//
        //Functions
        
    }


    
}
