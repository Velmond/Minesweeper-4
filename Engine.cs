namespace Minesweeper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Engine
    {
        const int ScoreToWin = GameField.FieldColumns * GameField.FieldRows - GameField.BombsCount;

        private static GameField gameField;
        private static ScoreBoard scoreBoard;
        private static Engine instance;

        private string command = string.Empty;

        private bool isGameOver;
        private bool isNewGame;
        private int currentScore;
        private int row;
        private int col;
        private bool isGameWon;

        private Engine()
        {
            this.IsGameOver = false;
            this.IsNewGame = true;
            this.CurrentScore = 0;
            this.Row = 0;
            this.Col = 0;
            this.IsGameWon = false;
            this.Command = string.Empty;
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

        public void Start()
        {
            do
            {
                if (this.IsNewGame)
                {
                    gameField = new GameField();
                    Console.WriteLine("Welcome to the game “Minesweeper”. Try to reveal all cells without mines." +
                    " Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
                    string fieldToString = gameField.ToString();
                    Console.WriteLine(fieldToString);
                    this.IsNewGame = false;
                }

                Console.Write("Enter row and column: ");
                this.Command = Console.ReadLine().Trim();

                if (this.Command.Length >= 3)
                {
                    bool rowIsValid = int.TryParse(command[0].ToString(), out row);
                    bool colIsValid = int.TryParse(command[2].ToString(), out col);
                    bool rowIsInRange = row <= gameField.Field.GetLength(0) && row >= 0;
                    bool colIsInRange = col <= gameField.Field.GetLength(1) && col >= 0;

                    if (rowIsValid && rowIsInRange && colIsValid && colIsInRange)
                    {
                        this.Command = "turn";
                    }
                }

                switch (this.Command)
                {
                    case "top":
                        //Console.Clear();
                        Console.WriteLine(scoreBoard.ToString());
                        break;
                    case "restart":
                        //Console.Clear();
                        this.IsGameOver = false;
                        this.IsNewGame = true;
                        continue;
                    case "exit":
                        Console.WriteLine("Good bye!");
                        break;
                    case "turn":
                        if (!gameField.Field[row, col].IsBomb)
                        {
                            //Console.Clear();

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

                        break;
                    default:
                        Console.WriteLine("\nIllegal move!\n");
                        break;
                }

                scoreBoard = new ScoreBoard();

                if (isGameOver)
                {

                    //Console.Clear();
                    gameField.RevealField();
                    Console.WriteLine(gameField.ToString());
                    Console.Write("\nBooooom! You were killed by a mine. You revealed {0} cells without mines." +
                        "Please enter your name for the top scoreboard: ", currentScore);

                    string personName = Console.ReadLine();
                    ScoreRecord record = new ScoreRecord(personName, currentScore);
                    scoreBoard.AddScore(record);
                    //Console.Clear();
                    Console.WriteLine(scoreBoard.ToString());

                    this.IsGameOver = false;
                    this.IsNewGame = true;
                    this.CurrentScore = 0;
                    continue;
                }

                if (this.IsGameWon)
                {
                    //Console.Clear();
                    gameField.RevealField();
                    Console.WriteLine(gameField.ToString());
                    Console.WriteLine("\nYou revealed all 35 cells.");
                    Console.WriteLine("Please enter your name for the top scoreboard: ");

                    string personName = Console.ReadLine();
                    ScoreRecord record = new ScoreRecord(personName, currentScore);
                    scoreBoard.AddScore(record);
                    Console.WriteLine(scoreBoard.ToString());

                    this.IsGameWon = false;
                    this.IsNewGame = true;
                    this.CurrentScore = 0;
                    continue;
                }
            } while (this.Command != "exit");
        }
    }
}
