namespace Minesweeper.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Text;

    [TestClass]
    public class GameFieldTests
    {
        [TestMethod]
        public void RevealPositionTest()
        {
            GameField gameField = new GameField();

            for (int i = 0; i < GameField.FieldRows; i++)
            {
                gameField.RevealPosition(i, i);
            }

            var expectedResult = true;

            for (int row = 0; row < GameField.FieldRows; row++)
            {
                for (int col = 0; col < GameField.FieldColumns; col++)
                {
                    var isHidden = this.IsPositionHidden(gameField.Field[row, col]);

                    if (row != col)
                    {
                        expectedResult = expectedResult && isHidden;
                    }
                    else
                    {
                        expectedResult = expectedResult && !isHidden;
                    }
                }
            }

            Assert.IsTrue(expectedResult);
        }

        [TestMethod]
        public void RevealFieldTest()
        {
            GameField gameField = new GameField();
            // uses RevealPosition(...) which is already tested
            gameField.RevealField();

            var expectedResult = true;

            for (int row = 0; row < GameField.FieldRows; row++)
            {
                for (int col = 0; col < GameField.FieldColumns; col++)
                {
                    var isHidden = this.IsPositionHidden(gameField.Field[row, col]);
                    expectedResult = expectedResult && !isHidden;
                }
            }

            Assert.IsTrue(expectedResult);
        }

        [TestMethod]
        public void NewFieldTest()
        {
            GameField gameField = new GameField();
            // already tested -> no position in the field is left hidden
            gameField.RevealField();
            // should reset all positions and should now be hidden
            gameField.SetNewField();

            var expectedResult = true;

            for (int row = 0; row < GameField.FieldRows; row++)
            {
                for (int col = 0; col < GameField.FieldColumns; col++)
                {
                    var isHidden = this.IsPositionHidden(gameField.Field[row, col]);
                    expectedResult = expectedResult && isHidden;
                }
            }

            Assert.IsTrue(expectedResult);
        }

        [TestMethod]
        public void SaveAndRestoreToHiddenTest()
        {
            GameField gameField = new GameField();
            var memento = gameField.Save();
            //already tested
            gameField.RevealField();
            gameField.RestoreFromSave(memento);
            var expectedResult = true;

            for (int row = 0; row < GameField.FieldRows; row++)
            {
                for (int col = 0; col < GameField.FieldColumns; col++)
                {
                    var isHidden = this.IsPositionHidden(gameField.Field[row, col]);
                    expectedResult = expectedResult && isHidden;
                }
            }

            Assert.IsTrue(expectedResult);
        }

        [TestMethod]
        public void SaveAndRestoreToRevealedTest()
        {
            GameField gameField = new GameField();
            //already tested
            gameField.RevealField();
            var memento = gameField.Save();
            gameField.SetNewField();
            gameField.RestoreFromSave(memento);
            var expectedResult = true;

            for (int row = 0; row < GameField.FieldRows; row++)
            {
                for (int col = 0; col < GameField.FieldColumns; col++)
                {
                    var isHidden = this.IsPositionHidden(gameField.Field[row, col]);
                    expectedResult = expectedResult && !isHidden;
                }
            }

            Assert.IsTrue(expectedResult);
        }

        [TestMethod]
        public void ToStringNonRevealedTest()
        {
            GameField gameField = new GameField();
            var expectedBuilder = new StringBuilder();
            expectedBuilder.AppendLine();
            expectedBuilder.AppendLine("    0 1 2 3 4 5 6 7 8 9");
            expectedBuilder.AppendLine("   ---------------------");

            for (int i = 0; i < GameField.FieldRows; i++)
            {
                expectedBuilder.AppendLine(string.Format("{0} | ? ? ? ? ? ? ? ? ? ? |", i));
            }

            expectedBuilder.AppendLine("   ---------------------");
            expectedBuilder.AppendLine();

            var expected = expectedBuilder.ToString();
            var actual = gameField.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToStringRevealedTest()
        {
            GameField gameField = new GameField();
            // already tested
            gameField.RevealField();
            char[,] fieldChars = new char[GameField.FieldRows, GameField.FieldColumns];

            for (int row = 0; row < GameField.FieldRows; row++)
            {
                for (int col = 0; col < GameField.FieldColumns; col++)
                {
                    // Just in case check if the value has not been changed and is still set to '?'
                    if (gameField.Field[row, col].Value == '?')
                    {
                        throw new Exception("Position should be revealed with value different than '?'.");
                    }

                    fieldChars[row, col] = gameField.Field[row, col].Value;
                }
            }

            var expectedBuilder = new StringBuilder();
            expectedBuilder.AppendLine();
            expectedBuilder.AppendLine("    0 1 2 3 4 5 6 7 8 9");
            expectedBuilder.AppendLine("   ---------------------");

            for (int row = 0; row < GameField.FieldRows; row++)
            {
                expectedBuilder.Append(string.Format("{0} | ", row));

                for (int col = 0; col < GameField.FieldColumns; col++)
                {
                    expectedBuilder.Append(string.Format("{0} ", fieldChars[row, col]));
                }

                expectedBuilder.Append("|");
                expectedBuilder.AppendLine();
            }

            expectedBuilder.AppendLine("   ---------------------");
            expectedBuilder.AppendLine();

            var expected = expectedBuilder.ToString();
            var actual = gameField.ToString();

            Assert.AreEqual(expected, actual);
        }

        private bool IsPositionHidden(Position position)
        {
            if (position.IsHidden && position.Value == '?')
            {
                return true;
            }
            else if (!position.IsHidden && position.Value != '?')
            {
                return false;
            }
            else
            {
                throw new ArgumentException("Position can either be hidden with value '?', or not hidden with value anything other than '?'.");
            }
        }
    }
}
