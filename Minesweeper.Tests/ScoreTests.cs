namespace Minesweeper.Tests
{
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Scoring;
    using Scoring.Contracts;

    [TestClass]
    public class ScoreTests
    {
        private IScoreBoard scoreBoard;
        private IScoreRecord recordToAdd;

        [TestInitialize]
        public void Init()
        {
            this.scoreBoard = new ScoreBoard();
            this.recordToAdd = new ScoreRecord("name", 1);
        }

        [TestMethod]
        public void AddScoreEntryToEmptyScoreBoardTest()
        {
            this.scoreBoard.AddScore(this.recordToAdd);

            var expected = new List<IScoreRecord>() { this.recordToAdd };
            var actual = this.scoreBoard.HighScores;
            Assert.ReferenceEquals(expected, actual);
        }

        [TestMethod]
        public void AddScoreEntryToNonFullScoreBoardTest()
        {
            this.scoreBoard = new ScoreBoard(new List<IScoreRecord>()
            {
                new ScoreRecord("name1", 4),
                new ScoreRecord("name2", 2),
                new ScoreRecord("name3", 1)
            });

            this.recordToAdd = new ScoreRecord("name", 3);
            this.scoreBoard.AddScore(this.recordToAdd);

            var expected = new List<IScoreRecord>()
            {
                new ScoreRecord("name1", 6),
                this.recordToAdd,
                new ScoreRecord("name2", 4),
                new ScoreRecord("name3", 1)
            };

            var actual = this.scoreBoard.HighScores;

            Assert.ReferenceEquals(expected, actual);
        }

        [TestMethod]
        public void AddScoreEntryToFullScoreBoardTest()
        {
            this.scoreBoard = new ScoreBoard(new List<IScoreRecord>()
            {
                new ScoreRecord("name1", 7),
                new ScoreRecord("name2", 6),
                new ScoreRecord("name3", 4),
                new ScoreRecord("name4", 3),
                new ScoreRecord("name5", 2),
                new ScoreRecord("name6", 1)
            });

            this.recordToAdd = new ScoreRecord("name", 5);
            this.scoreBoard.AddScore(this.recordToAdd);

            var expected = new List<IScoreRecord>()
            {
                new ScoreRecord("name1", 7),
                new ScoreRecord("name2", 6),
                this.recordToAdd,
                new ScoreRecord("name3", 4),
                new ScoreRecord("name4", 3),
                new ScoreRecord("name5", 2)
            };

            var actual = this.scoreBoard.HighScores;

            Assert.ReferenceEquals(expected, actual);
        }

        [TestMethod]
        public void AddLowestScoreEntryToFullScoreBoardTest()
        {
            this.scoreBoard = new ScoreBoard(new List<IScoreRecord>()
            {
                new ScoreRecord("name1", 7),
                new ScoreRecord("name2", 6),
                new ScoreRecord("name3", 5),
                new ScoreRecord("name4", 4),
                new ScoreRecord("name5", 3),
                new ScoreRecord("name6", 2)
            });

            this.recordToAdd = new ScoreRecord("name", 1);
            this.scoreBoard.AddScore(this.recordToAdd);

            var expected = new List<IScoreRecord>()
            {
                new ScoreRecord("name1", 7),
                new ScoreRecord("name2", 6),
                new ScoreRecord("name3", 5),
                new ScoreRecord("name4", 4),
                new ScoreRecord("name5", 3),
                new ScoreRecord("name6", 2)
            };

            var actual = this.scoreBoard.HighScores;

            Assert.ReferenceEquals(expected, actual);
        }

        [TestMethod]
        public void ScoreRecordToStringTest()
        {
            IScoreRecord score = new ScoreRecord("name1", 7);

            var expected = "name1 --> 7";
            var actual = score.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToStringEmptyScoreBoardTest()
        {
            this.scoreBoard = new ScoreBoard();

            var expectedBuilder = new StringBuilder();
            expectedBuilder.AppendLine("Scoreboard:");
            expectedBuilder.AppendLine("No records to display!");

            var expected = expectedBuilder.ToString();
            var actual = this.scoreBoard.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToStringNonEmptyScoreBoardTest()
        {
            this.scoreBoard = new ScoreBoard(new List<IScoreRecord>()
            {
                new ScoreRecord("name1", 2),
                new ScoreRecord("name2", 1)
            });

            var expectedBuilder = new StringBuilder();
            expectedBuilder.AppendLine("Scoreboard:");
            expectedBuilder.AppendLine("1. name1 --> 2 cells");
            expectedBuilder.AppendLine("2. name2 --> 1 cells");
            expectedBuilder.AppendLine();

            var expected = expectedBuilder.ToString();
            var actual = this.scoreBoard.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToStringFullScoreBoardTest()
        {
            this.scoreBoard = new ScoreBoard(new List<IScoreRecord>()
            {
                new ScoreRecord("name1", 6),
                new ScoreRecord("name2", 5),
                new ScoreRecord("name3", 4),
                new ScoreRecord("name4", 3),
                new ScoreRecord("name5", 2),
                new ScoreRecord("name6", 1)
            });

            var expectedBuilder = new StringBuilder();
            expectedBuilder.AppendLine("Scoreboard:");
            expectedBuilder.AppendLine("1. name1 --> 6 cells");
            expectedBuilder.AppendLine("2. name2 --> 5 cells");
            expectedBuilder.AppendLine("3. name3 --> 4 cells");
            expectedBuilder.AppendLine("4. name4 --> 3 cells");
            expectedBuilder.AppendLine("5. name5 --> 2 cells");
            expectedBuilder.AppendLine("6. name6 --> 1 cells");
            expectedBuilder.AppendLine();

            var expected = expectedBuilder.ToString();
            var actual = this.scoreBoard.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ResetTest()
        {
            this.scoreBoard = new ScoreBoard(new List<IScoreRecord>()
            {
                new ScoreRecord("name1", 7),
                new ScoreRecord("name2", 6),
                new ScoreRecord("name3", 5),
                new ScoreRecord("name4", 4),
                new ScoreRecord("name5", 3),
                new ScoreRecord("name6", 2)
            });

            this.scoreBoard.Reset();

            var expected = new List<IScoreRecord>();
            var actual = this.scoreBoard.HighScores;
            Assert.ReferenceEquals(expected, actual);
        }
    }
}
