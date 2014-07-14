﻿namespace Minesweeper
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GameField
    {
        public const int FieldRows = 5;
        public const int FieldColumns = 10;
        public const int BombsCount = 15;

        private Position[,] gameField;

        public GameField()
        {
            this.Field = GenerateGameField();
        }

        public Position[,] Field
        {
            get { return this.gameField; }
            private set { this.gameField = value; }
        }

        public void RevealField()
        {
            for (int row = 0; row < this.Field.GetLength(0); row++)
            {
                for (int col = 0; col < this.Field.GetLength(1); col++)
                {
                    RevealPosition(row, col);
                }
            }
        }

        public void RevealPosition(int row, int col)
        {
            if (this.Field[row, col].IsBomb)
            {
                this.Field[row, col].Reveal('*');
            }
            else
            {
                int surroundingBombsCount = this.SurroundingBombsCount(row, col);
                this.Field[row, col].Reveal(surroundingBombsCount.ToString()[0]);
            }
        }

        private int SurroundingBombsCount(int row, int col)
        {
            int surroundingBombsCount = 0;
            if (row - 1 >= 0)
            {
                if (this.Field[row - 1, col].IsBomb)
                {
                    surroundingBombsCount++;
                }
            }

            if (row + 1 < FieldRows)
            {
                if (this.Field[row + 1, col].IsBomb)
                {
                    surroundingBombsCount++;
                }
            }

            if (col - 1 >= 0)
            {
                if (this.Field[row, col - 1].IsBomb)
                {
                    surroundingBombsCount++;
                }
            }

            if (col + 1 < FieldColumns)
            {
                if (this.Field[row, col + 1].IsBomb)
                {
                    surroundingBombsCount++;
                }
            }

            if ((row - 1 >= 0) && (col - 1 >= 0))
            {
                if (this.Field[row - 1, col - 1].IsBomb)
                {
                    surroundingBombsCount++;
                }
            }

            if ((row - 1 >= 0) && (col + 1 < FieldColumns))
            {
                if (this.Field[row - 1, col + 1].IsBomb)
                {
                    surroundingBombsCount++;
                }
            }

            if ((row + 1 < FieldRows) && (col - 1 >= 0))
            {
                if (this.Field[row + 1, col - 1].IsBomb)
                {
                    surroundingBombsCount++;
                }
            }

            if ((row + 1 < FieldRows) && (col + 1 < FieldColumns))
            {
                if (this.Field[row + 1, col + 1].IsBomb)
                {
                    surroundingBombsCount++;
                }
            }

            return surroundingBombsCount;
        }

        public override string ToString()
        {
            StringBuilder toString = new StringBuilder();

            toString.AppendLine();
            toString.AppendLine("    0 1 2 3 4 5 6 7 8 9");
            toString.AppendLine("   ---------------------");

            for (int i = 0; i < FieldRows; i++)
            {
                toString.Append(string.Format("{0} | ", i));

                for (int j = 0; j < FieldColumns; j++)
                {
                    toString.Append(string.Format("{0} ", this.Field[i, j].Value));
                }

                toString.Append("|");
                toString.AppendLine();
            }

            toString.AppendLine("   ---------------------");
            toString.AppendLine();

            return toString.ToString();
        }

        private Position[,] GenerateGameField()
        {
            Position[,] gameField = new Position[FieldRows, FieldColumns];

            for (int i = 0; i < FieldRows; i++)
            {
                for (int j = 0; j < FieldColumns; j++)
                {
                    gameField[i, j] = new Position();
                }
            }

            List<int> bombs = new List<int>();

            while (bombs.Count < BombsCount)
            {
                Random random = new Random();
                int bomb = random.Next(FieldRows * FieldColumns);

                if (!bombs.Contains(bomb))
                {
                    bombs.Add(bomb);
                }
            }

            foreach (int bomb in bombs)
            {
                int row = bomb / FieldColumns;
                int column = bomb % FieldColumns;

                if (column == 0 && bomb != 0)
                {
                    row--;
                    column = FieldColumns;
                }
                else
                {
                    column++;
                }

                gameField[row, column - 1].MakeBomb();
            }

            return gameField;
        }
    }
}
