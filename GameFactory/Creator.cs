// ********************************
// <copyright file="Creator.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper.GameFactory
{
    using System;
    using Scoring;
    using Saving.Contracts;
    using Scoring.Contracts;
    using Rendering.Contracts;

    /// <summary>
    /// Defines default abstract rules of the Creator's successors.
    /// </summary>
    public abstract class Creator
    {
        public abstract IGameFieldSave CreateGameFieldSave();

        public abstract IScoreRecord CreateScoreRecord(string name, int score);

        public abstract IRenderer CreateRenderer(ScoreBoard scoreBoard, GameField gameField);

        // Both GameField and ScoreBoard have concrete implementation
        // since modification or changes in the structures of the classes
        // is not expected in the long term.

        public abstract GameField CreateGameField();

        public abstract ScoreBoard CreateScoreBoard();
    }
}
