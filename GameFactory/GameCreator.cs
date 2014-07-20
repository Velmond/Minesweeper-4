﻿namespace Minesweeper.GameFactory
{
    using System;
    using Saving;
    using Scoring;
    using Rendering;
    using Saving.Contracts;
    using Scoring.Contracts;
    using Rendering.Contracts;
    
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

        public override IRenderer CreateRenderer(ScoreBoard scoreBoard, GameField gameField)
        {
            return new Renderer(scoreBoard, gameField);
        }

        public override GameField CreateGameField()
        {
            return new GameField();
        }

        public override ScoreBoard CreateScoreBoard()
        {
            return new ScoreBoard();
        }
    }
}
