﻿// ********************************
// <copyright file="GameStateManager.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper
{
    using System;
    using Minesweeper.Field.Contracts;

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
        private IGameField gameField;

        private GameStateManager()
        {
            this.IsGameOver = false;
            this.IsNewGame = true;
            this.IsGameOn = true;
        }

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
        public IGameField GameField
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
    }
}
