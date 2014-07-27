namespace Minesweeper.Controls
{
    using Contracts;
    using GameFactory;
    using Saving.Contracts;
    using Scoring;
    using Rendering.Contracts;

    public class ControlManager : ISaveControls
    {
        private Creator creator;
        private IRenderer renderer;
        private ScoreBoard scoreBoard;
        private IGameFieldSave gameFieldSave;

        public ControlManager(IRenderer renderer, ScoreBoard scoreBoard, Creator gameCreator, IGameFieldSave gameFieldSave)
        {
            this.Creator = gameCreator;
            this.Renderer = renderer;
            this.ScoreBoard = scoreBoard;
            this.GameFieldSave = gameFieldSave;
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

        public ScoreBoard ScoreBoard
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

        /// <summary>
        /// Save the current state of the game field
        /// </summary>
        public void SaveCommand()
        {
            this.GameFieldSave.SavedField = Creator.GameField.Save();
            this.Renderer.RenderSaveDone();
        }

        /// <summary>
        /// Loads the saved state of the game field
        /// </summary>
        public void RestoreSaveCommand ()
        {
            if (this.GameFieldSave.SavedField != null)
            {
                Creator.GameField.RestoreFromSave(this.GameFieldSave.SavedField);
                this.Renderer.RenderGameField();
            }
        }

        /// <summary>
        /// Executes command entered by the player
        /// </summary>
        /// <param name="executeCommand">The command entered by the player</param>
        public void ExecuteCommand (string command)
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
        public void ExitApplicationCommand ()
        {
            this.Renderer.RenderApplicationExit();
            this.Creator.IsGameOn = false;
        }

        /// <summary>
        /// Stars a new game
        /// </summary>
        public void RestartApplicationCommand ()
        {
            this.ScoreBoard.Reset();
            this.Creator.IsGameOver = false;
            this.Creator.IsNewGame = true;
        }

        /// <summary>
        /// Displays the high scores on the console
        /// </summary>
        public void DisplayScoreBoardCommand ()
        {
            this.renderer.RenderScoreBoard();
        }
    }
}
