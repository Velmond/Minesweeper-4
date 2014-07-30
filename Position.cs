// ********************************
// <copyright file="Position.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper
{
    /// <summary>
    /// Represents a single cell of the Minesweeper game field
    /// </summary>
    public class Position
    {
        private const char DefaultValue = '?';
        private bool isBomb;
        private bool isHidden;
        private char value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Position"/> class which is not a bomb
        /// </summary>
        public Position()
            : this(DefaultValue, true, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Position"/> class which can be a bomb
        /// </summary>
        /// <param name="value">Single character which represents the symbol for the position</param>
        /// <param name="isHidden">Boolean variable which shows if the position is hidden</param>
        /// <param name="isBomb">Boolean variable which decides if the position is bomb</param>
        public Position(char value, bool isHidden, bool isBomb)
        {
            this.IsBomb = isBomb;
            this.IsHidden = isHidden;
            this.Value = value;
        }

        /// <summary>
        /// Gets a value indicating whether the given <see cref="Position"/> class instance is a bomb or not
        /// </summary>
        public bool IsBomb
        {
            get
            {
                return this.isBomb;
            }

            private set
            {
                this.isBomb = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the given <see cref="Position"/> class instance is still hidden or has been visited by the player
        /// </summary>
        public bool IsHidden
        {
            get
            {
                return this.isHidden;
            }

            private set
            {
                this.isHidden = value;
            }
        }

        /// <summary>
        /// Gets the symbol which will display the given position to the player
        /// </summary>
        public char Value
        {
            get
            {
                return this.value;
            }

            private set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Creates a bomb in the given position
        /// </summary>
        public void MakeBomb()
        {
            this.IsBomb = true;
        }

        /// <summary>
        /// Reveals the position value after the player visit it
        /// </summary>
        /// <param name="value">Char which will represent the value of the position when the player visit it</param>
        public void Reveal(char value)
        {
            // If position is not hidden it already has a value which should not be able to change
            if (this.IsHidden)
            {
                this.IsHidden = false;
                this.Value = value;
            }
        }
    }
}
