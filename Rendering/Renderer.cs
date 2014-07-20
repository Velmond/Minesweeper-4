namespace Minesweeper.Rendering
{
    using System;
    using Scoring;
    using Contracts;

    /// <summary>
    /// Implements rendering for a console application.
    /// </summary>
    public class Renderer : IRenderer
    {
        private ScoreBoard scoreBoard;
        private GameField gameField;

        public ScoreBoard ScoreBoard
        {
            get { return this.scoreBoard; }

            private set
            {
                this.scoreBoard = value;
            }
        }

        public GameField GameField
        {
            get { return this.gameField; }

            private set
            {
                this.gameField = value;
            }
        }

        public Renderer(ScoreBoard scoreBoard, GameField gameField)
        {
            this.ScoreBoard = scoreBoard;
            this.GameField = gameField;
        }

        public void RenderScoreBoard()
        {
            Console.WriteLine(this.ScoreBoard.ToString());
        }

        public void RenderGameField()
        {
            Console.WriteLine(this.GameField.ToString());
        }

        public void RenderNewGame()
        {
            Console.WriteLine("Welcome to the game “Minesweeper”. Try to reveal all cells without mines. "
                            + "Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
            this.RenderGameField();
        }

        public void RenderGameWon()
        {
            this.RenderGameField();
            Console.WriteLine("\nYou revealed all 35 cells.");
            Console.WriteLine("Please enter your name for the top scoreboard: ");
        }

        public void RenderGameOver(int currentPlayerScore)
        {
            this.RenderGameField();
            Console.Write("\nBooooom! You were killed by a mine. You revealed {0} cells without mines. " +
                          "Please enter your name for the top scoreboard: ", currentPlayerScore);
        }

        public void RenderApplicationExit()
        {
            Console.WriteLine("Good bye!");
        }

        // Messages

        public void RenderMessageInvalidCommand()
        {
            Console.WriteLine("Invalid command!");
        }

        public void RenderMessageInvalidCoordinates()
        {
            Console.WriteLine("These coordinates are outside the field.");
        }
    }
}
