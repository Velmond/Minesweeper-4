// ********************************
// <copyright file="GameCreator.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper.GameFactory
{
    using System;

    using Controls;
    using Saving;
    using Scoring;
    using Rendering;

    using Controls.Contracts;
    using Saving.Contracts;
    using Scoring.Contracts;
    using Rendering.Contracts;
    
    /// <summary>
    /// Implements Creator with concrete types for instantiating.
    /// </summary>
    public class GameCreator : Creator
    {
        public GameCreator()
        {
            this.IsGameOver = false;
            this.IsNewGame = true;
            this.IsGameOn = true;
            this.GameField = this.CreateGameField();
        }

        public override IGameFieldSave CreateGameFieldSave()
        {
            return new GameFieldSave();
        }

        public override IScoreRecord CreateScoreRecord(string name, int score)
        {
            return new ScoreRecord(name, score);
        }

        public override IRenderer CreateRenderer(ScoreBoard scoreBoard, GameField gameField)
        {
            return new Renderer(scoreBoard, gameField);
        }

        public override GameField CreateGameField()
        {
            return new GameField();
        }

        public override ScoreBoard CreateScoreBoard()
        {
            return new ScoreBoard();
        }
    }
}
