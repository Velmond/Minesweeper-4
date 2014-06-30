namespace Minesweeper
{
    using System;

    public class MinesweeperGame
    {
        static void Main()
        {
            const int ScoreToWin = GameField.FieldColumns * GameField.FieldRows - GameField.BombsCount;
            GameField gameField = new GameField();
            ScoreBoard scoreBoard = new ScoreBoard();
            string command = string.Empty;

            bool isGameOver = false;
            bool isNewGame = true;
            int currentScore = 0;
            int row = 0;
            int col = 0;
            bool isGameWon = false;

            do
            {
                if (isNewGame)
                {
                    gameField = new GameField();
                    Console.WriteLine("Welcome to the game “Minesweeper”. Try to reveal all cells without mines." +
                    " Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
                    string fieldToString = gameField.ToString();
                    Console.WriteLine(fieldToString);
                    isNewGame = false;
                }

                Console.Write("Enter row and column: ");
                command = Console.ReadLine().Trim();

                if (command.Length >= 3)
                {
                    bool rowIsValid = int.TryParse(command[0].ToString(), out row);
                    bool colIsValid = int.TryParse(command[2].ToString(), out col);
                    bool rowIsInRange = row <= gameField.Field.GetLength(0) && row >= 0;
                    bool colIsInRange = col <= gameField.Field.GetLength(1) && col >= 0;

                    if (rowIsValid && rowIsInRange && colIsValid && colIsInRange)
                    {
                        command = "turn";
                    }
                }

                switch (command)
                {
                    case "top":
                        //Console.Clear();
                        Console.WriteLine(scoreBoard.ToString());
                        break;
                    case "restart":
                        //Console.Clear();
                        isGameOver = false;
                        isNewGame = true;
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
                                currentScore++;
                            }

                            if (currentScore == ScoreToWin)
                            {
                                isGameWon = true;
                            }
                            else
                            {
                                Console.WriteLine(gameField.ToString());
                            }
                        }
                        else
                        {
                            isGameOver = true;
                        }

                        break;
                    default:
                        Console.WriteLine("\nIllegal move!\n");
                        break;
                }

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

                    isGameOver = false;
                    isNewGame = true;
                    currentScore = 0;
                    continue;
                }

                if (isGameWon)
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

                    isGameWon = false;
                    isNewGame = true;
                    currentScore = 0;
                    continue;
                }
            } while (command != "exit");

            //Console.Clear();
            Console.WriteLine("Made by Pavlin Panev 2010 - all rights reserved!");
            //Console.WriteLine("Press any key to exit.");
            //Console.Read();
        }
    }
}
