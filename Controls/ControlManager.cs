namespace Minesweeper.Controls
{
    using Minesweeper.Controls.Contracts;
    using Minesweeper.GameFactory;
    using Minesweeper.Field.Contracts;
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

            set
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

            set
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

            set
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

            set
            {
                this.renderer = value;
            }
        }

        public GameStateManager GameState
        {
            get { return this.gameState; }
            private set { this.gameState = value; }
        }

        /// <summary>
        /// Save the current state of the game field
        /// </summary>
        public void SaveCommand()
        {
            this.GameFieldSave.SavedField = this.GameState.GameField.Save();
            this.Renderer.RenderSaveDone();
        }

        /// <summary>
        /// Loads the saved state of the game field
        /// </summary>
        public void RestoreSaveCommand()
        {
            if (this.GameFieldSave.SavedField != null)
            {
                this.GameState.GameField.RestoreFromSave(this.GameFieldSave.SavedField);
                this.Renderer.RenderGameField();
            }
        }

        /// <summary>
        /// Executes command entered by the player
        /// </summary>
        /// <param name="executeCommand">The command entered by the player</param>
        public void ExecuteCommand(string command)
        {
            switch (command)
            {
                case "top":
                    this.DisplayScoreBoardCommand();
                    break;

                case "restart":
                    this.RestartApplicationCommand();
                    break;

                case "exit":
                    this.ExitApplicationCommand();
                    break;

                case "save":
                    this.SaveCommand();
                    break;

                case "restore":
                    this.RestoreSaveCommand();
                    break;

                default:
                    this.Renderer.RenderMessageInvalidCommand();
                    break;
            }
        }

        /// <summary>
        /// Renders good bye message and exits the application
        /// </summary>
        public void ExitApplicationCommand()
        {
            this.Renderer.RenderApplicationExit();
            this.GameState.IsGameOn = false;
        }

        /// <summary>
        /// Stars a new game
        /// </summary>
        public void RestartApplicationCommand()
        {
            this.ScoreBoard.Reset();
            this.GameState.IsGameOver = false;
            this.GameState.IsNewGame = true;
        }

        /// <summary>
        /// Displays the high scores on the console
        /// </summary>
        public void DisplayScoreBoardCommand()
        {
            this.renderer.RenderScoreBoard();
        }
    }
}
