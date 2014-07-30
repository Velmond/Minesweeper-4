// ********************************
// <copyright file="Engine.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper
{
    using System;

    using Minesweeper.Controls;
    using Minesweeper.Controls.Contracts;
    using Minesweeper.GameFactory;
    using Minesweeper.Field;
    using Minesweeper.Field.Contracts;
    using Minesweeper.Rendering.Contracts;
    using Minesweeper.Scoring.Contracts;
    using Minesweeper.UserInput.Contracts;

    /// <summary>
    /// Contains the main game logic and executes all necessary operations and commands
    /// </summary>
    public class Engine
    {
        private const int FieldDimensions = 2;

        private static Engine instance;

        private GameStateManager gameState;
        private IScoreBoard scoreBoard;
        private Creator creator;
        private ISaveControls controlManager;
        private IGameFieldSave gameFieldSave;
        private IRenderer renderer;
        private IUserInput input;
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

            this.GameState = GameStateManager.Instance;
            this.GameState.GameField = this.Creator.CreateGameField();
            this.GameFieldSave = this.Creator.CreateGameFieldSave();
            this.IsGameWon = false;
            this.CurrentScore = 0;
            this.ScoreBoard = this.Creator.CreateScoreBoard();
            this.Renderer = this.Creator.CreateRenderer(this.ScoreBoard, this.GameState.GameField);
            this.Input = this.Creator.CreateUserInputRequester();
            this.ControlManager = new ControlManager(this.Renderer, this.ScoreBoard, this.Creator, this.GameFieldSave, this.GameState);
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

        public ISaveControls ControlManager
        {
            get
            {
                return this.controlManager;
            }

            private set
            {
                this.controlManager = value;
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
        public IScoreBoard ScoreBoard
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
        /// Encapsulates <see cref="GameState"/>
        /// </summary>
        public GameStateManager GameState
        {
            get
            {
                return this.gameState;
            }

            private set
            {
                this.gameState = value;
            }
        }

        /// <summary>
        /// Encapsulates <see cref="UserInput"/> 
        /// </summary>
        public IUserInput Input
        {
            get
            {
                return this.input;
            }

            private set
            {
                this.input = value;
            }
        }

        /// <summary>
        /// Gets <see cref="Renderer"/> to display the game on the console
        /// </summary>
        public IRenderer Renderer
        {
            get
            {
                return this.renderer;
            }

            private set
            {
                this.renderer = value;
            }
        }

        /// <summary>
        /// Gets <see cref="Creator"/> which creates the objects necessary for the game
        /// </summary>
        public Creator Creator
        {
            get
            {
                return this.creator;
            }

            private set
            {
                this.creator = value;
            }
        }

        /// <summary>
        /// Starts the application and runs the game loop
        /// </summary>
        public void Start()
        {
            do
            {
                if (this.GameState.IsNewGame)
                {
                    this.NewGame();
                }

                this.ReadCommand();

                if (this.GameState.IsGameOver)
                {
                    this.GameOver(false);
                    continue;
                }

                if (this.IsGameWon)
                {
                    this.GameOver(true);
                }
            }
            while (this.GameState.IsGameOn);
        }

        /// <summary>
        /// Finishes the current game, records the player name and score and starts a new game
        /// </summary>
        /// <param name="isVictory">Shows if the game is won or lost</param>
        private void GameOver(bool isVictory)
        {
            this.GameState.GameField.RevealField();

            if (isVictory)
            {
                this.Renderer.RenderGameWon();
            }
            else
            {
                this.Renderer.RenderGameOver(this.CurrentScore);
            }

            this.GameState.IsGameOver = false;
            this.IsGameWon = false;
            this.RecordResult();
            this.Renderer.RenderScoreBoard();
            this.GameState.IsNewGame = true;
            this.CurrentScore = 0;
            this.Renderer.RenderCommandRequest();
            var command = this.Input.RequestCommand();
            this.controlManager.ExecuteCommand(command);
        }

        /// <summary>
        /// Adds the result of the player to the score board
        /// </summary>
        private void RecordResult()
        {
            string personName = this.Input.RequestUserName();
            IScoreRecord record = this.Creator.CreateScoreRecord(personName, this.CurrentScore);
            this.ScoreBoard.AddScore(record);
        }

        /// <summary>
        /// Creates new game field and start new game
        /// </summary>
        private void NewGame()
        {
            this.GameState.GameField.SetNewField();

            this.Renderer.RenderNewGame();

            this.GameState.IsNewGame = false;
        }

        /// <summary>
        /// Reveals the game field cell entered by the player
        /// </summary>
        private void Reveal()
        {
            if (!this.GameState.GameField.Field[this.row, this.col].IsBomb)
            {
                if (this.GameState.GameField.Field[this.row, this.col].IsHidden)
                {
                    this.GameState.GameField.RevealPosition(this.row, this.col);
                    this.CurrentScore++;
                }

                if (this.GameState.GameField.Revealed == GameField.MaxToReveal)
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
                this.GameState.IsGameOver = true;
            }
        }

        /// <summary>
        /// Read the input from the console and validates the input
        /// </summary>
        private void ReadCommand()
        {
            this.Renderer.RenderCoordinatesRequest();
            string currentCommand = this.Input.RequestCommand();

            while (string.IsNullOrEmpty(currentCommand))
            {
                this.Renderer.RenderCoordinatesRequest();
                currentCommand = this.Input.RequestCommand();
            }

            string[] commandElements = currentCommand.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (commandElements.Length == FieldDimensions)
            {
                bool rowIsValid = int.TryParse(commandElements[0], out this.row);
                bool colIsValid = int.TryParse(commandElements[1], out this.col);
                bool isInRange = this.IsInRange(this.row, this.col);

                if (rowIsValid && colIsValid && isInRange)
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

        private bool IsInRange(int row, int col)
        {
            bool rowIsInRange = row >= 0 && row < this.GameState.GameField.Field.GetLength(0);
            bool colIsInRange = col >= 0 && col < this.GameState.GameField.Field.GetLength(1);

            return rowIsInRange && colIsInRange;
        }
    }
}
