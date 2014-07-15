namespace Minesweeper
{
    using System;

    public class Engine
    {
        private readonly int ScoreToWin = GameField.FieldColumns * GameField.FieldRows - GameField.BombsCount;

        private static Engine instance;

        private GameField gameField;
        private GameFieldSave gameFieldSave;
        private string command;
        private bool isGameOver;
        private bool isNewGame;
        private bool isGameWon;
        private int currentScore;
        private int row;
        private int col;
        private ScoreBoard scoreBoard;

        private Engine()
        {
            this.GameFieldSave = new GameFieldSave();
            this.Command = string.Empty;
            this.IsGameOver = false;
            this.IsNewGame = true;
            this.IsGameWon = false;
            this.CurrentScore = 0;
            this.ScoreBoard = new ScoreBoard();
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

        public string Command
        {
            get
            {
                return this.command;
            }

            private set
            {
                this.command = value;
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

        public GameFieldSave GameFieldSave
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
        
        private void DisplayScoreBoardCommand()
        {
            Console.WriteLine(this.ScoreBoard.ToString());
        }

        private void RestartGameCommand()
        {
            this.ScoreBoard.Reset();
            this.IsGameOver = false;
            this.IsNewGame = true;
        }

        private void ExitApplicationCommand()
        {
            Console.WriteLine("Good bye!");
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
            }
        }

        private void GameOver()
        {
            this.GameField.RevealField();
            Console.WriteLine(this.GameField.ToString());
            Console.Write("\nBooooom! You were killed by a mine. You revealed {0} cells without mines." +
                " Please enter your name for the top scoreboard: ", this.CurrentScore);

            string personName = Console.ReadLine();
            ScoreRecord record = new ScoreRecord(personName, this.CurrentScore);
            this.ScoreBoard.AddScore(record);
            Console.WriteLine(this.ScoreBoard.ToString());

            this.IsGameOver = false;
            this.IsNewGame = true;
            this.CurrentScore = 0;
        }

        private void GameWon()
        {
            this.GameField.RevealField();
            Console.WriteLine(this.GameField.ToString());
            Console.WriteLine("\nYou revealed all 35 cells.");
            Console.WriteLine("Please enter your name for the top scoreboard: ");

            string personName = Console.ReadLine();
            IScoreRecord record = new ScoreRecord(personName, this.CurrentScore);
            this.ScoreBoard.AddScore(record);
            Console.WriteLine(this.ScoreBoard.ToString());

            this.IsGameWon = false;
            this.IsNewGame = true;
            this.CurrentScore = 0;
        }

        private void NewGame()
        {
            this.GameField = new GameField();
            Console.WriteLine("Welcome to the game “Minesweeper”. Try to reveal all cells without mines." +
                " Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
            Console.WriteLine(this.GameField.ToString());
            this.IsNewGame = false;
        }

        private void Reveal()
        {
            bool rowIsValid = int.TryParse(this.Command[0].ToString(), out this.row);
            bool colIsValid = int.TryParse(this.Command[2].ToString(), out this.col);
            bool rowIsInRange = (this.row < this.GameField.Field.GetLength(0)) && (this.row >= 0);
            bool colIsInRange = (this.col < this.GameField.Field.GetLength(1)) && (this.col >= 0);

            if (rowIsValid && rowIsInRange && colIsValid && colIsInRange)
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
                        Console.WriteLine(this.GameField.ToString());
                    }
                }
                else
                {
                    this.IsGameOver = true;
                }
            }
            else if (rowIsValid && colIsValid)
            {
                Console.WriteLine("These coordinates are outside the filed");
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

                Console.Write("Enter row and column: ");
                this.Command = Console.ReadLine().Trim();

                if (this.Command.Length >= 3)
                {
                    this.Reveal();
                }

                switch (this.Command)
                {
                    case "top":
                        this.DisplayScoreBoardCommand();
                        break;

                    case "restart":
                        this.RestartGameCommand();
                        continue;

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
                        break;
                }

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
            while (this.Command != "exit");
        }
    }
}
