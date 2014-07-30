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
    using Minesweeper.Scoring.Contracts;
    using Minesweeper.Field.Contracts;

    /// <summary>
    /// Implements rendering for a console application.
    /// </summary>
    public class Renderer : IRenderer
    {
        private IScoreBoard scoreBoard;
        private IGameField gameField;

        /// <summary>
        /// Initializes a new instance of the <see cref="Renderer"/> class with given score board and game field
        /// </summary>
        /// <param name="scoreBoard">Instance of <see cref="ScoreBoard"/> which will be rendered</param>
        /// <param name="gameField">Instance of <see cref="GameField"/> which will be rendered</param>
        public Renderer(IScoreBoard scoreBoard, IGameField gameField)
        {
            this.ScoreBoard = scoreBoard;
            this.GameField = gameField;
        }

        /// <summary>
        /// Gets the value of the <see cref="ScoreBoard"/> which will be rendered
        /// </summary>
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

        /// <summary>
        /// Gets the value of the <see cref="GameField"/> which will be rendered
        /// </summary>
        public IGameField GameField
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
            Console.Clear();
            this.WriteInstructions();
            Console.WriteLine(this.GameField.ToString());
        }

        /// <summary>
        /// Displays welcome message and new game field
        /// </summary>
        public void RenderNewGame()
        {
            this.WriteInstructions();
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
            Console.Write(
                "\nBooooom! You were killed by a mine. You revealed {0} cells without mines." +
                " Please enter your name for the top scoreboard: ",
                currentPlayerScore);
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

        /// <summary>
        /// Displays a message when the <see cref="Engine"/> class calls its SaveCommand() method.
        /// </summary>
        public void RenderSaveDone()
        {
            Console.WriteLine("Your game is saved, you can restore it at any time by typing 'restore'.");
        }

        public void RenderCoordinatesRequest()
        {
            Console.Write("Enter row and column: ");
        }

        public void RenderCommandRequest()
        {
            Console.Write("What do you want to do now? Type 'restart' to start a new game, 'top' to view the scoreboard, 'exit' to leave the game or 'restore' to return to your last save: ");
        }

        private void WriteInstructions()
        {
            Console.WriteLine("Welcome to the game “Minesweeper”. Try to reveal all cells without mines. "
                            + "Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game. Type 'save' in order to save your game and 'restore' if you want to load a previous game.");
        }
    }
}
