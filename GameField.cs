// ********************************
// <copyright file="GameField.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Contracts;
    using Saving;

    /// <summary>
    /// Two dimensional array of <see cref="Position"/> class which represents the minesweeper game field
    /// </summary>
    public class GameField : IGameField
    {
        public const int FieldRows = 5;
        public const int FieldColumns = 10;
        public const int BombsCount = 15;
        public const int MaxToReveal = (FieldColumns * FieldRows) - BombsCount;

        private Position[,] gameField;
        private int revealed;

        /// <summary>
        /// Initializes new instance of the <see cref="GameField"/> class.
        /// </summary>
        public GameField()
        {
            this.SetNewField();
        }

        /// <summary>
        /// Gets a matrix with all the positions in the field
        /// </summary>
        public Position[,] Field
        {
            get
            {
                return this.gameField;
            }

            private set
            {
                this.gameField = value;
            }
        }

        public int Revealed
        {
            get
            {
                return this.revealed;
            }

            set
            {
                this.revealed = value;
            }
        }
        /// <summary>
        /// Prepares new game field for the player.
        /// </summary>
        public void SetNewField()
        {
            this.Field = this.GenerateGameField();
            this.Revealed = 0;
        }

        /// <summary>
        /// Reveals all positions in the field
        /// </summary>
        public void RevealField()
        {
            for (int row = 0; row < FieldRows; row++)
            {
                for (int col = 0; col < FieldColumns; col++)
                {
                    this.RevealPosition(row, col);
                }
            }
        }

        /// <summary>
        /// Reveals a position in the field by given coordinates
        /// </summary>
        /// <param name="row">Integer which represents the row of the position we want to reveal</param>
        /// <param name="col">Integer which represents the column of the position we want to reveal</param>
        public void RevealPosition(int row, int col)
        {
            if (this.Field[row, col].IsHidden)
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
        }

        /// <summary>
        /// Creates a memento of the game field from which it could eventually be saved.
        /// </summary>
        /// <returns>A GameFieldMemento instantiation with the current Field</returns>
        public GameFieldMemento Save()
        {
            return new GameFieldMemento(this.Field, this.Revealed);
        }

        /// <summary>
        /// Restores the game field from a given memento.
        /// </summary>
        /// <param name="memento">The memento from which the Field should be restored.</param>
        public void RestoreFromSave(GameFieldMemento memento)
        {
            this.Field = memento.Field;
        }

        /// <summary>
        /// Displays the minesweeper field
        /// </summary>
        /// <returns>String representing all the positions of the Minesweeper field</returns>
        public override string ToString()
        {
            StringBuilder minesweepweField = new StringBuilder();

            minesweepweField.AppendLine();
            minesweepweField.AppendLine("    0 1 2 3 4 5 6 7 8 9");
            minesweepweField.AppendLine("   ---------------------");

            for (int i = 0; i < FieldRows; i++)
            {
                minesweepweField.Append(string.Format("{0} | ", i));

                for (int j = 0; j < FieldColumns; j++)
                {
                    minesweepweField.Append(string.Format("{0} ", this.Field[i, j].Value));
                }

                minesweepweField.Append("|");
                minesweepweField.AppendLine();
            }

            minesweepweField.AppendLine("   ---------------------");
            minesweepweField.AppendLine();

            return minesweepweField.ToString();
        }

        /// <summary>
        /// Counts the number of bombs surrounding given position
        /// </summary>
        /// <param name="row">Integer number representing the row of the <see cref="GameField"/> class position we want to visit</param>
        /// <param name="col">Integer number representing the column of the <see cref="GameField"/> class position we want to visit</param>
        /// <returns>Integer number equal to the number of bombs in the positions next to the given</returns>
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
        
        /// <summary>
        /// Creates a new game field instance
        /// </summary>
        /// <returns>Two dimensional array of <see cref="Position"/></returns>
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
