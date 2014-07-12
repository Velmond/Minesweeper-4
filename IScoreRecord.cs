namespace Minesweeper
{
    /// <summary>
    /// Provides a default template of score record data
    /// </summary>
    public interface IScoreRecord
    {
        string PlayerName { get; set; }

        int PlayerScore { get; set; }
    }
}
