namespace Minesweeper.Scoring.Contracts
{
    using System.Collections.Generic;

    public interface IScoreBoard
    {
        IList<IScoreRecord> HighScores { get; set; }

        /// <summary>
        /// Clears all current high scores kept in IList<ScoreRecord> highScores.
        /// </summary>
        void Reset();

        /// <summary>
        /// Checks whether the given score record meets the requirements in order to be added to the ScoreBoard
        /// </summary>
        /// <param name="currentScore">The current score result</param>
        void AddScore(IScoreRecord currentScore);
    }
}
