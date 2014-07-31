// ********************************
// <copyright file="ScoreBoard.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper.Scoring
{
    using System.Collections.Generic;
    using System.Text;
    using Contracts;

    /// <summary>
    /// Stores the best high scores
    /// </summary>
    public class ScoreBoard : IScoreBoard
    {
        private const int MaxNumberOfEntries = 6;
        private IList<IScoreRecord> highScores;

        /// <summary>
        /// Default constructor which provides List<ScoreRecord> as a collection.
        /// </summary>
        public ScoreBoard()
            : this(new List<IScoreRecord>(MaxNumberOfEntries))
        {
        }

        /// <summary>
        /// Optional consutructor allowing a specific highScores collection type.
        /// </summary>
        /// <param name="collection">highScores collection. Must be of type IList<ScoreRecord></param>
        public ScoreBoard(IList<IScoreRecord> collection)
        {
            this.HighScores = collection;
        }

        public IList<IScoreRecord> HighScores
        {
            get
            {
                return this.highScores;
            }

            set
            {
                this.highScores = value;
            }
        }

        /// <summary>
        /// Clears all current high scores kept in IList<ScoreRecord> highScores.
        /// </summary>
        public void Reset()
        {
            this.HighScores.Clear();
        }

        /// <summary>
        /// Checks whether the given score record meets the requirements in order to be added to the ScoreBoard
        /// </summary>
        /// <param name="currentScore">The current score result</param>
        public void AddScore(IScoreRecord currentScore)
        {
            int lastHighScoreId = this.HighScores.Count - 1;
            
            if (this.HighScores.Count == 0)
            {
                this.HighScores.Add(currentScore);
            }
            else if (currentScore.PlayerScore > this.HighScores[lastHighScoreId].PlayerScore || this.HighScores.Count < MaxNumberOfEntries)
            {
                for (int i = 0; i < this.HighScores.Count; i++)
                {
                    IScoreRecord iterationScore = this.HighScores[i];

                    if (iterationScore.PlayerScore < currentScore.PlayerScore)
                    {
                        this.HighScores.Insert(i, currentScore);
                        break;
                    }
                    else if (iterationScore.PlayerScore == currentScore.PlayerScore)
                    {
                        this.SortEqualHighScores(iterationScore, currentScore, i);
                        break;
                    }
                    else if (i == lastHighScoreId)
                    {
                        this.HighScores.Add(currentScore);
                        break;
                    }
                }
            }

            if (this.HighScores.Count > MaxNumberOfEntries)
            {
                this.HighScores.RemoveAt(MaxNumberOfEntries);
            }
        }

        /// <summary>
        /// Generates a string representation of the score board.
        /// </summary>
        /// <returns>Returns a string with all the scores</returns>
        public override string ToString()
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine("Scoreboard:");

            if (this.HighScores.Count > 0)
            {
                for (int i = 0; i < this.HighScores.Count; i++)
                {
                    output.AppendLine(string.Format("{0}. {1} cells", i + 1, this.HighScores[i].ToString()));
                }

                output.AppendLine();
            }
            else
            {
                output.AppendLine("No records to display!");
            }

            return output.ToString();
        }

        /// <summary>
        /// Sorts two equal high scores by their holders' names.
        /// </summary>
        /// <param name="iterationScore">Comparer score</param>
        /// <param name="currentScore">The current player score</param>
        /// <param name="index">Index of the comparer score</param>
        private void SortEqualHighScores(IScoreRecord iterationScore, IScoreRecord currentScore, int index)
        {
            if (iterationScore.PlayerName.CompareTo(currentScore.PlayerName) > 0)
            {
                this.HighScores.Insert(index, currentScore);
            }
            else
            {
                this.HighScores.Insert(index + 1, currentScore);
            }
        }
    }
}
