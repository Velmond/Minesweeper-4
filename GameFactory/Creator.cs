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
    using Controls.Contracts;

    /// <summary>
    /// Defines default abstract rules of the Creator's successors.
    /// </summary>
    public abstract class Creator
    {
        private bool isNewGame;
        private bool isGameOver;
        private bool isGameOn;
        private GameField gameField;

        public GameField GameField
        {
            get
            {
                return this.gameField;
            }
            set
            {
                this.gameField = value;
            }
        }

        public bool IsGameOn
        {
            get
            {
                return this.isGameOn;
            }
            set
            {
                this.isGameOn = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current game is finished or not 
        /// </summary>
        public bool IsGameOver
        {
            get
            {
                return this.isGameOver;
            }
            set
            {
                this.isGameOver = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether new game must be started or not
        /// </summary>
        public bool IsNewGame
        {
            get
            {
                return this.isNewGame;
            }
            set
            {
                this.isNewGame = value;
            }
        }

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
