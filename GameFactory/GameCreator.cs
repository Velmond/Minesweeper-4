// ********************************
// <copyright file="GameCreator.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper.GameFactory
{
    using Minesweeper.Contracts;
    using Rendering;
    using Rendering.Contracts;
    using Saving;
    using Saving.Contracts;
    using Scoring;
    using Scoring.Contracts;
    using UserInput;
    using UserInput.Contracts;
    
    /// <summary>
    /// Implements Creator with concrete types for instantiating.
    /// </summary>
    public class GameCreator : Creator
    {
        public override IGameFieldSave CreateGameFieldSave()
        {
            return new GameFieldSave();
        }

        public override IScoreRecord CreateScoreRecord(string name, int score)
        {
            return new ScoreRecord(name, score);
        }

        public override IRenderer CreateRenderer(IScoreBoard scoreBoard, IGameField gameField)
        {
            return new Renderer(scoreBoard, gameField);
        }

        public override IUserInput CreateUserInputRequester()
        {
            return new ConsoleInput();
        }

        public override IGameField CreateGameField()
        {
            return new GameField();
        }

        public override IScoreBoard CreateScoreBoard()
        {
            return new ScoreBoard();
        }
    }
}
