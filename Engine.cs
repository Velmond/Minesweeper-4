namespace Minesweeper
{
    using System;
    using System.Linq;

    public class Engine
    {
        private readonly int ScoreToWin = GameField.FieldColumns * GameField.FieldRows - GameField.BombsCount;

        private static GameField gameField;
        private static Engine instance;

        private string command = String.Empty;

        private bool isGameOver;
        private bool isNewGame;
        private int currentScore;
        private int row;
        private int col;
        private bool isGameWon;
        private ScoreBoard scoreBoard;

        private Engine()
        {
            this.IsGameOver = false;
            this.IsNewGame = true;
            this.CurrentScore = 0;
            this.Row = 0;
            this.Col = 0;
            this.IsGameWon = false;
            this.Command = string.Empty;
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

        public int Col
        {
            get
            {
                return this.col;
            }

            private set
            {
                this.col = value;
            }
        }

        public int Row
        {
            get
            {
                return this.row;
            }

            private set
            {
                this.row = value;
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

        private void GameOver()
        {
            //Console.Clear();
            gameField.RevealField();
            Console.WriteLine(gameField.ToString());
            Console.Write("\nBooooom! You were killed by a mine. You revealed {0} cells without mines. Please enter your name for the top scoreboard: ", currentScore);

            string personName = Console.ReadLine();
            ScoreRecord record = new ScoreRecord(personName, currentScore);
            this.ScoreBoard.AddScore(record);
            //Console.Clear();
            Console.WriteLine(this.ScoreBoard.ToString());

            this.IsGameOver = false;
            this.IsNewGame = true;
            this.CurrentScore = 0;
        }

        private void GameWon()
        {
            // Console.Clear();
            gameField.RevealField();
            Console.WriteLine(gameField.ToString());
            Console.WriteLine("\nYou revealed all 35 cells.");
            Console.WriteLine("Please enter your name for the top scoreboard: ");

            string personName = Console.ReadLine();
            IScoreRecord record = new ScoreRecord(personName, this.currentScore);
            this.ScoreBoard.AddScore(record);
            Console.WriteLine(this.ScoreBoard.ToString());

            this.IsGameWon = false;
            this.IsNewGame = true;
            this.CurrentScore = 0;
        }

        private void NewGame()
        {
            gameField = new GameField();
            Console.WriteLine("Welcome to the game “Minesweeper”. Try to reveal all cells without mines. Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
            string fieldToString = gameField.ToString();
            Console.WriteLine(fieldToString);
            this.IsNewGame = false;
        }

        private void Reveal()
        {
            bool rowIsValid = int.TryParse(command[0].ToString(), out this.row);
            bool colIsValid = int.TryParse(command[2].ToString(), out this.col);
            bool rowIsInRange = (this.row < gameField.Field.GetLength(0)) && (this.row >= 0);
            bool colIsInRange = (this.col < gameField.Field.GetLength(1)) && (this.col >= 0);

            if (rowIsValid && rowIsInRange && colIsValid && colIsInRange)
            {
                if (!gameField.Field[this.row, this.col].IsBomb)
                {
                    // Console.Clear();
                    if (gameField.Field[row, col].IsHidden)
                    {
                        gameField.RevealPosition(row, col);
                        this.CurrentScore++;
                    }

                    if (this.CurrentScore == ScoreToWin)
                    {
                        this.IsGameWon = true;
                    }
                    else
                    {
                        Console.WriteLine(gameField.ToString());
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

                    default:
                        break;
                }

                if (this.isGameOver)
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
