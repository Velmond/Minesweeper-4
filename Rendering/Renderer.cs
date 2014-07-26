// ********************************
// <copyright file="Renderer.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper.Rendering
{
    using System;
    using Contracts;
    using Scoring;

    /// <summary>
    /// Implements rendering for a console application.
    /// </summary>
    public class Renderer : IRenderer
    {
        private ScoreBoard scoreBoard;
        private GameField gameField;

        /// <summary>
        /// Initializes a new instance of the <see cref="Renderer"/> class with given score board and game field
        /// </summary>
        /// <param name="scoreBoard">Instance of <see cref="ScoreBoard"/> which will be rendered</param>
        /// <param name="gameField">Instance of <see cref="GameField"/> which will be rendered</param>
        public Renderer(ScoreBoard scoreBoard, GameField gameField)
        {
            this.ScoreBoard = scoreBoard;
            this.GameField = gameField;
        }

        /// <summary>
        /// Gets the value of the <see cref="ScoreBoard"/> which will be rendered
        /// </summary>
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

        /// <summary>
        /// Gets the value of the <see cref="GameField"/> which will be rendered
        /// </summary>
        public GameField GameField
        {
            get { return this.gameField; }

            private set
            {
                this.gameField = value;
            }
        }

        /// <summary>
        /// Displays the full score board to the player
        /// </summary>
        public void RenderScoreBoard()
        {
            Console.WriteLine(this.ScoreBoard.ToString());
        }
        
        /// <summary>
        /// Displays the whole game field to te player
        /// </summary>
        public void RenderGameField()
        {
            Console.WriteLine(this.GameField.ToString());
        }

        /// <summary>
        /// Displays welcome message and new game field
        /// </summary>
        public void RenderNewGame()
        {
            Console.WriteLine("Welcome to the game “Minesweeper”. Try to reveal all cells without mines. "
                            + "Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
            this.RenderGameField();
        }

        /// <summary>
        /// Displays the whole game field and congratulation message
        /// </summary>
        public void RenderGameWon()
        {
            this.RenderGameField();
            Console.WriteLine("\nYou revealed all 35 cells.");
            Console.WriteLine("Please enter your name for the top scoreboard: ");
        }

        /// <summary>
        /// Displays the whole game field, losing message and the score
        /// </summary>
        /// <param name="currentPlayerScore">Integer equal to the number of cells visited by the player</param>
        public void RenderGameOver(int currentPlayerScore)
        {
            this.RenderGameField();
            Console.Write("\nBooooom! You were killed by a mine. You revealed {0} cells without mines. " +
                          "Please enter your name for the top scoreboard: ", currentPlayerScore);
        }

        /// <summary>
        /// Displays good bye message
        /// </summary>
        public void RenderApplicationExit()
        {
            Console.WriteLine("Good bye!");
        }

        /// <summary>
        /// Displays message showing the entered command is invalid
        /// </summary>
        public void RenderMessageInvalidCommand()
        {
            Console.WriteLine("Invalid command!");
        }

        /// <summary>
        /// Displays message when the instance of the <see cref="GameField"/> class does not contain the given coordinates
        /// </summary>
        public void RenderMessageInvalidCoordinates()
        {
            Console.WriteLine("These coordinates are outside the field.");
        }
    }
}
