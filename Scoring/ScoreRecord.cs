namespace Minesweeper.Scoring
{
    using System;
    using Contracts;

    public class ScoreRecord : IScoreRecord
    {
        private string playerName;
        private int playerScore;

        public ScoreRecord(string playerName, int score)
        {
            this.PlayerName = playerName;
            this.PlayerScore = score;
        }

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

        public override string ToString()
        {
            return String.Format("{0} --> {1}", this.PlayerName, this.PlayerScore);
        }
    }
}
