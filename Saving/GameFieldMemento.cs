// ********************************
// <copyright file="GameFieldMemento.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper.Saving
{
    using System;

    /// <summary>
    /// Class for saving the game field information.
    /// </summary>
    public class GameFieldMemento
    {
        /// <summary>
        /// The array in which all the information about the game field is held.
        /// </summary>
        private Position[,] gameField;

        /// <summary>
        /// Class for saving the game field information.
        /// </summary>
        /// <param name="gameField">The game field of which the information should be saved.</param>
        public GameFieldMemento(Position[,] gameField)
        {
            this.Field = gameField;
        }

        /// <summary>
        /// The array in which all the information about the game field is held
        /// </summary>
        public Position[,] Field
        {
            get
            {
                return this.gameField;
            }

            private set
            {
                if (value == null)
                {
                    throw new NullReferenceException("Game field to be saved cannot be null.");
                }

                this.gameField = CopyArray(value);
            }
        }

        /// <summary>
        /// Creates a deep copy of the given array (not by reference).
        /// </summary>
        /// <param name="array">Array that needs to be copied.</param>
        /// <returns>A deep copy of the given array.</returns>
        private static Position[,] CopyArray(Position[,] array)
        {
            Position[,] resultArray = new Position[array.GetLength(0), array.GetLength(1)];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    resultArray[i, j] = new Position(array[i, j].Value, array[i, j].IsHidden, array[i, j].IsBomb);
                }
            }

            return resultArray;
        }
    }
}
