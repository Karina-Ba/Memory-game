using System;

namespace B20_Ex02_01
{
    public class Board
    {
        private Tile[,] m_Tile;
        private int m_RowBorder;
        private int m_ColumnBorder;
        
        public Board(int i_RowSize, int i_ColumnSize)
        {
            this.m_ColumnBorder = i_ColumnSize;
            this.m_RowBorder = i_RowSize;

            this.m_Tile = new Tile[i_RowSize, i_ColumnSize];
            createRandomBoard();
        }

        private class Tile
        {
            private object m_ContentOfTile;
            private int m_Row;
            private int m_Column;
            private bool m_IsOpen;

            public Tile(object i_ContentOfTile, int i_Row, int i_Column)
            {
                this.m_Column = i_Column;
                this.m_Row = i_Row;
                this.m_ContentOfTile = i_ContentOfTile;
                this.m_IsOpen = false;
            }

            public bool IsOpen
            {
                get { return this.m_IsOpen; }
                set { this.m_IsOpen = value; }
            }

            public object returnContentOfTile()
            {
                if (this.m_IsOpen)
                {
                    return this.m_ContentOfTile;
                }
                else
                {
                    return ' ';
                }
            }
        }

        private void createRandomBoard()
        {
            int[,] arrayForTileCreation = new int[2, 18];

            for (int i = 0; i < 18; ++i)
            {
                arrayForTileCreation[0, i] = i;
            }

            int maxNumber = (this.m_ColumnBorder * this.m_RowBorder) / 2;
            Random random = new Random();
            int randomNumber = random.Next(0, maxNumber);
            object ContentOfTile;

            for (int i = 0; i < this.m_RowBorder; ++i)
            {
                for (int j = 0; j < this.m_ColumnBorder; ++j)
                {
                    if (arrayForTileCreation[1, randomNumber] < 2)
                    {
                        ContentOfTile = Convert.ToChar(arrayForTileCreation[0, randomNumber] + 'A');
                        this.m_Tile[i,j]= new Tile(ContentOfTile, i, j);
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

        public void printBoard()
        {
            //Console.WriteLine(); GRAPHICS

            for (int i = 0; i < this.m_RowBorder; ++i)
            {
                for (int j = 0; j < this.m_ColumnBorder; ++j)
                {
                    Console.Write(this.m_Tile[i,j].returnContentOfTile().ToString());
                }
                Console.WriteLine("");
            }
            
        }
    }


    
}
