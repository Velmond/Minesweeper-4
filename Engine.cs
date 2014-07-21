namespace Minesweeper
{
    using System;
    using Scoring;
    using Saving.Contracts;
    using Scoring.Contracts;
    using Rendering.Contracts;
    using GameFactory;

    public class Engine
    {
        private readonly int scoreToWin = GameField.FieldColumns * GameField.FieldRows - GameField.BombsCount;

        private const int FieldDimensions = 2;

        private static Engine instance;

        private GameField gameField;
        private ScoreBoard scoreBoard;
        private Creator creator;
        private IGameFieldSave gameFieldSave;
        private IRenderer renderer;
        //private string command;
        private bool isGameOver;
        private bool isNewGame;
        private bool isGameOn;
        private bool isGameWon;
        private int currentScore;
        private int row;
        private int col;
        
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

        public IRenderer Renderer
        {
            get { return this.renderer; }
            private set { this.renderer = value; }
        }

        public Creator Creator
        {
            get { return this.creator; }
            private set { this.creator = value; }
        }
        
        private void DisplayScoreBoardCommand()
        {
            this.renderer.RenderScoreBoard();
        }

        private void RestartGameCommand()
        {
            this.ScoreBoard.Reset();
            this.IsGameOver = false;
            this.IsNewGame = true;
        }

        private void ExitApplicationCommand()
        {
            this.Renderer.RenderApplicationExit();
        }

        private void SaveCommand()
        {
            this.GameFieldSave.SavedField = this.GameField.Save();
        }

        private void RestoreCommand()
        {
            if (this.GameFieldSave.SavedField != null)
            {
                this.GameField.RestoreFromSave(this.GameFieldSave.SavedField);
                this.Renderer.RenderGameField();
            }
        }

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

        private void RecordResult()
        {
            string personName = Console.ReadLine();
            IScoreRecord record = this.Creator.CreateScoreRecord(personName, this.CurrentScore);
            this.ScoreBoard.AddScore(record);
        }

        private void NewGame()
        {
            this.GameField.SetNewField();

            this.Renderer.RenderNewGame();

            this.IsNewGame = false;
        }

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
                    this.isGameOn = false;
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

        private void Reveal()
        {
            if (!this.GameField.Field[this.row, this.col].IsBomb)
            {
                if (this.GameField.Field[this.row, this.col].IsHidden)
                {
                    this.GameField.RevealPosition(this.row, this.col);
                    this.CurrentScore++;
                }

                if (this.CurrentScore == scoreToWin)
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

        private void ReadCommand()
        {
            Console.Write("Enter row and columnz: ");
            string currentCommand = Console.ReadLine();

            while (string.IsNullOrEmpty(currentCommand))
            {
                Console.Write("Enter row and columnz: ");
                currentCommand = Console.ReadLine();
            }

            string[] commandElements = currentCommand.Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);

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
    }
}
