namespace Minesweeper
{
    public class ScoreRecord
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
            get { return this.playerName; }
            set { this.playerName = value; }
        }

        public int PlayerScore
        {
            get { return this.playerScore; }
            set { this.playerScore = value; }
        }

        public override string ToString()
        {
            return string.Format("{0} --> {1}", this.PlayerName, this.PlayerScore);
        }
    }
}
