namespace Minesweeper
{
    using System.Collections.Generic;
    using System.Text;

    public class ScoreBoard
    {
        private const int MaxNumberOfEntries = 6;
        private IList<ScoreRecord> highScores;

        public ScoreBoard()
        {
            this.HighScores = new List<ScoreRecord>(MaxNumberOfEntries);
        }

        public IList<ScoreRecord> HighScores
        {
            get { return this.highScores; }
            set { this.highScores = value; }
        }

        public void AddScore(ScoreRecord currentScore)
        {
            if (this.HighScores.Count == 0)
            {
                this.HighScores.Add(currentScore);
            }
            else if (this.HighScores.Count < MaxNumberOfEntries ||
                currentScore.PlayerScore > this.HighScores[this.HighScores.Count - 1].PlayerScore)
            {
                for (int i = 0; i < this.HighScores.Count; i++)
                {
                    if (this.HighScores[i].PlayerScore < currentScore.PlayerScore)
                    {
                        this.HighScores.Insert(i, currentScore);
                        break;
                    }
                    else if (this.HighScores[i].PlayerScore == currentScore.PlayerScore)
                    {
                        if (this.HighScores[i].PlayerName.CompareTo(currentScore.PlayerName) > 0)
                        {
                            this.HighScores.Insert(i, currentScore);
                        }
                        else
                        {
                            this.HighScores.Insert(i + 1, currentScore);
                        }

                        break;
                    }
                    else if (i == this.HighScores.Count - 1)
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

        public override string ToString()
        {
            StringBuilder toString = new StringBuilder();

            toString.AppendLine("Scoreboard:");

            if (this.HighScores.Count > 0)
            {
                for (int i = 0; i < this.HighScores.Count; i++)
                {
                    toString.AppendLine(string.Format("{0}. {1} cells", i + 1, this.HighScores[i].ToString()));
                }

                toString.AppendLine();
            }
            else
            {
                toString.AppendLine("No records to display!");
            }

            return toString.ToString();
        }
    }
}
