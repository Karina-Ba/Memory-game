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
            private char m_ContentOfTile;
            private int m_RowSize;
            private int m_ColumnSize;

            public char ContentOfTile
            {
                get { return this.m_ContentOfTile; }
                set { this.m_ContentOfTile = value; }
            }

            public int RowSize
            {
                get { return this.m_RowSize; }
                set { this.m_RowSize = value; }
            }

            public int ColumnSize
            {
                get { return this.m_ColumnSize; }
                set { this.m_ColumnSize = value; }
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

            for (int i = 0; i < this.m_RowBorder; ++i)
            {
                for (int j = 0; j < this.m_ColumnBorder; ++j)
                {
                    if (arrayForTileCreation[1, randomNumber] < 2)
                    {
                        this.m_Tile[i,j]= new Tile();
                        this.m_Tile[i, j].ContentOfTile = Convert.ToChar(arrayForTileCreation[0, randomNumber] + 65); //ascii of A 65
                        ++arrayForTileCreation[1, randomNumber];
                        this.m_Tile[i, j].RowSize = i;
                        this.m_Tile[i, j].ColumnSize = j;
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
            for (int i = 0; i < this.m_RowBorder; ++i)
            {
                for (int j = 0; j < this.m_ColumnBorder; ++j)
                {
                    Console.Write(this.m_Tile[i,j].ContentOfTile.ToString());
                }
                Console.Write("\n");
            }
            
        }
    }


    
}
