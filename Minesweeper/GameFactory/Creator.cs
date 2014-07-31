// ********************************
// <copyright file="Creator.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper.GameFactory
{
    using System;

    using Minesweeper.Field.Contracts;
    using Minesweeper.Rendering.Contracts;
    using Minesweeper.Scoring.Contracts;
    using Minesweeper.UserInput.Contracts;

    /// <summary>
    /// Defines default abstract rules of the Creator's successors.
    /// </summary>
    public abstract class Creator
    {
        public abstract IGameFieldSave CreateGameFieldSave();

        public abstract IScoreRecord CreateScoreRecord(string name, int score);

        public abstract IRenderer CreateRenderer(IScoreBoard scoreBoard, IGameField gameField);

        public abstract IUserInput CreateUserInputRequester();

        public abstract IGameField CreateGameField();

        public abstract IScoreBoard CreateScoreBoard();
    }
}
