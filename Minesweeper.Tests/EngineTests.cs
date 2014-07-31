using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper.Controls;
using Minesweeper.GameFactory;
using Minesweeper.Controls.Contracts;

namespace Minesweeper.Tests
{
    [TestClass]
    public class EngineTests
    {
        private Engine engine;

        [TestInitialize]
        public void Inin()
        {
            engine = Engine.Instance;
        }

        [TestMethod]
        public void DefaultValueSetForIsGameWonTest()
        {
            Assert.AreEqual(false, engine.IsGameWon);
        }

        [TestMethod]
        public void DefaultValueSetForCurrentScoreTest()
        {
            Assert.AreEqual(0, engine.CurrentScore);
        }
    }
}
