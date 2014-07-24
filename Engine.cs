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
    using Rendering.Contracts;
    using Saving.Contracts;
    using Scoring;
    using Scoring.Contracts;

    /// <summary>
    /// Contains the main game logic and executes all necessary operations and commands
    /// </summary>
    public class Engine
    {
        private const int ScoreToWin = (GameField.FieldColumns * GameField.FieldRows) - GameField.BombsCount;

        private const int FieldDimensions = 2;

        private static Engine instance;

        private GameField gameField;
        private ScoreBoard scoreBoard;
        private Creator creator;
        private IGameFieldSave gameFieldSave;
        private IRenderer renderer;
        private bool isGameOver;
        private bool isNewGame;
        private bool isGameOn;
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
            this.IsGameOver = false;
            this.IsNewGame = true;
            this.IsGameWon = false;
            this.CurrentScore = 0;
            this.GameField = this.Creator.CreateGameField();
            this.ScoreBoard = this.Creator.CreateScoreBoard();
            this.isGameOn = true;
            this.Renderer = this.Creator.CreateRenderer(this.ScoreBoard, this.GameField);
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
        /// Gets the current <see cref="GameField"/>
        /// </summary>
        public GameField GameField
        {
            get
            {
                return this.gameField;
            }

            private set
            {
                this.gameField = value;
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
        /// Gets a value indicating whether new game must be started or not
        /// </summary>
        public bool IsNewGame
        {
            get
            {
                return this.isNewGame;
            }

            private set
            {
                this.isNewGame = value;
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

            private set
            {
                this.isGameOver = value;
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
                if (this.IsNewGame)
                {
                    this.NewGame();
                }

                this.ReadCommand();
                if (this.IsGameOver)
                {
                    this.GameOver();
                    continue;
                }

                if (this.IsGameWon)
                {
                    this.GameWon();
                    continue;
                }
            }
            while (this.isGameOn);
        }

        /// <summary>
        /// Displays the high scores on the console
        /// </summary>
        private void DisplayScoreBoardCommand()
        {
            this.renderer.RenderScoreBoard();
        }

        /// <summary>
        /// Stars a new game
        /// </summary>
        private void RestartGameCommand()
        {
            this.ScoreBoard.Reset();
            this.IsGameOver = false;
            this.IsNewGame = true;
        }

        /// <summary>
        /// Renders good bye message and exits the application
        /// </summary>
        private void ExitApplicationCommand()
        {
            this.Renderer.RenderApplicationExit();
            this.isGameOn = false;
        }

        /// <summary>
        /// Save the current state of the game field
        /// </summary>
        private void SaveCommand()
        {
            this.GameFieldSave.SavedField = this.GameField.Save();
        }

        /// <summary>
        /// Loads the saved state of the game field
        /// </summary>
        private void RestoreCommand()
        {
            if (this.GameFieldSave.SavedField != null)
            {
                this.GameField.RestoreFromSave(this.GameFieldSave.SavedField);
                this.Renderer.RenderGameField();
            }
        }

        /// <summary>
        /// Finishes the current game, records the player name and starts a new game
        /// </summary>
        private void GameOver()
        {
            this.GameField.RevealField();
            this.Renderer.RenderGameOver(this.CurrentScore);
            this.RecordResult();
            this.Renderer.RenderScoreBoard();
            this.IsGameOver = false;
            this.IsNewGame = true;
            this.CurrentScore = 0;
        }

        /// <summary>
        /// Displays winning message, records the player name and starts a new game
        /// </summary>
        private void GameWon()
        {
            this.GameField.RevealField();
            this.Renderer.RenderGameWon();
            this.RecordResult();
            this.Renderer.RenderScoreBoard();
            this.IsGameWon = false;
            this.IsNewGame = true;
            this.CurrentScore = 0;
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
            this.GameField.SetNewField();

            this.Renderer.RenderNewGame();

            this.IsNewGame = false;
        }

        /// <summary>
        /// Executes command entered by the player
        /// </summary>
        /// <param name="executeCommand">The command entered by the player</param>
        private void ExecuteCommand(string executeCommand)
        {
            switch (executeCommand)
            {
                case "top":
                    this.DisplayScoreBoardCommand();
                    break;

                case "restart":
                    this.RestartGameCommand();
                    break;

                case "exit":
                    this.ExitApplicationCommand();
                    break;

                case "save":
                    this.SaveCommand();
                    break;

                case "restore":
                    this.RestoreCommand();
                    break;

                default:
                    this.Renderer.RenderMessageInvalidCommand();
                    break;
            }
        }

        /// <summary>
        /// Reveals the game field cell entered by the player
        /// </summary>
        private void Reveal()
        {
            if (!this.GameField.Field[this.row, this.col].IsBomb)
            {
                if (this.GameField.Field[this.row, this.col].IsHidden)
                {
                    this.GameField.RevealPosition(this.row, this.col);
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
                this.IsGameOver = true;
            }
        }

        /// <summary>
        /// Read the input from the console and validates the input
        /// </summary>
        private void ReadCommand()
        {
            Console.Write("Enter row and columnz: ");
            string currentCommand = Console.ReadLine();

            while (string.IsNullOrEmpty(currentCommand))
            {
                Console.Write("Enter row and columnz: ");
                currentCommand = Console.ReadLine();
            }

            string[] commandElements = currentCommand.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (commandElements.Length == FieldDimensions)
            {
                bool rowIsValid = int.TryParse(commandElements[0], out this.row);
                bool colIsValid = int.TryParse(commandElements[1], out this.col);
                bool rowIsInRange = (this.row < this.GameField.Field.GetLength(0)) && (this.row >= 0);
                bool colIsInRange = (this.col < this.GameField.Field.GetLength(1)) && (this.col >= 0);

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
                    this.ExecuteCommand(currentCommand);
                }
            }
            else
            {
                this.ExecuteCommand(currentCommand);
            }
        }
    }
}
