namespace Minesweeper.Rendering.Contracts
{
    using Scoring;

    /// <summary>
    /// Defines default rendering rules and behavior.
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Keeps reference to the current game session score board.
        /// </summary>
        ScoreBoard ScoreBoard { get; }

        /// <summary>
        /// Keeps reference to the current game session field.
        /// </summary>
        GameField GameField { get; }

        void RenderScoreBoard();

        void RenderGameField();

        void RenderNewGame();

        void RenderGameWon();

        /// <summary>
        /// Visual representation on game over.
        /// </summary>
        /// <param name="currentPlayerScore">Mandatory for displaying player's score.</param>
        void RenderGameOver(int currentPlayerScore);

        void RenderApplicationExit();

        // Messages

        void RenderMessageInvalidCommand();

        void RenderMessageInvalidCoordinates();
    }
}
