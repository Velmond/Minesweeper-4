// ********************************
// <copyright file="GameStateManager.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper
{
    using System;

    /// <summary>
    /// Keeps essential data for the game session.
    /// Main purpose: Data transfer among classes.
    /// </summary>
    public class GameStateManager
    {
        private static GameStateManager instance;

        private bool isNewGame;
        private bool isGameOver;
        private bool isGameOn;
        private GameField gameField;

        public static GameStateManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameStateManager();
                }

                return instance;
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

        /// <summary>
        /// Keeps reference to Engine's GameField
        /// </summary>
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

        private GameStateManager()
        {
            this.IsGameOver = false;
            this.IsNewGame = true;
            this.IsGameOn = true;
        }
    }
}
