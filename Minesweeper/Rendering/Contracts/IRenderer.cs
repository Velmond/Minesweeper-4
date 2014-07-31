// ********************************
// <copyright file="IRenderer.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper.Rendering.Contracts
{
    using Minesweeper.Field.Contracts;
    using Minesweeper.Scoring.Contracts;

    /// <summary>
    /// Defines default rendering rules and behavior.
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Keeps reference to the current game session score board.
        /// </summary>
        IScoreBoard ScoreBoard { get; }

        /// <summary>
        /// Keeps reference to the current game session field.
        /// </summary>
        IGameField GameField { get; }

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

        //// Messages

        void RenderMessageInvalidCommand();

        void RenderMessageInvalidCoordinates();

        void RenderCoordinatesRequest();

        void RenderCommandRequest();

        void RenderSaveDone();
    }
}
