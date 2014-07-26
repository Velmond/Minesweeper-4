// ********************************
// <copyright file="ScoreRecord.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper.Scoring
{
    using System;
    using Contracts;

    /// <summary>
    /// Implements the <see cref="IScoreRecord"/> interface and and stores the last player score
    /// </summary>
    public class ScoreRecord : IScoreRecord
    {
        private string playerName;
        private int playerScore;

        /// <summary>
        /// Initializes a new instance of <see cref="ScoreRecord"/> class and sets the parameters of the instance
        /// </summary>
        /// <param name="playerName">String which represents player's name</param>
        /// <param name="score">Integer equal to the number of the revealed by the player cells</param>
        public ScoreRecord(string playerName, int score)
        {
            this.PlayerName = playerName;
            this.PlayerScore = score;
        }

        /// <summary>
        /// Gets the name of the player and vallidates the input when setting it
        /// </summary>
        public string PlayerName
        {
            get
            {
                return this.playerName;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("The player name cannot be null.");
                }

                if (value == String.Empty)
                {
                    throw new ArgumentException("The player name cannot be an empty string.");
                }

                this.playerName = value;
            }
        }

        /// <summary>
        /// Gets the score of the players and vallidates the input when setting it
        /// </summary>
        public int PlayerScore
        {
            get
            {
                return this.playerScore;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("The player score must be a positive value");
                }

                this.playerScore = value;
            }
        }

        /// <summary>
        /// Overrides the default method to show the score to the player
        /// </summary>
        /// <returns>Formated string containing the name and the score of the player</returns>
        public override string ToString()
        {
            return String.Format("{0} --> {1}", this.PlayerName, this.PlayerScore);
        }
    }
}
