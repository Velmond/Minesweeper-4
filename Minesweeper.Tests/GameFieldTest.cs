namespace Minesweeper.Tests
{
    using System;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Minesweeper.Field;
    using Minesweeper.Field.Contracts;

    [TestClass]
    public class GameFieldTest
    {
        private IGameField gameField;

        [TestInitialize]
        public void Init()
        {
            this.gameField = new GameField();
        }

        [TestMethod]
        public void RevealPositionTest()
        {
            for (int i = 0; i < GameField.FieldRows; i++)
            {
                this.gameField.RevealPosition(i, i);
            }

            var expectedResult = true;

            for (int row = 0; row < GameField.FieldRows; row++)
            {
                for (int col = 0; col < GameField.FieldColumns; col++)
                {
                    bool isHidden = this.IsPositionHidden(this.gameField.Field[row, col]);

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
            this.gameField.RevealField();

            var expectedResult = true;

            for (int row = 0; row < GameField.FieldRows; row++)
            {
                for (int col = 0; col < GameField.FieldColumns; col++)
                {
                    bool isHidden = this.IsPositionHidden(this.gameField.Field[row, col]);
                    expectedResult = expectedResult && !isHidden;
                }
            }

            Assert.IsTrue(expectedResult);
        }

        [TestMethod]
        public void NewFieldTest()
        {
            this.gameField.RevealField();
            this.gameField.SetNewField();

            var expectedResult = true;

            for (int row = 0; row < GameField.FieldRows; row++)
            {
                for (int col = 0; col < GameField.FieldColumns; col++)
                {
                    bool isHidden = this.IsPositionHidden(this.gameField.Field[row, col]);
                    expectedResult = expectedResult && isHidden;
                }
            }

            Assert.IsTrue(expectedResult);
        }

        [TestMethod]
        public void SaveAndRestoreToHiddenTest()
        {
            GameFieldMemento memento = this.gameField.Save();
            this.gameField.RevealField();
            this.gameField.RestoreFromSave(memento);
            bool expectedResult = true;

            for (int row = 0; row < GameField.FieldRows; row++)
            {
                for (int col = 0; col < GameField.FieldColumns; col++)
                {
                    bool isHidden = this.IsPositionHidden(this.gameField.Field[row, col]);
                    expectedResult = expectedResult && isHidden;
                }
            }

            Assert.IsTrue(expectedResult);
        }

        [TestMethod]
        public void SaveAndRestoreToRevealedTest()
        {
            this.gameField.RevealField();
            GameFieldMemento memento = this.gameField.Save();
            this.gameField.SetNewField();
            this.gameField.RestoreFromSave(memento);
            bool expectedResult = true;

            for (int row = 0; row < GameField.FieldRows; row++)
            {
                for (int col = 0; col < GameField.FieldColumns; col++)
                {
                    bool isHidden = this.IsPositionHidden(this.gameField.Field[row, col]);
                    expectedResult = expectedResult && !isHidden;
                }
            }

            Assert.IsTrue(expectedResult);
        }

        [TestMethod]
        public void ToStringNonRevealedTest()
        {
            StringBuilder expectedBuilder = new StringBuilder();
            expectedBuilder.AppendLine();
            expectedBuilder.AppendLine("    0 1 2 3 4 5 6 7 8 9");
            expectedBuilder.AppendLine("   ---------------------");

            for (int i = 0; i < GameField.FieldRows; i++)
            {
                expectedBuilder.AppendLine(string.Format("{0} | ? ? ? ? ? ? ? ? ? ? |", i));
            }

            expectedBuilder.AppendLine("   ---------------------");
            expectedBuilder.AppendLine();

            string expected = expectedBuilder.ToString();
            string actual = this.gameField.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToStringRevealedTest()
        {
            this.gameField.RevealField();
            char[,] fieldChars = new char[GameField.FieldRows, GameField.FieldColumns];

            for (int row = 0; row < GameField.FieldRows; row++)
            {
                for (int col = 0; col < GameField.FieldColumns; col++)
                {
                    // Just in case check if the value has not been changed and is still set to '?'
                    if (this.gameField.Field[row, col].Value == '?')
                    {
                        throw new Exception("Position should be revealed with value different than '?'.");
                    }

                    fieldChars[row, col] = this.gameField.Field[row, col].Value;
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

            string expected = expectedBuilder.ToString();
            string actual = this.gameField.ToString();

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
