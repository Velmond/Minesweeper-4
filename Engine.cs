// ********************************
// <copyright file="Engine.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper
{
    using System;

    using GameFactory;
    using Scoring;
    using Controls;

    using Controls.Contracts;
    using Rendering.Contracts;
    using Saving.Contracts;
    using Scoring.Contracts;

    /// <summary>
    /// Contains the main game logic and executes all necessary operations and commands
    /// </summary>
    public class Engine
    {
        private const int ScoreToWin = (GameField.FieldColumns * GameField.FieldRows) - GameField.BombsCount;

        private const int FieldDimensions = 2;

        private static Engine instance;

        private ScoreBoard scoreBoard;
        private Creator creator;
        private ISaveControls controlManager;
        private IGameFieldSave gameFieldSave;
        private IRenderer renderer;
        private bool isGameWon;
        private int currentScore;
        private int row;
        private int col;
        
        /// <summary>
        /// Prevents a default instance of the <see cref="Engine"/> class from being created from outside
        /// </summary>
        private Engine()
        {
            this.Creator = new GameCreator();

            this.GameFieldSave = this.Creator.CreateGameFieldSave();
            this.IsGameWon = false;
            this.CurrentScore = 0;
            this.ScoreBoard = this.Creator.CreateScoreBoard();
            this.Renderer = this.Creator.CreateRenderer(this.ScoreBoard, Creator.GameField);
            this.ControlManager = new ControlManager(this.Renderer, this.ScoreBoard, this.Creator, this.GameFieldSave);
        }

        public ISaveControls ControlManager
        {
            get
            {
                return this.controlManager;
            }
            set
            {
                this.controlManager = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="Engine"/> and ensures only a single instance of the <see cref="Engine"/> class can be created
        /// </summary>
        public static Engine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Engine();
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the game is won or not
        /// </summary>
        public bool IsGameWon
        {
            get
            {
                return this.isGameWon;
            }

            private set
            {
                this.isGameWon = value;
            }
        }

        /// <summary>
        /// Gets the current score of the player
        /// </summary>
        public int CurrentScore
        {
            get
            {
                return this.currentScore;
            }

            private set
            {
                this.currentScore = value;
            }
        }

        /// <summary>
        /// Gets <see cref="Scoring.ScoreBoard"/> with all the high scores for the game
        /// </summary>
        public ScoreBoard ScoreBoard
        {
            get
            {
                return this.scoreBoard;
            }

            private set
            {
                this.scoreBoard = value;
            }
        }

        /// <summary>
        /// Gets <see cref="GameFieldSave"/> class which can save the game
        /// </summary>
        public IGameFieldSave GameFieldSave
        {
            get
            {
                return this.gameFieldSave;
            }

            private set
            {
                this.gameFieldSave = value;
            }
        }

        /// <summary>
        /// Gets <see cref="Renderer"/> to display the game on the console
        /// </summary>
        public IRenderer Renderer
        {
            get { return this.renderer; }
            private set { this.renderer = value; }
        }

        /// <summary>
        /// Gets <see cref="Creator"/> which creates the objects necessary for the game
        /// </summary>
        public Creator Creator
        {
            get { return this.creator; }
            private set { this.creator = value; }
        }

        /// <summary>
        /// Starts the application and runs the game loop
        /// </summary>
        public void Start()
        {
            do
            {
                if (creator.IsNewGame)
                {
                    this.NewGame();
                }

                this.ReadCommand();
                if (this.Creator.IsGameOver)
                {
                    this.GameOver(false);
                    continue;
                }

                if (this.IsGameWon)
                {
                    this.GameOver(true);
                }
            }
            while (this.Creator.IsGameOn);
        }

        /// <summary>
        /// Finishes the current game, records the player name and score and starts a new game
        /// </summary>
        /// <param name="isVictory">Shows if the game is won or lost</param>
        private void GameOver(bool isVictory)
        {
            Creator.GameField.RevealField();
            if (isVictory)
            {
                this.Renderer.RenderGameWon();
            }
            else
            {
                this.Renderer.RenderGameOver(this.CurrentScore);
            }

            this.Creator.IsGameOver = false;
            this.IsGameWon = false;
            this.RecordResult();
            this.Renderer.RenderScoreBoard();
            this.Creator.IsNewGame = true;
            this.CurrentScore = 0;
            Console.Write("What do you want to do now? Type 'restart' to start a new game, 'top' to view the scoreboard, 'exit' to leave the game or 'restore' to return to your last save: ");
            var command = Console.ReadLine();
            this.controlManager.ExecuteCommand(command);
        }

        /// <summary>
        /// Adds the result of the player to the score board
        /// </summary>
        private void RecordResult()
        {
            string personName = Console.ReadLine();
            IScoreRecord record = this.Creator.CreateScoreRecord(personName, this.CurrentScore);
            this.ScoreBoard.AddScore(record);
        }

        /// <summary>
        /// Creates new game field and start new game
        /// </summary>
        private void NewGame()
        {
            Creator.GameField.SetNewField();

            this.Renderer.RenderNewGame();

            this.Creator.IsNewGame = false;
        }

        /// <summary>
        /// Reveals the game field cell entered by the player
        /// </summary>
        private void Reveal()
        {
            if (!Creator.GameField.Field[this.row, this.col].IsBomb)
            {
                if (Creator.GameField.Field[this.row, this.col].IsHidden)
                {
                    Creator.GameField.RevealPosition(this.row, this.col);
                    this.CurrentScore++;
                }

                if (this.CurrentScore == ScoreToWin)
                {
                    this.IsGameWon = true;
                }
                else
                {
                    this.Renderer.RenderGameField();
                }
            }
            else
            {
                this.Creator.IsGameOver = true;
            }
        }

        /// <summary>
        /// Read the input from the console and validates the input
        /// </summary>
        private void ReadCommand()
        {
            Console.Write("Enter row and column: ");
            string currentCommand = Console.ReadLine();

            while (string.IsNullOrEmpty(currentCommand))
            {
                Console.Write("Enter row and column: ");
                currentCommand = Console.ReadLine();
            }

            string[] commandElements = currentCommand.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (commandElements.Length == FieldDimensions)
            {
                bool rowIsValid = int.TryParse(commandElements[0], out this.row);
                bool colIsValid = int.TryParse(commandElements[1], out this.col);
                bool rowIsInRange = (this.row < Creator.GameField.Field.GetLength(0)) && (this.row >= 0);
                bool colIsInRange = (this.col < Creator.GameField.Field.GetLength(1)) && (this.col >= 0);

                if (rowIsValid && rowIsInRange && colIsValid && colIsInRange)
                {
                    this.Reveal();
                }
                else if (rowIsValid && colIsValid)
                {
                    this.Renderer.RenderMessageInvalidCoordinates();
                }
                else
                {
                    this.controlManager.ExecuteCommand(currentCommand);
                }
            }
            else
            {
                this.controlManager.ExecuteCommand(currentCommand);
            }
        }
    }
}
