﻿namespace Minesweeper.Controls
{
    using Minesweeper.Controls.Commands;
    using Minesweeper.Controls.Contracts;
    using Minesweeper.Field.Contracts;
    using Minesweeper.GameFactory;
    using Minesweeper.Rendering.Contracts;
    using Minesweeper.Scoring.Contracts;

    public class ControlManager : ISaveControls
    {
        private Creator creator;
        private IRenderer renderer;
        private IScoreBoard scoreBoard;
        private IGameFieldSave gameFieldSave;
        private GameStateManager gameState;

        public ControlManager(IRenderer renderer, IScoreBoard scoreBoard, Creator gameCreator, IGameFieldSave gameFieldSave, GameStateManager gameState)
        {
            this.Creator = gameCreator;
            this.Renderer = renderer;
            this.ScoreBoard = scoreBoard;
            this.GameFieldSave = gameFieldSave;
            this.GameState = gameState;
        }

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
        /// Save the current state of the game field
        /// </summary>
        public void SaveCommand()
        {
            this.GameFieldSave.SavedField = this.GameState.GameField.Save();
        }

        /// <summary>
        /// Loads the saved state of the game field
        /// </summary>
        public void RestoreSaveCommand()
        {
            if (this.GameFieldSave.SavedField != null)
            {
                this.GameState.GameField.RestoreFromSave(this.GameFieldSave.SavedField);
            }
        }

        /// <summary>
        /// Executes command entered by the player
        /// </summary>
        /// <param name="executeCommand">The command entered by the player</param>
        public void ExecuteCommand(string command)
        {
            IRenderCommand renderCommand;
            switch (command)
            {
                case "top":
                    renderCommand = new RenderScoreBoardCommand(this.Renderer);
                    break;

                case "restart":
                    this.RestartApplicationCommand();
                    renderCommand = new RenderScoreBoardCommand(this.Renderer);
                    break;

                case "exit":
                    this.ExitApplicationCommand();
                    renderCommand = new RenderExitApplicationCommand(this.Renderer);
                    break;

                case "save":
                    this.SaveCommand();
                    renderCommand = new RenderSaveCommand(this.Renderer);
                    break;

                case "restore":
                    this.RestoreSaveCommand();
                    renderCommand = new RenderRestoreSaveCommand(this.Renderer);
                    break;

                default:
                    renderCommand = new RenderMessageInvalidCommand(this.Renderer);
                    break;
            }

            renderCommand.Execute();
        }

        /// <summary>
        /// Renders good bye message and exits the application
        /// </summary>
        public void ExitApplicationCommand()
        {
            this.GameState.IsGameOn = false;
        }

        /// <summary>
        /// Stars a new game
        /// </summary>
        public void RestartApplicationCommand()
        {
            ////this.ScoreBoard.Reset();
            this.GameState.IsGameOver = false;
            this.GameState.IsNewGame = true;
        }
    }
}
